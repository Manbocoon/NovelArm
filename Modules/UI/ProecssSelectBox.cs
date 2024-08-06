using NovelArm.Modules.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NovelArm
{
    internal class ProcessSelectBox : IDisposable
    {
        public void Dispose()
        { }

        /// <summary>
        /// Alt Tab Switcher에 보이는 앱들을 나열하여 선택하도록 합니다.
        /// </summary>
        /// <param name="caption">폼의 제목입니다.</param>
        /// <returns>빈 기본값 구조체: 선택하지 않았거나 자기 자신을 선택한 경우 || 이외: 프로세스 값 반환</returns>
        internal AltTabSwitcher.ProcessInfo ShowDialog(string caption)
        {
            AltTabSwitcher.ProcessInfo procInfo = new AltTabSwitcher.ProcessInfo();

            List<AltTabSwitcher.ProcessInfo> procs = null;
            ImageList imageList = new ImageList();

            using (var altTab = new AltTabSwitcher())
                procs = altTab.GetAltTabSwitcherApps();
            
            foreach (var proc in procs)
            {
                using (var processes = new Processes())
                    imageList.Images.Add(proc.Pid.ToString(), processes.GetSmallWindowIcon(proc.MainWindowHandle));
            }

            Form form = new Form
            {
                Text = caption,
                StartPosition = FormStartPosition.CenterParent,
                ClientSize = new System.Drawing.Size(750, 400),
                TopMost = true,
                MinimizeBox = false,
                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Font = new System.Drawing.Font("맑은 고딕", 10)
            };


            ListView listView = new ListView
            {
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(form.Width - 35, form.Height - 105),
                View = View.Details,
                SmallImageList = imageList,
                FullRowSelect = true
            };
            listView.Columns.Add("창 이름", 660);
            listView.Columns.Add("PID", 60);
            for (int index=0; index<procs.Count; ++index)
            {
                listView.Items.Add(new ListViewItem(new string[] { procs[index].MainWindowText, procs[index].Pid.ToString() }, procs[index].Pid.ToString()));
            }
            form.Controls.Add(listView);

            // Select Button
            Button okButton = new Button
            {
                DialogResult = DialogResult.OK,
                Name = "okButton",
                Size = new System.Drawing.Size(form.Width - 35, 35),
                Text = "선택",
                Location = new System.Drawing.Point(10, form.Height - 85)
            };
            form.Controls.Add(okButton);

            // Events
            okButton.Click += new EventHandler(delegate (object obj, EventArgs evt)
            {
                if (listView.SelectedItems.Count == 0)
                    procInfo = default;

                else
                    procInfo = procs[listView.SelectedIndices[0]];
            });
            form.FormClosing += new FormClosingEventHandler(delegate (object obj, FormClosingEventArgs evt)
            {
                if (listView.SelectedItems.Count == 0)
                    procInfo = default;

                else
                    procInfo = procs[listView.SelectedIndices[0]];
            });

            DialogResult result = form.ShowDialog();
            form.Dispose();

            return procInfo;
        }
    }
}
