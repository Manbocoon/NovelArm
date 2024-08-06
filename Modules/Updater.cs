using NovelArm.Modules.Systems;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NovelArm.Modules
{

    public class Updater : IDisposable
    {
        #region Properties
        internal readonly string timeFilePath = Program.PATH + @"\Settings\Updater.json";
        internal readonly string releaseURL = "https://github.com/Manbocoon/NovelArm/releases";
        internal readonly string downloadURL = "https://github.com/Manbocoon/NovelArm/releases/latest/download/";
        internal struct AppInfo
        {
            public string Name { get; set; }
            public string Version { get; set; }
            public string Status { get; set; }
            public string PatchNote { get; set; }
            public DateTime lastCheckedTime { get; set; }
        }
        #endregion

        public void Dispose()
        { }



        /// <summary>
        /// 네트워크가 연결되어 있는지 확인합니다. 실제 웹에 안정적으로 연결 가능한지는 확인하지 않습니다.
        /// </summary>
        internal bool IsNetworkAvailable()
        {
            bool isConnected = false;

            try
            { isConnected = NativeMethods.InternetGetConnectedState(out int desc, 0); }

            catch (Exception)
            { return false; }

            return isConnected;
        }

        /// <summary>
        /// 현재 최신 버전에 대한 정보를 Github에서 불러옵니다.
        /// </summary>
        internal AppInfo GetInfoFromWeb()
        {
            AppInfo appInfo = new AppInfo();
            string nameWithVersion = null;
            StringBuilder patchNote = new StringBuilder(null);

            DateTime lastCheckedDate = ReadLastCheckedDate();
            TimeSpan date_span = (DateTime.Now - lastCheckedDate);

            // 트래픽 부하 방지, 5분의 쿨타임
            if (date_span.TotalSeconds < 300)
                return appInfo;

            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                try
                {
                    Stream webData = webClient.OpenRead(releaseURL + "/latest");
                    StreamReader webDataReader = new StreamReader(webData);
                    byte extractedData = 0;
                    bool readingPatchNote = false;
                    
                    while (!webDataReader.EndOfStream)
                    {
                        string webDataLine = webDataReader.ReadLine().Trim();

                        // 이름과 버전값 추출
                        if (webDataLine.Contains("<title>Release"))
                        {
                            nameWithVersion = webDataLine;
                            ++extractedData;
                        }

                        // 패치 노트 끝날 때까지 여러줄 추출
                        else if (webDataLine.Contains("og:description"))
                        {
                            HighlightPatchNote(ref webDataLine);
                            patchNote.AppendLine(webDataLine);
                            readingPatchNote = true;
                            ++extractedData;
                        }
                        else if (readingPatchNote)
                        {
                            if (String.IsNullOrWhiteSpace(webDataLine))
                                continue;
                            HighlightPatchNote(ref webDataLine);

                            patchNote.AppendLine(webDataLine);
                            if (webDataLine.Trim().EndsWith("/>"))
                                readingPatchNote = false;
                        }

                        // 모든 값을 추출했으면 더 읽지 않고 중단
                        if (extractedData > 2 && !readingPatchNote)
                            break;
                    }

                    webDataReader.Dispose();
                    webData.Dispose();
                }

                catch (Exception ex)
                {
                    appInfo.Status = $"UPDATE_CHECK ERROR || {ex.Message}";
                    return appInfo;
                }
            }

            try
            {
                // 이름과 버전값
                string content = nameWithVersion;
                string pattern = @"<title>Release ([+-]?[[0-9]*[.]]?[0-9]+) · [\w]+/([\w]+) · GitHub</title>";
                Match match = Regex.Match(content, pattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    GroupCollection groups = match.Groups;
                    appInfo.Version = groups[1].Value;
                    appInfo.Name = groups[2].Value;
                }

                // 패치노트
                patchNote.RemoveExt("og:description\" content=\"", removeType: 1);
                patchNote.RemoveExt( "\" />", removeType: 2);
                appInfo.PatchNote = patchNote.ToString();

                // 마지막 업데이트 확인 시간
                appInfo.lastCheckedTime = DateTime.Now;

                // 다운로드 주소
                /*
                    downloadURL + "NovelArm.exe"
                    downloadURL + "NovelArm.zip"
                */
                SaveLastCheckedDate(ref appInfo);
            }

            catch (Exception)
            {
                appInfo.Name = "SERVER NOT AVAILABLE";
            }

            return appInfo;
        }

        private DateTime ReadLastCheckedDate()
        {
            AppInfo loggedUpdaterInfo = Json.ReadJsonFromFile<AppInfo>(timeFilePath);
            DateTime lastUpdateTime = loggedUpdaterInfo.lastCheckedTime;
            if (DateTime.Compare(loggedUpdaterInfo.lastCheckedTime, DateTime.Now) > 0)
                lastUpdateTime = DateTime.MinValue;
            
            return lastUpdateTime;
        }

        private bool SaveLastCheckedDate(ref AppInfo appInfo)
        {
            string text = $"{appInfo.Name}\n{appInfo.Version}\n{appInfo.lastCheckedTime}\n{appInfo.PatchNote}";
            
            Settings.CreateDirectory();
            
            if (!Json.WriteJsonToFile<AppInfo>(timeFilePath, appInfo))
                return false;

            return true;
        }

        internal void HighlightPatchNote(ref string textLine)
        {
            string[] highlightTexts = new string[] { "기능 추가 및 변경", "버그 수정", "효율성 향상" };

            foreach (string highlightText in highlightTexts)
            {
                if (textLine.Contains(highlightText))
                {
                    textLine = $"\n{textLine} ▼";
                    return;
                }
            }
        }


        /// <summary>
        /// 바이너리를 스스로 업데이트합니다.
        /// </summary>
        /// <returns>업데이트 성공 여부입니다.</returns>
        internal string UpdateMySelf(ref int numberForFun)
        {
            string status = null;
            string newFilePath = null;

            // Github에서 최신 실행파일 다운로드
            newFilePath = $@"{Program.PATH}\{Files.GetUniqueFileName()}.new";
            using (WebClient client = new WebClient())
            {
                try
                { client.DownloadFile(downloadURL + "NovelArm.exe", newFilePath); }
                // { File.Copy("C:\\NovelArm.exe", newFilePath); } 테스트 코드
                catch (Exception ex)
                { return "ERROR | " + ex.Message; }
            }

            // 현재 설정 저장
            using (var settings = new AppSettings())
                settings.SaveToFile();
            using (var settings = new OverlaySettings())
                settings.SaveToFile();

            // 현재 프로세스의 파일명 변경
            string originalFilePath = $@"{Program.PATH}\{Files.GetUniqueFileName()}.old";
            Program.configForm.fileLock.Dispose();
            File.Move(Program.EXE_PATH, originalFilePath);

            // 최신버전 실행파일로 교체
            File.Move(newFilePath, Program.EXE_PATH);

            // 업데이트 옵션으로 새로운 파일 실행
            new Process().ExecuteWithArguments(
                filePath: Program.EXE_PATH,
                arguments: $"/Update {Process.GetCurrentProcess().Id} {Path.GetFileName(originalFilePath)} {numberForFun}"
                );
            Environment.Exit(0);

            return status;
        }


        /// <summary>
        /// 두 버전값을 비교합니다.
        /// </summary>
        /// <param name="v1">버전1</param>
        /// <param name="v2">버전2</param>
        /// <returns>-1: v2가 더 최신임 || 0: 두 버전이 같음 || 1: v1이 더 최신임 || -1000: 오류</returns>
        internal static int CompareVersionStrings(string v1, string v2)
        {
            if (String.IsNullOrWhiteSpace(v1) || !v1.Contains("."))
                return 0;
            if (String.IsNullOrWhiteSpace(v2) || !v2.Contains("."))
                return 0;

            int rc = -1000;

            v1 = v1.ToLower();
            v2 = v2.ToLower();

            if (v1 == v2)
                return 0;

            string[] v1parts = v1.Split('.');
            string[] v2parts = v2.Split('.');

            for (int i = 0; i < v1parts.Length; i++)
            {
                if (v2parts.Length < i + 1)
                    break;

                string v1Token = v1parts[i];
                string v2Token = v2parts[i];

                int x;
                bool v1Numeric = int.TryParse(v1Token, out x);
                bool v2Numeric = int.TryParse(v2Token, out x);

                if (v1Numeric && v2Numeric)
                {
                    while (v1Token.Length < v2Token.Length)
                        v1Token = "0" + v1Token;
                    while (v2Token.Length < v1Token.Length)
                        v2Token = "0" + v2Token;
                }

                rc = String.Compare(v1Token, v2Token, StringComparison.Ordinal);
                if (rc != 0)
                    break;
            }

            if (rc == 0)
            {
                if (v1parts.Length > v2parts.Length)
                    rc = 1; // v1 is higher version than v2
                else if (v2parts.Length > v1parts.Length)
                    rc = -1; // v1 is lower version than v2
            }

            if (rc == 0 || rc == -1000)
                return rc;
            else
                return rc < 0 ? -1 : 1;
        }
    }
}
