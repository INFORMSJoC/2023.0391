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
    public partial class NewDisplay : Form
    {
        //private List<List<StraightLine>> LineTrackCollection = new List<List<StraightLine>>();
        private List<List<StraightLine>> UpLineTrackCollection = new List<List<StraightLine>>();
        private List<List<StraightLine>> DownLineTrackCollection = new List<List<StraightLine>>();
        private List<MultiSignal> SignalSet = new List<MultiSignal>();
        public NewDisplay()
        {
            this.DoubleBuffered = true;//设置本窗体
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            InitializeComponent();
            foreach (Control ctr in this.panel1.Controls)
            {
                if (ctr is MultiSignal)
                {
                    MultiSignal xx = ctr as MultiSignal;
                    SignalSet.Add(xx);
                }
            }
            foreach (MultiSignal xx in SignalSet)
            {
                if (xx.SignalNum == 2)
                {
                    xx.LeftColor = 2;
                    xx.RightColor = 1;
                }
                if (xx.SignalNum == 3)
                {
                    xx.LeftColor = 2;
                    xx.RightColor = 2;
                    xx.MiddleColor = 1;
                }
            }
            GroupLineTrack();
            textBox1.Text = DownLineTrackCollection[1][1].Sstart.ToString();
            //Graphics g1 = new Graphics ;

        }
        private int timeindex = 0;
        private List<StraightLine> ListSet = new List<StraightLine>();
        //private List<MultiSignal> SignalSet = new List<MultiSignal>();

        private void GroupLineTrack()
        {
            //for (int i = 0; i < 1; i++)
            {
                DownLineTrackCollection.Add(new List<StraightLine>());//Segment[0]，从宋家庄站到肖村站
                DownLineTrackCollection[0].Add(straightLine175);
                DownLineTrackCollection[0].Add(straightLine174);
                DownLineTrackCollection[0].Add(straightLine173);
                DownLineTrackCollection[0].Add(straightLine154);
                 DownLineTrackCollection[0].Add(straightLine153);
                DownLineTrackCollection[0].Add(straightLine151);
                DownLineTrackCollection[0].Add(straightLine150);
                DownLineTrackCollection[0].Add(straightLine126);
                DownLineTrackCollection[0].Add(straightLine118);
                DownLineTrackCollection[0].Add(straightLine107);
                DownLineTrackCollection[0].Add(straightLine104);
                DownLineTrackCollection[0].Add(straightLine96);
                DownLineTrackCollection[0].Add(straightLine95);
                DownLineTrackCollection[0].Add(straightLine94);
                DownLineTrackCollection[0].Add(straightLine88);
                DownLineTrackCollection[0].Add(straightLine87);
                DownLineTrackCollection[0].Add(straightLine84);
                DownLineTrackCollection.Add(new List<StraightLine>());//Segment[1]，从肖村站到小红门站
                DownLineTrackCollection[1].Add(straightLine84);
                DownLineTrackCollection[1].Add(straightLine82);
                DownLineTrackCollection[1].Add(straightLine69);
                DownLineTrackCollection[1].Add(straightLine68);
                DownLineTrackCollection[1].Add(straightLine67);
                DownLineTrackCollection[1].Add(straightLine66);
                DownLineTrackCollection[1].Add(straightLine59);
                DownLineTrackCollection[1].Add(straightLine51);
                DownLineTrackCollection[1].Add(straightLine46);
                DownLineTrackCollection.Add(new List<StraightLine>());//Segment[2]，从小红门站站到旧宫站
                DownLineTrackCollection[2].Add(straightLine46);
                DownLineTrackCollection[2].Add(straightLine354);
                DownLineTrackCollection[2].Add(straightLine353);
                DownLineTrackCollection[2].Add(straightLine352);
                DownLineTrackCollection[2].Add(straightLine351);
                DownLineTrackCollection[2].Add(straightLine350);
                DownLineTrackCollection[2].Add(straightLine349);
                DownLineTrackCollection[2].Add(straightLine342);
                DownLineTrackCollection[2].Add(straightLine341);
                DownLineTrackCollection[2].Add(straightLine340);
                DownLineTrackCollection[2].Add(straightLine339);
                DownLineTrackCollection[2].Add(straightLine340);
                DownLineTrackCollection[2].Add(straightLine339);
                DownLineTrackCollection[2].Add(straightLine133);
                DownLineTrackCollection[2].Add(straightLine43);
                DownLineTrackCollection[2].Add(straightLine333);
                DownLineTrackCollection.Add(new List<StraightLine>());//Segment[3]，从旧宫站到亦庄桥站
                DownLineTrackCollection[3].Add(straightLine333);
                DownLineTrackCollection[3].Add(straightLine36);
                DownLineTrackCollection[3].Add(straightLine332);
                DownLineTrackCollection[3].Add(straightLine331);
                DownLineTrackCollection[3].Add(straightLine330);
                DownLineTrackCollection[3].Add(straightLine329);
                DownLineTrackCollection[3].Add(straightLine328);
                DownLineTrackCollection[3].Add(straightLine327);
                DownLineTrackCollection[3].Add(straightLine326);
                DownLineTrackCollection[3].Add(straightLine325);
                DownLineTrackCollection[3].Add(straightLine131);
                DownLineTrackCollection[3].Add(straightLine136);
                DownLineTrackCollection[3].Add(straightLine134);
                DownLineTrackCollection[3].Add(straightLine130);
                DownLineTrackCollection[3].Add(straightLine113);
                DownLineTrackCollection.Add(new List<StraightLine>());//Segment[4]，从亦庄桥站到亦庄文化园站
                DownLineTrackCollection[4].Add(straightLine113);
                DownLineTrackCollection[4].Add(straightLine310);
                DownLineTrackCollection[4].Add(straightLine105);
                DownLineTrackCollection[4].Add(straightLine140);
                DownLineTrackCollection[4].Add(straightLine142);
                DownLineTrackCollection[4].Add(straightLine308);
                DownLineTrackCollection.Add(new List<StraightLine>());//Segment[5]，从亦庄文化园站到万源街站
                DownLineTrackCollection[5].Add(straightLine308);
                DownLineTrackCollection[5].Add(straightLine307);
                DownLineTrackCollection[5].Add(straightLine299);
                DownLineTrackCollection[5].Add(straightLine298);
                DownLineTrackCollection[5].Add(straightLine297);
                DownLineTrackCollection[5].Add(straightLine296);
                DownLineTrackCollection[5].Add(straightLine295);
                DownLineTrackCollection[5].Add(straightLine294);
                DownLineTrackCollection[5].Add(straightLine119);
                DownLineTrackCollection[5].Add(straightLine109);
                DownLineTrackCollection.Add(new List<StraightLine>());//Segment[6]，从万源街站到荣京东街站
                DownLineTrackCollection[6].Add(straightLine109);
                DownLineTrackCollection[6].Add(straightLine112);
                DownLineTrackCollection[6].Add(straightLine288);
                DownLineTrackCollection[6].Add(straightLine287);
                DownLineTrackCollection[6].Add(straightLine286);
                DownLineTrackCollection[6].Add(straightLine285);
                DownLineTrackCollection[6].Add(straightLine284);
                DownLineTrackCollection[6].Add(straightLine110);
                DownLineTrackCollection[6].Add(straightLine282);
                DownLineTrackCollection.Add(new List<StraightLine>());//Segment[7]，从荣京东街站到荣昌东街站
                DownLineTrackCollection[7].Add(straightLine282);
                DownLineTrackCollection[7].Add(straightLine281);
                DownLineTrackCollection[7].Add(straightLine273);
                DownLineTrackCollection[7].Add(straightLine272);
                DownLineTrackCollection[7].Add(straightLine271);
                DownLineTrackCollection[7].Add(straightLine270);
                DownLineTrackCollection[7].Add(straightLine269);
                DownLineTrackCollection[7].Add(straightLine268);
                DownLineTrackCollection[7].Add(straightLine100);
                DownLineTrackCollection.Add(new List<StraightLine>());//Segment[8]，从荣昌东街站到同济南路站
                DownLineTrackCollection[8].Add(straightLine100);
                DownLineTrackCollection[8].Add(straightLine265);
                DownLineTrackCollection[8].Add(straightLine264);
                DownLineTrackCollection[8].Add(straightLine263);
                DownLineTrackCollection[8].Add(straightLine262);
                DownLineTrackCollection[8].Add(straightLine261);
                DownLineTrackCollection[8].Add(straightLine260);
                DownLineTrackCollection[8].Add(straightLine259);
                DownLineTrackCollection[8].Add(straightLine258);
                DownLineTrackCollection[8].Add(straightLine257);
                DownLineTrackCollection[8].Add(straightLine246);
                DownLineTrackCollection[8].Add(straightLine235);
                DownLineTrackCollection[8].Add(straightLine233);
                DownLineTrackCollection.Add(new List<StraightLine>());//Segment[9]，从同济南路站到经海路站
                DownLineTrackCollection[9].Add(straightLine233);
                DownLineTrackCollection[9].Add(straightLine231);
                DownLineTrackCollection[9].Add(straightLine228);
                DownLineTrackCollection[9].Add(straightLine215);
                DownLineTrackCollection[9].Add(straightLine214);
                DownLineTrackCollection[9].Add(straightLine213);
                DownLineTrackCollection[9].Add(straightLine212);
                DownLineTrackCollection[9].Add(straightLine211);
                DownLineTrackCollection[9].Add(straightLine210);
                DownLineTrackCollection[9].Add(straightLine209);
                DownLineTrackCollection[9].Add(straightLine208);
                DownLineTrackCollection[9].Add(straightLine206);
                DownLineTrackCollection[9].Add(straightLine122);
                DownLineTrackCollection.Add(new List<StraightLine>());//Segment[10]，从经海路站到次渠南站
                DownLineTrackCollection[10].Add(straightLine122);
                DownLineTrackCollection[10].Add(straightLine203);
                DownLineTrackCollection[10].Add(straightLine197);
                DownLineTrackCollection[10].Add(straightLine75);
                DownLineTrackCollection[10].Add(straightLine74);
                DownLineTrackCollection[10].Add(straightLine65);
                DownLineTrackCollection[10].Add(straightLine99);
                DownLineTrackCollection[10].Add(straightLine101);
                DownLineTrackCollection[10].Add(straightLine114);
                DownLineTrackCollection[10].Add(straightLine45);
                DownLineTrackCollection[10].Add(straightLine115);
                DownLineTrackCollection[10].Add(straightLine98);
                DownLineTrackCollection[10].Add(straightLine79);
                DownLineTrackCollection.Add(new List<StraightLine>());//Segment[11]，从次渠南站到次渠站
                DownLineTrackCollection[11].Add(straightLine79);
                DownLineTrackCollection[11].Add(straightLine76);
                DownLineTrackCollection[11].Add(straightLine61);
                DownLineTrackCollection[11].Add(straightLine63);
                DownLineTrackCollection[11].Add(straightLine73);
                DownLineTrackCollection[11].Add(straightLine62);
                DownLineTrackCollection[11].Add(straightLine58);
                DownLineTrackCollection[11].Add(straightLine44);
                DownLineTrackCollection.Add(new List<StraightLine>());//Segment[12]，从次渠站到亦庄火车站
                DownLineTrackCollection[12].Add(straightLine44);
                DownLineTrackCollection[12].Add(straightLine42);
                DownLineTrackCollection[12].Add(straightLine41);
                DownLineTrackCollection[12].Add(straightLine40);
                DownLineTrackCollection[12].Add(straightLine39);
                DownLineTrackCollection[12].Add(straightLine38);
                DownLineTrackCollection[12].Add(straightLine28);
                DownLineTrackCollection[12].Add(straightLine26);
                DownLineTrackCollection[12].Add(straightLine24);
                DownLineTrackCollection.Add(new List<StraightLine>());//Segment[13]，从亦庄火车站到台州车辆段
                DownLineTrackCollection[13].Add(straightLine24);
                DownLineTrackCollection[13].Add(straightLine22);
                DownLineTrackCollection[13].Add(straightLine20);
                DownLineTrackCollection[13].Add(straightLine14);
                DownLineTrackCollection[13].Add(straightLine12);
                DownLineTrackCollection[13].Add(straightLine10);
                DownLineTrackCollection[13].Add(straightLine8);
                DownLineTrackCollection[13].Add(straightLine6);
                DownLineTrackCollection[13].Add(straightLine4);
                DownLineTrackCollection[13].Add(straightLine2);

                UpLineTrackCollection.Add(new List<StraightLine>());//Segment[0]，从台州车辆段到亦庄火车站
                UpLineTrackCollection[0].Add(straightLine1);
                UpLineTrackCollection[0].Add(straightLine3);
                UpLineTrackCollection[0].Add(straightLine5);
                UpLineTrackCollection[0].Add(straightLine7);
                UpLineTrackCollection[0].Add(straightLine9);
                UpLineTrackCollection[0].Add(straightLine11);
                UpLineTrackCollection[0].Add(straightLine13);
                UpLineTrackCollection[0].Add(straightLine19); 
                UpLineTrackCollection[0].Add(straightLine21);
                UpLineTrackCollection[0].Add(straightLine23);
                UpLineTrackCollection.Add(new List<StraightLine>());//Segment[1]，从亦庄火车站到次渠站
                UpLineTrackCollection[1].Add(straightLine23);
                UpLineTrackCollection[1].Add(straightLine25);
                UpLineTrackCollection[1].Add(straightLine27);
                UpLineTrackCollection[1].Add(straightLine31);
                UpLineTrackCollection[1].Add(straightLine32);
                UpLineTrackCollection[1].Add(straightLine33);
                UpLineTrackCollection[1].Add(straightLine34);
                UpLineTrackCollection[1].Add(straightLine35);
                UpLineTrackCollection[1].Add(straightLine55);
                UpLineTrackCollection.Add(new List<StraightLine>());  //Segment[2]，从次渠站到次渠南站
                UpLineTrackCollection[2].Add(straightLine55);
                UpLineTrackCollection[2].Add(straightLine57);
                UpLineTrackCollection[2].Add(straightLine60);
                UpLineTrackCollection[2].Add(straightLine64);
                UpLineTrackCollection[2].Add(straightLine194);
                UpLineTrackCollection[2].Add(straightLine195);
                UpLineTrackCollection[2].Add(straightLine196);
                UpLineTrackCollection[2].Add(straightLine78);
                UpLineTrackCollection.Add(new List<StraightLine>());  //Segment[3]，从次渠南站到经海路站
                UpLineTrackCollection[3].Add(straightLine78);
                UpLineTrackCollection[3].Add(straightLine85);
                UpLineTrackCollection[3].Add(straightLine54);
                UpLineTrackCollection[3].Add(straightLine106);
                UpLineTrackCollection[3].Add(straightLine116);
                UpLineTrackCollection[3].Add(straightLine198);
                UpLineTrackCollection[3].Add(straightLine199);
                UpLineTrackCollection[3].Add(straightLine200);
                UpLineTrackCollection[3].Add(straightLine201);
                UpLineTrackCollection[3].Add(straightLine202);
                UpLineTrackCollection[3].Add(straightLine204);
                UpLineTrackCollection[3].Add(straightLine205);
                UpLineTrackCollection.Add(new List<StraightLine>());//Segment[4]，从经海路站到同济南路站
                UpLineTrackCollection[4].Add(straightLine205);
                UpLineTrackCollection[4].Add(straightLine207);
                UpLineTrackCollection[4].Add(straightLine216);
                UpLineTrackCollection[4].Add(straightLine217);
                UpLineTrackCollection[4].Add(straightLine218);
                UpLineTrackCollection[4].Add(straightLine219);
                UpLineTrackCollection[4].Add(straightLine220);
                UpLineTrackCollection[4].Add(straightLine221);
                UpLineTrackCollection[4].Add(straightLine222);
                UpLineTrackCollection[4].Add(straightLine223);
                UpLineTrackCollection[4].Add(straightLine224);
                UpLineTrackCollection[4].Add(straightLine226);
                UpLineTrackCollection[4].Add(straightLine222);
                UpLineTrackCollection[4].Add(straightLine223);
                UpLineTrackCollection[4].Add(straightLine224);
                UpLineTrackCollection[4].Add(straightLine226);
                UpLineTrackCollection[4].Add(straightLine222);
                UpLineTrackCollection[4].Add(straightLine223);
                UpLineTrackCollection[4].Add(straightLine224);
                UpLineTrackCollection[4].Add(straightLine226);
                UpLineTrackCollection[4].Add(straightLine227);
                UpLineTrackCollection[4].Add(straightLine232);
                UpLineTrackCollection[4].Add(straightLine234);
                UpLineTrackCollection.Add(new List<StraightLine>());//Segment[5]，从同济南路站到荣昌东街站
                UpLineTrackCollection[5].Add(straightLine234);
                UpLineTrackCollection[5].Add(straightLine241);
                UpLineTrackCollection[5].Add(straightLine242);
                UpLineTrackCollection[5].Add(straightLine247);
                UpLineTrackCollection[5].Add(straightLine248);
                UpLineTrackCollection[5].Add(straightLine249);
                UpLineTrackCollection[5].Add(straightLine250);
                UpLineTrackCollection[5].Add(straightLine251);
                UpLineTrackCollection[5].Add(straightLine252);
                UpLineTrackCollection[5].Add(straightLine253);
                UpLineTrackCollection[5].Add(straightLine254);
                UpLineTrackCollection[5].Add(straightLine266);
                UpLineTrackCollection[5].Add(straightLine267);
                UpLineTrackCollection[5].Add(straightLine255);
                UpLineTrackCollection[5].Add(straightLine256);
                UpLineTrackCollection.Add(new List<StraightLine>());//Segment[6]，从荣昌东街站到荣京东街站
                UpLineTrackCollection[6].Add(straightLine256);
                UpLineTrackCollection[6].Add(straightLine274);
                UpLineTrackCollection[6].Add(straightLine275);
                UpLineTrackCollection[6].Add(straightLine276);
                UpLineTrackCollection[6].Add(straightLine277);
                UpLineTrackCollection[6].Add(straightLine278);
                UpLineTrackCollection[6].Add(straightLine279);
                UpLineTrackCollection[6].Add(straightLine280);
                UpLineTrackCollection[6].Add(straightLine283);
                UpLineTrackCollection.Add(new List<StraightLine>());//Segment[7]，从荣京东街站到万源街站
                UpLineTrackCollection[7].Add(straightLine283);
                UpLineTrackCollection[7].Add(straightLine49);
                UpLineTrackCollection[7].Add(straightLine289);
                UpLineTrackCollection[7].Add(straightLine290);
                UpLineTrackCollection[7].Add(straightLine291);
                UpLineTrackCollection[7].Add(straightLine292);
                UpLineTrackCollection[7].Add(straightLine293);
                UpLineTrackCollection[7].Add(straightLine129);
                UpLineTrackCollection.Add(new List<StraightLine>());//Segment[8]，从万源街站到亦庄文化园站
                UpLineTrackCollection[8].Add(straightLine129);
                UpLineTrackCollection[8].Add(straightLine121);
                UpLineTrackCollection[8].Add(straightLine300);
                UpLineTrackCollection[8].Add(straightLine301);
                UpLineTrackCollection[8].Add(straightLine302);
                UpLineTrackCollection[8].Add(straightLine303);
                UpLineTrackCollection[8].Add(straightLine304);
                UpLineTrackCollection[8].Add(straightLine305);
                UpLineTrackCollection[8].Add(straightLine306);
                UpLineTrackCollection[8].Add(straightLine309);
                UpLineTrackCollection.Add(new List<StraightLine>());//Segment[9]，从亦庄文化园站到亦庄桥站
                UpLineTrackCollection[9].Add(straightLine309);
                UpLineTrackCollection[9].Add(straightLine128);
                UpLineTrackCollection[9].Add(straightLine141);
                UpLineTrackCollection[9].Add(straightLine311);
                UpLineTrackCollection[9].Add(straightLine312);
                UpLineTrackCollection[9].Add(straightLine138);
                UpLineTrackCollection[9].Add(straightLine313);
                UpLineTrackCollection.Add(new List<StraightLine>());//Segment[10]，从亦庄桥站到旧宫站
                UpLineTrackCollection[10].Add(straightLine313);
                UpLineTrackCollection[10].Add(straightLine137);
                UpLineTrackCollection[10].Add(straightLine124);
                UpLineTrackCollection[10].Add(straightLine315);
                UpLineTrackCollection[10].Add(straightLine316);
                UpLineTrackCollection[10].Add(straightLine125);
                UpLineTrackCollection[10].Add(straightLine123);
                UpLineTrackCollection[10].Add(straightLine317);
                UpLineTrackCollection[10].Add(straightLine318);
                UpLineTrackCollection[10].Add(straightLine319);
                UpLineTrackCollection[10].Add(straightLine127);
                UpLineTrackCollection[10].Add(straightLine135);
                UpLineTrackCollection[10].Add(straightLine334);
                UpLineTrackCollection.Add(new List<StraightLine>());//Segment[11]，从旧宫站到小红门站
                UpLineTrackCollection[11].Add(straightLine334);
                UpLineTrackCollection[11].Add(straightLine335);
                UpLineTrackCollection[11].Add(straightLine37);
                UpLineTrackCollection[11].Add(straightLine336);
                UpLineTrackCollection[11].Add(straightLine337);
                UpLineTrackCollection[11].Add(straightLine338); 
                UpLineTrackCollection[11].Add(straightLine343);
                UpLineTrackCollection[11].Add(straightLine344);
                UpLineTrackCollection[11].Add(straightLine345);
                UpLineTrackCollection[11].Add(straightLine346);
                UpLineTrackCollection[11].Add(straightLine347);
                UpLineTrackCollection[11].Add(straightLine348);
                UpLineTrackCollection[11].Add(straightLine120);
                UpLineTrackCollection[11].Add(straightLine47);
                UpLineTrackCollection.Add(new List<StraightLine>());//Segment[12]，从小红门站到肖村站
                UpLineTrackCollection[12].Add(straightLine47);
                UpLineTrackCollection[12].Add(straightLine50);
                UpLineTrackCollection[12].Add(straightLine70);
                UpLineTrackCollection[12].Add(straightLine71);
                UpLineTrackCollection[12].Add(straightLine72);
                UpLineTrackCollection[12].Add(straightLine77);
                UpLineTrackCollection[12].Add(straightLine80);
                UpLineTrackCollection[12].Add(straightLine81);
                UpLineTrackCollection[12].Add(straightLine83);
                UpLineTrackCollection.Add(new List<StraightLine>());//Segment[13]，从肖村站到宋家庄站
                UpLineTrackCollection[13].Add(straightLine83);
                UpLineTrackCollection[13].Add(straightLine86);
                UpLineTrackCollection[13].Add(straightLine89);
                UpLineTrackCollection[13].Add(straightLine90);
                UpLineTrackCollection[13].Add(straightLine91);
                UpLineTrackCollection[13].Add(straightLine92);
                UpLineTrackCollection[13].Add(straightLine93);
                UpLineTrackCollection[13].Add(straightLine108);
                UpLineTrackCollection[13].Add(straightLine111);
                UpLineTrackCollection[13].Add(straightLine117);
                UpLineTrackCollection[13].Add(straightLine148);
                UpLineTrackCollection[13].Add(straightLine149);
                UpLineTrackCollection[13].Add(straightLine152);
                UpLineTrackCollection[13].Add(straightLine155);
                UpLineTrackCollection[13].Add(straightLine170);
                UpLineTrackCollection[13].Add(straightLine171);
                UpLineTrackCollection[13].Add(straightLine172);
            }
            foreach (List<StraightLine> Sline in UpLineTrackCollection)
            {
                if (Sline.Count == 0) { continue; }
                double length = Math.Abs(Sline[Sline.Count - 1].Location.X + Sline[Sline.Count - 1].Size.Width - Sline[0].Location.X);
                //List<double> size = new List<double>(Sline.Count+1);
                double startpoint = 0;
                for (int i = 0; i < Sline.Count; i++)
                {
                    //if (i == Sline.Count - 1)
                    //{ Sline[i].Fstart = startpoint; Sline[i].Fend = startpoint + Sline[i].Size.Width / length; }
                    Sline[i].Sstart = startpoint;
                    Sline[i].Send = startpoint + Sline[i].Size.Width / length;
                    startpoint = Sline[i].Send;
                }
            }
            foreach (List<StraightLine> Sline in DownLineTrackCollection)
            {
                if (Sline.Count == 0) { continue; }
                double length = Math.Abs(Sline[0].Location.X - Sline[Sline.Count - 1].Location.X  + Sline[0].Size.Width);
                //List<double> size = new List<double>(Sline.Count+1);
                double startpoint = 0;
                for (int i = 0; i < Sline.Count; i++)
                {
                    //if (i == Sline.Count - 1)
                    //{ Sline[i].Fstart = startpoint; Sline[i].Fend = startpoint + Sline[i].Size.Width / length; }
                    Sline[i].Sstart = startpoint;
                    Sline[i].Send = startpoint + Sline[i].Size.Width / length;
                    startpoint = Sline[i].Send;
                }
            }
        }
        private void UpdateUI()
        {
            //if (timeindex < 4)
            //{ LineTrackCollection[0][timeindex].ForeColor = Color.Red; }
            foreach (List<StraightLine> segmentocc in DownLineTrackCollection.Union(UpLineTrackCollection))
            {
                foreach (StraightLine S in segmentocc) { S.Occupied = false; S.TextSegment = null; }
            }
            foreach (MultiSignal xx in SignalSet)
            {
                if (xx.SignalNum == 2)
                {
                    if (xx.RightColor == 2)
                    {
                        xx.LeftColor = 2;
                        xx.RightColor = 1;
                    //xx.MiddleColor = 0;
                    }

                }
            }
            foreach (train k in Form1.Trains)
            {
                // Locate every trains on the network
                // 0 --> Up direction
                // 1 --> Down direction
                if (k.TrainDirection == 0)
                {
                    if ((k.Segment >= 1)&&(k.Segment+1<UpLineTrackCollection.Count))
                    {
                        if (k.Position <= UpLineTrackCollection[k.Segment + 1][0].Send)
                        {
                            UpLineTrackCollection[k.Segment + 1][0].Occupied = true;
                            UpLineTrackCollection[k.Segment + 1][0].TextSegment = k.Trainnidex.ToString();
                        }
                        else if (k.Position > UpLineTrackCollection[k.Segment + 1][UpLineTrackCollection[k.Segment + 1].Count() - 2].Send)
                        {
                            UpLineTrackCollection[k.Segment + 1].Last().Occupied = true;
                            UpLineTrackCollection[k.Segment + 1].Last().TextSegment = k.Trainnidex.ToString();
                        }
                        else
                        {
                            for (int i = 1; i < UpLineTrackCollection[k.Segment + 1].Count - 1; i++)
                            {
                                if (k.Position >= UpLineTrackCollection[k.Segment + 1][i].Sstart && k.Position < UpLineTrackCollection[k.Segment + 1][i].Send)
                                {
                                    UpLineTrackCollection[k.Segment + 1][i].Occupied = true;
                                    //k.Trainnidex
                                    UpLineTrackCollection[k.Segment + 1][i].TextSegment = k.Trainnidex.ToString();
                                }
                            }
                        }
                    }
                }
                else //(k.TrainDirection == 1)
                {
                    if (k.Segment >= 1)
                    {
                        textBox1.Text = k.Segment.ToString();
                        if (k.Position <= DownLineTrackCollection[k.Segment - 1][0].Send)
                        {
                            DownLineTrackCollection[k.Segment - 1][0].Occupied = true;
                        }
                        else if (k.Position > DownLineTrackCollection[k.Segment - 1][DownLineTrackCollection[k.Segment - 1].Count() - 2].Send)
                        {
                            DownLineTrackCollection[k.Segment - 1].Last().Occupied = true;
                        }
                        else
                        {
                            for (int i = 1; i < DownLineTrackCollection[k.Segment - 1].Count - 1; i++)
                            {
                                if (k.Position >= DownLineTrackCollection[k.Segment - 1][i].Sstart && k.Position < DownLineTrackCollection[k.Segment - 1][i].Send)
                                {
                                    DownLineTrackCollection[k.Segment - 1][i].Occupied = true;
                                }
                            }
                        }
                    }
                }
            }
            // Change the colors
            foreach (List < StraightLine > segmentocc in DownLineTrackCollection.Union(UpLineTrackCollection))
            {
                foreach (StraightLine S in segmentocc)
                {
                    if (S.Occupied == true)
                    {
                        S.ForeColor = Color.Red;
                        if (S.IfSignal > 0)
                        {
                            string nn = "multiSignal" + S.IfSignal.ToString();
                            //Find
                            MultiSignal sig = this.Controls.Find(nn, true).FirstOrDefault() as MultiSignal;
                            sig.LeftColor =0;
                            sig.RightColor =2;
                        }
                        //Font ff = new Font("Times New Roman", 20);
                        ////Brush bf = new Brush(Color.Black, 20);
                        //string txt = "T "+S.TextSegment;
                        //TrainLabel1.Text = txt;
                        //TrainLabel1.ForeColor = Color.White;
                    }
                    else
                    {
                        S.ForeColor = Color.SteelBlue;
                    }
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateUI();
            timeindex += 1;
            //multiSignal2.LeftColor = 3;
            this.Invalidate();
           // this.Refresh();
        }

        private void NewDisplay_Load(object sender, EventArgs e)
        {

        }

        private void label58_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void straightLine254_Click(object sender, EventArgs e)
        {

        }
    }
}
