using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace NovelArm.Modules
{
    internal static class PointExt
    {
        /// <summary>
        /// Point.ToString() -> ToPoint() -> System.Drawing.Point
        /// </summary>
        internal static Point ToPoint(this string pointStr)
        {
            var g = Regex.Replace(pointStr, @"[\{\}a-zA-Z=]", "").Split(',');

            return new Point(int.Parse(g[0]), int.Parse(g[1]));
        }

    }
}
