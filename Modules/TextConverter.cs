using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using NovelArm.Modules.System;

namespace NovelArm.Modules
{
    internal static class TextConverter
    {
        #region Properties
        private static Thread _worker;
        private static bool terminate = false;
        private static string lastCbText = null, lastCbRTF = null, CbText = null, CbRTF = null;

        internal static bool cbChanged = false, cbWorking = false;
        internal static bool useAutoConvert = true, useKeybind = false;
        internal static List<int> keybinds = new List<int> { 17, 18, 123 };
        #endregion


        /// <summary>
        /// 백그라운드 스레드가 실행할 작업
        /// </summary>
        private static void Works()
        {
            while (!terminate)
            {
                int maxSleepTime = 500;
                int keybindSleepTime = 75;
                int additionalSleepTime = maxSleepTime - keybindSleepTime;
                // 자동 변환 설정을 사용 중이라면
                if (useAutoConvert)
                {
                    // 클립보드 변화가 없으면 취소
                    if (!cbChanged)
                    {
                        Thread.Sleep(additionalSleepTime);
                        continue;
                    }

                    CbText = null; CbRTF = null;
                    Program.configForm.Invoke((MethodInvoker)delegate ()
                    {
                        if (Clipboard.ContainsText())
                        {
                            CbText = Clipboard.GetText(TextDataFormat.UnicodeText);
                            CbRTF = Clipboard.GetText(TextDataFormat.Rtf);
                        }
                    });

                    // 텍스트가 아니면 취소
                    if (String.IsNullOrEmpty(CbText))
                    {
                        cbChanged = false;
                        Thread.Sleep(additionalSleepTime);
                        continue;
                    }

                    // 만약 같은 글이거나, 이미 변환이 완료된 데이터를 여러 번 반복하여 작업하게 되는 경우 취소
                    if (    (lastCbRTF == CbRTF && String.IsNullOrEmpty(CbRTF))
                        ||  (lastCbText == CbText && lastCbRTF != CbRTF))
                    {
                        cbChanged = false;
                        Thread.Sleep(additionalSleepTime);
                        continue;
                    }
                    cbWorking = true;

                    // 클립보드 값 변환 처리 시작
                    Program.configForm.Invoke((MethodInvoker)delegate ()
                    {
                        // 새로운 PlainText 클립보드 생성
                        string newText = ChangeNewLine(ref CbText);
                        Clipboard.SetText(newText, TextDataFormat.UnicodeText);

                        // 클립보드 기록이 활성화되어 있다면, 최신 데이터를 제외한 모든 중복값 제거
                        // 여기 수정, 최신 말고 제일 구형의 것만 남기기
                        if (_Clipboard.IsHistoryEnabled())
                            _Clipboard.RemoveDuplicateItems(newText);
                    });

                    // 최근 항목을 현재 값으로 설정
                    lastCbText = CbText;
                    lastCbRTF = CbRTF;
                    cbChanged = false;
                    cbWorking = false;

                    Thread.Sleep(additionalSleepTime);
                }

                // 단축키 설정을 사용 중이라면
                else if (useKeybind)
                {
                    // 단축키를 누르지 않았으면 취소
                    if (!Keybind.PressedAllKeybinds(keybinds))
                    {
                        Thread.Sleep(keybindSleepTime);   continue;
                    }

                    // 클립보드 데이터를 얻은 뒤 비어있다면 취소
                    Program.configForm.Invoke((MethodInvoker)delegate ()
                    {
                        if (Clipboard.ContainsText())
                        {
                            CbText = Clipboard.GetText(TextDataFormat.UnicodeText);
                            CbRTF = Clipboard.GetText(TextDataFormat.Rtf);
                        }
                    });
                    if (String.IsNullOrEmpty(CbText))
                    {
                        Thread.Sleep(keybindSleepTime);   continue;
                    }

                    // 클립보드 값 변환 처리 시작
                    cbWorking = true;
                    Program.configForm.Invoke((MethodInvoker)delegate ()
                    {
                        string newText = ChangeNewLine(ref CbText);
                        Clipboard.SetText(newText, TextDataFormat.UnicodeText);

                        SendKeys.Send("^v");

                        // 변환된 신규 클립보드 데이터 삭제
                        if (_Clipboard.IsHistoryEnabled())
                            _Clipboard.RemoveDuplicateItems(newText, workReverse: true);

                        // 기존 클립보드 원복
                        if (String.IsNullOrEmpty(CbRTF))
                            Clipboard.SetText(CbText, TextDataFormat.UnicodeText);
                        else
                            Clipboard.SetText(CbRTF, TextDataFormat.Rtf);
                    });
                    cbWorking = false;
                    cbChanged = false;
                }

                GC.Collect(0);
                Thread.Sleep(keybindSleepTime);
            }
        }


        /// <summary>
        /// 전달받은 텍스트의 줄바꿈을 LF(\n)에서 CRLF(\r\n)로 변경하여 반환합니다.
        /// </summary>
        internal static string ChangeNewLine(ref string text)
        {
            if (String.IsNullOrEmpty(text))
                return null;

            // Replace LF to CRLF
            if (!text.Contains("\r\n"))
                return text.Replace("\n", "\r\n");

            return text;
        }

        internal static void Run()
        {
            terminate = false;

            _worker = new Thread(Works);
            _worker.IsBackground = true;
            _worker.Start();
        }

        internal static void Terminate()
        {
            terminate = true;
        }
    }
}
