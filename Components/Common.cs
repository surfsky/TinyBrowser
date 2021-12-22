using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TinyBrowser.Components
{
    public class Common
    {
        // <summary>获取版本号</summary>
        public static string GetVersion()
        {
            var v = Assembly.GetExecutingAssembly().GetName().Version;
            return $"{v.Major}.{v.Minor}.{v.Build}";
        }


    }
}
