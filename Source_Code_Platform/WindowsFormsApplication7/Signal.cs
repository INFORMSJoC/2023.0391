using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;

namespace WindowsFormsApplication7
{
    public partial class Signal : UserControl
    {
        /// <summary>
        /// 中心颜色
        /// </summary>
        public Color CenterColor = Color.White;
        /// <summary>
        /// 外围颜色
        /// </summary>
        public Color SurroundColor = Color.Red;

        /// <summary>
        /// 灭灯颜色
        /// </summary>
        public Color DarkColor = Color.Gray;

        /// <summary>
        /// 闪烁时间ms
        /// </summary>
        public int TwinkleTime = 0;

        Color CurrentColor = Color.Gray;

        public Signal()
        {
            InitializeComponent();
            this.Paint += Signal_Paint;
            CurrentColor = SurroundColor;
            //new Thread(delegate ()
            //{
            //    bool ChangeColor = false;
            //    while (!this.IsDisposed)
            //    {
            //        if (TwinkleTime != 0)
            //        {
            //            ChangeColor = !ChangeColor;

            //            CurrentColor = ChangeColor == true ? SurroundColor : DarkColor;
            //            this.Invalidate();
            //            Thread.Sleep(TwinkleTime);
            //        }
            //    }
            //}).Start();
        }

        private void Signal_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, this.Size.Width, this.Size.Height);
            SolidBrush pthGrBrush = new SolidBrush(ForeColor);
            //pthGrBrush.CenterColor = CenterColor;
            // Color[] colors = { CurrentColor };
            //pthGrBrush.SurroundColors = colors;
            e.Graphics.FillEllipse(pthGrBrush, 0, 0, this.Size.Width, this.Size.Height);
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            //使控件边界也为圆形
            GraphicsPath graphics = new GraphicsPath();
            graphics.AddEllipse(0, 0, this.Width, this.Height);
            this.Region = new System.Drawing.Region(graphics);
            base.OnPaint(pevent);
            base.OnPaint(pevent);
        }
    }
}