using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Text.Json;
using System.Text;

namespace NovelArm.Modules
{
    internal class Settings : IDisposable
    {
        #region Properties
        internal Dictionary<string, List<ControlInfo>> controlData = new Dictionary<string, List<ControlInfo>>();
        internal struct ControlInfo
        {
            public string Name { get; set; }
            public string Text { get; set; }
            public bool Checked { get; set; }
        }
        private JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };

        private string Path = Program.PATH + @"\Settings";
        private string appFile = Program.PATH + @"\Settings\Application.json";
        #endregion

        public void Dispose()
        { }

        internal void CreateDirectory()
        {
            Directory.CreateDirectory(Path);
            // Directory.CreateDirectory(Path + @"\Locales");
        }



        /// <summary>
        /// Json 데이터를 읽어들여 모든 컨트롤에 적용합니다. 이벤트 실행은 하지 않습니다.
        /// </summary>
        public bool LoadFromFile()
        {
            if (!File.Exists(appFile))
                return false;

            string jsonString = File.ReadAllText(appFile, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            if (!Json.IsValid(jsonString))
                return false;

            // Json 파일로부터 데이터 읽기
            controlData = JsonSerializer.Deserialize<Dictionary<string, List<ControlInfo>>>(jsonString);

            // 읽은 데이터들을 컨트롤에 적용
            WriteControlsData();

            // 이벤트 실행은 메인 configForm에서 PerformControlEvents() 호출

            return true;
        }

        /// <summary>
        /// 현재 모든 컨트롤 설정을 Json 파일로 저장합니다.
        /// </summary>
        public bool SaveToFile()
        {
            if (File.Exists(Path + @"\Application.json"))
            {
                try
                { File.Delete(appFile); }

                catch (Exception)
                {
                    MessageBox.Show("설정 파일을 수정할 권한이 없거나, 다른 프로세스가 사용 중입니다.", Program.APP_NAME, 0, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            
            // 모든 컨트롤 값 불러오기
            Control.ControlCollection formControls = Program.configForm.Controls;
            controlData.Clear();
            ReadControlsData(formControls);

            CreateDirectory();

            // JSON 파일로 저장
            string JsonOutput = JsonSerializer.Serialize(controlData, jsonOptions);
            File.WriteAllText(appFile, JsonOutput, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));

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
                    Checked = false
                };

                // Checked 항목이 있는 컨트롤이면 체크 여부까지 기록
                if (checkableControls.Contains<string>(type))
                {
                    if (type == "CheckBox")
                        controlInfo.Checked = ((CheckBox)control).Checked;
                    else if (type == "RadioButton")
                        controlInfo.Checked = ((RadioButton)control).Checked;
                }

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
            string[] checkableControls = new string[] { "CheckBox", "RadioButton" };
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

                    if (checkableControls.Contains<string>(controlType))
                    {
                        if (controlType == "CheckBox")
                            ((CheckBox)control).Checked = controlInfo.Checked;
                        else if (controlType == "RadioButton")
                            ((RadioButton)control).Checked = controlInfo.Checked;
                    }

                    control.Text = controlInfo.Text;
                }

            }
        }

    }
}
