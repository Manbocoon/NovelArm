using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelArm.Modules.System
{
    internal static class Files
    {


        /// <summary>
        /// 사용자 Temp 폴더에 제로 바이트의 임시 파일을 생성하고 그 경로를 반환합니다.
        /// </summary>
        /// <returns>파일 경로를 반환하고, 실패했다면 null을 반환합니다.</returns>
        internal static string GenerateUniqueFile()
        {
            try
            { return Path.GetTempFileName(); }

            catch (IOException)
            { return null; }
        }

        /// <summary>
        /// 파일을 만들지는 않고, 고유 GUID 이름을 생성하여 반환합니다.
        /// </summary>
        /// <returns>고유 GUID를 반환합니다.</returns>
        internal static string GetUniqueFileName()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
