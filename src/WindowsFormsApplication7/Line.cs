using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication7
{
    [Serializable]
    public class Line
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public Point Middle { get; set; }
        public Color LineColor { get; set; }
        public Color LineColorPast { get; set; }
        public Line FormerLine { get; set; }
        public bool LineStart = false;
        public bool LineEnd = false;
        public Line AfterLine { get; set; }
        public float Linewidth { get; set; }
        public float LinewidthPast { get; set; }
        public int ServiceID { get; set; }
        public string VehicleID { get; set; }
        public int stationID { get; set; }
        public bool Selected { get; set; }
        public static long TotalCount;
        private int _X1;
        private int _Y1;
        private int _X2;
        private int _Y2;

        public int X1
        {
            get { return _X1; }
            set { _X1 = value; }
        }

        public int Y1
        {
            get { return _Y1; }
            set { _Y1 = value; }
        }

        public int X2
        {
            get { return _X2; }
            set { _X2 = value; }
        }

        public int Y2
        {
            get { return _Y2; }
            set { _Y2 = value; }
        }

        public Line(int _X1, int _Y1, int _X2, int _Y2)
        {
            X1 = _X1;
            Y1 = _Y1;
            X2 = _X2;
            Y2 = _Y2;
            TotalCount += 1;
        }

        public Line()
        {
            X1 = 0;
            Y1 = 0;
            X2 = 0;
            Y2 = 0;
            TotalCount += 1;
        }
        public Line(Color c, float w, Point s, Point e, int si, int stid, string vehicle)
        {
            LineColor = c;
            Linewidth = w;
            LinewidthPast = w + 1;
            Start = s;
            End = e;
            ServiceID = si;
            stationID = stid;
            VehicleID = vehicle;
        }
        public void Draw(Graphics G)
        {
            LineColorPast = Color.DarkGreen;
            int c = Form1.LeftLiubai + Form1.t;
            if (End.X < c)
            {
                using (Pen pen = new Pen(LineColorPast, LinewidthPast)) G.DrawLine(pen, Start, End);
            }
            else if (Start.X<c)
            {
                Point middle = new Point(c, Start.Y+(End.Y-Start.Y)*(c-Start.X)/(End.X-Start.X));
                using (Pen pen = new Pen(LineColorPast, LinewidthPast)) G.DrawLine(pen, Start, middle);
                using (Pen pen = new Pen(LineColor, Linewidth)) G.DrawLine(pen, middle, End);
            }
            else
            {
                using (Pen pen = new Pen(LineColor, Linewidth)) G.DrawLine(pen, Start, End);
            }
        }

        public void RealDraw(Graphics G)
        {
            using (Pen pen = new Pen(Color.Red, Linewidth)) G.DrawLine(pen, Start, End);
        }

        public bool HitTest(Point Pt)
        {
            if ((Pt.X < Start.X && Pt.X < End.X) || (Pt.X > Start.X && Pt.X > End.X) || (Pt.Y < Start.Y && Pt.Y < End.Y) || (Pt.Y > Start.Y && Pt.Y > End.Y))
            {
                return false;
            }
            float dy = End.Y - Start.Y;
            float dx = End.X - Start. X;
            float Z = dy * Pt.X - dx * Pt.Y + Start.Y * End.X - Start.X * End.Y;
            float N = dy * dy+dx * dx;
            float dist = (float)(Math.Abs(Z)/Math.Sqrt(N));
            return dist < Linewidth / 2f;
        }

        ~Line()
        {
            TotalCount -= 1;
        }
    }
}
