using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Timers;
using System.Drawing.Drawing2D;
using WindowsFormsApplication7.BaseClasss;
using WindowsFormsApplication7.Algorithm;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using ILOG.Concert;
using ILOG.CPLEX;
//using WpfApp1;
using System.Reflection;
// using System.Threading.Tasks; 
[assembly: System.Windows.Media.DisableDpiAwareness]

namespace WindowsFormsApplication7
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        static extern bool AllocConsole();
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        public static extern void FreeConsole();

        public delegate void delUpdateUI();
        ThreadStart threadStart;
        Thread myUpdateThread;
        // List<Line> ListLine = new List<Line>();
        public int jiange = 10000; //Unit ms
        public int TimeStart = 0;  //Unit ms
        public int Interval = 200;
        public bool judge = true;
        // 鼠标悬停、拖拽功能
        public List<Line> MyLines = new List<Line>();
        public Point MouseDownLocation;
        private bool IsMouseDown = false;
        private bool MoveControl = false;
        private int m_StartX;
        private int m_StartY;
        private int m_CurX;
        private int m_CurY;
        Point Point1 = new Point();
        Point Point2 = new Point();
        Point StartDownLocation = new Point();
        Point EndLocation = new Point();
        List<Point> InitilizedTrains = new List<Point>();
        // 运行图显示功能
        public int UpLiubai = 50;
        public int DownLiubai = 20;
        public static int LeftLiubai = 150;
        public int RightLiubai = 100;
        public Pen stapen = new Pen(Color.Green, 2);
        public Pen Timepen = new Pen(Color.Green, 2);
        public Pen vehiclepen = new Pen(Color.Black, 3);
        public string CurrentFilePath = "";
        public static Parameters MySet = new Parameters();
        public static List<train> Trains = new List<train>();
        public static List<train> RescheduledTrains = new List<train>();
        public static List<station> Stations;
        public static List<section> Sections = new List<section>();
        public static List<PassengerVolume> PassVolume = new List<PassengerVolume>();
        public static List<PassengerVolume> RecordPassVolume = new List<PassengerVolume>();
        public static List<PassengerVolume> OutPassVolume = new List<PassengerVolume>();
        public static List<PassengerVolume> OutRecordPassVolume = new List<PassengerVolume>();
        public static int TrainControlState = 0;
        public static float flowrate = 0.2F;
        public static int StationCount;
        // 0 --> Keep constant
        // 1
        // 2
        public static int TimeRegulationState = 0;
        private bool IsNumberic(string oText)
        {
            try
            {
                int var1 = Convert.ToInt32(oText);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void DrawLayoutLines()
        {
            Sections = new List<section>();
            FileStream tfilestream = new FileStream
                ("Input data\\区间运行时间.txt", System.IO.FileMode.Open);
            StreamReader rs = new StreamReader(tfilestream);
            while (rs.EndOfStream == false)
            {
                string temstr = rs.ReadLine().Trim();
                if ((temstr != "") && (temstr.IsNormalized() == true))
                {
                    decimal rtime = decimal.Parse(temstr);
                    section temSec = new section();
                    temSec.SectionIndex = Sections.Count;
                    //Sections.Count;
                    temSec.RubTime = int.Parse(temstr);
                    Sections.Add(temSec);
                }
            }
            rs.Close();
            rs.Dispose();
            tfilestream.Dispose();

            Stations = new List<station>();
            tfilestream = new FileStream("Input data\\车站信息.txt", FileMode.Open);
            rs = new StreamReader(tfilestream, Encoding.Default);
            while (rs.EndOfStream == false)
            {
                string temstr = rs.ReadLine().Trim();
                if (temstr != "")
                {
                    StationCount += 1;
                }
            }
            rs.Close();
            rs.Dispose();
            tfilestream.Dispose();
            for (int i =0; i<StationCount; i++)
            {
                station s = new station();
                s.Stationindex = StationCount - i -1;
                s.StationName = (s.Stationindex+1).ToString();
                if (i==0)
                {
                    s.YPos = UpLiubai;
                }
                else
                {
                    s.YPos = Stations[i-1].YPos + (int)((decimal)Sections[s.Stationindex].RubTime * MySet.Zongdix);
                }
                Stations.Add(s);
            }
            Stations.Reverse();
            stapen = new Pen(MySet.StaBush, (float)MySet.StaWidth);
            Timepen = new Pen(MySet.TimeBush, (float)MySet.TimeWidth);
            vehiclepen = new Pen(MySet.VehicleBush, (float)MySet.VehicleWidth);
            Graphics temGra = Graphics.FromHwnd(this.Handle);
            SizeF MaxSizef = new SizeF(0, 0);
            for (int i = 0; i < Stations.Count; i++)
            {
                SizeF temsizeF = temGra.MeasureString(Stations[i].StationName, MySet.StaNameFont);
                if (temsizeF.Width > MaxSizef.Width)
                {
                    MaxSizef = temsizeF;
                }
            }
            LeftLiubai = (int)MaxSizef.Width + 75;
            Bitmap tbitmap = new Bitmap((int)(LeftLiubai + MySet.TimeSec * MySet.Hengdix + RightLiubai),
                (int)(UpLiubai + Stations[0].YPos + DownLiubai));
            Graphics Gra = Graphics.FromImage(tbitmap);
            //Gra.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Gra.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Gra.SmoothingMode = SmoothingMode.AntiAlias;
            Gra.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Gra.CompositingQuality = CompositingQuality.HighQuality;
            // Input 
            //MySet.CurrentTime
            for (int i = 0; i < Stations.Count; i++) // 车站线
            {
                if (i > 0 && i < Stations.Count)
                {
                    stapen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                }
                else
                {
                    stapen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                }
                Gra.DrawLine(stapen, GetPoint_Layout(0, i), GetPoint_Layout(MySet.TimeSec, i));
                Gra.DrawString(MySet.StationNameYizhuang[StationCount-i-1], MySet.StaNameFont, Brushes.Black,
                    new PointF(0, Stations[i].YPos - MaxSizef.Height / 2));
                //Stations[i].StationName
            }

            for (int i = 0; i <= MySet.TimeSec; i++) // 时间线
            {
                if (i % MySet.fenge == 0 || i == MySet.TimeSec)
                {
                    if (i > 0 && i < MySet.TimeSec)
                    {
                        Timepen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    }
                    else
                    {
                        Timepen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    }
                    Gra.DrawLine(Timepen, GetPoint_Layout(i, 0), GetPoint_Layout(i, Stations.Count - 1));
                    String timestr = GetTimeString(i + MySet.StartTime);
                    SizeF temsizef = Gra.MeasureString(timestr, MySet.TimeStrFont);
                    Gra.DrawString(timestr, MySet.TimeStrFont, Brushes.Black,
                        new PointF(GetPoint_Layout(i, 0).X - temsizef.Width / 2, (UpLiubai - temsizef.Height) / 2));
                    Gra.DrawString(timestr, MySet.TimeStrFont, Brushes.Black,
                        new PointF(GetPoint_Layout(i, Stations.Count - 1).X - temsizef.Width / 2, GetPoint_Layout(i, 0).Y + (UpLiubai + DownLiubai - temsizef.Height) / 2));
                }
            }
            pictureBox1.Size = tbitmap.Size;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Image = tbitmap;
        }
         public void DrawTrains() 
        {
            stapen = new Pen(MySet.StaBush, (float)MySet.StaWidth);
            Timepen = new Pen(MySet.TimeBush, (float)MySet.TimeWidth);
            vehiclepen = new Pen(MySet.VehicleBush, (float)MySet.VehicleWidth);
            Graphics temGra = Graphics.FromHwnd(this.Handle);
            SizeF MaxSizef = new SizeF(0, 0);
            for (int i = 0; i < Stations.Count; i++)
            {
                SizeF temsizeF = temGra.MeasureString(Stations[i].StationName, MySet.StaNameFont);
                if (temsizeF.Width > MaxSizef.Width)
                {
                    MaxSizef = temsizeF;
                }
            }
            LeftLiubai = (int)MaxSizef.Width + 75;
            Bitmap tbitmap = new Bitmap((int)(LeftLiubai + MySet.TimeSec * MySet.Hengdix + RightLiubai),
                (int)(UpLiubai + Stations[0].YPos + DownLiubai));
            Graphics Gra = Graphics.FromImage(tbitmap);
            //Gra.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Gra.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Gra.SmoothingMode = SmoothingMode.AntiAlias;
            Gra.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Gra.CompositingQuality = CompositingQuality.HighQuality;
            // Input 
            //MySet.CurrentTime
            for (int i = 0; i < Stations.Count; i++) // 车站线
            {
                if (i > 0 && i < Stations.Count)
                {
                    stapen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                }
                else
                {
                    stapen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                }
                Gra.DrawLine(stapen, GetPoint_Layout(0, i), GetPoint_Layout(MySet.TimeSec, i));
                Gra.DrawString(MySet.StationNameYizhuang[StationCount - i - 1], MySet.StaNameFont, Brushes.Black,
                    new PointF(0, Stations[i].YPos - MaxSizef.Height / 2));
            }

            for (int i = 0; i <= MySet.TimeSec; i++) // 时间线
            {
                if (i % MySet.fenge == 0 || i == MySet.TimeSec)
                {
                    if (i > 0 && i < MySet.TimeSec)
                    {
                        Timepen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    }
                    else
                    {
                        Timepen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    }
                    Gra.DrawLine(Timepen, GetPoint_Layout(i, 0), GetPoint_Layout(i, Stations.Count - 1));
                    String timestr = GetTimeString(i + MySet.StartTime);
                    SizeF temsizef = Gra.MeasureString(timestr, MySet.TimeStrFont);
                    Gra.DrawString(timestr, MySet.TimeStrFont, Brushes.Black,
                        new PointF(GetPoint_Layout(i, 0).X - temsizef.Width / 2, (UpLiubai - temsizef.Height) / 2));
                    Gra.DrawString(timestr, MySet.TimeStrFont, Brushes.Black,
                        new PointF(GetPoint_Layout(i, Stations.Count - 1).X - temsizef.Width / 2, GetPoint_Layout(i, 0).Y + (UpLiubai + DownLiubai - temsizef.Height) / 2));
                }
            }
            PlannedLines.Clear();
            for (int i = 0; i < Trains.Count; i++)   //画列车线 
            {
                PlannedLines.Add(new List<Line>() { });
                Pen trainpen = new Pen(Trains[i].ShowColor, (float)Trains[i].ShowWidth);
                switch (Trains[i].ShowStyle)
                {
                    case "实线":
                        trainpen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                        break;
                    case "虚线":
                        trainpen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    break;
                    case "点虚线":
                        trainpen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    break;
                }
                int LineStart = 0;
                Line Medialine_Dwell = new Line();
                Line Medialine = new Line();
                for (int j = 0; j < Trains[i].Arrive.Count; j++)
                {
                    if (j > 0)
                    {
                        if (Trains[i].Arrive[j] == 0 || Trains[i].Depart[j] == 0 || Trains[i].Depart[j - 1]==0)
                        continue;
                        Point Apoint = GetPoint(Trains[i].Arrive[j], GetStationindex(Trains[i].PassStations[j]));
                        Point Dpoint = GetPoint(Trains[i].Depart[j], GetStationindex(Trains[i].PassStations[j]));
                        Point PreDpoint = GetPoint(Trains[i].Depart[j - 1], GetStationindex(Trains[i].PassStations[j - 1]));
                        Medialine_Dwell = new Line(vehiclepen.Color, vehiclepen.Width, new Point(Apoint.X, Apoint.Y), new Point(Dpoint.X, Dpoint.Y), i, j, Trains[i].VehicleNo);
                        Medialine = new Line(vehiclepen.Color, vehiclepen.Width, new Point(PreDpoint.X, PreDpoint.Y), new Point(Apoint.X, Apoint.Y), i, j, Trains[i].VehicleNo);
                        PlannedLines.Last().Add(Medialine);
                        if (j < Trains[i].Arrive.Count - 1)
                        {                                   
                            newlines.Add(Medialine_Dwell);
                            PlannedLines.Last().Add(Medialine_Dwell);
                        }
                        if (LineStart == 0)
                        { Medialine.LineStart = true; }
                        newlines.Add(Medialine);
                        
                        LineStart++;
                    }
                    else
                    {
                        if (Trains[i].Arrive[j] == 0 || Trains[i].Depart[j] == 0)
                        continue;
                        if (i > 0)
                        {
                            if (Trains[i].VehicleNo == Trains[i - 1].VehicleNo)
                            {
                                Point APoint = GetPoint(Trains[i - 1].Depart[Trains[i - 1].Depart.Count - 1],
                                    GetStationindex(Trains[i - 1].PassStations[Trains[i - 1].PassStations.Count - 1]));
                                Point BPoint = GetPoint(Trains[i].Depart[0], GetStationindex(Trains[i].PassStations[0]));
                                int staindex = GetStationindex(Trains[i - 1].PassStations[Trains[i - 1].PassStations.Count - 1]);
                                Point MidlePoint;
                                if (staindex == 0)
                                {
                                    MidlePoint = new Point((int)(APoint.X + BPoint.X) / 2, APoint.Y - (int)(50 * MySet.Zongdix));
                                }
                                else
                                {
                                    MidlePoint = new Point((APoint.X + BPoint.X) / 2, APoint.Y + (int)(50 * MySet.Zongdix));
                                }
                                Line trainline1 = new Line();
                                trainline1.X1 = MidlePoint.X; trainline1.Y1 = MidlePoint.Y; trainline1.X2 = BPoint.X; trainline1.Y2 = BPoint.Y;
                                //MyLines.Add(trainline1);
                            }
                        }
                    }
                }
                if (Convert.ToInt32(Trains[i].PassStations[1])< Convert.ToInt32(Trains[i].PassStations[0]))
                {
                    Trains[i].TrainDirection = 1;
                }
            }

            pictureBox1.Size = tbitmap.Size;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Image = tbitmap;
        }
        public Point GetPoint_Layout(float time, int Stationindex)
        {
            decimal Ypos = Stations[Stationindex].YPos;
            decimal Xpos = (LeftLiubai + (decimal)(time ) * MySet.Hengdix);
            return new Point((int)Xpos, (int)Ypos);
        }
        public Point GetPoint(float time, int Stationindex)
        {
            decimal Ypos = Stations[Stationindex].YPos;
            decimal Xpos = (LeftLiubai + (decimal)(time-MySet.ShowStartTime) * MySet.Hengdix);
            return new Point((int)Xpos, (int)Ypos); 
        }
        public int GetStationindex(string Staname) 
        {
            int GetStationindexOut = -1;
            foreach (station sta in Stations)
            {
                if (sta.StationName == Staname)
                {
                    GetStationindexOut = sta.Stationindex;
                }
            }
            return GetStationindexOut;
        }
        public string GetTimeString(int time)
        {
            int H = (int)(time / 3600);
            int M = (int)((time - H * 3600) / 60);
            return (H.ToString() + ":" + M.ToString("00"));
        }

        private void 输入运行图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Sections = new List<section>();
            //FileStream tfilestream = new FileStream 
            //    ("Input data\\区间运行时间.txt", System.IO.FileMode.Open);
            //StreamReader rs = new StreamReader(tfilestream);
            //while (rs.EndOfStream == false)
            //{
            //    string temstr = rs.ReadLine().Trim();
            //    if((temstr != "")&&(temstr.IsNormalized() == true))
            //        // To be verified
            //    {
            //        decimal rtime = decimal.Parse(temstr);
            //        section temSec = new section();
            //        temSec.SectionIndex = Sections.Count;
            //            //Sections.Count;
            //        temSec.RubTime = int.Parse(temstr);
            //        Sections.Add(temSec);
            //    }
            //}
            //rs.Close();
            //rs.Dispose();
            //tfilestream.Dispose();

            //Stations = new List<station>();
            //tfilestream = new FileStream("Input data\\车站信息.txt", FileMode.Open);
            //rs = new StreamReader(tfilestream, Encoding.Default);
            //while(rs.EndOfStream == false)
            //{
            //    string temstr = rs.ReadLine().Trim();
            //    if (temstr != "")
            //    {
            //        station temsta = new station();
            //        temsta.Stationindex = Stations.Count;
            //        temsta.StationName = temstr;
            //        if (temsta.Stationindex == 0)
            //        {
            //            temsta.YPos = UpLiubai;
            //        }
            //        else 
            //        {
            //            temsta.YPos = Stations[Stations.Count - 1].YPos + 
            //                (int)((decimal)Sections[temsta.Stationindex - 1].RubTime * MySet.Zongdix);
            //        }
            //        Stations.Add(temsta);
            //    }       
            //}
            //rs.Close();
            //rs.Dispose();
            //tfilestream.Dispose();
            // 读取客流
            OpenFileDialog pa = new OpenFileDialog();
            FileStream tfilestream = new FileStream("Input data\\亦庄线客流.csv", FileMode.Open);
            StreamReader rs = new StreamReader(tfilestream, Encoding.Default);
            int a = 0; int stationindex = 0;
            while (rs.EndOfStream == false)
            {            
                string temstr = rs.ReadLine().Trim();
                if (temstr !="")
                {           
                    string[] str = temstr.Split(',');
                    if (IsNumberic(str[0]))
                    {                 
                        if (Convert.ToInt32(str[0])<=13) {
                            stationindex = Convert.ToInt32(str[0]) - 1;
                            int time = 0;
                            for (int i = 0; i < str.Count(); i++)
                            {
                                if (IsNumberic(str[i]))
                                {                                   
                                    float b =Convert.ToInt32(str[i])*flowrate*0.2F;
                                    time += 1800;
                                    Point cc = new Point();
                                    cc = GetPoint( time, Convert.ToInt32(str[0]) - 1);
                                    //cc = new Point(time/100, (int)(Stations[Convert.ToInt32(str[0]) - 1].YPos - b));
                                    RecordPassVolume.Add(new PassengerVolume(cc, b, time, Color.Orange));
                                }
                            }
                            a = 1;
                        }                       
                    }
                    else if (a == 1)
                    {
                        int time = 0;
                        for (int i = 0; i < str.Count(); i++)
                        {
                            if (IsNumberic(str[i]))
                            {
                                float b = Convert.ToInt32(str[i]) * flowrate * 0.2F;
                                time += 1800;
                                Point cc = new Point();
                                cc.X = GetPoint(time, stationindex).X;
                                cc.Y = Stations[stationindex].YPos - (int)b;
                                //cc = new Point(time/100, (int)(Stations[Convert.ToInt32(str[0]) - 1].YPos - b));
                                OutRecordPassVolume.Add(new PassengerVolume(cc, b, time, Color.Black));
                            }
                        }
                        a = 0;
                    }
                }
            }
            rs.Close();
            rs.Dispose();
            tfilestream.Dispose();
            // 读取时刻表信息
            Trains = new List<train>();
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "NEW_FILTER(*.csv)|*.csv";
            if (od.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tfilestream = new FileStream(od.FileName, FileMode.Open);
                rs = new StreamReader(tfilestream, Encoding.Default);
                String preTrainNo = "";
                while (rs.EndOfStream == false)
                {
                    string temstr = rs.ReadLine().Trim();
                    if ((temstr != "") && (temstr.Contains("服务号") == false))
                    {
                        string[] str = temstr.Split(',');
                        if (preTrainNo == "" || str[1] != preTrainNo)
                        {
                            preTrainNo = str[1];
                            train temtrain = new train();
                            temtrain.Trainnidex = Trains.Count;
                            temtrain.VehicleNo = str[0];
                            temtrain.TrainNO = str[1];
                            temtrain.ShowColor = MySet.DefaultTrainBush;
                            temtrain.ShowWidth = MySet.DefaultTrainWidth;
                            Trains.Add(temtrain);
                        }
                        train ttrain = Trains[Trains.Count - 1];
                        ttrain.PassStations.Add(str[2]);
                        ttrain.Arrive.Add(float.Parse(str[3]));
                        ttrain.Depart.Add(float.Parse(str[4]));
                        ttrain.VehicleNo =  str[0];
                    }
                }
                rs.Close();
                tfilestream.Close();
                DrawTrains();
                TrainSimulator();
                DataDisplay();
                //MessageBox.Show("A" + RecordPassVolume.Count);
               // textBox2.Text = Trains.Count.ToString();
            }
            else
            {
                MessageBox.Show("未选择文件！");
            }
        }        
        private void dockPanel1_ActiveContentChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Stations != null)
            {
                MySet.Hengdix += 0.01m;
                DrawTrains();
            }
            else 
            {
                MessageBox.Show("请先加载实际数据");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Stations != null)
            {
                if (MySet.Hengdix > 0.08m)
                { 
                    MySet.Hengdix -= 0.01m;
                    DrawTrains();
                }
            }
            else
            {
                MessageBox.Show("请先加载实际数据");
            }
        }
        List<Line> newlines = new List<Line>();
        List<List<Line>> PlannedLines = new List<List<Line>>();
        List<List<Line>> RescheduledLines = new List<List<Line>>();
        // List<Line> reallines = new List<Line>();
        List<List<Line>> RealLines = new List<List<Line>>();

        public Form1()
        {
            InitializeComponent();
            //NewDisplay displaydark = new NewDisplay();
            //displaydark.Show();
            this.WindowState = FormWindowState.Maximized;
        }
        
        public static int t=0;
        private Line DrawLine = new Line();
        private void ShowMessage(object sender, EventArgs e)
        {
            MyLines.Clear();
            DrawLine.X1 = t+ LeftLiubai + (int)MySet.CurrentTime;
            // DrawLine.Y1 = Stations[Stations.Count - 1].YPos;
            DrawLine.X2 = t+ LeftLiubai + (int)MySet.CurrentTime;
            DrawLine.Y2 = UpLiubai;
            DrawLine.Y1 = 500;
            
            t = t + 1;
            // Time line update
            float timeincreased = (float)MySet.Hengdix;
            MySet.CurrentTime = MySet.CurrentTime +  (1.0F / timeincreased);
            MyLines.Add(DrawLine);
            
            pictureBox1.Invalidate();
            // Train simulation
            //newTrainSimulation();
            // Add real train running lines
            //PassengerSimulator();
            // Show results
            for (int i = 0; i < Trains.Count; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = Trains[i].Segment;
                dataGridView2.Rows[i].Cells[1].Value = Trains[i].Position;
                dataGridView2.Rows[i].Cells[2].Value = Trains[i].PassStations[1];
                dataGridView2.Rows[i].Cells[3].Value = (int)Trains[i].RemainTime;
                dataGridView2.Rows[i].Cells[4].Value = Trains[i].Velocity;
                dataGridView2.Rows[i].Cells[5].Value = Trains[i].CurrentDwellTime;
            }
        }
        //int[] LineIndex = new int[Trains.Count];
        void newTrainSimulation()
        {
            Point Start = new Point();
            Point End = new Point();
            for (int k = 0; k < Trains.Count; k++)
            {
                int FormerSegment = Trains[k].Segment;
                Trains[k].TrainSimulation();
                if (Trains[k].Segment != 0 && Trains[k].Segment!= Trains[k].PassStations.Count)
                {
                    if (Trains[k].Segment == FormerSegment)
                    {
                        if (Trains[k].Position == 1)
                        {
                            Start = RealLines[k][RealLines[k].Count - 1].End;
                            End.X = Start.X;
                            End.Y = Start.Y;
                            RealLines[k].Add(new Line(vehiclepen.Color, vehiclepen.Width, Start, End, 0, 0, Trains[k].VehicleNo));
                        }
                       
                        End.X = LeftLiubai + (int)(((int)MySet.CurrentTime - 100) * MySet.Hengdix);
                        End.X = LeftLiubai + (int)(((int)MySet.CurrentTime ) * MySet.Hengdix);
                        if (Trains[k].TrainDirection == 0)
                        {
                            End.Y = (int)(Stations[Stations.Count - Convert.ToInt32(Trains[k].PassStations[Trains[k].Segment-1])].YPos
                                - Trains[k].Position * Sections[12 - Trains[k].Segment].RubTime * (float)MySet.Zongdix);
                        }
                        else
                        {
                            End.Y = (int)(Stations[Stations.Count - Convert.ToInt32(Trains[k].PassStations[Trains[k].Segment - 1])].YPos
                                + Trains[k].Position * Sections[Trains[k].Segment - 1].RubTime * (float)MySet.Zongdix);
                        }
                        RealLines[k][RealLines[k].Count - 1].End = End;
                    }
                    else if(Trains[k].Segment != FormerSegment)
                    {
                        if (FormerSegment == 0)
                        {
                            Start = GetPoint((int)MySet.CurrentTime - 100, Stations.Count - Convert.ToInt32(Trains[k].PassStations[0]));
                            Start = GetPoint((int)MySet.CurrentTime, Stations.Count - Convert.ToInt32(Trains[k].PassStations[0]));
                            End.X = Start.X;
                            End.Y = Start.Y;
                            RealLines[k].Add(new Line(vehiclepen.Color, vehiclepen.Width, Start, End, 0, 0, Trains[k].VehicleNo));
                        }
                        else
                        {
                            Start = RealLines[k][RealLines[k].Count - 1].End;
                            End.X = Start.X;
                            End.Y = Start.Y;
                            RealLines[k].Add(new Line(vehiclepen.Color, vehiclepen.Width, Start, End, 0, 0, Trains[k].VehicleNo));
                        }
                    }
                }
            }
        }


        private void TrainSimulator()
        {
            for (int i = 0; i < Sections.Count; i++)
            {
                for (int t = 0; t < Sections[i].RubTime; t++)
                {
                    if (t == 0)
                    {
                        Sections[i].Velocity.Add(0);
                    }
                    else if (t < Sections[i].RubTime / 4)
                        //else if (t < 10)
                    {
                        Sections[i].Velocity.Add((0.2F-MySet.DavisB) + Sections[i].Velocity[t - 1]);
                    }
                    else if (t < Sections[i].RubTime * 3 / 4)
                    {
                        Sections[i].Velocity.Add((-MySet.DavisB) + Sections[i].Velocity[t - 1]);
                    }
                    else
                    {
                        Sections[i].Velocity.Add((-0.2F-MySet.DavisB) + Sections[i].Velocity[t - 1]);
                    }
                    
                }
            }
        }
        private void PassengerSimulator()
        {
            Point po = new Point(100, 100);
            //
            //BaseClasss.PassengerVolume pavo = new BaseClasss.PassengerVolume(po, 30);
            foreach (PassengerVolume pavo in RecordPassVolume)
            {
                if (MySet.CurrentTime >= pavo.time)
                {
                    PassVolume.Add(pavo);
                    //RecordPassVolume.Remove(pavo);
                }
            }
            foreach (PassengerVolume pavo in OutRecordPassVolume)
            {
                if (MySet.CurrentTime >= pavo.time)
                {
                    OutPassVolume.Add(pavo);
                    //RecordPassVolume.Remove(pavo);
                }
            }
            //PassVolume.AddRange(RecordPassVolume);
            //OutPassVolume.AddRange(OutRecordPassVolume);
        }
        private void 控制器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fangzhen fangzhen = new Fangzhen();
            fangzhen.OnUserRequest += new Fangzhen.UserRequest(ShowMessage);
            fangzhen.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (Stations != null)
            {
                if (MySet.Hengdix > 0.08m)
                {
                    MySet.Hengdix -= 0.01m;
                    DrawTrains();
                }
            }
            else
            {
                MessageBox.Show("请先加载实际数据");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics Gra = e.Graphics;
            Gra.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Gra.SmoothingMode = SmoothingMode.AntiAlias;
            Gra.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Gra.CompositingQuality = CompositingQuality.HighQuality;
            
            int i, x1, y1, x2, y2;
            for (i = 0; i <= MyLines.Count - 1; i++)
            {
                Pen LinePen = new Pen(Color.FromArgb(255, 255, 0, 0), 3);
                new Line(Color.Red, 4, GetPoint(MyLines[0].X1, Stations.Count-1), GetPoint(MyLines[0].X2, 0), 0, 0, "11").Draw(Gra);
            }
            foreach (Point p in InitilizedTrains)
            {
                Point pp = new Point();
                if (p.Y < MySet.StationNameYizhuang.Count())
                {
                    pp = GetPoint(p.X, p.Y);
                    Gra.DrawEllipse(new Pen(Color.DarkRed, 2), pp.X - (float)3.5, pp.Y - (float)7.5, 15, 15);               
                }
                else
                {
                    pp = GetPoint(p.X, MySet.StationNameYizhuang.Count()*2 -1-  p.Y);
                    Gra.DrawEllipse(new Pen(Color.DarkBlue, 2), pp.X - (float)3.5, pp.Y - (float)7.5, 15, 15);
                    // The other direction
                }
            }
            if (toolStripButton4.Checked)
            {
                //foreach (Line L in newlines)
                foreach(List < Line > LL in PlannedLines){foreach (Line L in LL){L.Draw(Gra);} }
            }
            if (toolStripButton10.Checked)
            {
                foreach (List<Line> LL in PlannedLines)
                {
                    foreach (Line L in LL)
                    {
                        if (L.LineStart == true)
                        {
                            int cc = 0;
                            PointF qq = L.Start;
                            DrawTextOnSegment(Gra, Brushes.Black, MySet.ServiceFont, "T" + L.ServiceID+"; "+L.VehicleID, ref cc, ref qq, L.End, true);
                            Gra.ResetTransform();
                        }
                    }
                }
            }
            if (toolStripButton11.Checked)
            {
                foreach (List<Line> LQ in RealLines)
                {
                    // foreach()
                    foreach (Line L in LQ)
                    {
                      L.RealDraw(Gra);
                    }
                }
            }
            if (toolStripButton12.Checked)
            {
                foreach (PassengerVolume pv in PassVolume)
                {
                    pv.Draw(Gra);
                }
                foreach (PassengerVolume pvout in OutPassVolume)
                {
                    pvout.Draw(Gra);
                }
            }
            foreach (List<Line> LQ in RescheduledLines)
            {
                foreach (Line L in LQ)
                {
                    L.Draw(Gra);
                }
            }
            if (IsMouseDown == true)
            {
                Pen dashed_pen = new Pen(Color.Blue, 1);
                e.Graphics.DrawLine(dashed_pen, Point1.X, Point1.Y, Point2.X, Point2.Y);
            }
        }

        // Draw some text along a line segment.
        // Leave char_num pointing to the next character to be drawn.
        // Leave start_point holding the last point used.
        private void DrawTextOnSegment(Graphics gr, Brush brush,
            Font font, string txt, ref int first_ch,
            ref PointF start_point, PointF end_point,
            bool text_above_segment)
        {
            float dx = end_point.X - start_point.X;
            float dy = end_point.Y - start_point.Y;
            float dist = (float)Math.Sqrt(dx * dx + dy * dy);
            dx /= dist;
            dy /= dist;

            // See how many characters will fit.
            int last_ch = first_ch;
            while (last_ch < txt.Length)
            {
                string test_string =
                    txt.Substring(first_ch, last_ch - first_ch + 1);
                if (gr.MeasureString(test_string, font).Width > dist)
                {
                    // This is one too many characters.
                    last_ch--;
                    break;
                }
                last_ch++;
            }
            if (last_ch < first_ch) return;
            if (last_ch >= txt.Length) last_ch = txt.Length - 1;
            string chars_that_fit =
               // txt.Substring(first_ch, last_ch - first_ch + 1);
               txt.Substring(first_ch, txt.Length);
            // Rotate and translate to position the characters.
            GraphicsState state = gr.Save();
            if (text_above_segment)
            {
                gr.TranslateTransform(0,
                    -gr.MeasureString(chars_that_fit, font).Height,
                    MatrixOrder.Append);
            }
            float angle = (float)(180 * Math.Atan2(dy, dx) / Math.PI);
            gr.RotateTransform(angle, MatrixOrder.Append);
            gr.TranslateTransform(start_point.X, start_point.Y,
                MatrixOrder.Append);

            // Draw the characters that fit.
            gr.DrawString(chars_that_fit, font, brush, 0, 0);

            // Restore the saved state.
            gr.Restore(state);

            // Update first_ch and start_point.
            first_ch = last_ch + 1;
            float text_width =
                gr.MeasureString(chars_that_fit, font).Width;
            start_point = new PointF(
                start_point.X + dx * text_width,
                start_point.Y + dy * text_width);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            //textBox1.Text = LineChosenIndicator.ToString();
            if (e.Button == MouseButtons.Left)
            {
                m_StartX = e.X;
                m_StartY = e.Y;
                m_CurX = e.X;
                m_CurY = e.Y;
                StartDownLocation = e.Location;
            }
        }

        private string positionToTime(int x)
        {  
            return SecondToHour(((float)(x-LeftLiubai)/(float)MySet.Hengdix + MySet.ShowStartTime));
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Pen dashed_pen = new Pen(Color.Green, 1);
            //toolStripTextBox1.Text="x: "+e.X.ToString()+"; y: "+ e.Y.ToString();
            toolStripTextBox1.Text = positionToTime(e.X);
            toolStripTextBox1.TextBoxTextAlign = HorizontalAlignment.Center;
            dashed_pen.DashStyle = DashStyle.Dash;
            if (IsMouseDown == false) return;
            if (MoveControl == false) return;
            m_CurX = e.X;
            m_CurY = e.Y;
            int i;
            i = MyLines.Count - 1;
            if (i >= 0)
            {
                Point1.X = e.X + MyLines[i].X1 - StartDownLocation.X;
                Point1.Y = e.Y + MyLines[i].Y1 - StartDownLocation.Y;
                Point2.X = e.X + MyLines[i].X2 - StartDownLocation.X;
                Point2.Y = e.Y + MyLines[i].Y2 - StartDownLocation.Y;
            }
            pictureBox1.Invalidate();
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (MoveControl == false)
            {
                MoveControl = true;
             //   toolStripButton1 = BorderStyle.Fixed3D;
            }
            else
            {
                MoveControl = false;
              //  toolStripButton1.BorderStyle = BorderStyle.None;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //myGroupBox3.Visible = false;
            //myGroupBox4.Visible = false;
            //button9.Visible = false;
            DrawLayoutLines();
            // dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //for (int i = 0; i < Stations.Count;i++)
            //{
            //    if (i <  dataGridView1.Columns.Count)
            //    {
            //        dataGridView1.Columns[i].HeaderCell.Value = MySet.StationNameYizhuang[i];
            //    }
            //    else
            //    {
            //        dataGridView1.Columns.Add(MySet.StationNameYizhuang[i], MySet.StationNameYizhuang[i]);
            //    }              
            //}
            //DataDisplay();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (Stations != null)
            {
                if (MySet.Hengdix > 0.08m)
                {
                    MySet.Hengdix -= 0.01m;
                    DrawTrains();
                }
            }
            else
            {
                MessageBox.Show("请先加载实际数据");
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (Stations != null)
            {
                MySet.Hengdix += 0.01m;
                DrawTrains();
            }
            else
            {
                MessageBox.Show("请先加载实际数据");
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (Stations != null)
            {
                if (MySet.Hengdix > 0.08m)
                {
                    MySet.Hengdix -= 0.01m;
                    DrawTrains();
                }
            }
            else
            {
                MessageBox.Show("请先加载实际数据");
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (MoveControl == false)
            {
                MoveControl = true;
                toolStripButton9.BackColor = Color.Red;
            }
            else
            {
                MoveControl = false;
                toolStripButton9.BackColor = SystemColors.Control;
            }
        }
        List<Line> PickedLines = new List<Line>();
        bool LineChosenIndicator = false;
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            LineChosenIndicator = false;
            string TrainID = "aa";
            Line X = new Line();
            List<Line> SelectedLines = new List<Line>();
            foreach (List<Line> LL in PlannedLines)
            {
                foreach (Line L in LL)
                { 
                    //L.LineColor = L.HitTest(e.Location) ? Color.Red : Color.Black;
                    if (L.HitTest(e.Location) == true)
                    {
                        LineChosenIndicator = true;
                        X = L;
                        TrainID = L.VehicleID;
                    }
                    //if (L.HitTest(e.Location))
                    //{
                    //     textBox1.Text = "Starting point " + L.Start.ToString();
                    //} 
                }
            }
            if (toolStripButton13.Checked == false)
            {
                if (LineChosenIndicator == true)
                {
                    foreach (Line l in PlannedLines[X.ServiceID]) {SelectedLines.Add(l); }
                    //textBox1.Text = GetTime(PlannedLines[X.ServiceID][0].Start.X);
                    //textBox1.Text = SecondToHour((double)Trains[X.ServiceID].Depart[0]);
                }
            }
            else
            {
                if (LineChosenIndicator)
                {
                    foreach (List<Line> LL in PlannedLines)
                    {
                        foreach (Line l in LL) {
                            if(l.VehicleID == TrainID)
                            {
                                SelectedLines.Add(l); }         
                        }
                    }
                }
            }
            if (LineChosenIndicator == false)
            {
                foreach (Line L in PickedLines) { L.LineColor = vehiclepen.Color; L.Linewidth = vehiclepen.Width; }
                pictureBox1.Invalidate();
                PickedLines.Clear();
                //textBox1.Text = PickedLines.Count().ToString();
                //PickedLines = new List<Line>();
            }
            else
            {
                //textBox1.Text = PickedLines.Count().ToString();
                PickedLines.AddRange(SelectedLines);
                foreach (Line L in PickedLines)
                {
                    L.LineColor = Color.Red; L.Linewidth = L.Linewidth + 2;
                }
                pictureBox1.Invalidate();
                //PickedLines.Clear();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (Line L in newlines) L.Draw(e.Graphics);
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            //foreach(Line L in newlines)
            //    L.LineColor = L.HitTest(e.Location) ? Color.Red : Color.Black;
            //panel2.Invalidate();
        }
        //public profile train_profile = new profile();
        
        private void 速度曲线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DashboardForm newboard = new DashboardForm();
            newboard.Show();
        }

        private void cPU监视ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CPUJianshi jianshi = new CPUJianshi();
            jianshi.Show();
        }

        //private void 线型_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    float bb;
        //    bb = 线型.SelectedIndex;
        //    foreach (Line L in newlines)
        //    {
        //        L.Linewidth = bb;
        //        pictureBox1.Invalidate();
        //    }
        //}
        //protected override void OnClosing(CancelEventArgs e)
        //{
        //    DialogResult result = MessageBox.Show("是否确认关", "警告",
        //                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //    e.Cancel = result != DialogResult.Yes;
        //    base.OnClosing(e);
        //}
        private void DataDisplay()
        {
            for (int i = 0; i < Trains.Count; i++)
            {
                RealLines.Add(new List<Line> { });
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[i].HeaderCell.Value = (i+1).ToString();
                // int index = i;
                for (int j = 0; j < Trains[0].Arrive.Count; j++)
                {
                   // dataGridView1.Rows[index].Cells[j].Value = Trains[i].Arrive[j];
                }
            }
            for (int i = 0; i < Trains.Count; i++)
            {
                int index = dataGridView2.Rows.Add();
                //dataGridView2.Rows[index].HeaderCell.Value = (i + 1).ToString();
            }
        }

        private void 线路拓扑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Layout layout = new Layout();
            //layout.OnUserRequest += new Layout.UserRequest(ShowMessage);
            layout.Show();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void 调度监视ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Display ShowDisplay = new Display();
            ShowDisplay.Show();
            //layout.OnUserRequest += new Layout.UserRequest(ShowMessage);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string str = dt.ToString("yyyy-MM-dd HH:mm:ss");
            sevenSegmentArray1.Value = str;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.SetToolTip(this.button1, "列车按照计划图运行");
        }
        private void button3_MouseEnter(object sender, EventArgs e)
        {
            ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.SetToolTip(this.button3, "列车按照当前状态继续运行");
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.SetToolTip(this.button2, "列车以赶点的方式运行");
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.SetToolTip(this.button4, "按照运行调整后的运行图运行");
        }

        private void button9_MouseEnter(object sender, EventArgs e)
        {
            ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.SetToolTip(this.button9, "故障注入");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (TrainControlState != 1)
            {
                button1.BackColor = Color.MistyRose;
                TrainControlState = 1;
            }
            else
            {
                button1.BackColor = Color.Transparent;
                TrainControlState = 0;
            }
            
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (toolStripButton4.Checked == true)
            {
                toolStripButton4.Checked = false;
            }
            else { toolStripButton4.Checked = true;}
            pictureBox1.Invalidate();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            if (toolStripButton11.Checked == true)
            {
                toolStripButton11.Checked = false;
            }
            else { toolStripButton11.Checked = true; }
            pictureBox1.Invalidate();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (toolStripButton12.Checked == true)
            {
                toolStripButton12.Checked = false;
            }
            else { toolStripButton12.Checked = true; }
            pictureBox1.Invalidate();
        }

        private void 运行模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TrainControlState != 1)
            {
                运行模式ToolStripMenuItem.BackColor = Color.MistyRose;
                TrainControlState = 1;
                myGroupBox3.Visible = true;
                myGroupBox4.Visible = true;
                button9.Visible = true;
            }
            else
            {
                运行模式ToolStripMenuItem.BackColor = Color.Transparent;
                TrainControlState = 0;
                myGroupBox3.Visible = false;
                myGroupBox4.Visible = false;
                button9.Visible = false;
            }
        }

        private void toolStripButton10_Click_1(object sender, EventArgs e)
        {
            if (toolStripButton10.Checked == true)
            {
                toolStripButton10.Checked = false;
            }
            else { toolStripButton10.Checked = true; }
            pictureBox1.Invalidate();
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            if (toolStripButton13.Checked == true)
            {
                toolStripButton13.Checked = false;
            }
            else { toolStripButton13.Checked = true; }
            pictureBox1.Invalidate();
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            if (toolStripButton14.Checked == true)
            {
                toolStripButton14.Checked = false;
            }
            else { toolStripButton14.Checked = true; }
            pictureBox1.Invalidate();
        }

        #region 秒转换小时 SecondToHour
        /// <summary>
        /// 秒转换小时
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string SecondToHour(double time)
        {
            string str = "";
            int hour = 0;
            int minute = 0;
            int second = 0;
            second = Convert.ToInt32(time);

            if (second > 60)
            {
                minute = second / 60;
                second = second % 60;
            }
            if (minute > 60)
            {
                hour = minute / 60;
                minute = minute % 60;
            }
            return (hour + "小时" + minute + "分钟"
                + second + "秒");
        }
        public static int HourToSecond(int hour, int minute, int second)
        {
            return (hour*3600+ minute*60 + second);
        }
        #endregion

        // Leave start_point holding the last point used.
        private void DrawTextOnPoint(Graphics gr, Brush brush,
            Font font, string txt, ref int first_ch,
            ref PointF start_point, PointF end_point,
            bool text_above_segment)
        {
            float dx = end_point.X - start_point.X;
            float dy = end_point.Y - start_point.Y;
            float dist = (float)Math.Sqrt(dx * dx + dy * dy);
            dx /= dist;
            dy /= dist;

            // See how many characters will fit.
            int last_ch = first_ch;
            while (last_ch < txt.Length)
            {
                string test_string =
                    txt.Substring(first_ch, last_ch - first_ch + 1);
                if (gr.MeasureString(test_string, font).Width > dist)
                {
                    // This is one too many characters.
                    last_ch--;
                    break;
                }
                last_ch++;
            }
            if (last_ch < first_ch) return;
            if (last_ch >= txt.Length) last_ch = txt.Length - 1;
            string chars_that_fit =
               // txt.Substring(first_ch, last_ch - first_ch + 1);
               txt.Substring(first_ch, txt.Length);
            // Rotate and translate to position the characters.
            GraphicsState state = gr.Save();
            if (text_above_segment)
            {
                gr.TranslateTransform(0,
                    -gr.MeasureString(chars_that_fit, font).Height,
                    MatrixOrder.Append);
            }
            float angle = (float)(180 * Math.Atan2(dy, dx) / Math.PI);
            gr.RotateTransform(angle, MatrixOrder.Append);
            gr.TranslateTransform(start_point.X, start_point.Y,
                MatrixOrder.Append);

            // Draw the characters that fit.
            gr.DrawString(chars_that_fit, font, brush, 0, 0);

            // Restore the saved state.
            gr.Restore(state);

            // Update first_ch and start_point.
            first_ch = last_ch + 1;
            float text_width =
                gr.MeasureString(chars_that_fit, font).Width;
            start_point = new PointF(
                start_point.X + dx * text_width,
                start_point.Y + dy * text_width);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            //contextMenuStrip1.Enabled = (LineChosenIndicator ==false);
            运行线设置ToolStripMenuItem.Enabled = (LineChosenIndicator == true);
        }
        private void 运行线设置ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            LineAdjustment lineAdjustment = new LineAdjustment();
            lineAdjustment.OnUserRequestLineAdjustment += new LineAdjustment.UserRequest(TrainRegulation);
            lineAdjustment.Show();
        }
        public static string RightMove;
        public void TrainRegulation(object sender, EventArgs e)
        {
            //PickedLines
            // 右移动
            if (toolStripButton13.Checked)
            {
                int ServiceCompare = -10;
                foreach (Line L in PickedLines)
                {
                    if (L.ServiceID != ServiceCompare)
                    {
                        for (int i = 0; i < Trains[L.ServiceID].Arrive.Count(); i++)
                        {
                            Trains[L.ServiceID].Arrive[i] += Convert.ToInt32(RightMove);
                            Trains[L.ServiceID].Depart[i] += Convert.ToInt32(RightMove);
                        }
                        ServiceCompare = L.ServiceID;
                    }
                    else { continue; }
                }
            }
            else
            {
                int ServiceCompare = -10;
                foreach(Line L in PickedLines)
                {
                    if (L.ServiceID != ServiceCompare)
                    {
                        for (int i = 0; i < Trains[L.ServiceID].Arrive.Count(); i++)
                        {
                            Trains[L.ServiceID].Arrive[i] += Convert.ToInt32(RightMove);
                            Trains[L.ServiceID].Depart[i] += Convert.ToInt32(RightMove);
                        }
                        ServiceCompare = L.ServiceID;
                    }
                    else { continue; }
                }
            }
            DrawTrains();
            int ServiceC = -10;
            foreach (Line L in PickedLines) {
                if (L.ServiceID != ServiceC)
                {
                    foreach (Line l in PlannedLines[L.ServiceID]) { l.LineColor = Color.Red; l.Linewidth = l.Linewidth + 2; }
                    ServiceC = L.ServiceID;
                }
                else { continue; }
            }
            pictureBox1.Invalidate();
        }
        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            WriterFile("Input data\\输出文件.csv", "列车号, 服务号	, 车站, 到达, 出发", false);
            int ServiceIndex = 0;
            foreach (train T in Trains)
            {
                ServiceIndex++;
                string aa;
                for (int i = 0; i < T.Arrive.Count(); i++)
                {               
                    aa = T.VehicleNo +","+ServiceIndex +"," +T.PassStations[i]+","+T.Arrive[i]+","+T.Depart[i];
                    {
                        WriterFile("Input data\\输出文件.csv", aa, true);
                    }
                }
            }
        }


        public static void WriterFile(string path, string message, bool append)
        {
            using (StreamWriter swtrain = new StreamWriter(path, append, Encoding.Default))
            {
                swtrain.WriteLine(message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Resheduling resheduling = new Resheduling();
            resheduling.TrainReschedulingInilize += new Resheduling.UserRequest(InitilizeRescheduling);
            resheduling.Show();
        }

        public void InitilizeRescheduling(object sender, EventArgs e)
        {
            DrawLine.X1 = t + LeftLiubai + (int)MySet.CurrentTime;
            DrawLine.X2 = t + LeftLiubai + (int)MySet.CurrentTime;
            DrawLine.Y2 = UpLiubai;
            DrawLine.Y1 = 500;
            MyLines.Add(DrawLine);
            for (int i = 0; i < Resheduling.VehicleSegment.Count(); i++)
            {
                InitilizedTrains.Add(new Point((int)MySet.CurrentTime, Resheduling.VehicleSegment[i]));
            }   
            pictureBox1.Invalidate();
        }
        
        private void button8_Click(object sender, EventArgs e)
        {
            #region
            AllocConsole();
            MessageBox.Show("Being the algorithm");
            Method method = new Method();
            method.NewMain(e);
            Console.WriteLine("转换完成，开始输出...");
            #endregion
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream fStream = new FileStream ("Input data//savett.tt", FileMode.Create);
            //StreamReader rs = new StreamReader(fStream);
            BinaryFormatter serializationFormat = new BinaryFormatter();
            serializationFormat.Serialize(fStream, Trains);
            //serializationFormat.Serialize(fStream, PlannedLines);
            serializationFormat.Serialize(fStream, Resheduling.VehicleSegment);
            serializationFormat.Serialize(fStream, MySet);
            fStream.Close();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "NEW_FILTER(*.tt)|*.tt";
            if (od.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                
                BinaryFormatter serializationFormat = new BinaryFormatter();
                string Filename = od.FileName;
                FileStream fileStream = new FileStream(Filename, FileMode.Open);
                Trains = serializationFormat.Deserialize(fileStream) as List<train>;
                Resheduling.VehicleSegment = serializationFormat.Deserialize(fileStream) as List<int>;
                MySet = serializationFormat.Deserialize(fileStream) as Parameters;
                //PlannedLines = serializationFormat.Deserialize(fileStream) as List<List<Line>>;
                fileStream.Close();
                InitilizeRescheduling(new object(), new EventArgs());
            }
            else
            {
                MessageBox.Show("请选择文件！");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RescheduledLines = DrawTrainsToLines(RescheduledTrains);
            MessageBox.Show("调整车次数量为 "+ RescheduledLines.Count());
            pictureBox1.Invalidate();
        }

        public List<List<Line>> DrawTrainsToLines(List<train> listtrain)
        {
            List<List<Line>> listline = new List<List<Line>>();
            listline.Clear();
            for (int i = 0; i < listtrain.Count; i++)   //画列车线 
            {
                listline.Add(new List<Line>() { });
                Line Medialine_Dwell = new Line();
                Line Medialine = new Line();
                for (int j = 0; j < listtrain[i].Arrive.Count; j++)
                {
                    if (j > 0)
                    {
                        if (listtrain[i].Arrive[j] == 0 || listtrain[i].Depart[j] == 0 || listtrain[i].Depart[j - 1] == 0)
                            continue;
                        Point Apoint = GetPoint(listtrain[i].Arrive[j], GetStationindex(listtrain[i].PassStations[j]));
                        Point Dpoint = GetPoint(listtrain[i].Depart[j], GetStationindex(listtrain[i].PassStations[j]));
                        Point PreDpoint = GetPoint(listtrain[i].Depart[j - 1], GetStationindex(listtrain[i].PassStations[j - 1]));
                        Medialine_Dwell = new Line(Color.Red, vehiclepen.Width, new Point(Apoint.X, Apoint.Y), new Point(Dpoint.X, Dpoint.Y), i, j, listtrain[i].VehicleNo);
                        Medialine = new Line(Color.Red, vehiclepen.Width, new Point(PreDpoint.X, PreDpoint.Y), new Point(Apoint.X, Apoint.Y), i, j, listtrain[i].VehicleNo);
                        listline.Last().Add(Medialine);
                        if (j < listtrain[i].Arrive.Count - 1)
                        {
                            listline.Last().Add(Medialine_Dwell);
                        }
                    }
                }
            }
            return listline;
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            //WpfApp1.MainWindow newwindow = new WpfApp1.MainWindow();
            //var wpfwindow = new 
            //wpfwindow.Show();
            var newwindow = new Window1();
            newwindow.ShowDialog();
            Form1.MySet.CurrentTime = Form1.HourToSecond(Convert.ToInt32(dataGridView2.Rows[0].Cells[0].Value),
                Convert.ToInt32(dataGridView2.Rows[0].Cells[1].Value), Convert.ToInt32(dataGridView2.Rows[0].Cells[2].Value));
            for (int i = 0; i < InputParameters.TrainPosition.Count(); i++)
            {
                //VehicleSegment.Add();
                for (int j = 0; j < StationCount*2; j++)
                {
                    if (InputParameters.TrainPosition[i] == j)
                    {
                        Resheduling.VehicleSegment.Add(j);
                        break;
                    }
                }
            }
            MySet.CurrentTime = InputParameters.CurrentTime;
            InitilizeRescheduling(new object(), new EventArgs());
        }
    }
}
