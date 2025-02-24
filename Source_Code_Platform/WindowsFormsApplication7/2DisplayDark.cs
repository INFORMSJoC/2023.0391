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
    public partial class _2DisplayDark : Form
    {
        public _2DisplayDark()
        {
            InitializeComponent();
            //pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED 
                return cp;
            }
        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void _2DisplayDark_Load(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void straightLine32_Click(object sender, EventArgs e)
        {

        }

        private void straightLine39_Click(object sender, EventArgs e)
        {

        }

        private void straightLine44_Click(object sender, EventArgs e)
        {

        }

        private void straightLine64_Click(object sender, EventArgs e)
        {

        }

        private void straightLine76_Click(object sender, EventArgs e)
        {

        }

        private void straightLine129_Click(object sender, EventArgs e)
        {

        }

        private void multiSignal20_Load(object sender, EventArgs e)
        {

        }

        private void straightLine111_Click(object sender, EventArgs e)
        {

        }
    }
}
