using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AJBauer;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication7
{

    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
            chart1.BringToFront();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            chart1.ChartAreas.Clear();
            ChartArea chartarea1 = new ChartArea("c1");
            chart1.ChartAreas.Add(chartarea1);
            
            chart1.ChartAreas[0].AxisX.Title = "列车运行时间（秒）";
            chart1.ChartAreas[0].AxisY.Title = "列车速度（m/s）";
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 15;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 200;
            chart1.Series.Clear();
            Series series1 = new Series("Train s1");
            series1.ChartArea = "c1";
            chart1.Series.Add(series1);
            chart1.Titles.Add("S01");
           
            chart1.Titles[0].Text = "列车运行速度曲线";
            chart1.Series[0].YValueMembers = "Speed (m/s)";
            chart1.Series[0].XValueMember = "Time (s)";
            chart1.Titles[0].ForeColor = Color.RoyalBlue;
            chart1.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            chart1.Series[0].Color = Color.Red;
            chart1.Series[0].ChartType = SeriesChartType.Line;
            //chart1.Series[0]
        }
        private float fCapacityUtilizationPct = 0.75F;
        private Random r = new Random();
        private float fInputVariancePct = 0.0F;
        private float fOutputVariancePct = 0.0F;
        private float fLoadVariancePct = 0.0F;

        private bool bInputOnline = true;
        private bool bOutputOnline = true;
        private bool bCapacityOnline = true;
        private bool bLoadOnline = true;

        private IList<float> inputValues = new List<float>();
        private IList<float> outputValues = new List<float>();
        private IList<float> capacityValues = new List<float>();
        private IList<float> loadValues = new List<float>();

        private static int maxValues = 10000;
        private Queue<double> dataQueue = new Queue<double>(10000);
        private int curValue = 0;
        private int num = 1;//每次删除增加几个点

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //if(Fangzhen.ati)
            if (Fangzhen.time_set.judge==true)
            {
                UpdateQueueValue();
            }
            
            this.chart1.Series[0].Points.Clear();
            for (int i = 0; i < dataQueue.Count; i++)
            {
                this.chart1.Series[0].Points.AddXY((i + 1), dataQueue.ElementAt(i));
            }

            //fCapacityUtilizationPct = bound(bumpPct(fCapacityUtilizationPct+20), 0F, 1.0F);
            fCapacityUtilizationPct = bound(bumpPct(Form1.Sections[0].Velocity[(int)Form1.MySet.CurrentTime / 10]/70), 0F, 1.0F);
            fInputVariancePct = bound(bumpPct(fInputVariancePct), -0.5F, 0.5F);
            fOutputVariancePct = bound(bumpPct(fOutputVariancePct), -0.5F, 0.5F);
            fLoadVariancePct = bound(bumpPct(fLoadVariancePct), -0.5F, 0.5F);

            bInputOnline = stillOnline(labelInput, bInputOnline);
            bOutputOnline = stillOnline(labelOutput, bOutputOnline);
            bCapacityOnline = stillOnline(labelCapacity, bCapacityOnline);
            bLoadOnline = stillOnline(labelLoad, bLoadOnline);

            updateGaugeWidget(bCapacityOnline, gaugeCapacityUtilization, 
                labelCapacityUtilization, fCapacityUtilizationPct);
            updateGaugeWidget(bInputOnline, gaugeInputVariance, 
                labelInputVariance, fInputVariancePct);
            updateGaugeWidget(bOutputOnline, gaugeOutputVariance, 
                labelOutputVariance, fOutputVariancePct);
            updateGaugeWidget(bLoadOnline, gaugeLoadVariance, 
                labelLoadVariance, fLoadVariancePct);

            accumulateValue(inputValues, fInputVariancePct, bInputOnline, labelInputVarianceGraph);
            accumulateValue(outputValues, fOutputVariancePct, bOutputOnline, labelOutputVarianceGraph);
            accumulateValue(capacityValues,fCapacityUtilizationPct,bCapacityOnline,labelCapacityUtilitzationGraph);
            accumulateValue(loadValues, fLoadVariancePct, bLoadOnline, labelLoadVarianceGraph);
            System.Windows.Forms.Application.DoEvents();

            timer1.Enabled = true;
        }
        private void UpdateQueueValue()
        {

            if (dataQueue.Count > 100)
            {
                //先出列
                for (int i = 0; i < num; i++)
                {
                    //dataQueue.Dequeue();
                }
            }
            {
                //Form1.MySet.CurrentTime / 10;
                Random r = new Random();
                for (int i = 0; i < num; i++)
                {
                    //对curValue只取[0,360]之间的值
                    //curValue = curValue % 360;
                    //对得到的正玄值，放大50倍，并上移50
                    dataQueue.Enqueue(Form1.Sections[0].Velocity[(int)Form1.MySet.CurrentTime / 10]);
                    //curValue = curValue + 10;
                }
            }
        }

        private void updateGaugeWidget(bool isOnline, AGauge gauge, Label lbl, float val)
        {
            if (isOnline)
            {
                //capacity utilization
                gauge.Value = (val * 100.0F);
                lbl.Text = string.Format("{0:0%}", val);
                //get color from gauge range
                lbl.ForeColor = gauge.ValueColor(val * 100f);
                gauge.NeedleColor1 = AGauge.NeedleColorEnum.Blue;
            }
            else
            {
                gauge.NeedleColor1 = AGauge.NeedleColorEnum.Gray;
            }
        }

        private void accumulateValue(IList<float> values,float pct,bool bOnline,Label surface)
        {
            //limit to last N values
 	        while (values.Count >= maxValues)
            {
                values.RemoveAt(0);
            }
            //add new value, or placeholder if offline
            if (bOnline)
            {
                values.Add(pct);
            }
            else
            {
                values.Add(float.NaN);
            }
            surface.Invalidate();   //make label redraw itself
        }

        private float bound(float val, float low, float high)
        {
            if (val > high) val = high;
            if (val < low) val = low;
            return val;
        }

        /// <summary>
        /// generate a random double +/- 0.5 and divide it by 10
        /// and add it to the current value to 'bump' it up or
        /// down slightly
        /// </summary>
        /// <param name="fval">current value</param>
        /// <returns>new 'bumped' value</returns>
        private float bumpPct(float fval)
        {
            fval += (float)(r.NextDouble() - 0.5F) / 100.0F;
            return fval;
        }

        private bool stillOnline(Label lbl, bool lastState)
        {
            //simulated 95% uptime
            bool nextState = (r.NextDouble() <= 0.95) ? true : false;
            if (lastState && !nextState)
            {
                //now offline
                lbl.ForeColor = System.Drawing.Color.Red;
                lbl.Text = "OFFLINE";
            }
            else if (!lastState && nextState)
            {
                //now online
                lbl.ForeColor = System.Drawing.Color.Green;
                lbl.Text = "ONLINE";
            }
            return nextState;
        }

        private IDictionary<string,Form> openForms = new Dictionary<string,Form>();

        private void panel2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string key = ((Control)sender).Name;
            if (openForms.ContainsKey(key))
            {
                Form f = openForms[key];
                f.BringToFront();
                return;
            }
            string tag = ((Control)sender).Tag as string;
            if (tag == null)
            {
                tag = string.Empty;
            }
            Form frm = new DetailGridForm();
            frm.Text = tag + " Details";
            frm.Tag = key;
            openForms.Add(key, frm);
            frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
            frm.Show();
        }

        void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form frm = sender as Form;
            if (frm != null)
            {
                openForms.Remove((string)frm.Tag);
            }
        }

        private void ViamDashboardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form frm in openForms.Values)
            {
                frm.FormClosed -= new FormClosedEventHandler(frm_FormClosed);
                frm.Close();
            }
            openForms.Clear();
            openForms = null;
        }

        private void labelCapacityUtilitzationGraph_Paint(object sender, PaintEventArgs e)
        {
            //capacity is 0-100 percent
            drawGraph(e,capacityValues,0,100,gaugeCapacityUtilization);
        }

        private void labelInputVarianceGraph_Paint(object sender, PaintEventArgs e)
        {
            //input variance is -50 to 50 percent
            drawGraph(e,inputValues,-50,50,gaugeInputVariance);
        }

        private void labelOutputVarianceGraph_Paint(object sender, PaintEventArgs e)
        {
            //output variance is -50 to 50 percent
            drawGraph(e,outputValues,-50,50,gaugeOutputVariance);
        }

        private void labelLoadVarianceGraph_Paint(object sender, PaintEventArgs e)
        {
            //load variance is -50 to 50 percent
            drawGraph(e,loadValues,-50,50,gaugeLoadVariance);
        }

        /// <summary>
        /// draw a bar-graph for a set of values clipped to lowBound and highBound;
        /// missing values are draw as a light gray bar for the full height available
        /// </summary>
        /// <param name="e">PaintEventArgs for drawing</param>
        /// <param name="values">value set</param>
        /// <param name="lowBound">low-bound of value range</param>
        /// <param name="highBound">high-bound of value range</param>
        private void drawGraph(PaintEventArgs e, IList<float> values, 
            int lowBound, int highBound, AGauge gauge)
        {
            RectangleF bounds = e.Graphics.VisibleClipBounds;
            e.Graphics.FillRegion(Brushes.White, new Region(bounds));
            if (values.Count <= 0)
            {
                //nothing to draw yet
                return;
            }

            System.Diagnostics.Debug.Assert(highBound > lowBound);

            //if lowBound is less than zero, we move the zero-axis to the middle
            //and draw bars above or below it. Otherwise all bars are drawn 'up'.
            bool bSplitBar = lowBound < 0;
            //note drawing area origin is upper-left corner so must invert the height to draw bars
            float zeroY = bSplitBar ? bounds.Height / 2 : bounds.Height;
            float range = highBound - lowBound;
            //scale height by the value range
            float hscale = bounds.Height / range;
            //scale width by the number of values
            float wscale = bounds.Width / values.Count;
            int i = 0;
            foreach (float val in values)
            {
                if (float.IsNaN(val))
                {
                    //draw gray bar to show missing value
                    e.Graphics.FillRectangle(Brushes.LightGray, i * wscale, 0, wscale-1f, bounds.Height);
                }
                else
                {
                    //get brush color from associate gauge range
                    SolidBrush brsh = new SolidBrush(gauge.ValueColor(val * 100f));

                    if (bSplitBar)
                    {
                        // split-bar display
                        // +------- <--upper limit
                        // |
                        // | X <-------up-bar
                        // | X   
                        // --X-X-X- <--zero axis @ zeroY
                        // |     X
                        // |     X <---down-bar
                        // |      
                        // +------- <--lower limit
                        if (val > 0)
                        {
                            float barHeight = (val != 0) ? (val * range * hscale) : 1f;
                            //up-bar
                            e.Graphics.FillRectangle(brsh,
                                i * wscale, zeroY - (val * range * hscale),
                                wscale-1f, barHeight);
                        }
                        else if (val == 0)
                        {
                            //special case for zero, extend 1px above and below zero-axis
                            e.Graphics.FillRectangle(brsh,
                                i * wscale, zeroY - 1f,
                                wscale - 1f, 3f);
                        }
                        else
                        {
                            //down-bar
                            e.Graphics.FillRectangle(brsh,
                                i * wscale, zeroY,
                                wscale - 1f, val * -range * hscale);
                        }
                    }
                    else
                    {
                        // vertical-bar display
                        // +------- <--upper limit
                        // |
                        // | X <-------up-bar
                        // | X   
                        // --X-X-X- <--zero axis
                        //up-bar
                        float barHeight = (val != 0) ? (val * range * hscale) : 1f;
                        e.Graphics.FillRectangle(brsh,
                            i * wscale, zeroY - (val * range * hscale),
                            wscale-1f, barHeight);
                    }
                    brsh.Dispose();
                }
                ++i;
            }
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            //in a real app we would make a new control type instead of using a label
            //and set its style to optimized double buffer
            this.DoubleBuffered = true;
        }

        private void labelLoadVarianceGraph_Click(object sender, EventArgs e)
        {

        }
        // Draw the train profile
        
        //private void panel6_Paint(object sender, PaintEventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    Pen LinePen = new Pen(Color.Red, 3);
        //    //g.DrawLine(LinePen, (float)2, (float)panel6.Height, (float)20, Form1.MySet.CurrentTime);Form1.MySet.CurrentTime+1
        //    Pen curvepen = new Pen(Color.Red, 3);
        //    int time = Form1.MySet.CurrentTime/10;
        //    int SegmentRunning = 1000;
        //    PointF[] CurvePointF = new PointF[time];
        //    //PointF[] CurvePointF = new PointF[100];
        //    float keys = 0;
        //    float values = 0;
        //    for (int i = 0; i < time; i++)
        //    {
        //        keys = i;
        //        values = 1;
                
        //        CurvePointF[i] = new PointF(keys, axisTransfer(panel6.Height, Form1.Sections[0].Velocity[i]) );
        //    }
        //    // panel6.Height-keys
        //    g.DrawCurve(curvepen, CurvePointF);
        //}
        private float axisTransfer(float panelHeight, float trainVelocity)
        {
            float afterTransfer = panelHeight - panelHeight * (trainVelocity/ Form1.Sections[0].VelocityLimit);
            return afterTransfer;
        }
            private void button1_Click(object sender, EventArgs e)
        {
            //textBox1.Text = "Test: " + Form1.Trains.Count.ToString();
            textBox1.Text = "test " + Form1.MySet.CurrentTime / 10 + "Test2 "+ Form1.Sections[0].RubTime;
        }
    }
}