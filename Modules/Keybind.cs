using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NovelArm.Modules
{
    internal static class Keybind
    {
        #region Properties
        internal static string[] blacklist = { "Menu", "ControlKey", "ShiftKey" };
        internal static string[] modifiers = { "Control", "Alt", "Shift" };
        #endregion

        internal static IList<string> ReplaceRawData(string keyDataString, bool restoreData = false)
        {
            List<string> keybinds = new List<string>();
            string normalKey = null;
            string[] delimiter = restoreData ? new string[] { " + " } : new string[] { ", " }; 
            foreach (string singleKeyData in keyDataString.Split(delimiter, StringSplitOptions.None))
            {
                if (blacklist.Contains<string>(singleKeyData))               
                    continue;              

                if (!modifiers.Contains<string>(singleKeyData))
                {
                    normalKey = singleKeyData;
                    continue;
                }

                else               
                    keybinds.Add(singleKeyData == "Control" ? "Ctrl" : singleKeyData);
                
            }

            if (normalKey == null)           
                return null;

            List<string> newKeybinds = new List<string>();
            foreach (string modifier in modifiers)
            {
                string compareKey = modifier;
                if (compareKey == "Control")
                    compareKey = "Ctrl";

                if (keybinds.Contains(compareKey))
                    newKeybinds.Add(compareKey);
            }

            newKeybinds.Add(normalKey);

            return newKeybinds;
        }

        internal static List<int> ReadKeysFromData(IList<string> keyData)
        {
            List<int> keybinds = new List<int>();

            foreach (string keydata in keyData)
            {
                switch (keydata)
                {
                    case "Ctrl":
                        keybinds.Add((int)Keys.ControlKey);
                        break;

                    case "Shift":
                        keybinds.Add((int)Keys.ShiftKey);
                        break;

                    case "Alt":
                        keybinds.Add((int)Keys.Menu);
                        break;

                    default:
                        keybinds.Add((int)(Keys)Enum.Parse(typeof(Keys), keydata, true));
                        break;
                }
            }

            return keybinds;
        }

        /// <summary>
        /// 사용자가 설정한 Keybind를 실시간으로 모두 입력했는지 확인합니다.
        /// </summary>
        /// <param name="keybinds">int형 key값의 모음입니다.</param>
        /// <returns>제시된 Key들을 모두 눌렀는지의 여부입니다.</returns>
        internal static bool PressedAllKeybinds(IList<int> keybinds)
        {
            // 단축키가 설정되지 않은 경우 누르지 않은 것으로 판단
            if (keybinds.Count == 0)
                return false;

            // 각 키들에 모두 KeyDown이 입력되었는지 확인
            int pressedKeyCount = 0;
            for (byte i = 0; i < keybinds.Count; ++i)
            {
                if (System.NativeMethods.GetAsyncKeyState(keybinds[i]) > 32767)
                    ++pressedKeyCount;
            }

            // 모든 단축키를 전부 누르지 않았다면 누르지 않은 것으로 판단
            if (pressedKeyCount != keybinds.Count)
            {
                return false;
            }

            // 약 10초 동안 단축키들 중 하나라도 떨어지면 입력 성공 처리
            for (int c=0; c<3333; ++c)
            {
                pressedKeyCount = 0;
                for (byte i = 0; i < keybinds.Count; ++i)
                {
                    if (System.NativeMethods.GetAsyncKeyState(keybinds[i]) > 32767)
                        ++pressedKeyCount;
                }

                if (pressedKeyCount < keybinds.Count)
                    return true;

                Thread.Sleep(3);
            }

            return false;
        }
    }
}
