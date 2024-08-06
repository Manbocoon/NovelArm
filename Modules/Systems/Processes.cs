using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NovelArm.Modules.Systems
{
    internal class Processes : IDisposable
    {
        public void Dispose()
        { }

        #region Properties
        private const int WM_GETICON = 0x007F;
        private const int GCL_HICON = -14;
        #endregion

        /// <summary>
        /// 64 bit version maybe loses significant 64-bit specific information
        /// </summary>
        internal IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 4)
                return new IntPtr((long)NativeMethods.GetClassLong32(hWnd, nIndex));
            else
                return NativeMethods.GetClassLong64(hWnd, nIndex);
        }

        internal Image GetSmallWindowIcon(IntPtr hWnd)
        {
            // IntPtr ICON_SMALL2 = new IntPtr(2);
            IntPtr IDI_APPLICATION = new IntPtr(0x7F00);

            try
            {
                IntPtr hIcon = default(IntPtr);

                hIcon = NativeMethods.SendMessage(hWnd, WM_GETICON, 2, 0);

                if (hIcon == IntPtr.Zero)
                    hIcon = GetClassLongPtr(hWnd, GCL_HICON);

                if (hIcon == IntPtr.Zero)
                    hIcon = NativeMethods.LoadIcon(IntPtr.Zero, (IntPtr)0x7F00/*IDI_APPLICATION*/);

                if (hIcon != IntPtr.Zero)
                    return new Bitmap(Icon.FromHandle(hIcon).ToBitmap(), 16, 16);
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
