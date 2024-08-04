using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NovelArm.Modules.Systems
{

    internal static class FileWatcher
    {
        #region Properties
        internal static FileSystemWatcher watcher = new FileSystemWatcher();
        internal static bool finding = false;
        internal static string selectedApp = "Scrivener";
        internal static string draftFilePath { get; set; } = null;
        internal static string verifiedDraft { get; set; } = "";

        internal static bool EventEnabled
        {
            get { return watcher.EnableRaisingEvents; }
            set { watcher.EnableRaisingEvents = value; }
        }

        internal static string FileFilter
        {
            get { return watcher.Filter; }
            set { watcher.Filter = value; }
        }

        internal static string DirPath
        {
            get { return watcher.Path; }
            set 
            {
                if (Directory.Exists(value))
                    watcher.Path = value ?? "C:\\";
            }
        }
        #endregion

        /// <summary>
        /// 새로운 FileSystemWatcher 이벤트를 실행합니다.
        /// </summary>
        /// <param name="watcherDirectoryPath">하위 파일들의 변화를 감지할 디렉터리 경로입니다.</param>
        /// <param name="fileExtensionFilter">파일을 확장자로 필터링하여 감지합니다. (*.*, *.hwp, *.hwpx, *.rtf 등)</param>
        internal static void Run(string watcherDirectoryPath, string fileExtensionFilter)
        {
            watcher.Path = watcherDirectoryPath ?? "C:\\";
            watcher.Filter = fileExtensionFilter;
            watcher.NotifyFilter = NotifyFilters.LastWrite; // NotifyFilters.LastWrite | NotifyFilters.Size;
            watcher.IncludeSubdirectories = true;
            watcher.Changed += Watcher_Changed;
        }

        /// <summary>
        /// FileSystemWatcher 이벤트를 해제합니다.
        /// </summary>
        internal static void Release()
        {
            watcher.Changed -= Watcher_Changed;
        }

        private static void Watcher_Changed(object sender, FileSystemEventArgs evt)
        {
            if (evt.ChangeType != WatcherChangeTypes.Changed)
                return;

            if (String.IsNullOrEmpty(DirPath) || !Directory.Exists(DirPath))
                return;

            string fullFilePath = evt.FullPath;
            string fileName = Path.GetFileName(fullFilePath);
            string fileExtension = Path.GetExtension(fullFilePath);
            string targetFileExtension = ".extension";


            // 스크리브너
            if (selectedApp == "Scrivener")
            {
                fileName = fileName.Replace(".new.", ".").Replace(".old.", ".");
                fullFilePath = Path.Combine(Path.GetDirectoryName(fullFilePath), fileName);
                targetFileExtension = ".rtf";
            }

            else if (selectedApp == "Notepad")
            {
                targetFileExtension = ".txt";
            }

            // 설정된 앱의 원고 파일과 다른 확장자면 취소
            if (fileExtension != targetFileExtension)
                return;

            // 20초 이내에 파일 읽어들이기
            string fileContent = null;
            bool read_success = false;
            for (int count=0; count<80; ++count)
            {
                try
                {
                    Encoding fileEncoding = TextConverter.GetEncoding(fullFilePath);
                    fileContent = File.ReadAllText(fullFilePath, fileEncoding).Trim();
                    read_success = true;
                }
                catch (IOException) { read_success = false; }

                if (read_success)
                    break;

                System.Threading.Thread.Sleep(250);
            }

            // 확장자에 따라 다르게 처리
            if (fileExtension == ".rtf")
                verifiedDraft = TextConverter.RtfToPlainText(fileContent);
            
            else if (fileExtension == ".txt")
                verifiedDraft = fileContent;
            
            verifiedDraft = verifiedDraft.Trim();

            // 오버레이 화면에 표시
            Program.configForm.charDisplay.SyncDraft();

            // 새로운 원고를 찾는 중이라면
            /*
            if (finding)
            {
                Program.configForm.Invoke((MethodInvoker)delegate () { 
                    overlayHwnd = Program.configForm.charDisplay.Handle;
                    overlayForm = Program.configForm.charDisplay;
                });

                switch (selectedApp)
                {
                    case "Scrivener":
                        // 제대로 된 원고 파일이 아닐 경우 취소    content.rtf || content.new.rtf
                        if (!fileName.Contains("content"))
                            return;

                        // 스크리브너가 자동 생성하는 .old | .new 무시
                        fileName = fileName.Replace(".new.", ".").Replace(".old.", ".");
                        fullFilePath = Path.Combine(Path.GetDirectoryName(fullFilePath), fileName);

                        string draft = null, draft_plain = null;
                        try
                        {
                            draft = File.ReadAllText(fullFilePath, new UTF8Encoding(false)).Trim();
                            draft_plain = TextConverter.RtfToPlainText(draft);
                            draft_plain = draft_plain.Length > 700 ? draft_plain.Substring(0, 700) : draft_plain;
                        }
                        catch (Exception) { }
                        
                        if (string.IsNullOrEmpty(draft_plain))
                            return;

                        if (overlayHwnd != IntPtr.Zero)
                            NativeMethods.SetForegroundWindow(overlayHwnd);

                        DialogResult userAction = DialogResult.None;
                        Program.configForm.Invoke((MethodInvoker)delegate () {
                            userAction = MessageBox.Show(overlayForm, $"방금 탐지한 원고 파일을 최대 700자만큼 보여드리겠습니다. 찾으시던 원고가 맞나요?\n\n====================\n\n{draft_plain.Trim()}", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        });
                        if (userAction == DialogResult.Yes)
                        {
                            finding = false;
                            draftFilePath = fullFilePath;
                            return;
                        }
                        break;

                    case "Notepad":

                        break;

                    default:
                        break;
                }
            }
            
            // 원고가 설정되었고 해당 파일도 존재할 경우
            else if (File.Exists(draftFilePath))
            {
                if (selectedApp == "Scrivener")
                {
                    fileName = fileName.Replace(".new.", ".").Replace(".old.", ".");
                    fullFilePath = Path.Combine(Path.GetDirectoryName(fullFilePath), fileName);
                }

                // 파일 읽어들이기
                string fileContent = null;
                bool read_success = false;
                while (!read_success)
                {
                    try
                    {
                        fileContent = File.ReadAllText(fullFilePath, new UTF8Encoding(false)).Trim();
                        read_success = true;
                    }
                    catch (IOException) { read_success = false; }

                    System.Threading.Thread.Sleep(250);
                }

                // 확장자에 따라 다르게 처리
                if (fileExtension == ".rtf")
                {
                    verifiedDraft = TextConverter.RtfToPlainText(fileContent);
                }

                else if (fileExtension == ".txt")
                {
                    verifiedDraft = fileContent;
                }
                verifiedDraft = verifiedDraft.Trim();

                // 오버레이 화면에 표시
                Program.configForm.charDisplay.SyncDraft();
            }
            */




        }

    }
}
