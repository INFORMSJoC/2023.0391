using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace WindowsFormsApplication7
{
    public partial class Fangzhen : Form
    {
        System.Timers.Timer aTimer;
        public class JxqSet
        {
            public int TimeStart = 0;  //Unit ms
            public int Interval = 20;
            public bool judge = false;
        }
        public static JxqSet time_set = new JxqSet();
     
        public Fangzhen()
        {
            InitializeComponent();
            //DialogResult CloseFangzhen = fangzhen.ShowDialog();
        }
        
        public delegate void UserRequest(object sender, EventArgs e);
        public event UserRequest OnUserRequest;
        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            time_set.TimeStart += (int)aTimer.Interval;
            sevenSegmentArray2.Value = time_set.TimeStart.ToString();
            if (time_set.TimeStart%20==0)
            {
                OnUserRequest(this, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed  += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            time_set.judge = aTimer.Enabled;
           // OnUserRequest(this, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            aTimer.Enabled = false;
            time_set.judge = aTimer.Enabled;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            aTimer.Enabled = false;
            time_set.judge = aTimer.Enabled;
            time_set.TimeStart = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (aTimer.Interval > 20)
            {
                aTimer.Interval = aTimer.Interval - 20;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            aTimer.Interval = aTimer.Interval + 20;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            DialogResult result = MessageBox.Show("是否结束仿真", "警告",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            e.Cancel = result != DialogResult.Yes;
            base.OnClosing(e);
            aTimer.Enabled = false;
            time_set.judge = aTimer.Enabled;
        }
    }
}
