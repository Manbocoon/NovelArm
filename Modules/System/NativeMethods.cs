using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NovelArm.Modules.System
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern UInt16 GetAsyncKeyState(Int32 vKey);

        [DllImport("wininet.dll")]
        internal static extern bool InternetGetConnectedState(out int Description, int ReservedValue);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        [DllImport("user32")]
        internal static extern int SetForegroundWindow(IntPtr hwnd);

    }
}
