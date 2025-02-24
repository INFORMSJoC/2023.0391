using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication7
{
    public partial class DetailGridForm : Form
    {
        public DetailGridForm()
        {
            InitializeComponent();
        }

        private void DetailGridForm_Load(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Add(new object[] { "West Westchester", "400 MW", "2.3%" });
            this.dataGridView1.Rows.Add(new object[] { "South Southampton", "250 MW", "-8.1%" });
            this.dataGridView1.Rows.Add(new object[] { "Lower Upperton", "100 MW", "-4.2%" });
            this.dataGridView1.Rows.Add(new object[] { "Upper Casington", "100 MW", "52.7%" });
        }
    }
}