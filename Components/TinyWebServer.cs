using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using App.Core;
using System.Runtime.Caching;
using System;
using System.Threading.Tasks;
using System.Drawing;


/************************************\
 * Tiny Http Web Server             *
 * Huseyin Atasoy                   *
 * www.atasoyweb.net                *
 * huseyin@atasoyweb.net            *
 * Modify surfsky.github.com 2021   *
\************************************/
namespace TinyBrowser.Components
{
    /// <summary>端口类型</summary>
    public enum PortType
    {
        TCP,
        UDP
    }

    /// <summary>中间件接口</summary>
    public interface IMiddleware
    {
        bool Process(Socket clientSocket, string url, string method);
    }

    /// <summary>响应</summary>
    public class ReplyData
    {
        public string Code { get; set; } = "200 OK";
        public byte[] Bytes { get; set; }
        public string Mime { get; set; }

        public ReplyData() { }
        public ReplyData(string text)
        {
            this.Bytes = text.ToBytes();
            this.Code = "200 OK";
            this.Mime = "text/html";
        }
    }



    /// <summary>
    /// 微型网页服务器，主要用于输出文本文件及图片文件
    /// </summary>
    /// <remarks>
    /// Done:
    /// - MimeTyps
    /// - Files
    /// - NotFound
    /// - Cache
    /// 
    /// TODO: 
    /// - Cookie
    /// - Middleware (eg. logger)
    /// - Route
    /// </remarks>
    public class TinyWebServer
    {
        // private
        private int _timeout = 8; // Time limit for data transfers.
        private Encoding _encoding = Encoding.UTF8; // To encode string
        private Socket _serverSocket; // Our server socket
        private string _rootPath; // Root path of our contents

        //----------------------------------------------------------
        // Public
        //----------------------------------------------------------
        public List<IMiddleware> Middlewares = new List<IMiddleware>();
        public IPAddress IP { get; set; }
        public int Port { get; set; }

        /// <summary>Is it running?</summary>
        public bool Running = false;

        /// <summary>Content types that are supported by our server. You can add more...</summary>
        /// <remarks>To see other types: http://www.webmaster-toolkit.com/mime-types.shtml</remarks>
        public Dictionary<string, string> Mimes = new Dictionary<string, string>()
        { 
            //{ "extension", "content type" }
            { "htm", "text/html" },
            { "html", "text/html" },
            { "js", "text/javascript" },
            { "jsx", "text/javascript" },
            { "xml", "text/xml" },
            { "txt", "text/plain" },
            { "css", "text/css" },
            { "json", "text/json" },
            { "md", "text/markdown" },
            { "qml", "text/qml" },
            { "yml", "text/yml" },
            { "png", "image/png" },
            { "gif", "image/gif" },
            { "jpg", "image/jpg" },
            { "jpeg", "image/jpeg" },
            { "svg", "image/svg" },
            { "zip", "application/zip"},
            { "rar", "application/rar"},
            { "7z", "application/7zip"}
        };

        //----------------------------------------------------------
        // Start & Stop server
        //----------------------------------------------------------
        /// <summary>Start web server</summary>
        /// <param name="max">max client connection</param>
        /// <param name="rootPath">eg. c:\</param>
        /// <returns></returns>
        public bool Start(string rootPath, int port, IPAddress ip=null, int max = 2000)
        {
            if (Running) 
                return false;
            try
            {
                this.IP = ip;
                this.Port = port;
                if (ip == null)
                    ip = IPAddress.Parse("127.0.0.1");

                // A tcp/ip socket (ipv4)
                _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _serverSocket.Bind(new IPEndPoint(ip, port));
                _serverSocket.Listen(max);
                _serverSocket.ReceiveTimeout = _timeout;
                _serverSocket.SendTimeout = _timeout;
                Running = true;
                this._rootPath = rootPath;
            }
            catch { return false; }

            // Our thread that will listen connection requests and create new threads to handle them.
            Thread thread = new Thread(() =>
            {
                while (Running)
                {
                    try
                    {
                        var clientSocket = _serverSocket.Accept();
                        // Create new thread to handle the request and continue to listen the socket.
                        Thread requestHandler = new Thread(() =>
                        {
                            clientSocket.ReceiveTimeout = _timeout;
                            clientSocket.SendTimeout = _timeout;
                            try { 
                                HandleRequest(clientSocket); 
                            }
                            catch {
                                try { clientSocket.Close(); } catch { }
                            }
                        });
                        requestHandler.Start();
                    }
                    catch{}
                }
            });
            thread.Start();
            return true;
        }

        /// <summary>Stop web server</summary>
        public void Close()
        {
            if (Running)
            {
                Running = false;
                try { _serverSocket.Close(); }
                catch { }
                _serverSocket = null;
            }
        }



        //----------------------------------------------------------
        // 路由
        //----------------------------------------------------------
        public Dictionary<string, Func<ReplyData>> Routes = new Dictionary<string, Func<ReplyData>>();
        public TinyWebServer AddRoute(string path, Func<ReplyData> func)
        {
            this.Routes[path] = func;
            return this;
        }
        public TinyWebServer AddRoute(string path, Func<byte[]> func, string mime = "image/png")
        {
            var data = new ReplyData();
            data.Bytes = func();
            data.Mime = mime;
            this.Routes[path] = () => data;
            return this;
        }
        public TinyWebServer AddTextRoute(string path, Func<string> func, string mime="text/html", Encoding encoding=null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var data = new ReplyData();
            data.Bytes = func().ToBytes(encoding);
            data.Mime = mime;
            this.Routes[path] = () => data;
            return this;
        }
        public TinyWebServer AddImageRoute(string path, Func<Image> func, string mime = "image/png")
        {
            var data = new ReplyData();
            data.Bytes = func().ToBytes();
            data.Mime = mime; 
            this.Routes[path] = () => data;
            return this;
        }


        //----------------------------------------------------------
        // Kernal logic
        //----------------------------------------------------------
        /// <summary>Handler</summary>
        private void HandleRequest(Socket clientSocket)
        {
            byte[] buffer = new byte[10240]; // 10 kb, just in case
            int n = clientSocket.Receive(buffer); // Receive the request
            string text = _encoding.GetString(buffer, 0, n);

            // 解析请求方法
            string httpMethod = text.Substring(0, text.IndexOf(" ")).ToUpper();
            if (httpMethod != "GET" && httpMethod != "POST")
            {
                ReplyNotImplemented(clientSocket);
                return;
            }

            // 解析请求地址
            int start = text.IndexOf(httpMethod) + httpMethod.Length + 1;
            int length = text.LastIndexOf("HTTP") - start - 1;
            var requestedUrl = text.Substring(start, length).UrlDecode();

            // 路由处理
            foreach (var route in this.Routes)
            {
                if (route.Key == requestedUrl)
                {
                    var reply = route.Value();
                    Reply(clientSocket, reply);
                    return;
                }
            }

            // 中间件处理
            foreach (var middleware in this.Middlewares)
            {
                if (middleware.Process(clientSocket, requestedUrl, httpMethod))
                    return;
            }
            FileMiddleware(clientSocket, requestedUrl, httpMethod);
        }

        /// <summary>文件处理</summary>
        private void FileMiddleware(Socket clientSocket, string requestedUrl, string httpMethod)
        {
            // 解析文件路径
            string requestedFile;
            requestedFile = requestedUrl.Split('?')[0];
            requestedFile = requestedFile.Replace("/", "\\").Replace("\\..", ""); // Not to go back

            // 根据路径中是否带有 . 来判断是否是个文件
            int start = requestedFile.LastIndexOf('.') + 1;
            if (start > 0)
            {
                // 如果是文件，尝试找到文件并发送到客户端
                int length = requestedFile.Length - start;
                string extension = requestedFile.Substring(start, length);
                if (Mimes.ContainsKey(extension))
                {
                    if (File.Exists(_rootPath + requestedFile))
                        ReplyOk(clientSocket, _rootPath + requestedFile, Mimes[extension]);
                    else
                        ReplyNotFound(clientSocket);
                }
            }
            else
            {
                // 如果是目录，尝试找到默认文件
                var length = requestedFile.Length;
                if (requestedFile.Substring(length - 1, 1) != "\\")
                    requestedFile += "\\";
                if (File.Exists(_rootPath + requestedFile + "index.htm"))
                    ReplyOk(clientSocket, _rootPath + requestedFile + "\\index.htm", "text/html");
                else if (File.Exists(_rootPath + requestedFile + "index.html"))
                    ReplyOk(clientSocket, _rootPath + requestedFile + "\\index.html", "text/html");
                else
                    ReplyNotFound(clientSocket);
            }
        }

        //----------------------------------------------------------
        // 回应
        //----------------------------------------------------------
        private void ReplyNotImplemented(Socket clientSocket)
        {
            ReplyText(clientSocket, "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"></head><body><h2>Tiny Web Server</h2><div>501 - Method Not Implemented</div></body></html>", "501 Not Implemented", "text/html");
        }

        private void ReplyNotFound(Socket clientSocket)
        {
            ReplyText(clientSocket, "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"></head><body><h2>Tiny Web Server</h2><div>404 - Not Found</div></body></html>", "404 Not Found", "text/html");
        }

        // Reply ok
        private void ReplyOk(Socket clientSocket, string filePath, string contentType)
        {
            var key = filePath.ToLower().MD5();

            // 图片文件默认做缓存，此处可扩展为缓存处理模块
            byte[] bytes;
            //if (filePath.IsImageFile())
            //{
            //    bytes = CacheHelper.GetCacheItem(key,
            //            () => File.ReadAllBytes(filePath),
            //            new TimeSpan(0, 5, 0)
            //            );
            //}
            //else
                bytes = File.ReadAllBytes(filePath);

            //
            ReplyOk(clientSocket, bytes, contentType);
            //ReplyOk(clientSocket, File.ReadAllBytes(filePath), contentType);
        }


        // Reply ok
        private void ReplyOk(Socket clientSocket, byte[] bytes, string mime)
        {
            ReplyBytes(clientSocket, bytes, "200 OK", mime);
        }

        // Reply strings
        private void ReplyText(Socket clientSocket, string content, string code, string mime)
        {
            byte[] bytes = _encoding.GetBytes(content);
            ReplyBytes(clientSocket, bytes, code, mime);
        }

        /// <summary>给客户端发响应</summary>
        void Reply(Socket clientSocket, ReplyData reply)
        {
            ReplyBytes(clientSocket, reply.Bytes, reply.Code, reply.Mime);
        }

        // Reply byte arrays
        private void ReplyBytes(Socket clientSocket, byte[] bytes, string code, string mime)
        {
            try
            {
                byte[] header = _encoding.GetBytes(
                                    "HTTP/1.1 " + code + "\r\n"
                                  + "Server: Tiny Web Server\r\n"
                                  + "Content-Length: " + bytes.Length.ToString() + "\r\n"
                                  + "Connection: close\r\n"
                                  + "Content-Type: " + mime + "\r\n\r\n");
                clientSocket.Send(header);
                clientSocket.Send(bytes);
                clientSocket.Close();
            }
            catch { }
        }

        //----------------------------------------------------------
        // 端口处理
        //----------------------------------------------------------
        /// <summary>寻找可用的端口</summary>
        public static int SearchEmptyPort(int startPort=5000, int count=1000)
        {
            for(int i=0; i< count; i++)
            {
                var port = startPort + i;
                if (!IsPortUsed(port, PortType.TCP))
                    return port;
            }
            return -1;
        }

        /// <summary>指定类型的端口是否已经被使用了</summary>
        /// <param name="port">端口号</param>
        /// <param name="type">端口类型</param>
        static bool IsPortUsed(int port, PortType type)
        {
            bool flag = false;
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipendpoints = null;
            if (type == PortType.TCP)
                ipendpoints = properties.GetActiveTcpListeners();
            else
                ipendpoints = properties.GetActiveUdpListeners();

            foreach (IPEndPoint ipendpoint in ipendpoints)
            {
                if (ipendpoint.Port == port)
                {
                    flag = true;
                    break;
                }
            }
            ipendpoints = null;
            properties = null;
            return flag;
        }
    }
}
