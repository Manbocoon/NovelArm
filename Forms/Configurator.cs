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
using System.IO;
using System.Diagnostics;
using WindowsRTClipboard = Windows.ApplicationModel.DataTransfer.Clipboard;

using NovelArm.Modules;
using NovelArm.Modules.Systems;

namespace NovelArm
{
    public partial class ConfigForm : Form
    {
        #region Properties
        internal FileLock fileLock;
        internal CharDisplay charDisplay = new CharDisplay();
        #endregion

        public ConfigForm()
        {
            InitializeComponent();
        }

        #region Form & Controls
        private void ConfigForm_Shown(object sender, EventArgs e)
        {
            // 시작 프로그램 등록되어 있으면 1회성 폼 숨기기
            if (Program.HIDE_WINDOW)
                Hide();
            
            else
                ItemSettings_Click(ItemSettings, new EventArgs());
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            // Lock
            fileLock = new FileLock(Program.EXE_PATH);

            // 캡션에 버전 표시
            Text += " " + Program.VERSION;

            // TabControl Headers 및 TabPage Location 설정
            tabMenu.Appearance = TabAppearance.FlatButtons;
            tabMenu.ItemSize = new Size(0, 1);
            tabMenu.SizeMode = TabSizeMode.Fixed;
            tabMenu.Top = panelMenu.Top - (tabMenu.Top - pageApp.Top);
            tabMenu.Height += 35;

            setLayout();

            // 클립보드, 변환, 글자수 세기, 파일 감지 등의 이벤트와 스레드 실행
            WindowsRTClipboard.ContentChanged += new EventHandler<object>(WinRTClipboard.OnContentChanged);
            TextConverter.Run();
            FileWatcher.Run(charDraftPath.Text, ".rtf");

            // 설정을 불러오기
            using (var settings = new AppSettings())
                settings.LoadFromFile();
            using (var settings = new OverlaySettings())
                settings.LoadFromFile();

            // 컨트롤 이벤트 실행
            PerformControlEvents();
            
            // 업데이트 확인
            if (checkUpdate.Checked)
            {
                Task.Run(CheckUpdate);
            }

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

            // 시작프로그램으로 실행되었다면 폼 숨기기
            if (Program.HIDE_WINDOW)
            {
                Opacity = 0;
                ShowInTaskbar = false;
                FormBorderStyle = FormBorderStyle.SizableToolWindow;
                WindowState = FormWindowState.Minimized;
            }

            Task.Run(() => 
            {
                while (true)
                {
                    Program.EmptyMyWorkingSet();

                    Thread.Sleep(10 * 60 * 1000); // 10 Minute
                }
            });
            
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
            // 메뉴
            SelectProperMenu();

            // 일반 탭
            regStartup_Click(regStartup, new EventArgs());

            // 글자수 탭
            FileWatcher.DirPath = Directory.Exists(charDraftPath.Text) ? charDraftPath.Text : "C:\\";
            charMainApp_SelectedIndexChanged(charMainApp, new EventArgs());
            charUseDisplay_Click(charUseDisplay, new EventArgs());
            charOverlayFormat_TextChanged(charOverlayFormat, new EventArgs());
            SetTextCounterOptions();

            // 변환 탭
            cbAutoConvert_Click(cbAutoConvert, new EventArgs());
            cbUseKeybind_Click(cbUseKeybind, new EventArgs());
            IList<string> keybinds = Keybind.ReplaceRawData(cbSetKeybind.Text, restoreData: true);
            TextConverter.keybinds = Keybind.ReadKeysFromData(keybinds);
        }

        private void CheckUpdate()
        {
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
                        DialogResult userAction = MessageBox.Show($"{numberForFun}번 지구에서 {Program.APP_NAME} 최신버전이 등장했대요!\n훔쳐올 테니까 업데이트 하실래요?\n\n\n[패치 노트]\n{appInfo.PatchNote}", "상태창", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (userAction == DialogResult.Yes)
                        {
                            string updateResult = updater.UpdateMySelf(ref numberForFun);
                            if (updateResult.Contains("ERROR"))
                            {
                                MessageBox.Show($"업데이트 과정에서 오류가 발생했어요!\n프로그램 하단의 \"모든 버전 확인\"을 눌러서 최상단 파일을 직접 받으시는 걸 추천드려요!\n\n{updateResult}", "알림", 0, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }
                }
            }
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
            FormBorderStyle = FormBorderStyle.FixedSingle;
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            Opacity = 100;
            Show();
        }

        private void ItemExit_Click(object sender, EventArgs e)
        {
            // 설정 저장
            using (var settings = new AppSettings())
                settings.SaveToFile();
            using (var settings = new OverlaySettings())
                settings.SaveToFile();

            Environment.Exit(0);
        }

        private void ItemRestart_Click(object sender, EventArgs e)
        {
            // 설정 저장
            using (var settings = new AppSettings())
                settings.SaveToFile();
            using (var settings = new OverlaySettings())
                settings.SaveToFile();

            // 재시작
            new Process().ExecuteWithArguments(
                filePath: Program.EXE_PATH,
                arguments: $"/restart {Process.GetCurrentProcess().Id}"
                );

            // 현재 앱은 종료
            Environment.Exit(0);
        }
        #endregion

        #region Menus
        private static Color selectedColor = Color.FromArgb(200, 225, 255);
        private static Color mouseHoverColor = Color.FromArgb(229, 243, 255);
        private static Color focusOutColor = Color.FromArgb(215, 215, 215);
        private static Color idleColor = Color.FromArgb(240, 240, 240);
        private List<Label> menus = new List<Label>();
        private void setLayout()
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

                menus.Add(labelMenu);
                panelMenu.Controls.Add(labelMenu);

            }

        }

        private void SelectProperMenu()
        {
            foreach (Control control in panelMenu.Controls)
            {
                if (    control.Name.StartsWith("Menu_")
                    &&  control is Label)
                {
                    control.BackColor = idleColor;

                    if (control.Name.Remove(0, 5) == tabMenu.SelectedIndex.ToString())
                    {
                        control.BackColor = selectedColor;
                    }
                }
            }
        }
        #endregion

        #region CheckBoxes
        private void regStartup_Click(object sender, EventArgs e)
        {
            if (regStartup.Checked)
                Registries.RegisterStartup();
            
            else
                Registries.UnregisterStartup();
        }

        private void charUseDisplay_Click(object sender, EventArgs e)
        {
            if (charUseDisplay.Checked)
            {
                Point restoredFormLoc = charDisplay.Location;

                charDisplay.Show();
                charDisplay.GenerateTextBitmap();
                charDisplay.SelectBitmap();

                charDisplay.Location = restoredFormLoc;
              
                // 오버레이의 20%조차 화면에 드러나지 않으면 위치 초기화
                if (!Program.IsOnScreen(charDisplay.Location, charDisplay.Size, 0.2))
                    charDisplay.Location = new Point((Program.SCREEN.Width - charDisplay.Width) / 2, (Program.SCREEN.Height - charDisplay.Height) / 2);

            }

            else
            {
                charDisplay.Hide();
                charDisplay.configForm.Hide();
            }
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

        #region TextBoxes
        private void charOverlayFormat_TextChanged(object sender, EventArgs e)
        {
            charDisplay.BitmapTextFormat = charOverlayFormat.Text;
        }

        private void charDraftPath_Click(object sender, EventArgs e)
        {
            if (charMainApp.SelectedIndex == -1)
            {
                MessageBox.Show("먼저 사용하시는 집필 프로그램이 무엇인지 설정해주세요.", "알림", 0, MessageBoxIcon.Exclamation);
                ActiveControl = charMainApp;
                charMainApp.DroppedDown = true;
                return;
            }

            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = $"{charMainApp.Text} 원고 파일이 들어있는 폴더를 선택해주세요.";
                if (    folderDialog.ShowDialog() == DialogResult.OK
                    &&  Directory.Exists(folderDialog.SelectedPath))
                {
                    charDraftPath.Text = folderDialog.SelectedPath;
                    FileWatcher.DirPath = folderDialog.SelectedPath;
                }
            }
            ActiveControl = null;
        }

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


        #region Buttons / Labels

        private void SetTextCounterOptions()
        {
            TextCounter.REMOVE_BLANKS = charBlank.Checked;
            TextCounter.REMOVE_MARKS = charMarks.Checked;
            TextCounter.PROCESS_SPECIALMARKS = charNovelpia.Checked;

            charDisplay.SyncDraft();
        }

        private void charBlank_Click(object sender, EventArgs e)
        {
            SetTextCounterOptions();
        }

        private void charMarks_Click(object sender, EventArgs e)
        {
            SetTextCounterOptions();
        }

        private void charNovelpia_Click(object sender, EventArgs e)
        {
            SetTextCounterOptions();
        }

        private void navigateGithub_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Manbocoon/NovelArm/releases");
            ActiveControl = null;
        }

        private void navigateDiscord_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/HeX6NYTmPE");
            ActiveControl = null;
        }

        private void resetProgram_Click(object sender, EventArgs e)
        {
            DialogResult userAction = MessageBox.Show("모든 설정이 초기화됩니다. 이 작업은 되돌릴 수 없습니다.\n계속하시겠습니까?", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (userAction == DialogResult.Yes)
            {
                // 설정 제거
                Settings.RemoveAllSettings();

                // 재시작
                new Process().ExecuteWithArguments(
                    filePath: Program.EXE_PATH,
                    arguments: $"/reset {Process.GetCurrentProcess().Id}"
                    );

                // 현재 앱은 종료
                Environment.Exit(0);
            }
            ActiveControl = null;
        }

        private void charQuickNovelpia_Click(object sender, EventArgs e)
        {
            ActiveControl = null;

            charBlank.Checked = true;
            charMarks.Checked = true;
            charNovelpia.Checked = true;

            SetTextCounterOptions();
        }

        private void charQuickNormal_Click(object sender, EventArgs e)
        {
            ActiveControl = null;

            charBlank.Checked = false;
            charMarks.Checked = false;
            charNovelpia.Checked = false;

            SetTextCounterOptions();
        }
        #endregion

        #region ComboBox
        private void charMainApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (charMainApp.SelectedIndex)
            {
                case -1: // 미선택
                    FileWatcher.EventEnabled = false;
                    break;

                case 0: // 메모장(Notepad)
                    FileWatcher.selectedApp = "Notepad";
                    FileWatcher.FileFilter = "*.txt";
                    FileWatcher.EventEnabled = true;
                    break;

                case 1: // 스크리브너(Scrivener 3)
                    FileWatcher.selectedApp = "Scrivener";
                    FileWatcher.FileFilter = "*.rtf";
                    FileWatcher.EventEnabled = true;
                    break;

/*
                case 2: // 한글(Hancom Office - Hwp)
                    charMainApp.SelectedIndex = -1;
                    MessageBox.Show("아직 준비 중인 항목입니다.", "", 0, MessageBoxIcon.Information);
                    FileWatcher.EventEnabled = false;
                    break;

                case 3: // 옵시디언(Obsidian)
                    charMainApp.SelectedIndex = -1;
                    MessageBox.Show("아직 준비 중인 항목입니다.", "", 0, MessageBoxIcon.Information);
                    FileWatcher.EventEnabled = false;
                    break;
*/

                default:
                    break;
            }


            ActiveControl = null;
        }



        #endregion

    }
}
