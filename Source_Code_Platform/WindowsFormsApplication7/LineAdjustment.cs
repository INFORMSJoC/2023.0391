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
    public partial class LineAdjustment : Form
    {
        public LineAdjustment()
        {
            InitializeComponent();
        }
        public delegate void UserRequest(object sender, EventArgs e);
        public event UserRequest OnUserRequestLineAdjustment;

        private void button1_Click(object sender, EventArgs e)
        {
            // Form1 ff = new Form1();
            // ff.TrainRegulation();
            Form1.RightMove = textBox1.Text;
            OnUserRequestLineAdjustment(this, e);  
            //RightMove textBox1.Text
        }
    }
}
