using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace NovelArm.Modules
{
    /// <summary>
    /// 이미지 처리에 사용될 함수들을 모아놓은 정적 객체입니다.
    /// </summary>
    internal static class Imaging
    {

        /// <summary>
        /// 특정 경로의 파일이 올바른 이미지인지 확인합니다.
        /// </summary>
        internal static bool IsSupportedImage(string file_path)
        {

            if (!File.Exists(file_path))
            {
                return false;
            }

            bool result = false;

            using (var imgStream = File.OpenRead(file_path))
            {
                try
                {
                    BitmapDecoder.Create(imgStream, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default);

                    result = true;
                }

                catch (NotSupportedException)
                {
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// 특정 경로의 이미지파일의 넓이를 구합니다.
        /// </summary>
        internal static Size GetImageSize(string file_path)
        {
            if (!File.Exists(file_path))
                return Size.Empty;

            Size result = new Size(0, 0);

            using (var imageStream = File.OpenRead(file_path))
            {
                try
                {
                    var decoder = BitmapDecoder.Create(imageStream, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default);
                    result.Width = decoder.Frames[0].PixelWidth;
                    result.Height = decoder.Frames[0].PixelHeight;
                }

                catch (NotSupportedException) { }
            }

            return result;
        }

        /// <summary>
        /// 넓이를 통해 이미지의 가로:세로 비율을 구하고, Point 변수를 통해 X:Y로 반환합니다.
        /// </summary>
        /// <param name="image">비율을 추출해낼 이미지의 넓이입니다.</param>
        internal static Point GetImageRatio(Size image)
        {
            int gcd = GetGCD(image.Width, image.Height);
            Point result = new Point(image.Width / gcd, image.Height / gcd);

            return result;
        }

        /// <summary>
        /// 어떤 두 수의 최대공약수를 구하는 함수입니다.
        /// </summary>
        /// <param name="x">첫번째 수입니다.</param>
        /// <param name="y">두번째 수입니다.</param>
        private static int GetGCD(int x, int y)
        {
            int gcd = 1;

            int max_value = x > y ? x : y;

            for (int i = 1; i <= max_value; ++i)
            {
                if (x % i == 0 && y % i == 0)
                {
                    gcd = i;
                }
            }

            return gcd;
        }
    }
}
