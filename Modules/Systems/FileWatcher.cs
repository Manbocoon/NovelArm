using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

            // 과부하 방지
            EventEnabled = false;

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
            {
                EventEnabled = true;
                return;
            }

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

                Thread.Sleep(250);
            }

            // 확장자에 따라 다르게 처리
            if (fileExtension == ".rtf")
                verifiedDraft = TextConverter.RtfToPlainText(fileContent);
            
            else if (fileExtension == ".txt")
                verifiedDraft = fileContent;
            
            verifiedDraft = verifiedDraft.Trim();

            // 오버레이 화면에 표시
            Program.configForm.charDisplay.SyncDraft();

            // 과부하 방지
            Thread.Sleep(250);
            EventEnabled = true;
        }

    }
}
