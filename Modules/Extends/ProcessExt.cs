using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovelArm.Modules
{
    internal static class ProcessExt
    {
        /// <summary>
        /// 특정 프로그램에 인수를 전달하며 실행하는 Process 확장 함수입니다.
        /// </summary>
        internal static void ExecuteWithArguments(this Process proc, string filePath, string arguments)
        {
            ProcessStartInfo procInfo = new ProcessStartInfo
            {
                FileName = filePath,
                Arguments = arguments,
                Verb = "runas",
                CreateNoWindow = false,
                UseShellExecute = false
            };

            proc.StartInfo = procInfo;
            proc.Start();
        }

        /// <summary>
        /// 특정 프로세스가 종료되기까지 기다립니다.
        /// <param name="procId">타겟 프로세스의 id입니다.</param>
        /// <param name="maxWaitMilliseconds">최대로 기다릴 Millisecond 시간입니다. 입력하지 않을 시 최대 65초만큼 기다립니다.</param>
        /// <param name="terminate">기다리면서 프로세스를 강제 종료시킬지의 여부입니다.</param>
        /// </summary>
        internal static void WaitForClose(int procId, ushort maxWaitMilliseconds = 65535, bool terminate = false)
        {
            // 0은 Windows 시스템 프로세스가 사용중인 id이므로 없는 값을 음수로 대체하여 표현
            if (procId == 0)
                procId = -1;

            // 프로세스 정보 획득
            Process proc = null;
            try { proc = Process.GetProcessById(procId); }
            catch (ArgumentException) { return; }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Reset();
            stopWatch.Start();

            while (proc != null && stopWatch.ElapsedMilliseconds < maxWaitMilliseconds)
            {
                try
                {
                    proc = Process.GetProcessById(procId);

                    if (terminate)
                        proc.Kill();
                }
                catch (ArgumentException) { return; }
                catch (Exception) { }

                Thread.Sleep(200);
            }
        }
    }
}
