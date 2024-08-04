using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Text.Json;
using System.Text;
using System.Drawing;
using System.Reflection;

namespace NovelArm.Modules
{
    internal static class Settings
    {
        #region Properties
        internal static string Path = Program.PATH + @"\Settings";
        internal static string appFile = Program.PATH + @"\Settings\Application.json";
        internal static string overlayFile = Program.PATH + @"\Settings\Overlay.json";
        #endregion

        internal static void CreateDirectory()
        {
            Directory.CreateDirectory(Path);
            // Directory.CreateDirectory(Path + @"\Locales");
        }

        internal static void RemoveAllSettings()
        {
            foreach (string filePath in Directory.GetFiles(Path))
            {
                try
                {
                    if (filePath.EndsWith(".json"))
                        File.Delete(filePath);
                }

                catch (Exception) { }
            }
        }
    }

    internal class AppSettings : IDisposable
    {
        #region Properties
        internal Dictionary<string, List<ControlInfo>> controlData = new Dictionary<string, List<ControlInfo>>();
        internal struct ControlInfo
        {
            public string Name { get; set; }
            public string Text { get; set; }
            public bool? Checked { get; set; }
            public int? SelectedIndex { get; set; }
        }
        #endregion

        public void Dispose()
        {
            GC.Collect(0);
        }

        internal void RemoveFile()
        {
            if (File.Exists(Settings.appFile))
            {
                try
                {
                    File.Delete(Settings.appFile);
                }
                catch (Exception) { }
            }
        }



        /// <summary>
        /// Json 데이터를 읽어들여 모든 컨트롤에 적용합니다. 이벤트 실행은 하지 않습니다.
        /// </summary>
        public bool LoadFromFile()
        {
            controlData = Json.ReadJsonFromFile<Dictionary<string, List<ControlInfo>>>(Settings.appFile);
            if (controlData == null)
                return false;

            WriteControlsData();

            // 이벤트 실행은 메인 configForm에서 PerformControlEvents() 호출

            return true;
        }

        /// <summary>
        /// 현재 모든 컨트롤 설정을 Json 파일로 저장합니다.
        /// </summary>
        public bool SaveToFile()
        {
            // 모든 컨트롤 값 불러오기
            Control.ControlCollection formControls = Program.configForm.Controls;
            controlData.Clear();
            ReadControlsData(formControls);

            Settings.CreateDirectory();

            // JSON 파일로 저장
            bool saveSucceed = Json.WriteJsonToFile(Settings.appFile, controlData);
            if (!saveSucceed)
                return false;

            return true;
        }

        /// <summary>
        /// Form의 모든 컨트롤에 접근하여 데이터를 읽고 controlData 딕셔너리에 저장합니다.
        /// </summary>
        /// <param name="controls">Control의 컬렉션입니다.</param>
        private void ReadControlsData(Control.ControlCollection controls)
        {
            string[] checkableControls = new string[] { "CheckBox", "RadioButton" };
            ControlInfo controlInfo = new ControlInfo();
            foreach (Control control in controls)
            {
                string type = control.GetType().Name;

                if (!controlData.ContainsKey(type))
                {
                    controlData.Add(type, new List<ControlInfo>());
                }

                // 리스트에 컨트롤 객체 정보 기록
                controlInfo = new ControlInfo
                {
                    Name = control.Name,
                    Text = control.Text,
                };
                controlInfo.Checked = control.GetPropertyValue("Checked") as bool?;
                controlInfo.SelectedIndex = control.GetPropertyValue("SelectedIndex") as int?;

                // 추가
                controlData[type].Add(controlInfo);

                // 만약 현재 컨트롤이 하위 컨트롤을 갖고 있는 객체라면
                if (control.Controls.Count > 0)
                {
                    // 재귀 호출
                    ReadControlsData(control.Controls);
                }
            }
        }

        /// <summary>
        /// 읽은 Json 데이터를 컨트롤에 적용합니다.
        /// </summary>
        private void WriteControlsData()
        {
            Control.ControlCollection formControls = Program.configForm.Controls;

            foreach (var controlItem in controlData)
            {
                string controlType = controlItem.Key;
                List<ControlInfo> controlInfos = controlItem.Value;
                
                foreach (ControlInfo controlInfo in controlInfos)
                {
                    Control control = formControls.Find(controlInfo.Name, true).FirstOrDefault();
                    if (control == null)
                        continue;

                    control.Text = controlInfo.Text;
                    control.SetPropertyValue("Checked", controlInfo.Checked ?? false);
                    try { control.SetPropertyValue("SelectedIndex", controlInfo.SelectedIndex ?? 0); }
                    catch (Exception) { control.SetPropertyValue("SelectedIndex", 0); }
                }
            }
        }

        

    }


    internal class OverlaySettings : IDisposable
    {
        #region Properties
        internal OverlayInfo overlayData = new OverlayInfo();

        [Serializable]
        internal struct OverlayInfo
        {
            public string TextFont { get; set; }
            public string TextColor { get; set; }
            public string OutlineColor { get; set; }
            public byte OutlineThickness { get; set; }
            public Point Location { get; set; }
        }

        private FontConverter fontConverter = new FontConverter();
        private ColorConverter colorConverter = new ColorConverter();
        #endregion

        public void Dispose()
        {
            GC.Collect(0);
        }

        internal void RemoveFile()
        {
            if (File.Exists(Settings.overlayFile))
            {
                try
                {
                    File.Delete(Settings.overlayFile);
                }
                catch (Exception) { }
            }
        }



        /// <summary>
        /// Json 데이터를 읽어들여 모든 컨트롤에 적용합니다. 이벤트 실행은 하지 않습니다.
        /// </summary>
        public bool LoadFromFile()
        {
            overlayData = Json.ReadJsonFromFile<OverlayInfo>(Settings.overlayFile);
            if (overlayData.Equals(new OverlayInfo()))
                return false;
            
            WriteOverlayData(ref Program.configForm.charDisplay);

            return true;
        }

        /// <summary>
        /// 현재 모든 컨트롤 설정을 Json 파일로 저장합니다.
        /// </summary>
        public bool SaveToFile()
        {
            // 모든 오버레이 값 불러오기
            overlayData = default;
            ReadOverlayData(ref Program.configForm.charDisplay);

            Settings.CreateDirectory();
            bool saveSucceed = Json.WriteJsonToFile<OverlayInfo>(Settings.overlayFile, overlayData);
            if (!saveSucceed)
                return false;
            
            return true;
        }

        /// <summary>
        /// Form의 모든 컨트롤에 접근하여 데이터를 읽고 controlData 딕셔너리에 저장합니다.
        /// </summary>
        /// <param name="controls">Control의 컬렉션입니다.</param>
        private void ReadOverlayData(ref CharDisplay overlayForm)
        {
            if (overlayForm == null)
                return;

            // Create the FontConverter.
            System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(Font));
            
            overlayData = new OverlayInfo 
            {
                TextFont = fontConverter.ConvertToString(overlayForm.TextFont),
                TextColor = colorConverter.ConvertToString(overlayForm.TextColor),
                OutlineColor = colorConverter.ConvertToString(overlayForm.OutlineColor),
                OutlineThickness = overlayForm.OutlineThickness,
                Location = overlayForm.Location
            };
        }

        /// <summary>
        /// 읽은 Json 데이터를 컨트롤에 적용합니다.
        /// </summary>
        private void WriteOverlayData(ref CharDisplay overlayForm)
        {
            if (overlayForm == null)
                return;
            
            overlayForm.SetPropertyValue("TextFont", fontConverter.ConvertFromString(overlayData.TextFont));
            overlayForm.SetPropertyValue("TextColor", colorConverter.ConvertFromString(overlayData.TextColor));
            overlayForm.SetPropertyValue("OutlineColor", colorConverter.ConvertFromString(overlayData.OutlineColor));
            overlayForm.SetPropertyValue("OutlineThickness", overlayData.OutlineThickness);
            overlayForm.SetPropertyValue("Location", overlayData.Location);
        }


    }

}
