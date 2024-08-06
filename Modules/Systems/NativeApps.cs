using System;
using System.Diagnostics;


namespace NovelArm.Modules.Systems
{
    internal static class NativeApps
    {

        internal static void SelectFileWithExplorer(string filePath)
        {
            string args = string.Format("/e, /select, \"{0}\"", filePath);

            ProcessStartInfo pInfo = new ProcessStartInfo();
            pInfo.FileName = "explorer.exe";
            pInfo.UseShellExecute = true;
            pInfo.Arguments = args;
            pInfo.Verb = "runas";
            Process.Start(pInfo);

        }
    }
}
