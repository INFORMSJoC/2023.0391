namespace WindowsFormsApplication7
{
    partial class DetailGridForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SourceSink = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SSCapacity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Variance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SourceSink,
            this.SSCapacity,
            this.Variance});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 4;
            this.dataGridView1.Size = new System.Drawing.Size(380, 147);
            this.dataGridView1.TabIndex = 0;
            // 
            // SourceSink
            // 
            this.SourceSink.HeaderText = "Source or Sink";
            this.SourceSink.Name = "SourceSink";
            this.SourceSink.ReadOnly = true;
            this.SourceSink.Width = 130;
            // 
            // SSCapacity
            // 
            this.SSCapacity.HeaderText = "Capacity";
            this.SSCapacity.Name = "SSCapacity";
            this.SSCapacity.ReadOnly = true;
            this.SSCapacity.Width = 80;
            // 
            // Variance
            // 
            this.Variance.HeaderText = "Variance";
            this.Variance.Name = "Variance";
            this.Variance.ReadOnly = true;
            this.Variance.Width = 80;
            // 
            // DetailGridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 147);
            this.Controls.Add(this.dataGridView1);
            this.Name = "DetailGridForm";
            this.Text = "Detail Grid Form";
            this.Load += new System.EventHandler(this.DetailGridForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceSink;
        private System.Windows.Forms.DataGridViewTextBoxColumn SSCapacity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Variance;
    }
}