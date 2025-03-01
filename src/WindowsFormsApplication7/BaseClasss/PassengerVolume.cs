using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication7.BaseClasss
{
    public class PassengerVolume
    {
        public Point Start { get; set; }
        public float Flow { get; set; }
        public float time { get; set; }
        public float rate = 0.4F;
        public Color color { get; set; }
        //public float Linewidth { get; set; }
        public static long TotalCount;
        public float width = 14;
        public PassengerVolume(Point p, float f, float t, Color r)
        {
            Start = p;
            Flow = f;
            time = t;
            color = r;
        }
        public void Draw(Graphics G)
        {  
            //Pen pen = new Pen(Color.Orange, 3);
            SolidBrush brush = new SolidBrush(color);
            //G.DrawRectangle(pen, 10, 10, 100, 50); 
            G.FillRectangle(brush, Start.X, Start.Y,width, Flow);
        }
    }
}
