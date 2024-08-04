using System;
using System.Drawing;
using System.Runtime.InteropServices;


namespace NovelArm.Modules.Systems
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern UInt16 GetAsyncKeyState(Int32 vKey);

        [DllImport("user32")]
        internal static extern Int32 GetCursorPos(out Point pt);

        [DllImport("user32.dll")]
        internal static extern int SetCursor(int hCursor);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("wininet.dll")]
        internal static extern bool InternetGetConnectedState(out int Description, int ReservedValue);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        [DllImport("user32")]
        internal static extern int SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("psapi.dll")]
        internal static extern int EmptyWorkingSet(IntPtr hwProc);

        [DllImport("user32")]
        internal static extern Int32 SetWindowLong(IntPtr hWnd, Int32 nIndex, Int32 dwNewLong);
        [DllImport("user32")]
        internal static extern Int32 GetWindowLong(IntPtr hWnd, Int32 nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst,
            ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc,
            Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr hObject);

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct ARGB
        {
            public byte Blue;
            public byte Green;
            public byte Red;
            public byte Alpha;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        internal const Int32 WS_EX_LAYERED = 0x80000;
        internal const Int32 HTCAPTION = 0x02;
        internal const Int32 WM_NCHITTEST = 0x84;
        internal const Int32 ULW_ALPHA = 0x02;
        internal const byte AC_SRC_OVER = 0x00;
        internal const byte AC_SRC_ALPHA = 0x01;

        internal const long WM_LBUTTONDBLCLK = 0x203;
    }
}
