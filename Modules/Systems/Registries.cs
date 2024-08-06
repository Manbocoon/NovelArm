using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NovelArm.Modules.Systems
{
    internal static class Registries
    {
        #region Properties
        private static RegistryKey Startup = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        private static RegistryKey Mouse = Registry.CurrentUser.OpenSubKey("Control Panel\\Mouse", false);
        #endregion



        internal static ushort GetMouseDoubleClickSpeed()
        {
            ushort.TryParse(Mouse.GetValue("DoubleClickSpeed", 500).ToString(), out ushort value);

            return value;
        }


        /// <summary>
        /// 프로그램이 윈도우 시작 프로그램에 등록된 상태인지 확인합니다.
        /// </summary>
        internal static bool IsStartupRegistered()
        {
            var regStartupValue = Startup.GetValue(Program.APP_NAME, false);
            if (!regStartupValue.Equals(false))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 프로그램을 윈도우 시작 프로그램에 등록합니다.
        /// </summary>
        internal static void RegisterStartup()
        {
            Startup.SetValue(Program.APP_NAME, $"\"{Program.EXE_PATH}\" -hideWindow");
        }


        /// <summary>
        /// 프로그램을 윈도우 시작 프로그램에서 제거합니다.
        /// </summary>
        internal static void UnregisterStartup()
        {
            Startup.DeleteValue(Program.APP_NAME, false);
        }
    }
}
