
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
            this.ItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tabMenu = new System.Windows.Forms.TabControl();
            this.pageApp = new System.Windows.Forms.TabPage();
            this.appGroup = new System.Windows.Forms.GroupBox();
            this.checkUpdate = new System.Windows.Forms.CheckBox();
            this.regStartup = new System.Windows.Forms.CheckBox();
            this.pageLiveCount = new System.Windows.Forms.TabPage();
            this.ready1 = new System.Windows.Forms.Label();
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
            this.panelMenu = new System.Windows.Forms.Panel();
            this.navigateGithub = new System.Windows.Forms.LinkLabel();
            this.TrayMenu.SuspendLayout();
            this.tabMenu.SuspendLayout();
            this.pageApp.SuspendLayout();
            this.appGroup.SuspendLayout();
            this.pageLiveCount.SuspendLayout();
            this.pageTarget.SuspendLayout();
            this.pageAutoComplete.SuspendLayout();
            this.pageConvert.SuspendLayout();
            this.cbGroup.SuspendLayout();
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
            this.ItemExit});
            this.TrayMenu.Name = "TrayMenu";
            this.TrayMenu.Size = new System.Drawing.Size(99, 54);
            // 
            // ItemSettings
            // 
            this.ItemSettings.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ItemSettings.Name = "ItemSettings";
            this.ItemSettings.Size = new System.Drawing.Size(98, 22);
            this.ItemSettings.Text = "설정";
            this.ItemSettings.Click += new System.EventHandler(this.ItemSettings_Click);
            // 
            // ItemSeparator2
            // 
            this.ItemSeparator2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ItemSeparator2.Name = "ItemSeparator2";
            this.ItemSeparator2.Size = new System.Drawing.Size(95, 6);
            // 
            // ItemExit
            // 
            this.ItemExit.Name = "ItemExit";
            this.ItemExit.Size = new System.Drawing.Size(98, 22);
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
            this.tabMenu.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tabMenu.Location = new System.Drawing.Point(199, 12);
            this.tabMenu.Name = "tabMenu";
            this.tabMenu.SelectedIndex = 0;
            this.tabMenu.Size = new System.Drawing.Size(531, 605);
            this.tabMenu.TabIndex = 3;
            // 
            // pageApp
            // 
            this.pageApp.Controls.Add(this.appGroup);
            this.pageApp.Location = new System.Drawing.Point(4, 26);
            this.pageApp.Name = "pageApp";
            this.pageApp.Padding = new System.Windows.Forms.Padding(3);
            this.pageApp.Size = new System.Drawing.Size(523, 575);
            this.pageApp.TabIndex = 0;
            this.pageApp.Text = "일반";
            this.pageApp.UseVisualStyleBackColor = true;
            // 
            // appGroup
            // 
            this.appGroup.Controls.Add(this.checkUpdate);
            this.appGroup.Controls.Add(this.regStartup);
            this.appGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.appGroup.Location = new System.Drawing.Point(3, 3);
            this.appGroup.Name = "appGroup";
            this.appGroup.Size = new System.Drawing.Size(517, 79);
            this.appGroup.TabIndex = 5;
            this.appGroup.TabStop = false;
            this.appGroup.Text = "시작 옵션";
            // 
            // checkUpdate
            // 
            this.checkUpdate.AutoSize = true;
            this.checkUpdate.Checked = true;
            this.checkUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
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
            this.regStartup.Checked = true;
            this.regStartup.CheckState = System.Windows.Forms.CheckState.Checked;
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
            this.pageLiveCount.Controls.Add(this.ready1);
            this.pageLiveCount.Location = new System.Drawing.Point(4, 26);
            this.pageLiveCount.Name = "pageLiveCount";
            this.pageLiveCount.Padding = new System.Windows.Forms.Padding(3);
            this.pageLiveCount.Size = new System.Drawing.Size(523, 575);
            this.pageLiveCount.TabIndex = 1;
            this.pageLiveCount.Text = "글자수 표시";
            this.pageLiveCount.UseVisualStyleBackColor = true;
            // 
            // ready1
            // 
            this.ready1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ready1.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ready1.Location = new System.Drawing.Point(3, 3);
            this.ready1.Name = "ready1";
            this.ready1.Size = new System.Drawing.Size(517, 32);
            this.ready1.TabIndex = 0;
            this.ready1.Text = "아직 준비 중이에요!";
            this.ready1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pageTarget
            // 
            this.pageTarget.Controls.Add(this.ready2);
            this.pageTarget.Location = new System.Drawing.Point(4, 26);
            this.pageTarget.Name = "pageTarget";
            this.pageTarget.Padding = new System.Windows.Forms.Padding(3);
            this.pageTarget.Size = new System.Drawing.Size(523, 575);
            this.pageTarget.TabIndex = 2;
            this.pageTarget.Text = "목표 설정";
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
            this.pageAutoComplete.Size = new System.Drawing.Size(523, 575);
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
            this.pageConvert.Size = new System.Drawing.Size(523, 575);
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
            this.cbGroup.Location = new System.Drawing.Point(3, 3);
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.Size = new System.Drawing.Size(517, 119);
            this.cbGroup.TabIndex = 4;
            this.cbGroup.TabStop = false;
            this.cbGroup.Text = "클립보드 데이터";
            // 
            // cbKeybindLabel
            // 
            this.cbKeybindLabel.AutoSize = true;
            this.cbKeybindLabel.Location = new System.Drawing.Point(6, 86);
            this.cbKeybindLabel.Name = "cbKeybindLabel";
            this.cbKeybindLabel.Size = new System.Drawing.Size(81, 17);
            this.cbKeybindLabel.TabIndex = 4;
            this.cbKeybindLabel.Text = "단축키 입력:";
            // 
            // cbSetKeybind
            // 
            this.cbSetKeybind.BackColor = System.Drawing.Color.White;
            this.cbSetKeybind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbSetKeybind.Location = new System.Drawing.Point(93, 83);
            this.cbSetKeybind.Name = "cbSetKeybind";
            this.cbSetKeybind.ReadOnly = true;
            this.cbSetKeybind.Size = new System.Drawing.Size(300, 25);
            this.cbSetKeybind.TabIndex = 3;
            this.cbSetKeybind.Text = "Ctrl + Oemtilde";
            this.cbSetKeybind.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cbSetKeybind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbSetKeybind_KeyDown);
            this.cbSetKeybind.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbSetKeybind_KeyUp);
            // 
            // cbUseKeybind
            // 
            this.cbUseKeybind.AutoSize = true;
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
            this.cbAutoConvert.Checked = true;
            this.cbAutoConvert.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoConvert.Location = new System.Drawing.Point(6, 24);
            this.cbAutoConvert.Name = "cbAutoConvert";
            this.cbAutoConvert.Size = new System.Drawing.Size(198, 21);
            this.cbAutoConvert.TabIndex = 1;
            this.cbAutoConvert.Text = "자동으로 일반 텍스트로 변환";
            this.cbAutoConvert.UseVisualStyleBackColor = true;
            this.cbAutoConvert.Click += new System.EventHandler(this.cbAutoConvert_Click);
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMenu.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelMenu.Location = new System.Drawing.Point(12, 12);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(181, 500);
            this.panelMenu.TabIndex = 4;
            // 
            // navigateGithub
            // 
            this.navigateGithub.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.navigateGithub.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.navigateGithub.Cursor = System.Windows.Forms.Cursors.Hand;
            this.navigateGithub.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.navigateGithub.Image = global::NovelArm.Properties.Resources.Github_Logo;
            this.navigateGithub.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.navigateGithub.LinkColor = System.Drawing.Color.Black;
            this.navigateGithub.Location = new System.Drawing.Point(12, 523);
            this.navigateGithub.Name = "navigateGithub";
            this.navigateGithub.Size = new System.Drawing.Size(181, 90);
            this.navigateGithub.TabIndex = 0;
            this.navigateGithub.TabStop = true;
            this.navigateGithub.Text = "모든 버전 확인";
            this.navigateGithub.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.navigateGithub.VisitedLinkColor = System.Drawing.Color.Black;
            this.navigateGithub.MouseClick += new System.Windows.Forms.MouseEventHandler(this.navigateGithub_MouseClick);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 629);
            this.Controls.Add(this.navigateGithub);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.tabMenu);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "ConfigForm";
            this.ShowInTaskbar = false;
            this.Text = "노벨암 (NovelArm)";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_FormClosing);
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.TrayMenu.ResumeLayout(false);
            this.tabMenu.ResumeLayout(false);
            this.pageApp.ResumeLayout(false);
            this.appGroup.ResumeLayout(false);
            this.appGroup.PerformLayout();
            this.pageLiveCount.ResumeLayout(false);
            this.pageTarget.ResumeLayout(false);
            this.pageAutoComplete.ResumeLayout(false);
            this.pageConvert.ResumeLayout(false);
            this.cbGroup.ResumeLayout(false);
            this.cbGroup.PerformLayout();
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
        private System.Windows.Forms.GroupBox appGroup;
        private System.Windows.Forms.CheckBox checkUpdate;
        private System.Windows.Forms.TextBox cbSetKeybind;
        private System.Windows.Forms.Label cbKeybindLabel;
        private System.Windows.Forms.LinkLabel navigateGithub;
        private System.Windows.Forms.Label ready1;
        private System.Windows.Forms.Label ready2;
        private System.Windows.Forms.Label ready3;
    }
}

