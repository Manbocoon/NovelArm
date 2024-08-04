
namespace NovelArm
{
    partial class ConfigForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ItemRestart = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tabMenu = new System.Windows.Forms.TabControl();
            this.pageApp = new System.Windows.Forms.TabPage();
            this.appSettingsGroup = new System.Windows.Forms.GroupBox();
            this.resetProgram = new System.Windows.Forms.Button();
            this.appStartGroup = new System.Windows.Forms.GroupBox();
            this.checkUpdate = new System.Windows.Forms.CheckBox();
            this.regStartup = new System.Windows.Forms.CheckBox();
            this.pageLiveCount = new System.Windows.Forms.TabPage();
            this.charDisplayGroup = new System.Windows.Forms.GroupBox();
            this.charUseDisplay = new System.Windows.Forms.CheckBox();
            this.charDraftPath = new System.Windows.Forms.TextBox();
            this.labelCharDraftPath = new System.Windows.Forms.Label();
            this.labelCharMainApp = new System.Windows.Forms.Label();
            this.charMainApp = new System.Windows.Forms.ComboBox();
            this.labelCharOverlayFormat = new System.Windows.Forms.Label();
            this.charOverlayFormat = new System.Windows.Forms.TextBox();
            this.charCountGroup = new System.Windows.Forms.GroupBox();
            this.charQuickNovelpia = new System.Windows.Forms.Button();
            this.charQuickNormal = new System.Windows.Forms.Button();
            this.labelQuickChar = new System.Windows.Forms.Label();
            this.separator_1 = new System.Windows.Forms.Label();
            this.charNovelpia = new System.Windows.Forms.CheckBox();
            this.charMarks = new System.Windows.Forms.CheckBox();
            this.charBlank = new System.Windows.Forms.CheckBox();
            this.pageTarget = new System.Windows.Forms.TabPage();
            this.ready2 = new System.Windows.Forms.Label();
            this.pageAutoComplete = new System.Windows.Forms.TabPage();
            this.ready3 = new System.Windows.Forms.Label();
            this.pageConvert = new System.Windows.Forms.TabPage();
            this.cbGroup = new System.Windows.Forms.GroupBox();
            this.cbKeybindLabel = new System.Windows.Forms.Label();
            this.cbSetKeybind = new System.Windows.Forms.TextBox();
            this.cbUseKeybind = new System.Windows.Forms.CheckBox();
            this.cbAutoConvert = new System.Windows.Forms.CheckBox();
            this.pageHelp = new System.Windows.Forms.TabPage();
            this.navigateDiscord = new System.Windows.Forms.Button();
            this.navigateGithub = new System.Windows.Forms.Button();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.TrayMenu.SuspendLayout();
            this.tabMenu.SuspendLayout();
            this.pageApp.SuspendLayout();
            this.appSettingsGroup.SuspendLayout();
            this.appStartGroup.SuspendLayout();
            this.pageLiveCount.SuspendLayout();
            this.charDisplayGroup.SuspendLayout();
            this.charCountGroup.SuspendLayout();
            this.pageTarget.SuspendLayout();
            this.pageAutoComplete.SuspendLayout();
            this.pageConvert.SuspendLayout();
            this.cbGroup.SuspendLayout();
            this.pageHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // TrayIcon
            // 
            this.TrayIcon.ContextMenuStrip = this.TrayMenu;
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.Text = "NovelArm - 웹소설 작가 도우미";
            this.TrayIcon.Visible = true;
            this.TrayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseClick);
            // 
            // TrayMenu
            // 
            this.TrayMenu.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TrayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemSettings,
            this.ItemSeparator2,
            this.ItemRestart,
            this.ItemExit});
            this.TrayMenu.Name = "TrayMenu";
            this.TrayMenu.Size = new System.Drawing.Size(111, 76);
            // 
            // ItemSettings
            // 
            this.ItemSettings.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ItemSettings.Name = "ItemSettings";
            this.ItemSettings.Size = new System.Drawing.Size(110, 22);
            this.ItemSettings.Text = "설정";
            this.ItemSettings.Click += new System.EventHandler(this.ItemSettings_Click);
            // 
            // ItemSeparator2
            // 
            this.ItemSeparator2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ItemSeparator2.Name = "ItemSeparator2";
            this.ItemSeparator2.Size = new System.Drawing.Size(107, 6);
            // 
            // ItemRestart
            // 
            this.ItemRestart.Name = "ItemRestart";
            this.ItemRestart.Size = new System.Drawing.Size(110, 22);
            this.ItemRestart.Text = "재시작";
            this.ItemRestart.Click += new System.EventHandler(this.ItemRestart_Click);
            // 
            // ItemExit
            // 
            this.ItemExit.Name = "ItemExit";
            this.ItemExit.Size = new System.Drawing.Size(110, 22);
            this.ItemExit.Text = "종료";
            this.ItemExit.Click += new System.EventHandler(this.ItemExit_Click);
            // 
            // tabMenu
            // 
            this.tabMenu.Controls.Add(this.pageApp);
            this.tabMenu.Controls.Add(this.pageLiveCount);
            this.tabMenu.Controls.Add(this.pageTarget);
            this.tabMenu.Controls.Add(this.pageAutoComplete);
            this.tabMenu.Controls.Add(this.pageConvert);
            this.tabMenu.Controls.Add(this.pageHelp);
            this.tabMenu.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tabMenu.Location = new System.Drawing.Point(199, 12);
            this.tabMenu.Name = "tabMenu";
            this.tabMenu.SelectedIndex = 0;
            this.tabMenu.Size = new System.Drawing.Size(531, 602);
            this.tabMenu.TabIndex = 3;
            // 
            // pageApp
            // 
            this.pageApp.Controls.Add(this.appSettingsGroup);
            this.pageApp.Controls.Add(this.appStartGroup);
            this.pageApp.Location = new System.Drawing.Point(4, 26);
            this.pageApp.Name = "pageApp";
            this.pageApp.Padding = new System.Windows.Forms.Padding(3);
            this.pageApp.Size = new System.Drawing.Size(523, 572);
            this.pageApp.TabIndex = 0;
            this.pageApp.Text = "일반";
            this.pageApp.UseVisualStyleBackColor = true;
            // 
            // appSettingsGroup
            // 
            this.appSettingsGroup.Controls.Add(this.resetProgram);
            this.appSettingsGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.appSettingsGroup.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.appSettingsGroup.Location = new System.Drawing.Point(3, 88);
            this.appSettingsGroup.Name = "appSettingsGroup";
            this.appSettingsGroup.Size = new System.Drawing.Size(517, 85);
            this.appSettingsGroup.TabIndex = 6;
            this.appSettingsGroup.TabStop = false;
            this.appSettingsGroup.Text = "설정";
            // 
            // resetProgram
            // 
            this.resetProgram.BackColor = System.Drawing.Color.White;
            this.resetProgram.Cursor = System.Windows.Forms.Cursors.Hand;
            this.resetProgram.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.resetProgram.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.resetProgram.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.resetProgram.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetProgram.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.resetProgram.Location = new System.Drawing.Point(11, 26);
            this.resetProgram.Name = "resetProgram";
            this.resetProgram.Size = new System.Drawing.Size(495, 40);
            this.resetProgram.TabIndex = 11;
            this.resetProgram.Text = "프로그램 초기화 후 재시작";
            this.resetProgram.UseVisualStyleBackColor = false;
            this.resetProgram.Click += new System.EventHandler(this.resetProgram_Click);
            // 
            // appStartGroup
            // 
            this.appStartGroup.Controls.Add(this.checkUpdate);
            this.appStartGroup.Controls.Add(this.regStartup);
            this.appStartGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.appStartGroup.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.appStartGroup.Location = new System.Drawing.Point(3, 3);
            this.appStartGroup.Name = "appStartGroup";
            this.appStartGroup.Size = new System.Drawing.Size(517, 85);
            this.appStartGroup.TabIndex = 5;
            this.appStartGroup.TabStop = false;
            this.appStartGroup.Text = "시작 옵션";
            // 
            // checkUpdate
            // 
            this.checkUpdate.AutoSize = true;
            this.checkUpdate.Checked = true;
            this.checkUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkUpdate.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkUpdate.Location = new System.Drawing.Point(6, 51);
            this.checkUpdate.Name = "checkUpdate";
            this.checkUpdate.Size = new System.Drawing.Size(167, 21);
            this.checkUpdate.TabIndex = 1;
            this.checkUpdate.Text = "프로그램 자동 업데이트";
            this.checkUpdate.UseVisualStyleBackColor = true;
            // 
            // regStartup
            // 
            this.regStartup.AutoSize = true;
            this.regStartup.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.regStartup.Location = new System.Drawing.Point(6, 24);
            this.regStartup.Name = "regStartup";
            this.regStartup.Size = new System.Drawing.Size(180, 21);
            this.regStartup.TabIndex = 0;
            this.regStartup.Text = "시작 프로그램에 등록하기";
            this.regStartup.UseVisualStyleBackColor = true;
            this.regStartup.Click += new System.EventHandler(this.regStartup_Click);
            // 
            // pageLiveCount
            // 
            this.pageLiveCount.Controls.Add(this.charDisplayGroup);
            this.pageLiveCount.Controls.Add(this.charCountGroup);
            this.pageLiveCount.Location = new System.Drawing.Point(4, 26);
            this.pageLiveCount.Name = "pageLiveCount";
            this.pageLiveCount.Padding = new System.Windows.Forms.Padding(3);
            this.pageLiveCount.Size = new System.Drawing.Size(523, 572);
            this.pageLiveCount.TabIndex = 1;
            this.pageLiveCount.Text = "글자수 표시";
            this.pageLiveCount.UseVisualStyleBackColor = true;
            // 
            // charDisplayGroup
            // 
            this.charDisplayGroup.Controls.Add(this.charUseDisplay);
            this.charDisplayGroup.Controls.Add(this.charDraftPath);
            this.charDisplayGroup.Controls.Add(this.labelCharDraftPath);
            this.charDisplayGroup.Controls.Add(this.labelCharMainApp);
            this.charDisplayGroup.Controls.Add(this.charMainApp);
            this.charDisplayGroup.Controls.Add(this.labelCharOverlayFormat);
            this.charDisplayGroup.Controls.Add(this.charOverlayFormat);
            this.charDisplayGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.charDisplayGroup.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.charDisplayGroup.Location = new System.Drawing.Point(3, 256);
            this.charDisplayGroup.Name = "charDisplayGroup";
            this.charDisplayGroup.Size = new System.Drawing.Size(517, 238);
            this.charDisplayGroup.TabIndex = 7;
            this.charDisplayGroup.TabStop = false;
            this.charDisplayGroup.Text = "감지 및 표시";
            // 
            // charUseDisplay
            // 
            this.charUseDisplay.AutoSize = true;
            this.charUseDisplay.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.charUseDisplay.Location = new System.Drawing.Point(6, 26);
            this.charUseDisplay.Name = "charUseDisplay";
            this.charUseDisplay.Size = new System.Drawing.Size(154, 21);
            this.charUseDisplay.TabIndex = 21;
            this.charUseDisplay.Text = "화면에 오버레이 표시";
            this.charUseDisplay.UseVisualStyleBackColor = true;
            this.charUseDisplay.Click += new System.EventHandler(this.charUseDisplay_Click);
            // 
            // charDraftPath
            // 
            this.charDraftPath.BackColor = System.Drawing.Color.White;
            this.charDraftPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.charDraftPath.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.charDraftPath.Location = new System.Drawing.Point(167, 113);
            this.charDraftPath.Name = "charDraftPath";
            this.charDraftPath.ReadOnly = true;
            this.charDraftPath.Size = new System.Drawing.Size(338, 25);
            this.charDraftPath.TabIndex = 20;
            this.charDraftPath.Text = "C:\\";
            this.charDraftPath.Click += new System.EventHandler(this.charDraftPath_Click);
            // 
            // labelCharDraftPath
            // 
            this.labelCharDraftPath.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelCharDraftPath.Location = new System.Drawing.Point(7, 119);
            this.labelCharDraftPath.Name = "labelCharDraftPath";
            this.labelCharDraftPath.Size = new System.Drawing.Size(151, 17);
            this.labelCharDraftPath.TabIndex = 19;
            this.labelCharDraftPath.Text = "원고 폴더 설정:";
            this.labelCharDraftPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCharMainApp
            // 
            this.labelCharMainApp.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelCharMainApp.Location = new System.Drawing.Point(6, 77);
            this.labelCharMainApp.Name = "labelCharMainApp";
            this.labelCharMainApp.Size = new System.Drawing.Size(151, 17);
            this.labelCharMainApp.TabIndex = 17;
            this.labelCharMainApp.Text = "집필 프로그램:";
            this.labelCharMainApp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // charMainApp
            // 
            this.charMainApp.BackColor = System.Drawing.SystemColors.Info;
            this.charMainApp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.charMainApp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.charMainApp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.charMainApp.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.charMainApp.FormattingEnabled = true;
            this.charMainApp.Items.AddRange(new object[] {
            "메모장(Notepad) 및 *.txt 작성 프로그램",
            "스크리브너(Scrivener 3)"});
            this.charMainApp.Location = new System.Drawing.Point(167, 73);
            this.charMainApp.Name = "charMainApp";
            this.charMainApp.Size = new System.Drawing.Size(338, 25);
            this.charMainApp.TabIndex = 16;
            this.charMainApp.SelectedIndexChanged += new System.EventHandler(this.charMainApp_SelectedIndexChanged);
            // 
            // labelCharOverlayFormat
            // 
            this.labelCharOverlayFormat.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelCharOverlayFormat.Location = new System.Drawing.Point(7, 161);
            this.labelCharOverlayFormat.Name = "labelCharOverlayFormat";
            this.labelCharOverlayFormat.Size = new System.Drawing.Size(151, 17);
            this.labelCharOverlayFormat.TabIndex = 13;
            this.labelCharOverlayFormat.Text = "문구:";
            this.labelCharOverlayFormat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // charOverlayFormat
            // 
            this.charOverlayFormat.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.charOverlayFormat.Location = new System.Drawing.Point(167, 155);
            this.charOverlayFormat.Multiline = true;
            this.charOverlayFormat.Name = "charOverlayFormat";
            this.charOverlayFormat.Size = new System.Drawing.Size(338, 66);
            this.charOverlayFormat.TabIndex = 12;
            this.charOverlayFormat.Text = "{@글자수} 글자";
            this.charOverlayFormat.TextChanged += new System.EventHandler(this.charOverlayFormat_TextChanged);
            // 
            // charCountGroup
            // 
            this.charCountGroup.Controls.Add(this.charQuickNovelpia);
            this.charCountGroup.Controls.Add(this.charQuickNormal);
            this.charCountGroup.Controls.Add(this.labelQuickChar);
            this.charCountGroup.Controls.Add(this.separator_1);
            this.charCountGroup.Controls.Add(this.charNovelpia);
            this.charCountGroup.Controls.Add(this.charMarks);
            this.charCountGroup.Controls.Add(this.charBlank);
            this.charCountGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.charCountGroup.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.charCountGroup.Location = new System.Drawing.Point(3, 3);
            this.charCountGroup.Name = "charCountGroup";
            this.charCountGroup.Size = new System.Drawing.Size(517, 253);
            this.charCountGroup.TabIndex = 6;
            this.charCountGroup.TabStop = false;
            this.charCountGroup.Text = "계산 방식";
            // 
            // charQuickNovelpia
            // 
            this.charQuickNovelpia.BackColor = System.Drawing.Color.White;
            this.charQuickNovelpia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.charQuickNovelpia.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.charQuickNovelpia.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.charQuickNovelpia.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.charQuickNovelpia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.charQuickNovelpia.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.charQuickNovelpia.Location = new System.Drawing.Point(10, 200);
            this.charQuickNovelpia.Name = "charQuickNovelpia";
            this.charQuickNovelpia.Size = new System.Drawing.Size(495, 40);
            this.charQuickNovelpia.TabIndex = 11;
            this.charQuickNovelpia.Text = "노벨피아";
            this.charQuickNovelpia.UseVisualStyleBackColor = false;
            this.charQuickNovelpia.Click += new System.EventHandler(this.charQuickNovelpia_Click);
            // 
            // charQuickNormal
            // 
            this.charQuickNormal.BackColor = System.Drawing.Color.White;
            this.charQuickNormal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.charQuickNormal.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.charQuickNormal.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.charQuickNormal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.charQuickNormal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.charQuickNormal.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.charQuickNormal.Location = new System.Drawing.Point(10, 152);
            this.charQuickNormal.Name = "charQuickNormal";
            this.charQuickNormal.Size = new System.Drawing.Size(495, 40);
            this.charQuickNormal.TabIndex = 10;
            this.charQuickNormal.Text = "카카오페이지 / 시리즈 / 문피아";
            this.charQuickNormal.UseVisualStyleBackColor = false;
            this.charQuickNormal.Click += new System.EventHandler(this.charQuickNormal_Click);
            // 
            // labelQuickChar
            // 
            this.labelQuickChar.AutoSize = true;
            this.labelQuickChar.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelQuickChar.Location = new System.Drawing.Point(6, 117);
            this.labelQuickChar.Name = "labelQuickChar";
            this.labelQuickChar.Size = new System.Drawing.Size(74, 20);
            this.labelQuickChar.TabIndex = 9;
            this.labelQuickChar.Text = "빠른 설정";
            // 
            // separator_1
            // 
            this.separator_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separator_1.Location = new System.Drawing.Point(-70, 108);
            this.separator_1.Name = "separator_1";
            this.separator_1.Size = new System.Drawing.Size(700, 2);
            this.separator_1.TabIndex = 8;
            // 
            // charNovelpia
            // 
            this.charNovelpia.AutoSize = true;
            this.charNovelpia.Checked = true;
            this.charNovelpia.CheckState = System.Windows.Forms.CheckState.Checked;
            this.charNovelpia.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.charNovelpia.Location = new System.Drawing.Point(6, 80);
            this.charNovelpia.Name = "charNovelpia";
            this.charNovelpia.Size = new System.Drawing.Size(208, 21);
            this.charNovelpia.TabIndex = 5;
            this.charNovelpia.Text = "일부 문자 특수처리 - 노벨피아";
            this.charNovelpia.UseVisualStyleBackColor = true;
            this.charNovelpia.Click += new System.EventHandler(this.charNovelpia_Click);
            // 
            // charMarks
            // 
            this.charMarks.AutoSize = true;
            this.charMarks.Checked = true;
            this.charMarks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.charMarks.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.charMarks.Location = new System.Drawing.Point(6, 53);
            this.charMarks.Name = "charMarks";
            this.charMarks.Size = new System.Drawing.Size(110, 21);
            this.charMarks.TabIndex = 4;
            this.charMarks.Text = "문장부호 제외";
            this.charMarks.UseVisualStyleBackColor = true;
            this.charMarks.Click += new System.EventHandler(this.charMarks_Click);
            // 
            // charBlank
            // 
            this.charBlank.AutoSize = true;
            this.charBlank.Checked = true;
            this.charBlank.CheckState = System.Windows.Forms.CheckState.Checked;
            this.charBlank.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.charBlank.Location = new System.Drawing.Point(6, 26);
            this.charBlank.Name = "charBlank";
            this.charBlank.Size = new System.Drawing.Size(84, 21);
            this.charBlank.TabIndex = 3;
            this.charBlank.Text = "공백 제외";
            this.charBlank.UseVisualStyleBackColor = true;
            this.charBlank.Click += new System.EventHandler(this.charBlank_Click);
            // 
            // pageTarget
            // 
            this.pageTarget.Controls.Add(this.ready2);
            this.pageTarget.Location = new System.Drawing.Point(4, 26);
            this.pageTarget.Name = "pageTarget";
            this.pageTarget.Padding = new System.Windows.Forms.Padding(3);
            this.pageTarget.Size = new System.Drawing.Size(523, 572);
            this.pageTarget.TabIndex = 2;
            this.pageTarget.Text = "목표";
            this.pageTarget.UseVisualStyleBackColor = true;
            // 
            // ready2
            // 
            this.ready2.Dock = System.Windows.Forms.DockStyle.Top;
            this.ready2.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ready2.Location = new System.Drawing.Point(3, 3);
            this.ready2.Name = "ready2";
            this.ready2.Size = new System.Drawing.Size(517, 32);
            this.ready2.TabIndex = 1;
            this.ready2.Text = "아직 준비 중이에요!";
            this.ready2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pageAutoComplete
            // 
            this.pageAutoComplete.Controls.Add(this.ready3);
            this.pageAutoComplete.Location = new System.Drawing.Point(4, 26);
            this.pageAutoComplete.Name = "pageAutoComplete";
            this.pageAutoComplete.Padding = new System.Windows.Forms.Padding(3);
            this.pageAutoComplete.Size = new System.Drawing.Size(523, 572);
            this.pageAutoComplete.TabIndex = 3;
            this.pageAutoComplete.Text = "자동 완성";
            this.pageAutoComplete.UseVisualStyleBackColor = true;
            // 
            // ready3
            // 
            this.ready3.Dock = System.Windows.Forms.DockStyle.Top;
            this.ready3.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ready3.Location = new System.Drawing.Point(3, 3);
            this.ready3.Name = "ready3";
            this.ready3.Size = new System.Drawing.Size(517, 32);
            this.ready3.TabIndex = 1;
            this.ready3.Text = "아직 준비 중이에요!";
            this.ready3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pageConvert
            // 
            this.pageConvert.Controls.Add(this.cbGroup);
            this.pageConvert.Location = new System.Drawing.Point(4, 26);
            this.pageConvert.Name = "pageConvert";
            this.pageConvert.Padding = new System.Windows.Forms.Padding(3);
            this.pageConvert.Size = new System.Drawing.Size(523, 572);
            this.pageConvert.TabIndex = 4;
            this.pageConvert.Text = "변환";
            this.pageConvert.UseVisualStyleBackColor = true;
            // 
            // cbGroup
            // 
            this.cbGroup.Controls.Add(this.cbKeybindLabel);
            this.cbGroup.Controls.Add(this.cbSetKeybind);
            this.cbGroup.Controls.Add(this.cbUseKeybind);
            this.cbGroup.Controls.Add(this.cbAutoConvert);
            this.cbGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbGroup.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbGroup.Location = new System.Drawing.Point(3, 3);
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.Size = new System.Drawing.Size(517, 145);
            this.cbGroup.TabIndex = 4;
            this.cbGroup.TabStop = false;
            this.cbGroup.Text = "클립보드 데이터";
            // 
            // cbKeybindLabel
            // 
            this.cbKeybindLabel.AutoSize = true;
            this.cbKeybindLabel.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbKeybindLabel.Location = new System.Drawing.Point(6, 104);
            this.cbKeybindLabel.Name = "cbKeybindLabel";
            this.cbKeybindLabel.Size = new System.Drawing.Size(81, 17);
            this.cbKeybindLabel.TabIndex = 4;
            this.cbKeybindLabel.Text = "단축키 입력:";
            // 
            // cbSetKeybind
            // 
            this.cbSetKeybind.BackColor = System.Drawing.Color.White;
            this.cbSetKeybind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbSetKeybind.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbSetKeybind.Location = new System.Drawing.Point(95, 101);
            this.cbSetKeybind.Name = "cbSetKeybind";
            this.cbSetKeybind.ReadOnly = true;
            this.cbSetKeybind.Size = new System.Drawing.Size(405, 25);
            this.cbSetKeybind.TabIndex = 3;
            this.cbSetKeybind.Text = "Ctrl + Oemtilde";
            this.cbSetKeybind.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cbSetKeybind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbSetKeybind_KeyDown);
            this.cbSetKeybind.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbSetKeybind_KeyUp);
            // 
            // cbUseKeybind
            // 
            this.cbUseKeybind.AutoSize = true;
            this.cbUseKeybind.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbUseKeybind.Location = new System.Drawing.Point(6, 51);
            this.cbUseKeybind.Name = "cbUseKeybind";
            this.cbUseKeybind.Size = new System.Drawing.Size(198, 21);
            this.cbUseKeybind.TabIndex = 2;
            this.cbUseKeybind.Text = "단축키로 변환된 값 붙여넣기";
            this.cbUseKeybind.UseVisualStyleBackColor = true;
            this.cbUseKeybind.Click += new System.EventHandler(this.cbUseKeybind_Click);
            // 
            // cbAutoConvert
            // 
            this.cbAutoConvert.AutoSize = true;
            this.cbAutoConvert.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbAutoConvert.Location = new System.Drawing.Point(6, 24);
            this.cbAutoConvert.Name = "cbAutoConvert";
            this.cbAutoConvert.Size = new System.Drawing.Size(198, 21);
            this.cbAutoConvert.TabIndex = 1;
            this.cbAutoConvert.Text = "자동으로 일반 텍스트로 변환";
            this.cbAutoConvert.UseVisualStyleBackColor = true;
            this.cbAutoConvert.Click += new System.EventHandler(this.cbAutoConvert_Click);
            // 
            // pageHelp
            // 
            this.pageHelp.Controls.Add(this.navigateDiscord);
            this.pageHelp.Controls.Add(this.navigateGithub);
            this.pageHelp.Location = new System.Drawing.Point(4, 26);
            this.pageHelp.Name = "pageHelp";
            this.pageHelp.Padding = new System.Windows.Forms.Padding(3);
            this.pageHelp.Size = new System.Drawing.Size(523, 572);
            this.pageHelp.TabIndex = 5;
            this.pageHelp.Text = "도움말";
            this.pageHelp.UseVisualStyleBackColor = true;
            // 
            // navigateDiscord
            // 
            this.navigateDiscord.BackColor = System.Drawing.Color.White;
            this.navigateDiscord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.navigateDiscord.Dock = System.Windows.Forms.DockStyle.Top;
            this.navigateDiscord.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.navigateDiscord.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.navigateDiscord.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.navigateDiscord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navigateDiscord.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.navigateDiscord.Image = global::NovelArm.Properties.Resources.Discord_Logo;
            this.navigateDiscord.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.navigateDiscord.Location = new System.Drawing.Point(3, 103);
            this.navigateDiscord.Name = "navigateDiscord";
            this.navigateDiscord.Size = new System.Drawing.Size(517, 100);
            this.navigateDiscord.TabIndex = 12;
            this.navigateDiscord.Text = "도움 받기 / 정보 공유";
            this.navigateDiscord.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.navigateDiscord.UseVisualStyleBackColor = false;
            this.navigateDiscord.Click += new System.EventHandler(this.navigateDiscord_Click);
            // 
            // navigateGithub
            // 
            this.navigateGithub.BackColor = System.Drawing.Color.White;
            this.navigateGithub.Cursor = System.Windows.Forms.Cursors.Hand;
            this.navigateGithub.Dock = System.Windows.Forms.DockStyle.Top;
            this.navigateGithub.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.navigateGithub.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.navigateGithub.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.navigateGithub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navigateGithub.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.navigateGithub.Image = global::NovelArm.Properties.Resources.Github_Logo;
            this.navigateGithub.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.navigateGithub.Location = new System.Drawing.Point(3, 3);
            this.navigateGithub.Name = "navigateGithub";
            this.navigateGithub.Size = new System.Drawing.Size(517, 100);
            this.navigateGithub.TabIndex = 11;
            this.navigateGithub.Text = "모든 버전 확인";
            this.navigateGithub.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.navigateGithub.UseVisualStyleBackColor = false;
            this.navigateGithub.Click += new System.EventHandler(this.navigateGithub_Click);
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMenu.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelMenu.Location = new System.Drawing.Point(12, 12);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(181, 602);
            this.panelMenu.TabIndex = 4;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 626);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.tabMenu);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "ConfigForm";
            this.Text = "노벨암(NovelArm)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_FormClosing);
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.Shown += new System.EventHandler(this.ConfigForm_Shown);
            this.TrayMenu.ResumeLayout(false);
            this.tabMenu.ResumeLayout(false);
            this.pageApp.ResumeLayout(false);
            this.appSettingsGroup.ResumeLayout(false);
            this.appStartGroup.ResumeLayout(false);
            this.appStartGroup.PerformLayout();
            this.pageLiveCount.ResumeLayout(false);
            this.charDisplayGroup.ResumeLayout(false);
            this.charDisplayGroup.PerformLayout();
            this.charCountGroup.ResumeLayout(false);
            this.charCountGroup.PerformLayout();
            this.pageTarget.ResumeLayout(false);
            this.pageAutoComplete.ResumeLayout(false);
            this.pageConvert.ResumeLayout(false);
            this.cbGroup.ResumeLayout(false);
            this.cbGroup.PerformLayout();
            this.pageHelp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.ContextMenuStrip TrayMenu;
        private System.Windows.Forms.ToolStripMenuItem ItemSettings;
        private System.Windows.Forms.ToolStripMenuItem ItemExit;
        private System.Windows.Forms.ToolStripSeparator ItemSeparator2;
        private System.Windows.Forms.TabControl tabMenu;
        private System.Windows.Forms.TabPage pageApp;
        private System.Windows.Forms.TabPage pageLiveCount;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.TabPage pageTarget;
        private System.Windows.Forms.TabPage pageAutoComplete;
        private System.Windows.Forms.TabPage pageConvert;
        private System.Windows.Forms.CheckBox regStartup;
        private System.Windows.Forms.CheckBox cbAutoConvert;
        private System.Windows.Forms.GroupBox cbGroup;
        private System.Windows.Forms.CheckBox cbUseKeybind;
        private System.Windows.Forms.GroupBox appStartGroup;
        private System.Windows.Forms.CheckBox checkUpdate;
        private System.Windows.Forms.TextBox cbSetKeybind;
        private System.Windows.Forms.Label cbKeybindLabel;
        private System.Windows.Forms.Label ready2;
        private System.Windows.Forms.Label ready3;
        private System.Windows.Forms.GroupBox charCountGroup;
        private System.Windows.Forms.Label separator_1;
        private System.Windows.Forms.CheckBox charNovelpia;
        private System.Windows.Forms.CheckBox charMarks;
        private System.Windows.Forms.CheckBox charBlank;
        private System.Windows.Forms.Label labelQuickChar;
        private System.Windows.Forms.Button charQuickNormal;
        private System.Windows.Forms.Button charQuickNovelpia;
        private System.Windows.Forms.GroupBox charDisplayGroup;
        private System.Windows.Forms.Label labelCharMainApp;
        private System.Windows.Forms.ComboBox charMainApp;
        private System.Windows.Forms.Label labelCharOverlayFormat;
        private System.Windows.Forms.TextBox charOverlayFormat;
        private System.Windows.Forms.ToolStripMenuItem ItemRestart;
        private System.Windows.Forms.GroupBox appSettingsGroup;
        private System.Windows.Forms.Button resetProgram;
        private System.Windows.Forms.Label labelCharDraftPath;
        private System.Windows.Forms.TabPage pageHelp;
        private System.Windows.Forms.Button navigateGithub;
        private System.Windows.Forms.Button navigateDiscord;
        private System.Windows.Forms.TextBox charDraftPath;
        private System.Windows.Forms.CheckBox charUseDisplay;
    }
}

