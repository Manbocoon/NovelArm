using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace NovelArm.Modules
{
    internal static class Json
    {
        #region Properties
        internal static JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        #endregion

        /// <summary>
        /// 주어진 json 데이터가 올바른지 확인합니다.
        /// </summary>
        /// <param name="jsonString">json 데이터 값입니다.</param>
        internal static bool IsValid(string jsonString)
        {
            if (String.IsNullOrWhiteSpace(jsonString))
                return false;

            try
            {
                JsonDocument.Parse(jsonString);    
            }

            catch (JsonException) { return false; }
            catch (ArgumentException) { return false; }

            return true;
        }


        /// <summary>
        /// 특정 파일로부터 JSON 데이터를 Generic 형태로 불러옵니다. 불러오지 못했을 경우 default(T)를 반환합니다.
        /// </summary>
        /// <param name="filePath">JSON 데이터 파일 경로입니다.</param>
        internal static T ReadJsonFromFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
                return default(T);

            string jsonString = File.ReadAllText(filePath, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            if (!Json.IsValid(jsonString))
                return default(T);

            return JsonSerializer.Deserialize<T>(jsonString);
        }

        /// <summary>
        /// JSON에 사용할 Generic 데이터를 특정 파일로 저장합니다. 저장하지 못했을 경우 false를 반환합니다.
        /// </summary>
        /// <param name="filePath">저장할 파일의 경로입니다.</param>
        /// <param name="jsonData">JSON으로 사용할 Generic 데이터입니다.</param>
        /// <returns>성공 여부를 반환합니다.</returns>
        internal static bool WriteJsonToFile<T>(string filePath, T jsonData)
        {
            if (File.Exists(filePath))
            {
                try
                { File.Delete(filePath); }

                catch (Exception)
                { return false; } 
            }

            // JSON 파일로 변환 후 Validate
            string JsonOutput = JsonSerializer.Serialize<T>(jsonData, jsonOptions);
            if (!Json.IsValid(JsonOutput))
                return false;

            // 파일로 저장
            File.WriteAllText(filePath, JsonOutput, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));

            return true;
        }

    }
}
