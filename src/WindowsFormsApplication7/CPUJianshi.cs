using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;


namespace WindowsFormsApplication7
{
    public partial class CPUJianshi : Form
    {
        private Thread cpuThread;
        private double[] cpuArray = new double[30];

        public CPUJianshi()
        {
            InitializeComponent();
        }
        private void getPerformanceCounters()
        {
            var cpuPerfCounter = new PerformanceCounter("Processor information", "% Processor Time", "_Total");
            while (true)
            {
                cpuArray[cpuArray.Length - 1] = Math.Round(cpuPerfCounter.NextValue(),0);
                Array.Copy(cpuArray, 1, cpuArray, 0, cpuArray.Length - 1);
                if (cpuchart.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate { UpdateCpuChart(); });
                }
                else
                {
                    //...
                }
            }
        }
        private void UpdateCpuChart()
        {
            cpuchart.Series["Series1"].Points.Clear();
            for (int i = 0; i < cpuArray.Length - 1; ++i)
            {
                cpuchart.Series["Series1"].Points.AddY(cpuArray[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cpuThread = new Thread(new ThreadStart(this.getPerformanceCounters));
            cpuThread.IsBackground = true;
            cpuThread.Start();
        }

        private void cpuchart_Click(object sender, EventArgs e)
        {

        }
    }
}
