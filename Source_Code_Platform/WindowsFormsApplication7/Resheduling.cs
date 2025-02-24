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
    [Serializable]
    public partial class Resheduling : Form
    {
        public List<string> TimeHour = new List<string>();
        public List<string> TimeMinute = new List<string>();
        public List<string> TimeSecond = new List<string>();
        public string[] TrainDirection = { "上行", "下行" };
        public static List<int> VehicleSegment = new List<int>();
        public delegate void UserRequest(object sender, EventArgs e);
        public event UserRequest TrainReschedulingInilize;
        public Resheduling()
        {
            Parameters Myset = new Parameters();
            for (int i = 0; i < 23; i++)
            {
                TimeHour.Add(i.ToString());
            }
            for (int i = 0; i < 59; i++)
            {
                TimeMinute.Add(i.ToString());
                TimeSecond.Add(i.ToString());
            }
            InitializeComponent();
            dataGridView2.Rows.Add();
            Column1.DataSource = Myset.StationNameYizhuang;
            Column2.DataSource = TrainDirection;
            Column5.DataSource = TimeHour;
            Column6.DataSource = TimeMinute;
            Column7.DataSource = TimeSecond;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.MySet.CurrentTime = Form1.HourToSecond(Convert.ToInt32(dataGridView2.Rows[0].Cells[0].Value),
                Convert.ToInt32(dataGridView2.Rows[0].Cells[1].Value), Convert.ToInt32(dataGridView2.Rows[0].Cells[2].Value));
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                //VehicleSegment.Add();
                for (int j = 0; j < Form1.MySet.StationNameYizhuang.Count(); j++)
                {
                    if (Form1.MySet.StationNameYizhuang[j] == dataGridView1.Rows[i].Cells[0].Value.ToString())
                    {
                        if (dataGridView1.Rows[i].Cells[1].Value.ToString() == "下行")
                        {
                            j=Form1.MySet.StationNameYizhuang.Count() * 2 - 1 - j;
                        }
                        VehicleSegment.Add(j);
                        break;
                    }
                }              
            }
            TrainReschedulingInilize(this, e);
            if (VehicleSegment.Count == 0)
            {
                MessageBox.Show("没有添加有效列车！");
            }
            else
            {
                TrainReschedulingInilize(this, e);
                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
            //dataGridView1.Rows[dataGridView1.RowCount-1].Cells[0].Value
        }
    }
}
