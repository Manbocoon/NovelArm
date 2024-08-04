using NovelArm.Modules.Systems;
using NovelArm.Modules.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NovelArm
{
    public partial class CharDisplayConfig : Form
    {
        #region Properties
        internal CharDisplay parentForm { get; set; }
        private Thread _parentMover;

        internal bool _Visible
        {
            get
            {
                bool value = false;
                Invoke((MethodInvoker)delegate () 
                {
                    value = Visible;
                });
                return value;
            }
        } 
        #endregion

        public CharDisplayConfig()
        {
            InitializeComponent();
        }

        private void CharDisplayConfig_Load(object sender, EventArgs e)
        {
            _parentMover = new Thread(MoveParentByDrag);
            _parentMover.IsBackground = true;
            _parentMover.Start();
        }


        protected override CreateParams CreateParams
        {
            get
            {
                // Add the layered extended style (WS_EX_LAYERED) to this window.
                CreateParams createParams = base.CreateParams;

                // Hide From Alt+Tab Switcher
                createParams.ExStyle |= 0x80;

                return createParams;
            }
        }


        private void MoveParentByDrag()
        {
            while (true)
            {
                if (NativeMethods.GetAsyncKeyState(1) > 32767)
                {
                    // 설정창 안 보이는 상태면 취소
                    if (!_Visible)
                    {
                        Thread.Sleep(100);
                        continue;
                    }

                    Point originalMouseLoc, currentMouseLoc, originalFormLoc;
                    NativeMethods.GetCursorPos(out originalMouseLoc);
                    originalFormLoc = parentForm.Location;
                    currentMouseLoc = Point.Empty;
                    
                    // 영역 바깥이면 클릭 끝날 때까지 대기했다가 취소
                    if (!parentForm.Bounds.Contains(originalMouseLoc))
                    {
                        while (NativeMethods.GetAsyncKeyState(1) > 32767)
                        { Thread.Sleep(10); }

                        Thread.Sleep(50);
                        continue;
                    }

                    while (NativeMethods.GetAsyncKeyState(1) > 32767)
                    {
                        NativeMethods.GetCursorPos(out currentMouseLoc);
                        int deltaX = currentMouseLoc.X - originalMouseLoc.X, deltaY = currentMouseLoc.Y - originalMouseLoc.Y;
                        Invoke((MethodInvoker)delegate ()
                        {
                            parentForm.Location = new Point(originalFormLoc.X + deltaX, originalFormLoc.Y + deltaY);
                            Location = new Point(parentForm.Left, parentForm.Top - Height);
                        });

                        Thread.Sleep(10);
                    }
                }

                Thread.Sleep(100);
            }

        }

        #region Buttons

        private void setFont_Click(object sender, EventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                fontDialog.Font = parentForm.TextFont;
                fontDialog.ShowEffects = true;

                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    parentForm.TextFont = fontDialog.Font;

                    parentForm.SetTextOptions(null, null, Color.Empty, Color.Empty, 0);
                    parentForm.GenerateTextBitmap();
                    parentForm.SelectBitmap();
                }
            }
            ActiveControl = null;
        }

        private void selectDraft_Click(object sender, EventArgs e)
        {
            /*
            MessageBox.Show("설정하신 폴더에서 원고를 탐지하겠습니다.\n\n이 메세지를 닫은 뒤, 사용하시는 집필 프로그램으로 원하는 원고에 아무 내용이나 쓰고 저장해주세요.", "알림", 0, MessageBoxIcon.Information);
            FileWatcher.finding = true;

            ActiveControl = null;
            */
        }
        #endregion

        private void setTextColor_Click(object sender, EventArgs e)
        {
            Color starting_color = parentForm.TextColor;
            ColorPicker colorPicker = new ColorPicker(starting_color) { Text = "글자 색상 설정"};
            if (colorPicker.ShowDialog() == DialogResult.OK)
            {
                parentForm.TextColor = colorPicker.PrimaryColor;
                parentForm.SetTextOptions(null, null, Color.Empty, Color.Empty, 0);
                parentForm.GenerateTextBitmap();
                parentForm.SelectBitmap();
            }

            colorPicker.Dispose();
            ActiveControl = null;
        }

        private void setOutlineColor_Click(object sender, EventArgs e)
        {
            bool valueChanged = false;
            Color starting_color = parentForm.OutlineColor;
            ColorPicker colorPicker = new ColorPicker(starting_color) { Text = "외곽선 색상 설정"};
            if (colorPicker.ShowDialog() == DialogResult.OK)
            {
                parentForm.OutlineColor = colorPicker.PrimaryColor;
                valueChanged = true;
            }

            colorPicker.Dispose();

            string OutlineThicknessStr = null;
            InputBox.ShowDialog(ref OutlineThicknessStr, "외곽선 굵기 입력", "오버레이 문구의 외곽선 굵기 값을 입력하세요. (0 ~ 255)");
            if (byte.TryParse(OutlineThicknessStr, out byte OutlineThickness))
            {
                parentForm.OutlineThickness = OutlineThickness;
                valueChanged = true;
            }

            if (valueChanged)
            {
                parentForm.SetTextOptions(null, null, Color.Empty, Color.Empty, 0);
                parentForm.GenerateTextBitmap();
                parentForm.SelectBitmap();
            }
            
            ActiveControl = null;
        }
    }
}
