using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NovelArm.Modules.System
{
    internal static class Startup
    {
        #region Properties
        private static RegistryKey regStartup = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        #endregion


        /// <summary>
        /// 프로그램이 윈도우 시작 프로그램에 등록된 상태인지 확인합니다.
        /// </summary>
        internal static bool IsRegistered()
        {
            var regStartupValue = regStartup.GetValue(Program.APP_NAME, false);
            if (!regStartupValue.Equals(false))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 프로그램을 윈도우 시작 프로그램에 등록합니다.
        /// </summary>
        internal static void Register()
        {
            regStartup.SetValue(Program.APP_NAME, Application.ExecutablePath);
        }


        /// <summary>
        /// 프로그램을 윈도우 시작 프로그램에서 제거합니다.
        /// </summary>
        internal static void Unregister()
        {
            regStartup.DeleteValue(Program.APP_NAME, false);
        }
    }
}
