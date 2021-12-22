using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TinyBrowser.Components
{
    /// <summary>
    /// Win32API
    /// </summary>
    public static class Win32
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern short GetAsyncKeyState(int nVirtKey);
        public const int VK_CONTROL = 0x11;
        public const int VK_ALT = 0x12;
    }
}
