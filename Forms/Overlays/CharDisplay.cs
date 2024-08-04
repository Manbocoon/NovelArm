using NovelArm.Modules;

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using NovelArm.Modules.Systems;
using System.Text;

namespace NovelArm
{
    /// <summary>
    /// 이미지 오버레이 창입니다.
    /// </summary>
    public partial class CharDisplay : Form
    {
        #region Properties
        private ushort SystemDoubleClickSpeed = 500;
        private Thread _keyDetector;
        internal Bitmap bitmapOnScreen;
        internal CharDisplayConfig configForm = new CharDisplayConfig();
        internal IntPtr thisHandle = IntPtr.Zero;
        internal int originalStyle;

        public string BitmapText { get; set; } = "더블클릭 = 설정/잠금 해제";
        public string BitmapTextFormat { get; set; } = "{@글자수} 글자";
        public Font TextFont { get; set; } = new Font("맑은 고딕", 36, FontStyle.Regular);
        public Color TextColor { get; set; } = Color.FromArgb(255, 255, 255);
        public Color OutlineColor { get; set; } = Color.FromArgb(0, 0, 0);
        public byte OutlineThickness { get; set; } = 7;
        private bool clickable_value = true;
        internal bool clickable
        {
            get
            {
                return clickable_value;
            }

            set
            {
                clickable_value = value;
                
                if (value)
                {
                    NativeMethods.SetWindowLong(thisHandle, -20, originalStyle);
                }

                else
                {
                    NativeMethods.SetWindowLong(thisHandle, -20, originalStyle | 0x80000 | 0x20);
                }

            }
        }

        internal StringBuilder lastDraft = new StringBuilder("");
        internal string verifiedDraft
        {
            get
            {
                return FileWatcher.verifiedDraft;
            }
        }
        #endregion


        public CharDisplay()
        {
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            Load += PerPixelAlphaForm_Load;
        }

        internal void DisposeChilds()
        {
            if (bitmapOnScreen != null)
            {
                bitmapOnScreen.Dispose();
            }
        }

        private void PerPixelAlphaForm_Load(object sender, EventArgs e)
        {
            TopMost = true;

            thisHandle = Handle;
            SystemDoubleClickSpeed = Registries.GetMouseDoubleClickSpeed();

            _keyDetector = new Thread(DetectMouseDblClick);
            _keyDetector.IsBackground = true;
            _keyDetector.Start();

            // 포개진 Sizer 폼이 클릭 가능하므로 클릭 불가능하도록 고정
            originalStyle = NativeMethods.GetWindowLong(this.Handle, -20);
            clickable = false;

            // 설정 및 드래그용 폼 표시
            configForm.parentForm = this;
            configForm.Show();
            configForm.Visible = false;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                // Add the layered extended style (WS_EX_LAYERED) to this window.
                CreateParams createParams = base.CreateParams;

                if (!DesignMode)
                    createParams.ExStyle |= NativeMethods.WS_EX_LAYERED;

                // Hide From Alt+Tab Switcher
                createParams.ExStyle |= 0x80;

                return createParams;
            }
        }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == NativeMethods.WM_NCHITTEST)
            {
                // Tell Windows that the user is on the title bar (caption)
                message.Result = (IntPtr)NativeMethods.HTCAPTION;
            }

            else
            {
                base.WndProc(ref message);
            }
        }


        #region Functions for Image
        internal void SelectBitmap()
        {
            SelectBitmap(ref bitmapOnScreen, 255);
        }

        internal void SelectBitmap(ref Bitmap bitmap)
        {
            SelectBitmap(ref bitmap, 255);
        }

        internal void SelectBitmap(ref Bitmap bitmap, int opacity)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
            {
                throw new ApplicationException("알파 채널을 지닌 32비트 ARGB 포맷의 PNG 파일이어야 합니다.");
            }

            Size bitmapSize = bitmap.Size;
            IntPtr screenDc = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr memDc = NativeMethods.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr hOldBitmap = IntPtr.Zero;

            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                hOldBitmap = NativeMethods.SelectObject(memDc, hBitmap);

                Size newSize = new Size(bitmap.Width, bitmap.Height);
                Point sourceLocation = new Point(0, 0);
                Point newLocation = new Point(Left, Top);

                NativeMethods.BLENDFUNCTION blend = new NativeMethods.BLENDFUNCTION();
                blend.BlendOp = NativeMethods.AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = (byte)opacity;
                blend.AlphaFormat = NativeMethods.AC_SRC_ALPHA;

                NativeMethods.UpdateLayeredWindow(Handle, screenDc, ref newLocation, ref newSize, memDc, ref sourceLocation, 0, ref blend, NativeMethods.ULW_ALPHA);
            }

            finally
            {
                // Release device context.
                NativeMethods.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    NativeMethods.SelectObject(memDc, hOldBitmap);
                    NativeMethods.DeleteObject(hBitmap);
                }
                NativeMethods.DeleteDC(memDc);
            }

            // 폼 크기를 이미지에 맞춤
            Invoke((MethodInvoker)delegate ()
            {
                Size = bitmapSize;

                ++Left;
                --Left;
            });

            GC.Collect(0);
        }

        internal void GenerateTextBitmap()
        {
            DisposeChilds();

            if (string.IsNullOrWhiteSpace(BitmapText))
            {
                bitmapOnScreen = new Bitmap(1, 1);
                return;
            }

            // 새로운 Text의 이미지 크기 계산
            Size bitmapSize = GraphicsHelper.MeasureString(BitmapText, TextFont);

            // 빈 이미지 생성
            bitmapOnScreen = new Bitmap(bitmapSize.Width, bitmapSize.Height, PixelFormat.Format32bppArgb);
            Graphics graphic = Graphics.FromImage(bitmapOnScreen);

            // 텍스트 정렬
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Near; // StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Far;

            // 텍스트 외곽선
            Pen outlinePen = new Pen(OutlineColor, OutlineThickness);
            outlinePen.LineJoin = LineJoin.Round; //prevent "spikes" at the path

            // 그라데이션
            Rectangle fullRect = new Rectangle(0, bitmapOnScreen.Height - TextFont.Height, bitmapOnScreen.Width, TextFont.Height);
            LinearGradientBrush gradientBrush = new LinearGradientBrush(fullRect,
                                                            TextColor, // ColorTranslator.FromHtml("#FF6493"),
                                                            TextColor, // ColorTranslator.FromHtml("#D00F14"),
                                                            90);

            Rectangle rect = new Rectangle(0, 0, bitmapOnScreen.Width, bitmapOnScreen.Height);
            GraphicsPath gPath = new GraphicsPath();
            gPath.AddString(BitmapText, TextFont.FontFamily, (int)TextFont.Style, TextFont.Size, rect, strFormat);

            graphic.SmoothingMode = SmoothingMode.AntiAlias;
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // TODO: shadow -> g.translate, fillpath once, remove translate
            graphic.DrawPath(outlinePen, gPath);
            graphic.FillPath(gradientBrush, gPath);
            
            gPath.Dispose();
            gradientBrush.Dispose();
            strFormat.Dispose();
            graphic.Dispose();
        }

        /// <summary>
        /// 오버레이 텍스트의 옵션을 설정합니다.
        /// </summary>
        /// <param name="text">표시할 텍스트입니다. null을 사용하면 기존 설정을 그대로 유지합니다.</param>
        /// <param name="text_color">글자 색깔입니다. Color.Empty를 사용하면 기존 설정을 그대로 유지합니다.</param>
        /// <param name="outline_color">외곽선 색깔입니다. Color.Empty를 사용하면 기존 설정을 그대로 유지합니다.</param>
        /// <param name="text_font">표시할 텍스트의 글꼴입니다. null을 사용하면 기존 설정을 그대로 유지합니다.</param>
        /// <param name="outline_thickness">외곽선의 굵기입니다. 0을 사용하면 기존 설정을 그대로 유지합니다.</param>
        internal void SetTextOptions(string text, Font text_font, Color text_color, Color outline_color, byte outline_thickness = 0)
        {
            // 값이 있으면 좌항, 없으면 우항
            BitmapText = text ?? BitmapText;
            TextFont = text_font ?? TextFont;
            TextColor = text_color == Color.Empty ? TextColor : text_color;
            OutlineColor = outline_color == Color.Empty ? OutlineColor : outline_color;
            OutlineThickness = outline_thickness == 0 ? OutlineThickness : outline_thickness;

        }
        #endregion



        internal void DetectMouseDblClick()
        {
            int sleepTime = 50;
            Stopwatch clickWatcher = new Stopwatch();
            while (true)
            {
                // 클릭하지 않으면 취소
                if (!Keybind.PressedAllKeybinds(1))
                {
                    Thread.Sleep(sleepTime);
                    continue;
                }

                // 마우스가 폼 영역 바깥이면 취소
                if (!Keybind.CursorInForm(this))
                {
                    Thread.Sleep(sleepTime);
                    continue;
                }

                // 다시 클릭할 때까지 대기
                // 시스템 더블클릭 간격 설정만큼의 시간이 지나면 취소
                clickWatcher.Reset();
                clickWatcher.Start();
                while (clickWatcher.ElapsedMilliseconds < SystemDoubleClickSpeed + sleepTime)
                {
                    if (Keybind.PressedAllKeybinds(1))
                    {
                        // 마우스가 폼 영역 안이면 더블클릭 성공 처리
                        if (!Keybind.CursorInForm(this))
                            break;

                        // 더블클릭 성공 시 설정 UI 보여주기
                        clickWatcher.Stop();
                        Invoke((MethodInvoker)delegate ()
                        {
                            configForm.Location = new Point(Left, Top - configForm.Height);
                            configForm.Visible = !configForm.Visible;
                        });
                        break;
                    }
                }

                Thread.Sleep(sleepTime);
            }
        }

        internal void CloseMouseDetector()
        {
            if (_keyDetector != null && _keyDetector.IsAlive)
                _keyDetector.Abort();
        }



        internal void SyncDraft()
        {
            if (this == null || !this.IsHandleCreated)
                return;

            lastDraft.Clear();
            lastDraft.Append(verifiedDraft);

            // 옵션에 따라 텍스트 처리
            int draftCharCount = TextCounter.CalculateDraft(lastDraft);

            // 오버레이 표시
            BitmapText = $"{BitmapTextFormat.Replace("{@글자수}", draftCharCount.ToString())}";
            GenerateTextBitmap();
            Invoke((MethodInvoker)delegate () {
                SelectBitmap(); 
            });
        }
    }
}