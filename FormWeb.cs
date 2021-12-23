using App.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TinyBrowser.Components;
using CefSharp;
using ICSharpCode.TextEditor.Document;
using System.Net;
using System.Reflection;

namespace TinyBrowser
{
    [ComVisible(true)]
    public partial class FormWeb : Form
    {
        string _basePath = "";  // 网页基础路径
        bool _showHtml = true;  // 是否查看网页源代码
        bool _userServer = false; // 是否使用嵌入服务器
        bool _actived = true;  // 窗口是否激活
        TinyWebServer _server = new TinyWebServer();
        KeyboardHook _hook = new KeyboardHook();

        //---------------------------------------------
        // 初始化
        //---------------------------------------------
        public FormWeb()
        {
            InitializeComponent();
        }

        /// <summary>直接展示某段 HTML 代码</summary>
        public FormWeb(string html, string basePath)
        {
            InitializeComponent();
            this.tbHtml.Text = html;
            _basePath = basePath;
            webView.WaitForInitialLoadAsync();
        }
        /// <summary>显示 URL 或某个文件</summary>
        /// <param name="userServer">对于文件是否启用内嵌网页服务器来查看</param>
        public FormWeb(string urlOrFile, bool userServer)
        {
            InitializeComponent();
            webView.WaitForInitialLoadAsync();
            tbUrl.Text = urlOrFile;
            _userServer = userServer;
        }

        private void FormPosition_Load(object sender, EventArgs e)
        {
            // 全局键盘钩子
            _hook.SetHook();
            _hook.OnKeyDownEvent += hook_OnKeyDownEvent;
            this.Activated += (s,a) => _actived = true;
            this.Deactivate += (s, a) => _actived = false;

            //
            this.tbHtml.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("XML");  // TODO: HTML JS 样式太丑了，以后再想办法美化
            webView.LoadingStateChanged += Browser_LoadingStateChanged;
            webView.AddressChanged += Browser_AddressChanged;
            webView.FrameLoadEnd += Browser_FrameLoadEnd;

            // 加载网页或代码
            var url = this.tbUrl.Text;
            var html = this.tbHtml.Text;
            if (_userServer)
                ShowWebWithServer(url);
            else if (url.IsNotEmpty())
                btnGo_Click(null, null);
            else if (html.IsNotEmpty())
                btnLoadHtml_Click(null, null);
        }

        /// <summary>处理全局钩子事件（快捷键）</summary>
        private void hook_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            if (this._actived)
            {
                if (e.KeyData == (Keys.H | Keys.Control)) this.Hide();   // Ctrl+H隐藏窗口
                if (e.KeyData == (Keys.C | Keys.Control)) this.Close();  // Ctrl+C 关闭窗口 
                if (e.KeyData == (Keys.Escape)) this.ToggleFullScreen(false);
            }
        }

        private void FormWeb_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._server.Close();
            this._hook.UnHook();
        }


        //-------------------------------------------------------
        // Web Server
        //-------------------------------------------------------
        /// <summary>启用服务器展示网页</summary>
        private void ShowWebWithServer(string filePath)
        {
            var name = filePath.GetFileName();
            var folder = filePath.GetFileFolder();
            var ip = IPAddress.Parse("127.0.0.1");
            var port = TinyWebServer.SearchEmptyPort(5000);
            var url = string.Format("http://127.0.0.1:{0}/{1}", port, name);
            _server.Close();
            _server.Start(folder, port, ip);
            _showHtml = true;
            webView.LoadUrl(url);
            this.tbUrl.Text = url;
            this.tabControl.SelectedIndex = 0;
        }

        //-------------------------------------------------------
        // 浏览器事件
        //-------------------------------------------------------
        private void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (_showHtml)
            {
                var task = e.Frame.GetSourceAsync();
                task.ContinueWith(t =>
                {
                    if (!t.IsFaulted)
                        Invoke(() => { this.tbHtml.Text = t.Result; });
                });
            }
        }

        private void Browser_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            Invoke(() =>
            {
                //this.tbUrl.Text = e.Address.UrlDecode();  // e.Address 是目录不是文件
            });
        }

        private void Browser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            Invoke(() =>
            {
                var status  = e.IsLoading ? "Loading" : "Ready";
                this.btnBack.Enabled = e.CanGoBack;
                this.btnForward.Enabled = e.CanGoForward;
            });
        }

        public void Invoke(Action action)
        {
            if (InvokeRequired)
                BeginInvoke(action);
            else
                action.Invoke();
        }


        //-------------------------------------------------------
        // 工具栏操作
        //-------------------------------------------------------
        // <summary>展示HTML网页</summary>
        private void btnLoadHtml_Click(object sender, EventArgs e)
        {
            _showHtml = false;
            webView.LoadHtml(
                this.tbHtml.Text,
                @"/", //_basePath,
                new UTF8Encoding(false)
                );
            this.tabControl.SelectedIndex = 0;
        }


        /// <summary>浏览</summary>
        private void btnGo_Click(object sender, EventArgs e)
        {
            _showHtml = true;
            this.webView.LoadUrl(this.tbUrl.Text);
            this.tabControl.SelectedIndex = 0;
        }

        private void tbUrl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.btnGo_Click(null, null);
        }

        /// <summary>后退</summary>
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.webView.Back();
        }

        /// <summary>前进</summary>
        private void btnForward_Click(object sender, EventArgs e)
        {
            this.webView.Forward();
        }

        /// <summary>刷新页面</summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.webView.Reload();
        }

        // <summary>彩蛋：出于开发方便，打开开发工具（ctrl+alt+双击）</summary>
        private void toolStrip_DoubleClick(object sender, EventArgs e)
        {
            if (Win32.GetAsyncKeyState(Win32.VK_CONTROL) < 0 && Win32.GetAsyncKeyState(Win32.VK_ALT) < 0)
                webView.ShowDevTools();
        }

        // <summary>开启 Web 服务器浏览网页</summary>
        private void btnLoadServer_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                var filePath = dlg.FileName;
                ShowWebWithServer(filePath);
            }
        }

        // <summary>加载本地网页</summary>
        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                var filePath = dlg.FileName;
                this.tbUrl.Text = filePath;
                btnGo_Click(null, null);
            }
        }

        // <summary>关于</summary>
        private void btnAbout_Click(object sender, EventArgs e)
        {
            var info = string.Format("TinyBrowser {0}\r\nhttps://github.com/surfsky/TinyBrowser", Common.GetVersion());
            MessageBox.Show(this, info, nameof(TinyBrowser));
        }

        // <summary>调试</summary>
        private void btnDebug_Click(object sender, EventArgs e)
        {
            webView.ShowDevTools();
        }

        //---------------------------------------------------------
        // 全屏控制
        //---------------------------------------------------------
        // <summary>切换全屏</summary>
        private void ToggleFullScreen(bool fullscreen)
        {
            if (fullscreen)
            {
                this.toolStrip.Visible = false;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.toolStrip.Visible = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.WindowState = FormWindowState.Normal;
            }
        }


        // <summary>全屏</summary>
        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            ToggleFullScreen(true);
        }


    }
}
