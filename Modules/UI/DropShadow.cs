using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace NovelArm.Modules.UI
{
    class DropShadow : IDisposable
    {
        public void Dispose()
        { }


        #region Shadowing

        #region Fields
        private byte thickness = 1;

        private bool _isAeroEnabled = false;
        private bool _isDraggingEnabled = false;
        private const int WM_NCHITTEST = 0x84;
        private const int WS_MINIMIZEBOX = 0x20000;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private const int CS_DBLCLKS = 0x8;
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        #endregion

        #region Structures

        [EditorBrowsable(EditorBrowsableState.Never)]
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

      
        #endregion

        #region Methods

        #region Public

        [DllImport("dwmapi.dll")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static bool IsCompositionEnabled()
        {
            if (Environment.OSVersion.Version.Major < 6) return false;

            bool enabled;
            DwmIsCompositionEnabled(out enabled);

            return enabled;
        }

        #endregion

        #region Private

        [DllImport("dwmapi.dll")]
        private static extern int DwmIsCompositionEnabled(out bool enabled);

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
         );

        private bool CheckIfAeroIsEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);

                return (enabled == 1) ? true : false;
            }
            return false;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// 특정 폼에 동서남북 4방향 모두 그림자를 씌웁니다.
        /// </summary>
        /// <param name="form">그림자를 씌울 폼입니다.</param>
        /// <param name="drawType">유형입니다. 0=그림자 제거,  1=외곽선 없는 그림자 적용,  2=외곽선 있는 그림자 적용</param>
        public void ApplyShadows(Form form, byte drawType)
        {
            var v = 2;

            if (form != null && form.IsHandleCreated)
            {
                return;
            }

            form.Invoke((MethodInvoker)delegate 
            {
                if (drawType == 0)
                    thickness = 0;

                // 프로그램에 표시되는 외곽 테두리를 없애기 위해 TransparencyKey에 안 쓰일만한 색 지정
                if (drawType == 1)
                {
                    form.TransparencyKey = Color.Wheat;
                    form.AllowTransparency = true;
                }

                DwmSetWindowAttribute(form.Handle, 2, ref v, 4);

                MARGINS margins = new MARGINS()
                {
                    bottomHeight = thickness,
                    leftWidth = thickness,
                    rightWidth = thickness,
                    topHeight = thickness
                };

                DwmExtendFrameIntoClientArea(form.Handle, ref margins);
            });

        }

        #endregion

        #endregion

        #endregion


    }
}
