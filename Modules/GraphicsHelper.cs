using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;

namespace NovelArm.Modules
{
    internal class GraphicsHelper : IDisposable
    {
        public void Dispose()
        { }

        internal static Size MeasureString(string text, Font font)
        {
            SizeF result;
            using (var graphic = Graphics.FromHwnd(IntPtr.Zero))
            {
                graphic.TextRenderingHint = TextRenderingHint.AntiAlias;
                result = graphic.MeasureString(text, font, int.MaxValue, StringFormat.GenericTypographic);
            }

            return result.ToSize();
        }
    }
}
