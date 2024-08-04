using NovelArm.Modules.Systems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NovelArm.Modules
{
    internal static class TextConverter
    {
        #region Properties
        private static RichTextBox richTextBox = new RichTextBox();
        private static Thread _worker;
        private static bool terminate = false;
        private static string lastCbText = null, lastCbRTF = null, CbText = null, CbRTF = null;

        internal static bool cbChanged = false, cbWorking = false;
        internal static bool useAutoConvert = true, useKeybind = false;
        internal static List<int> keybinds = new List<int>();
        #endregion


        /// <summary>
        /// 백그라운드 스레드가 실행할 작업
        /// </summary>
        private static void Works()
        {
            while (!terminate)
            {
                int maxSleepTime = 500;
                int keybindSleepTime = 50;
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
                        if (WinRTClipboard.IsHistoryEnabled())
                            WinRTClipboard.RemoveDuplicateItems(newText);
                    });

                    // 최근 항목을 현재 값으로 설정
                    lastCbText = CbText;
                    lastCbRTF = CbRTF;
                    cbChanged = false;
                    cbWorking = false;

                    GC.Collect(0);
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
                        if (WinRTClipboard.IsHistoryEnabled())
                            WinRTClipboard.RemoveDuplicateItems(newText, workReverse: true);

                        // 기존 클립보드 원복
                        if (String.IsNullOrEmpty(CbRTF))
                            Clipboard.SetText(CbText, TextDataFormat.UnicodeText);
                        else
                            Clipboard.SetText(CbRTF, TextDataFormat.Rtf);
                    });
                    cbWorking = false;
                    cbChanged = false;

                    GC.Collect(0);
                }

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

        internal static string RtfToPlainText(string Rtf)
        {
            string value = "";
            Program.configForm.Invoke((MethodInvoker)delegate ()
            {
                richTextBox.Rtf = Rtf;

                value = richTextBox.Text;
            });
            return value;
        }

        /// <summary>
        /// 텍스트 파일의 인코딩 형식을 가져옵니다. UTF-8 || UTF-8 BOM || UTF-16 BE || UTF-16 LE || ANSI
        /// </summary>
        /// <param name="filePath">파일 경로입니다.</param>
        /// <returns>System.Text.Encoding</returns>
        public static Encoding GetEncoding(string filePath)
        {
            // Referenced https://stackoverflow.com/a/48592382

            Byte[] bytes = File.ReadAllBytes(filePath);
            Encoding encoding = null;
            String text = null;
            UTF8Encoding encUtf8Bom = new UTF8Encoding(true, true);
            Boolean couldBeUtf8 = true;
            Byte[] preamble = encUtf8Bom.GetPreamble();
            Int32 prLen = preamble.Length;

            // UTF-16 BE || UTF-16 LE
            using (var reader = new StreamReader(filePath, Encoding.Default, true))
            {
                if (reader.Peek() >= 0)
                    reader.Read();

                encoding = reader.CurrentEncoding;

                if (encoding.BodyName.Contains("utf-16") || encoding.EncodingName.Contains("utf-16"))
                    return encoding;
            }
            encoding = null;

            if (bytes.Length >= prLen && preamble.SequenceEqual(bytes.Take(prLen)))
            {
                // UTF8 BOM found; use encUtf8Bom to decode.
                try
                {
                    text = encUtf8Bom.GetString(bytes, prLen, bytes.Length - prLen);
                    encoding = encUtf8Bom;
                }
                catch (ArgumentException)
                {
                    // Confirmed as not UTF-8!
                    couldBeUtf8 = false;
                }
            }
            if (couldBeUtf8 && encoding == null)
            {
                UTF8Encoding encUtf8NoBom = new UTF8Encoding(false, true);
                try
                {
                    text = encUtf8NoBom.GetString(bytes);
                    encoding = encUtf8NoBom;
                }
                catch (ArgumentException)
                {
                    // Confirmed as not UTF-8!
                }
            }
            // 실패 시 ANSI로 반환
            if (encoding == null)
            {
                encoding = Encoding.Default;
                text = encoding.GetString(bytes);
            }

            return encoding;
        }


    }
}
