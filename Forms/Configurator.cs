using Microsoft.Win32;
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

using NovelArm.Modules;
using NovelArm.Modules.System;
using System.IO;
using system = Windows.ApplicationModel.DataTransfer;
using System.Diagnostics;

namespace NovelArm
{
    public partial class ConfigForm : Form
    {
        #region Properties
        internal FileLock fileLock;
        #endregion

        public ConfigForm()
        {
            InitializeComponent();
        }

        #region Form & Controls
        private void ConfigForm_Load(object sender, EventArgs e)
        {
            // Lock
            fileLock = new FileLock(Program.EXE_PATH);

            // TabControl Headers 및 TabPage Location 설정
            tabMenu.Appearance = TabAppearance.FlatButtons;
            tabMenu.ItemSize = new Size(0, 1);
            tabMenu.SizeMode = TabSizeMode.Fixed;
            tabMenu.Top = panelMenu.Top - (tabMenu.Top - pageApp.Top);

            layoutMenuLabels();
            Hide();

            // 설정을 불러온 뒤 각 이벤트 실행
            using (var settings = new Settings())
                settings.LoadFromFile();
            PerformControlEvents();

            // 클립보드 감지
            system.Clipboard.ContentChanged += new EventHandler<object>(_Clipboard.OnContentChanged);
            //clipboardViewerNext = NativeMethods.SetClipboardViewer(Handle);

            // 클립보드 변환 스레드 실행
            if (cbAutoConvert.Checked || cbUseKeybind.Checked)
                TextConverter.Run();
            
            // 업데이트 확인         
            Updater.AppInfo appInfo = new Updater.AppInfo();
            using (Updater updater = new Updater())
            {
                if (!updater.IsNetworkAvailable())
                    return;

                appInfo = updater.GetInfoFromWeb();
                if (double.TryParse(appInfo.Version, out double newVersion))
                {
                    if (Program.VERSION < newVersion)
                    {
                        int numberForFun = new Random().Next(1, 1000001);
                        DialogResult userAction = MessageBox.Show($"{numberForFun}번 지구에서 {Program.APP_NAME} 최신버전이 등장했대요!\n훔쳐올 테니까 업데이트 하실래요?\n\n\n▼ 패치 노트 ▼\n{appInfo.PatchNote}", "상태창", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (userAction == DialogResult.Yes)
                        {
                            string updateResult = updater.UpdateMySelf(ref numberForFun);
                            if (updateResult.Contains("ERROR"))
                            {
                                MessageBox.Show($"업데이트 과정에서 오류가 발생했어요!\n프로그램 하단의 \"모든 버전 확인\"을 눌러서 최상단 파일을 직접 받으시는 걸 추천드려요!\n\n{updateResult}", "알림", 0, MessageBoxIcon.Exclamation);
                                GC.Collect();
                                return;
                            }
                        }
                    }
                }
            }

            // 만약 업데이트 직후라면 폼 보여주기
            if (Program.UPDATED)
                ItemSettings_Click(ItemSettings, new EventArgs());

            // 업데이트 잔여 파일이 있다면 삭제
            IEnumerable<FileInfo> trashFiles = new DirectoryInfo(Program.PATH).GetFilesByExtensions(".original", ".new", ".old");
            foreach (FileInfo fInfo in trashFiles)
            {
                if (fInfo.Exists)
                {
                    try
                    { File.Delete(fInfo.FullName); }

                    catch (Exception) { }
                }
            }


            GC.Collect();
        }

        private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Cancel program termination
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // TrayIcon.Visible = true;
                Hide();
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 설정을 불러온 이후 컨트롤들의 이벤트를 실행합니다.
        /// </summary>
        private void PerformControlEvents()
        {
            // 일반 탭
            regStartup_Click(regStartup, new EventArgs());

            // 변환 탭
            cbAutoConvert_Click(cbAutoConvert, new EventArgs());
            cbUseKeybind_Click(cbUseKeybind, new EventArgs());
            IList<string> keybinds = Keybind.ReplaceRawData(cbSetKeybind.Text, restoreData: true);
            TextConverter.keybinds = Keybind.ReadKeysFromData(keybinds);
        }
        #endregion

        #region ContextMenus & Tray
        private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                ItemSettings_Click(new object(), new EventArgs());
        }

        private void ItemSettings_Click(object sender, EventArgs e)
        {
            if (!Visible)
            {
                Show();
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
                // TrayIcon.Visible = false;
            }
        }

        private void ItemExit_Click(object sender, EventArgs e)
        {
            // 설정 저장
            using (var settings = new Settings())
                settings.SaveToFile();

            Environment.Exit(0);
        }
        #endregion

        #region Menus
        private static Color selectedColor = Color.FromArgb(200, 225, 255);
        private static Color mouseHoverColor = Color.FromArgb(229, 243, 255);
        private static Color focusOutColor = Color.FromArgb(215, 215, 215);
        private static Color idleColor = Color.FromArgb(240, 240, 240);
        private void layoutMenuLabels()
        {
            Point firstMenuLocation = new Point(10, 10);
            int margin = 45;
            for (byte index = 0; index < tabMenu.TabPages.Count; ++index)
            {
                Label labelMenu = new Label
                {
                    Name = "Menu_" + index,
                    Text = tabMenu.TabPages[index].Text,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = idleColor,
                    Font = panelMenu.Font,
                    AutoSize = false,
                    Size = new Size(panelMenu.Width - (firstMenuLocation.X * 2), 40),
                    Left = firstMenuLocation.X,
                    Top = firstMenuLocation.Y + (index * margin),
                    Cursor = Cursors.Hand,
                };

                // EventHandler for changing BackColor
                labelMenu.MouseEnter += new EventHandler(delegate (Object obj, EventArgs evt)
                {
                    Control _this = (Control)obj;

                    if (_this.BackColor != selectedColor)
                    {
                        _this.BackColor = mouseHoverColor;
                    }
                });

                // EventHandler for changing BackColor
                labelMenu.MouseLeave += new EventHandler(delegate (Object obj, EventArgs evt)
                {
                    Control _this = (Control)obj;
                    if (_this.BackColor != selectedColor)
                    {
                        _this.BackColor = idleColor;
                    }
                });

                // EventHandler for changing BackColor and showing a new page
                labelMenu.Click += new EventHandler(delegate (Object obj, EventArgs evt)
                {
                    Control _this = (Control)obj;
                    foreach (Control control in panelMenu.Controls)
                    {
                        if (control.BackColor == selectedColor)
                        {
                            control.BackColor = idleColor;
                        }
                    }

                    _this.BackColor = selectedColor;
                    tabMenu.SelectedIndex = _this.TabIndex;
                });

                if (index == 0)
                {
                    labelMenu.BackColor = selectedColor;
                }

                panelMenu.Controls.Add(labelMenu);
            }
        }

        #endregion

        #region CheckBoxes
        private void regStartup_Click(object sender, EventArgs e)
        {
            if (regStartup.Checked)
                Startup.Register();
            
            else
                Startup.Unregister();
        }

        private void cbAutoConvert_Click(object sender, EventArgs e)
        {
            if (cbAutoConvert.Checked)
            {
                TextConverter.useAutoConvert = true;
                if (cbUseKeybind.Checked)
                {
                    cbSetKeybind.Enabled = false;
                    cbUseKeybind.Checked = false;
                    TextConverter.useKeybind = false;
                }
            }

            else
                TextConverter.useAutoConvert = false;

            GC.Collect(0);
        }

        private void cbUseKeybind_Click(object sender, EventArgs e)
        {
            if (cbUseKeybind.Checked)
            {
                cbSetKeybind.Enabled = true;
                TextConverter.useKeybind = true;
                if (cbAutoConvert.Checked)
                {
                    cbAutoConvert.Checked = false;
                    TextConverter.useAutoConvert = false;
                }
            }

            else
            {
                cbSetKeybind.Enabled = false;
                TextConverter.useKeybind = false;
            }

            GC.Collect(0);
        }

        #endregion

        #region TextBoxes - KeyDown / Up
        private void cbSetKeybind_KeyDown(object sender, KeyEventArgs e)
        {
            IList<string> keybinds = Keybind.ReplaceRawData(e.KeyData.ToString());

            if (keybinds == null)
            {
                TextConverter.keybinds.Clear();
                cbSetKeybind.Text = "없음";
                return;
            }

            TextConverter.keybinds = Keybind.ReadKeysFromData(keybinds);
            cbSetKeybind.Text = String.Join(" + ", keybinds);
            TextConverter.useKeybind = false;

            if (cbSetKeybind.Text == "Ctrl + V")
            {
                cbSetKeybind.Text = "없음";
                MessageBox.Show("Ctrl + V는 Windows 기본 붙여넣기 단축키입니다. 사용할 수 없습니다.", "알림", 0, MessageBoxIcon.Exclamation);
            }
        }

        private void cbSetKeybind_KeyUp(object sender, KeyEventArgs e)
        {
            if (cbUseKeybind.Checked)
                TextConverter.useKeybind = true;

            // Lose focus
            ActiveControl = null;
        }
        #endregion

        private void navigateGithub_MouseClick(object sender, MouseEventArgs e)
        {
            using (Updater updater = new Updater())
                Process.Start(updater.releaseURL);
        }
    }
}
