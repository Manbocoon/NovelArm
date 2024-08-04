using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

using NovelArm.Modules;
using NovelArm.Modules.Systems;

namespace NovelArm
{
    static class Program
    {
        #region Properties
        internal static readonly string APP_NAME = Application.ProductName;
        internal static readonly string PATH = Application.StartupPath;
        internal static readonly string EXE_PATH = Application.ExecutablePath;

        internal static double VERSION = 99999.99999;
        internal static bool UPDATED = false;
        internal static bool HIDE_WINDOW = false;
        internal static readonly Process thisProcess = Process.GetCurrentProcess();
        internal static readonly IntPtr minWorkingSet = thisProcess.MinWorkingSet;
        internal static readonly IntPtr maxWorkingSet = thisProcess.MaxWorkingSet;

        internal static readonly Size SCREEN = Screen.PrimaryScreen.Bounds.Size;

        internal static ConfigForm configForm;
        #endregion

        internal static void EmptyMyWorkingSet()
        {
            NativeMethods.EmptyWorkingSet(thisProcess.Handle);
        }

        internal static void SetForegroundThis()
        {
            configForm.Invoke((MethodInvoker)delegate ()
            {
                NativeMethods.SetForegroundWindow(configForm.Handle);
            });
        }

        /// <summary>
        /// 특정 폼이 화면에 정해진 퍼센트만큼 보이는지의 여부를 반환합니다. 0%이면 폼이 화면에 아예 담겨있지 않은 경우입니다.
        /// </summary>
        /// <param name="formLoc"></param>
        /// <param name="formSize"></param>
        /// <param name="MinPercentOnScreen"></param>
        /// <returns></returns>
        internal static bool IsOnScreen(Point formLoc, Size formSize, double MinPercentOnScreen = 0.2)
        {
            double PixelsVisible = 0;
            Rectangle Rec = new Rectangle(formLoc, formSize);

            foreach (Screen Scrn in Screen.AllScreens)
            {
                Rectangle r = Rectangle.Intersect(Rec, Scrn.WorkingArea);
                if (r.Width != 0 & r.Height != 0)
                {
                    PixelsVisible += (r.Width * r.Height);
                }
            }
            // double percent = PixelsVisible / (Rec.Width * Rec.Height);
            return PixelsVisible >= (Rec.Width * Rec.Height) * MinPercentOnScreen;
        }

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

            // Arguments 처리
            string action = null;
            switch (args.Length)
            { 
                // 창 숨기기
                case 1:
                    action = args[0].ToLower();
                    if (action.EndsWith("hidewindow"))
                    {
                        HIDE_WINDOW = true;
                    }
                    break;

                // 재시작 | 설정 초기화
                case 2:
                    action = args[0].ToLower();

                    if (action.EndsWith("restart") || action.EndsWith("reset"))
                    {
                        int.TryParse(args[1], out int oldPid);

                        ProcessExt.WaitForClose(oldPid, maxWaitMilliseconds: 10000, terminate: false);
                    }
                    break;

                // 업데이트
                case 4:
                    action = args[0].ToLower();
                    string oldPidStr = args[1];
                    string oldFile = args[2];
                    string numberForFun = args[3];

                    if (action.EndsWith("update"))
                    {
                        int.TryParse(oldPidStr, out int oldPid);

                        ProcessExt.WaitForClose(oldPid, maxWaitMilliseconds: 10000, terminate: true);

                        UPDATED = true;
                        NativeMethods.SetForegroundWindow(Process.GetCurrentProcess().Handle);
                        MessageBox.Show($"{numberForFun}번 지구에서 성공적으로 {APP_NAME} {VERSION}을(를) 훔쳐왔어요!\n뒷일은 저에게 맡기시고 작가님께서 잘 활용하시길 바랄게요!", "상태창", 0, MessageBoxIcon.Information);
                    }
                    break;

                default:
                    break;
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
