using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WindowsFormsApplication7
{
    public partial class StraightLine : LineBase
    {
        private StraightLineTypes lineType;

        public StraightLine()
            : base()
        {
            lineType = StraightLineTypes.Horizontal;
        }

        [Category("Line Properties"),
         DefaultValue(typeof(StraightLineTypes), "StraightLineTypes.Horizontal")]
        public StraightLineTypes LineType
        {
            get
            {
                return lineType;
            }
            set
            {
                lineType = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 重写画线部分GDI实现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void LineBase_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            //pen = new Pen(ForeColor, Thickness);
            //pen = new Pen(ForeColor, (float)200);
            if (AntiAlias)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            }

            switch (lineType)
            {
                case StraightLineTypes.Horizontal:
                    DrawCenteredHorizontalLine(e.Graphics);
                    break;
                case StraightLineTypes.Vertical:
                    DrawCenteredVerticalLine(e.Graphics);
                    break;
                case StraightLineTypes.DiagonalAscending:
                    DrawCenteredDiagonalAscendingLine(e.Graphics);
                    break;
                case StraightLineTypes.DiagonalDescending:
                    DrawCenteredDiagonalDescendingLine(e.Graphics);
                    break;
                default: break;
            }
            //switch (TextOnLine)
            //{
            //    case 1:
            //        DrawTextOntheLines(e.Graphics);
            //        break;
            //    case 2:
            //        DrawTextOntheLines(e.Graphics);
            //        break;
            //    default:
            //        break;
            //}
        }

        //private void DrawTextOntheLines(Graphics g)
        //{
        //    Font ff = new Font("Times New Roman", 20);
        //    //Brush bf = new Brush(Color.Black, 20);
        //    string txt = TextSegment;
        //    g.DrawString(txt, ff, Brushes.Black, Width / 2, 0);
        //}
        #region 画线函数

        private void DrawCenteredHorizontalLine(Graphics g)
        {
            float penwidth = Thickness;
            pen = new Pen(ForeColor, Thickness);
            g.DrawLine(new Pen(ForeColor, Thickness), 0, Height / 2, Width, Height / 2);
            g.DrawLine(new Pen(Color.White, (float)0.5),
                Width, Height / 2 - Thickness / 2 - 2, Width, Height / 2 + Thickness / 2 + 2);
            g.DrawLine(new Pen(Color.White, (float)0.5),
                0, Height / 2 - Thickness / 2 - 2, 0, Height / 2 + Thickness / 2 + 2);
            Point[] mypoints = { new Point(0, Height / 2 + (int)penwidth),
                new Point(0, Height / 2 - (int)penwidth),
                new Point(Width, Height / 2 - (int)penwidth),
                new Point(Width, Height / 2 + (int)penwidth) };
            RegionControl(mypoints);
        }

        private void DrawCenteredVerticalLine(Graphics g)
        {
            float penwidth = Thickness;
            g.DrawLine(new Pen(ForeColor, Thickness), Width / 2, 0, Width / 2, Height);
            Point[] mypoints = { new Point(Width / 2 - (int)penwidth, 0),
                new Point(Width / 2 + (int)penwidth, 0),
                new Point(Width / 2 + (int)penwidth, Height),
                new Point(Width / 2 - (int)penwidth, Height) };
            RegionControl(mypoints);
        }

        private void DrawCenteredDiagonalAscendingLine(Graphics g)
        {
            float penwidth = Thickness;
            g.DrawLine(new Pen(ForeColor, Thickness), 0, Height, Width, 0);
            // g.DrawLine(new Pen(Color.White, (float)0.5),
            //                  Width, Height / 2 - Thickness / 2 - 2, Width, Height / 2 + Thickness / 2 + 2);
            Point[] mypoints = { new Point((int)penwidth, Height),
                new Point(0, Height - (int)penwidth),
                new Point(Width - (int)penwidth, 0),
                new Point(Width, (int)penwidth) };
            RegionControl(mypoints);
        }

        private void DrawCenteredDiagonalDescendingLine(Graphics g)
        {
            float penwidth = Thickness;
            g.DrawLine(new Pen(ForeColor, Thickness), 0, 0, Width, Height);
            // g.DrawLine(new Pen(Color.White, (float)0.5),
            //    Width, Height / 2 - Thickness / 2 - 2, Width, Height / 2 + Thickness / 2 + 2);
            Point[] mypoints = { new Point(0, (int)penwidth),
                new Point((int)penwidth, 0),
                new Point(Width, Height - (int)penwidth),
                new Point(Width - (int)penwidth, Height) };
            RegionControl(mypoints);
        }

        /// <summary>
        /// 按照点集合生成异形窗体
        /// </summary>
        /// <param name="points"></param>
        private void RegionControl(Point[] points)
        {
            GraphicsPath mygraphicsPath = new GraphicsPath();
            mygraphicsPath.AddPolygon(points);
            Region myregion = new Region(mygraphicsPath);
            this.Region = myregion;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000; // Turn off WS_CLIPCHILDREN 
                return parms;
            }
        }
        #endregion
    }
}
