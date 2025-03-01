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
    public partial class MultiSignal : UserControl
    {
        private bool direction;
        private int leftcolor;
        private int middlecolor;
        private int rightcolor;
        private int signalnum;
        SolidBrush RedBrush = new SolidBrush(Color.Red);
        SolidBrush GreenBrush = new SolidBrush(Color.Lime);
        SolidBrush GreyBrush = new SolidBrush(Color.Gray);
        SolidBrush leftbrush, rightbrush, middlebrush;
        public MultiSignal()
        {
            InitializeComponent();
            this.Paint += MultiSignal_Paint;
            //CurrentColor = SurroundColor;
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
        private void MultiSignal_Paint(object sender, PaintEventArgs e)
        {
            //bool SignalDirection = true;
            // DrawUpDirectionSignal(e.Graphics, SignalNum);
            if (direction == true)
            {
                DrawUpDirectionSignal(e.Graphics, signalnum, leftcolor, rightcolor, middlecolor);
            }
            else
            {
                DrawDownDirectionSignal(e.Graphics, signalnum, leftcolor, rightcolor, middlecolor);
            }
            //switch (direction)
            //{
            //    case true:
            //        DrawUpDirectionSignal(e.Graphics, signalnum, leftcolor, rightcolor, middlecolor);
            //        break;
            //    case false:
            //        DrawDownDirectionSignal(e.Graphics, signalnum, leftcolor, rightcolor, middlecolor);
            //        break;
            //}
        }
        private void DrawUpDirectionSignal(Graphics g, int num, int left, int right, int middle)
        {
            if (num == 2)
            {
                if (left == 0)
                { leftbrush = RedBrush; }
                else if (left == 1)
                { leftbrush = GreenBrush; }
                else { leftbrush = GreyBrush; }
                if (right == 0)
                { rightbrush = RedBrush; }
                else if (right == 1)
                { rightbrush = GreenBrush; }
                else { rightbrush = GreyBrush; }
                //SolidBrush leftbrush = GreenBrush;
                //this.Size = new System.Drawing.Size(150, 75);
                g.FillEllipse(leftbrush, 0, 0, this.Size.Width / 2, this.Size.Height - 1);
                g.DrawEllipse(new Pen(Color.White, 1), 0, 0, this.Size.Width / 2, this.Size.Height - 1);
                g.FillEllipse(rightbrush, this.Size.Width / 2, 0, this.Size.Width / 2 - 1, this.Size.Height - 1);
                g.DrawLine(new Pen(Color.Yellow, (float)2.5), 0, 0, 0, this.Width - 5);
                g.DrawEllipse(new Pen(Color.White, 1), this.Size.Width / 2, 0, this.Size.Width / 2 - 1, this.Size.Height - 1);
            }
            if (num == 3)
            {
                if (left == 0)
                { leftbrush = RedBrush; }
                else if (left == 1)
                { leftbrush = GreenBrush; }
                else { leftbrush = GreyBrush; }
                if (right == 0)
                { rightbrush = RedBrush; }
                else if (right == 1)
                { rightbrush = GreenBrush; }
                else { rightbrush = GreyBrush; }
                if (middle == 0)
                { middlebrush = RedBrush; }
                else if (middle == 1)
                { middlebrush = GreenBrush; }
                else { middlebrush = GreyBrush; }
                //this.Size = new System.Drawing.Size(225, 75);
                g.FillEllipse(leftbrush, 0, 0, this.Size.Width / 3, this.Size.Height - 1);
                g.DrawEllipse(new Pen(Color.White, 1), 0, 0, this.Size.Width / 3, this.Size.Height - 1);
                g.FillEllipse(middlebrush, this.Size.Width / 3, 0, this.Size.Width / 3 - 1, this.Size.Height - 1);
                g.DrawLine(new Pen(Color.Yellow, (float)2.5), 0, 0, 0, this.Width - 5);
                g.DrawEllipse(new Pen(Color.White, 1), this.Size.Width / 3, 0, this.Size.Width / 3 - 1, this.Size.Height - 1);
                g.FillEllipse(rightbrush, this.Size.Width * 2 / 3, 0, this.Size.Width / 3 - 1, this.Size.Height - 1);
                g.DrawEllipse(new Pen(Color.White, 1), this.Size.Width * 2 / 3, 0, this.Size.Width / 3 - 1, this.Size.Height - 1);
            }
        }
        private void DrawDownDirectionSignal(Graphics g, int num, int left, int right, int middle)
        {
            if (num == 2)
            {
                if (left == 0)
                { leftbrush = RedBrush; }
                else if (left == 1)
                { leftbrush = GreenBrush; }
                else { leftbrush = GreyBrush; }
                if (right == 0)
                { rightbrush = RedBrush; }
                else if (right == 1)
                { rightbrush = GreenBrush; }
                else { rightbrush = GreyBrush; }
                //SolidBrush leftbrush = GreenBrush;
                //this.Size = new System.Drawing.Size(150, 75);
                g.FillEllipse(leftbrush, 0, 0, this.Size.Width / 2, this.Size.Height - 1);
                g.DrawEllipse(new Pen(Color.White, 1), 0, 0, this.Size.Width / 2, this.Size.Height - 1);
                g.FillEllipse(rightbrush, this.Size.Width / 2, 0, this.Size.Width / 2 - 1, this.Size.Height - 1);
                g.DrawEllipse(new Pen(Color.White, 1), this.Size.Width / 2, 0, this.Size.Width / 2 - 1, this.Size.Height - 1);
                g.DrawLine(new Pen(Color.Yellow, (float)2.5), this.Size.Width - 1, 0, this.Size.Width - 1, this.Width - 5);
            }
            if (num == 3)
            {
                if (left == 0)
                { leftbrush = RedBrush; }
                else if (left == 1)
                { leftbrush = GreenBrush; }
                else { leftbrush = GreyBrush; }
                if (right == 0)
                { rightbrush = RedBrush; }
                else if (right == 1)
                { rightbrush = GreenBrush; }
                else { rightbrush = GreyBrush; }
                if (middle == 0)
                { middlebrush = RedBrush; }
                else if (middle == 1)
                { middlebrush = GreenBrush; }
                else { middlebrush = GreyBrush; }
                //this.Size = new System.Drawing.Size(225, 75);
                g.FillEllipse(leftbrush, 0, 0, this.Size.Width / 3, this.Size.Height - 1);
                g.DrawEllipse(new Pen(Color.White, 1), 0, 0, this.Size.Width / 3, this.Size.Height - 1);
                g.FillEllipse(middlebrush, this.Size.Width / 3, 0, this.Size.Width / 3 - 1, this.Size.Height - 1);
                g.DrawEllipse(new Pen(Color.White, 1), this.Size.Width / 3, 0, this.Size.Width / 3 - 1, this.Size.Height - 1);
                g.FillEllipse(rightbrush, this.Size.Width * 2 / 3, 0, this.Size.Width / 3 - 1, this.Size.Height - 1);
                g.DrawEllipse(new Pen(Color.White, 1), this.Size.Width * 2 / 3, 0, this.Size.Width / 3 - 1, this.Size.Height - 1);
                g.DrawLine(new Pen(Color.Yellow, (float)2.5), this.Size.Width - 1, 0, this.Size.Width - 1, this.Width - 5);
            }
        }

        [
        Category("Signal Properties"),
        DefaultValue(false)
        ]
        public bool Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
                Invalidate();
            }
        }
        [
        Category("Signal Properties"),
        DefaultValue(0)
        ]
        public int LeftColor
        {
            get
            {
                return leftcolor;
            }
            set
            {
                leftcolor = value;
                Invalidate();
            }
        }
        [
        Category("Signal Properties"),
        DefaultValue(0)
        ]
        public int RightColor
        {
            get
            {
                return rightcolor;
            }
            set
            {
                rightcolor = value;
                Invalidate();
            }
        }
        [
        Category("Signal Properties"),
        DefaultValue(0)
        ]
        public int MiddleColor
        {
            get
            {
                return middlecolor;
            }
            set
            {
                middlecolor = value;
                Invalidate();
            }
        }
        [
        Category("Signal Properties"),
        DefaultValue(0)
        ]
        public int SignalNum
        {
            get
            {
                return signalnum;
            }
            set
            {
                signalnum = value;
                Invalidate();
            }
        }
        //protected override void OnPaint(PaintEventArgs pevent)
        //{
        //    //使控件边界也为圆形
        //    //GraphicsPath graphics = new GraphicsPath();
        //    //graphics.AddEllipse(0, 0, this.Width, this.Height);
        //    //this.Region = new System.Drawing.Region(graphics);
        //    //base.OnPaint(pevent);
        //    //base.OnPaint(pevent);
        //}
    }
}



//namespace WindowsFormsApplication7
