using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication7
{
    public partial class Display : Form
    {
        public Display()
        {
            InitializeComponent();
        }
        private bool ledSwitch = true;
        [CategoryAttribute("状态"), Description("LED打开或关闭")]
        public bool LEDSwitch
        {
            set { ledSwitch = value; }
            get { return ledSwitch; }
        }
        private int TrainLength = 20;
        List<int> StationPositionX = new List<int>();
        List<int> StationPositionY = new List<int>();
        private void Display_Load(object sender, EventArgs e)
        {
            // Update the position of stations
            for (int i = 0; i < Form1.Stations.Count+1; i++)
            {
                StationPositionX.Add(this.Controls[i+17].Location.X);
                //this.Controls[i].Location.X;
                StationPositionY.Add(this.Controls[i + 17].Location.Y);
                    //this.Controls[i].Location.Y;
            }
        }

        private void timer1_Tick(object sender, EventArgs e) // Update the train states here
        {
            if (Form1.Trains[0].Segment!=0)  
            {
                if (this.ledControl1.LEDSwitch)
            {
                this.ledControl1.LEDSwitch = false;
                this.ledControl1.Invalidate();       // 程序中修改自定义控件，需要使控件无效重绘
            }
            else
            {
                this.ledControl1.LEDSwitch = true;
                this.ledControl1.Invalidate();
            }      
            }
            this.Refresh();
        }

        private void Display_Paint(object sender, PaintEventArgs e)
        {
            foreach (train k in Form1.Trains)
            {
                if (k.Segment != 0)
                {
                    e.Graphics.DrawLine(redPen, CreatLine(k.Segment, k.Position, k.TrainDirection).Start, CreatLine(k.Segment, k.Position, k.TrainDirection).End);
                }
            }
            //numberTextBoxExt17.Location.X
        }
        public Line CreatLine(int segment, float position, int direction)
        {
            // 判断是否是最后一个segment，否的话，判断相邻的两个segment是否在同一个y上；否的话，计算form的scale计算X坐标
            Line Tline = new Line();
            if (segment < StationPositionX.Count-1)
            {
                if (direction == 0)
                {
                    if (StationPositionY[segment] == StationPositionY[segment + 1])
                    {
                        Tline.Start = new Point(StationPositionX[segment] + 80 / 2 +
                            Convert.ToInt32((StationPositionX[segment + 1] - StationPositionX[segment]) * position),
                            StationPositionY[segment] + 16 + 24); // 80 is the length of station
                        Tline.End = new Point(StationPositionX[segment] + 80 / 2 +
                            Convert.ToInt32((StationPositionX[segment + 1] - StationPositionX[segment]) * position) + TrainLength,
                             StationPositionY[segment] + 16 + 24);
                    }
                    else
                    {
                        if (position < (this.Size.Width - StationPositionY[segment]) / (this.Size.Width - StationPositionY[segment] + StationPositionX[segment + 1]))
                        {
                            Tline.Start = new Point(StationPositionX[segment] + 80 / 2 +
                            Convert.ToInt32((this.Size.Width - StationPositionY[segment] + StationPositionX[segment + 1]) * position),
                            StationPositionY[segment] + 16 + 24);
                            Tline.End = new Point(StationPositionX[segment] + 80 / 2 +
                            Convert.ToInt32((this.Size.Width - StationPositionY[segment] + StationPositionX[segment + 1]) * position) + TrainLength,
                            StationPositionY[segment] + 16 + 24);
                        }
                        else
                        {
                            Tline.Start = new Point(Convert.ToInt32((position - (this.Size.Width - StationPositionY[segment]) /
                            (this.Size.Width - StationPositionY[segment] + StationPositionX[segment + 1])) * StationPositionX[segment + 1]), StationPositionY[segment + 1] + 16 +24);
                            Tline.End = new Point(Convert.ToInt32((position - (this.Size.Width - StationPositionY[segment]) /
                            (this.Size.Width - StationPositionY[segment] + StationPositionX[segment + 1])) * StationPositionX[segment + 1]) + TrainLength,
                            StationPositionY[segment + 1] + 16 + 24);
                        }
                    }
                }
                else // Up direction
                {
                    int DownSegment = StationPositionX.Count - segment;
                    if (DownSegment + 1 < StationPositionX.Count)
                    {
                       if (StationPositionY[DownSegment] == StationPositionY[DownSegment - 1])
                        {
                            Tline.Start = new Point(StationPositionX[DownSegment] + 80 / 2 +
                                Convert.ToInt32((StationPositionX[DownSegment - 1] - StationPositionX[DownSegment]) * position),
                                StationPositionY[DownSegment] + 178); // 80 is the length of station
                            Tline.End = new Point(StationPositionX[DownSegment] + 80 / 2 +
                                Convert.ToInt32((StationPositionX[DownSegment - 1] - StationPositionX[DownSegment]) * position) + TrainLength,
                                 StationPositionY[DownSegment] + 178);
                        }
                       else
                        {
                            if (position < (double)(StationPositionX[DownSegment]) / (double)(this.Size.Width - StationPositionX[DownSegment - 1] + StationPositionX[DownSegment]))
                            {
                                Tline.Start = new Point(StationPositionX[DownSegment] + 80 / 2 + 
                                    Convert.ToInt32((- StationPositionX[DownSegment]) * position), 
                                    StationPositionY[DownSegment] + 178);
                                Tline.End = new Point(StationPositionX[DownSegment] + 80 / 2 +
                                    Convert.ToInt32((- StationPositionX[DownSegment]) * position) + TrainLength,
                                StationPositionY[DownSegment] + 178);
                            }
                            else
                            {
                                Tline.Start = new Point(Convert.ToInt32((1-position)*  (this.Size.Width - StationPositionY[DownSegment - 1]) + StationPositionX[DownSegment - 1]), 
                                StationPositionY[DownSegment-1] + 178);
                                Tline.End = new Point(Convert.ToInt32((1 - position) * (this.Size.Width - StationPositionY[DownSegment - 1]) + StationPositionX[DownSegment - 1]) + TrainLength,
                                StationPositionY[DownSegment - 1] + 178);
                            }
                        }
                    }
                 //   textBox1.Text = DownSegment.ToString();
                    //textBox2.Text = ((double)(StationPositionX[DownSegment]) ).ToString();
                //    textBox3.Text = ((double)(this.Size.Width - StationPositionX[DownSegment - 1] + StationPositionX[DownSegment])).ToString();
                }
            }
            return Tline;
        }
        private Pen greenPen = new Pen(Color.Green, 1);
        private Pen redPen = new Pen(Color.Red, 5);
        private Pen blackPen = new Pen(Color.Black, 1);

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
