using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovelArm.Modules.Systems;

namespace NovelArm.Modules.Systems
{
    internal class AltTabSwitcher : IDisposable
    {
        public void Dispose()
        { }

        #region Properties
        private const int GWL_EXSTYLE = -20;
        private const int DWMWA_CLOAKED = 14;
        private const int DWM_CLOAKED_SHELL = 0x00000002;
        private const int GA_ROOTOWNER = 3;
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int WS_EX_TOPMOST = 0x00000008;
        private const int WS_EX_NOACTIVATE = 0x08000000;

        private List<ProcessInfo> Processes = new List<ProcessInfo>();
        internal struct ProcessInfo
        {
            public int Pid;
            public IntPtr MainWindowHandle;
            public string MainWindowText;
            public string Name;
        }

        #endregion

        internal List<ProcessInfo> GetAltTabSwitcherApps()
        {
            Processes.Clear();

            NativeMethods.EnumWindows(GetAltTabWindows, IntPtr.Zero);
            NativeMethods.EnumChildWindows(NativeMethods.GetDesktopWindow(), GetFullScreenUWPWindows, IntPtr.Zero);

            return Processes;
        }

        private bool GetAltTabWindows(IntPtr hWnd, IntPtr lparam)
        {
            if (IsAltTabWindow(hWnd))
                AddWindowToList(hWnd);

            return true;
        }

        private bool GetFullScreenUWPWindows(IntPtr hWnd, IntPtr lparam)
        {
            // Check only the windows whose class name is ApplicationFrameInputSinkWindow
            StringBuilder className = new StringBuilder(1024);
            NativeMethods.GetClassName(hWnd, className, className.Capacity);
            if (className.ToString() != "ApplicationFrameInputSinkWindow")
                return true;

            // Get the root owner of the window
            IntPtr rootOwner = NativeMethods.GetAncestor(hWnd, GA_ROOTOWNER);

            if (IsFullScreenUWPWindows(rootOwner))
                AddWindowToList(rootOwner);

            return true;
        }

        private bool IsAltTabWindow(IntPtr hWnd)
        {
            // The window must be visible
            if (!NativeMethods.IsWindowVisible(hWnd))
                return false;

            // The window must be a root owner
            if (NativeMethods.GetAncestor(hWnd, GA_ROOTOWNER) != hWnd)
                return false;

            // The window must not be cloaked by the shell
            NativeMethods.DwmGetWindowAttribute(hWnd, DWMWA_CLOAKED, out int cloaked, sizeof(uint));
            if (cloaked == DWM_CLOAKED_SHELL)
                return false;

            // The window must not have the extended style WS_EX_TOOLWINDOW
            int style = NativeMethods.GetWindowLong(hWnd, GWL_EXSTYLE);
            if ((style & WS_EX_TOOLWINDOW) != 0)
                return false;

            return true;
        }

        private bool IsFullScreenUWPWindows(IntPtr hWnd)
        {
            // Get the extended style of the window
            int style = NativeMethods.GetWindowLong(hWnd, GWL_EXSTYLE);

            // The window must have the extended style WS_EX_TOPMOST
            if ((style & WS_EX_TOPMOST) == 0)
                return false;

            // The window must not have the extended style WS_EX_NOACTIVATE
            if ((style & WS_EX_NOACTIVATE) != 0)
                return false;

            // The window must not have the extended style WS_EX_TOOLWINDOW
            if ((style & WS_EX_TOOLWINDOW) != 0)
                return false;

            return true;
        }

        private void AddWindowToList(IntPtr hWnd)
        {
            StringBuilder windowText = new StringBuilder(1024);
            NativeMethods.GetWindowText(hWnd, windowText, windowText.Capacity);
            string title = windowText.ToString();
            NativeMethods.GetWindowThreadProcessId(hWnd, out int pid);
            string processName = null;

            try { processName = Process.GetProcessById(pid).ProcessName; }
            catch (Exception) { }
            
            Processes.Add(new ProcessInfo
            {
                Pid = pid,
                MainWindowHandle = hWnd,
                MainWindowText = title,
                Name = processName
            });
        }
    }
}
