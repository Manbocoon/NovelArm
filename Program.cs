using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NovelArm
{
    static class Program
    {
        #region Properties
        internal static readonly string APP_NAME = Application.ProductName;
        internal static double VERSION = 99999.99999;
        internal static bool UPDATED = false;

        internal static readonly string PATH = Application.StartupPath;
        internal static readonly string EXE_PATH = Application.ExecutablePath;
        
        internal static ConfigForm configForm;
        #endregion

        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // 버전값
            string content = Application.ProductVersion;
            string pattern = @"(\d+[.]\d+)[.]\d+[.]\d+";
            Match match = Regex.Match(content, pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                double.TryParse(match.Groups[1].Value, out VERSION);

            // 업데이트로 실행된 경우
            if (args.Length == 4)
            {
                string action = args[0];
                string oldPidStr = args[1];
                string oldFile = args[2];
                string numberForFun = args[3];

                if (action.ToLower() == "-update" || action.ToLower() == "/update")
                {
                    Process oldProc = null;
                    int.TryParse(oldPidStr, out int oldPid);

                    while (oldProc != null)
                    {
                        try { oldProc = Process.GetProcessById(oldPid); oldProc.Kill(); }
                        catch (Exception) { }

                        Thread.Sleep(200);
                    }

                    try
                    { File.Delete($@"{Program.PATH}\{oldFile}"); }
                    catch (Exception) { }

                    UPDATED = true;
                    Modules.System.NativeMethods.SetForegroundWindow(Process.GetCurrentProcess().Handle);
                    MessageBox.Show($"{numberForFun} 지구에서 성공적으로 {APP_NAME} {VERSION}을(를) 훔쳐왔어요!\n뒷일은 저에게 맡기시고 작가님께서 잘 활용하시길 바랄게요!", "상태창", 0, MessageBoxIcon.Information);
                }
            }

            // 프로그램 중복실행 방지
            Mutex mutex = new Mutex(true, APP_NAME, out bool isNotDuplicated);
            bool isDuplicated = !isNotDuplicated;
            if (isDuplicated)
            {
                MessageBox.Show("프로그램이 이미 실행중입니다.\n\nWindows 작업 표시줄의 트레이 메뉴에서 확인해보세요.", APP_NAME, 0, MessageBoxIcon.Error);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            configForm = new ConfigForm();
            Application.Run(configForm);

            mutex.ReleaseMutex();
        }
    }
}
