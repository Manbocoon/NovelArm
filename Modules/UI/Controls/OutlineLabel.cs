using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NovelArm.Modules.UI
{
    public class OutlineLabel : Label
    {
        #region Properties
        public Color lineForeColor { get; set; }
        public float lineWidth { get; set; }
        #endregion

        public OutlineLabel()
        {
            lineForeColor = Color.White;
            lineWidth = 2;
        }

        protected override void OnPaint(PaintEventArgs evt)
        {
            evt.Graphics.FillRectangle(new SolidBrush(BackColor), ClientRectangle);

            using (GraphicsPath gPath = new GraphicsPath())
            using (Pen outlinePen = new Pen(lineForeColor, lineWidth) { LineJoin = LineJoin.Round })
            using (StringFormat sFormat = new StringFormat())
            using (Brush foreBrush = new SolidBrush(ForeColor))
            {
                gPath.AddString(Text, Font.FontFamily, (int)Font.Style,
                    Font.Size, ClientRectangle, sFormat);
                evt.Graphics.ScaleTransform(1.3f, 1.35f);
                evt.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                evt.Graphics.DrawPath(outlinePen, gPath);
                evt.Graphics.FillPath(foreBrush, gPath);
            }
        }
    }
}
