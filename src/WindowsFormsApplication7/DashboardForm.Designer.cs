namespace WindowsFormsApplication7
{
    partial class DashboardForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.labelCapacityUtilization = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelCapacityUtilitzationGraph = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelInputVariance = new System.Windows.Forms.Label();
            this.labelOutputVariance = new System.Windows.Forms.Label();
            this.labelLoadVariance = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelInputVarianceGraph = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelOutputVarianceGraph = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.labelLoadVarianceGraph = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.labelLoad = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelCapacity = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelOutput = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelInput = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.gaugeLoadVariance = new AJBauer.AGauge();
            this.gaugeOutputVariance = new AJBauer.AGauge();
            this.gaugeInputVariance = new AJBauer.AGauge();
            this.gaugeCapacityUtilization = new AJBauer.AGauge();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Train Velocity";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDoubleClick);
            this.label1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDoubleClick);
            // 
            // labelCapacityUtilization
            // 
            this.labelCapacityUtilization.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCapacityUtilization.Location = new System.Drawing.Point(74, 185);
            this.labelCapacityUtilization.Name = "labelCapacityUtilization";
            this.labelCapacityUtilization.Size = new System.Drawing.Size(62, 18);
            this.labelCapacityUtilization.TabIndex = 2;
            this.labelCapacityUtilization.Text = "--- %";
            this.labelCapacityUtilization.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.labelCapacityUtilitzationGraph);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labelCapacityUtilization);
            this.panel1.Controls.Add(this.gaugeCapacityUtilization);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 276);
            this.panel1.TabIndex = 3;
            // 
            // labelCapacityUtilitzationGraph
            // 
            this.labelCapacityUtilitzationGraph.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelCapacityUtilitzationGraph.BackColor = System.Drawing.Color.White;
            this.labelCapacityUtilitzationGraph.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelCapacityUtilitzationGraph.Location = new System.Drawing.Point(2, 211);
            this.labelCapacityUtilitzationGraph.Name = "labelCapacityUtilitzationGraph";
            this.labelCapacityUtilitzationGraph.Size = new System.Drawing.Size(211, 58);
            this.labelCapacityUtilitzationGraph.TabIndex = 3;
            this.labelCapacityUtilitzationGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.labelCapacityUtilitzationGraph_Paint);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(20, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 42);
            this.label8.TabIndex = 20;
            this.label8.Text = "Input Variance";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(18, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 42);
            this.label9.TabIndex = 21;
            this.label9.Text = "Output Variance";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(22, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 42);
            this.label10.TabIndex = 22;
            this.label10.Text = "Load Variance";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelInputVariance
            // 
            this.labelInputVariance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInputVariance.Location = new System.Drawing.Point(42, 119);
            this.labelInputVariance.Name = "labelInputVariance";
            this.labelInputVariance.Size = new System.Drawing.Size(62, 18);
            this.labelInputVariance.TabIndex = 23;
            this.labelInputVariance.Text = "--- %";
            this.labelInputVariance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelOutputVariance
            // 
            this.labelOutputVariance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOutputVariance.Location = new System.Drawing.Point(40, 119);
            this.labelOutputVariance.Name = "labelOutputVariance";
            this.labelOutputVariance.Size = new System.Drawing.Size(62, 18);
            this.labelOutputVariance.TabIndex = 24;
            this.labelOutputVariance.Text = "--- %";
            this.labelOutputVariance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelLoadVariance
            // 
            this.labelLoadVariance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLoadVariance.Location = new System.Drawing.Point(40, 119);
            this.labelLoadVariance.Name = "labelLoadVariance";
            this.labelLoadVariance.Size = new System.Drawing.Size(62, 18);
            this.labelLoadVariance.TabIndex = 25;
            this.labelLoadVariance.Text = "--- %";
            this.labelLoadVariance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.labelInputVarianceGraph);
            this.panel2.Controls.Add(this.gaugeInputVariance);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.labelInputVariance);
            this.panel2.Location = new System.Drawing.Point(220, 70);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(146, 208);
            this.panel2.TabIndex = 28;
            // 
            // labelInputVarianceGraph
            // 
            this.labelInputVarianceGraph.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelInputVarianceGraph.BackColor = System.Drawing.Color.White;
            this.labelInputVarianceGraph.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelInputVarianceGraph.Location = new System.Drawing.Point(1, 143);
            this.labelInputVarianceGraph.Name = "labelInputVarianceGraph";
            this.labelInputVarianceGraph.Size = new System.Drawing.Size(142, 58);
            this.labelInputVarianceGraph.TabIndex = 27;
            this.labelInputVarianceGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.labelInputVarianceGraph_Paint);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.labelOutputVarianceGraph);
            this.panel3.Controls.Add(this.gaugeOutputVariance);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.labelOutputVariance);
            this.panel3.Location = new System.Drawing.Point(369, 70);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(146, 208);
            this.panel3.TabIndex = 29;
            // 
            // labelOutputVarianceGraph
            // 
            this.labelOutputVarianceGraph.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelOutputVarianceGraph.BackColor = System.Drawing.Color.White;
            this.labelOutputVarianceGraph.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelOutputVarianceGraph.Location = new System.Drawing.Point(2, 143);
            this.labelOutputVarianceGraph.Name = "labelOutputVarianceGraph";
            this.labelOutputVarianceGraph.Size = new System.Drawing.Size(140, 58);
            this.labelOutputVarianceGraph.TabIndex = 28;
            this.labelOutputVarianceGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.labelOutputVarianceGraph_Paint);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.labelLoadVarianceGraph);
            this.panel4.Controls.Add(this.gaugeLoadVariance);
            this.panel4.Controls.Add(this.labelLoadVariance);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Location = new System.Drawing.Point(518, 70);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(146, 208);
            this.panel4.TabIndex = 30;
            // 
            // labelLoadVarianceGraph
            // 
            this.labelLoadVarianceGraph.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelLoadVarianceGraph.BackColor = System.Drawing.Color.White;
            this.labelLoadVarianceGraph.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelLoadVarianceGraph.Location = new System.Drawing.Point(-1, 143);
            this.labelLoadVarianceGraph.Name = "labelLoadVarianceGraph";
            this.labelLoadVarianceGraph.Size = new System.Drawing.Size(140, 58);
            this.labelLoadVarianceGraph.TabIndex = 28;
            this.labelLoadVarianceGraph.Click += new System.EventHandler(this.labelLoadVarianceGraph_Click);
            this.labelLoadVarianceGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.labelLoadVarianceGraph_Paint);
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.groupBox4);
            this.panel5.Controls.Add(this.groupBox3);
            this.panel5.Controls.Add(this.groupBox2);
            this.panel5.Controls.Add(this.groupBox1);
            this.panel5.Location = new System.Drawing.Point(220, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(444, 51);
            this.panel5.TabIndex = 31;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.labelLoad);
            this.groupBox4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(334, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(101, 41);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Load Monitor";
            // 
            // labelLoad
            // 
            this.labelLoad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLoad.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLoad.ForeColor = System.Drawing.Color.Green;
            this.labelLoad.Location = new System.Drawing.Point(3, 16);
            this.labelLoad.Name = "labelLoad";
            this.labelLoad.Size = new System.Drawing.Size(95, 22);
            this.labelLoad.TabIndex = 3;
            this.labelLoad.Text = "ONLINE";
            this.labelLoad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labelCapacity);
            this.groupBox3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(225, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(101, 41);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Capacity Monitor";
            // 
            // labelCapacity
            // 
            this.labelCapacity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelCapacity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCapacity.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCapacity.ForeColor = System.Drawing.Color.Green;
            this.labelCapacity.Location = new System.Drawing.Point(3, 16);
            this.labelCapacity.Name = "labelCapacity";
            this.labelCapacity.Size = new System.Drawing.Size(95, 22);
            this.labelCapacity.TabIndex = 3;
            this.labelCapacity.Text = "ONLINE";
            this.labelCapacity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelOutput);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(114, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(106, 41);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output Processor";
            // 
            // labelOutput
            // 
            this.labelOutput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelOutput.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOutput.ForeColor = System.Drawing.Color.Green;
            this.labelOutput.Location = new System.Drawing.Point(3, 16);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(100, 22);
            this.labelOutput.TabIndex = 3;
            this.labelOutput.Text = "ONLINE";
            this.labelOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelInput);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(7, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(101, 41);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input Processor";
            // 
            // labelInput
            // 
            this.labelInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelInput.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInput.ForeColor = System.Drawing.Color.Green;
            this.labelInput.Location = new System.Drawing.Point(3, 16);
            this.labelInput.Name = "labelInput";
            this.labelInput.Size = new System.Drawing.Size(95, 22);
            this.labelInput.TabIndex = 3;
            this.labelInput.Text = "ONLINE";
            this.labelInput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Yellow;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(221, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(443, 15);
            this.label2.TabIndex = 32;
            this.label2.Text = "Click on a gauge dial to see details";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(7, 597);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(113, 29);
            this.textBox1.TabIndex = 34;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(142, 597);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 35;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(2, 276);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(657, 245);
            this.chart1.TabIndex = 36;
            this.chart1.Text = "chart1";
            // 
            // gaugeLoadVariance
            // 
            this.gaugeLoadVariance.BackColor = System.Drawing.SystemColors.Control;
            this.gaugeLoadVariance.BaseArcColor = System.Drawing.Color.Gray;
            this.gaugeLoadVariance.BaseArcRadius = 40;
            this.gaugeLoadVariance.BaseArcStart = 180;
            this.gaugeLoadVariance.BaseArcSweep = 180;
            this.gaugeLoadVariance.BaseArcWidth = 2;
            this.gaugeLoadVariance.Cap_Idx = ((byte)(1));
            this.gaugeLoadVariance.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gaugeLoadVariance.CapPosition = new System.Drawing.Point(10, 10);
            this.gaugeLoadVariance.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gaugeLoadVariance.CapsText = new string[] {
        "",
        "",
        "",
        "",
        ""};
            this.gaugeLoadVariance.CapText = "";
            this.gaugeLoadVariance.Center = new System.Drawing.Point(70, 70);
            this.gaugeLoadVariance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gaugeLoadVariance.Location = new System.Drawing.Point(-1, 0);
            this.gaugeLoadVariance.MaxValue = 50F;
            this.gaugeLoadVariance.MinValue = -50F;
            this.gaugeLoadVariance.Name = "gaugeLoadVariance";
            this.gaugeLoadVariance.NeedleColor1 = AJBauer.AGauge.NeedleColorEnum.Blue;
            this.gaugeLoadVariance.NeedleColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gaugeLoadVariance.NeedleRadius = 40;
            this.gaugeLoadVariance.NeedleType = 0;
            this.gaugeLoadVariance.NeedleWidth = 3;
            this.gaugeLoadVariance.Range_Idx = ((byte)(1));
            this.gaugeLoadVariance.RangeColor = System.Drawing.Color.Green;
            this.gaugeLoadVariance.RangeEnabled = true;
            this.gaugeLoadVariance.RangeEndValue = 10F;
            this.gaugeLoadVariance.RangeInnerRadius = 40;
            this.gaugeLoadVariance.RangeOuterRadius = 50;
            this.gaugeLoadVariance.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Green,
        System.Drawing.Color.Red,
        System.Drawing.Color.Yellow,
        System.Drawing.Color.Yellow};
            this.gaugeLoadVariance.RangesEnabled = new bool[] {
        true,
        true,
        true,
        true,
        true};
            this.gaugeLoadVariance.RangesEndValue = new float[] {
        -20F,
        10F,
        50F,
        -10F,
        20F};
            this.gaugeLoadVariance.RangesInnerRadius = new int[] {
        40,
        40,
        40,
        40,
        40};
            this.gaugeLoadVariance.RangesOuterRadius = new int[] {
        50,
        50,
        50,
        50,
        50};
            this.gaugeLoadVariance.RangesStartValue = new float[] {
        -50F,
        -10F,
        20F,
        -20F,
        10F};
            this.gaugeLoadVariance.RangeStartValue = -10F;
            this.gaugeLoadVariance.ScaleLinesInterColor = System.Drawing.Color.RosyBrown;
            this.gaugeLoadVariance.ScaleLinesInterInnerRadius = 42;
            this.gaugeLoadVariance.ScaleLinesInterOuterRadius = 50;
            this.gaugeLoadVariance.ScaleLinesInterWidth = 1;
            this.gaugeLoadVariance.ScaleLinesMajorColor = System.Drawing.Color.RoyalBlue;
            this.gaugeLoadVariance.ScaleLinesMajorInnerRadius = 40;
            this.gaugeLoadVariance.ScaleLinesMajorOuterRadius = 50;
            this.gaugeLoadVariance.ScaleLinesMajorStepValue = 10F;
            this.gaugeLoadVariance.ScaleLinesMajorWidth = 2;
            this.gaugeLoadVariance.ScaleLinesMinorColor = System.Drawing.Color.DarkSalmon;
            this.gaugeLoadVariance.ScaleLinesMinorInnerRadius = 43;
            this.gaugeLoadVariance.ScaleLinesMinorNumOf = 1;
            this.gaugeLoadVariance.ScaleLinesMinorOuterRadius = 50;
            this.gaugeLoadVariance.ScaleLinesMinorWidth = 1;
            this.gaugeLoadVariance.ScaleNumbersColor = System.Drawing.Color.RoyalBlue;
            this.gaugeLoadVariance.ScaleNumbersFormat = null;
            this.gaugeLoadVariance.ScaleNumbersRadius = 62;
            this.gaugeLoadVariance.ScaleNumbersRotation = 90;
            this.gaugeLoadVariance.ScaleNumbersStartScaleLine = 1;
            this.gaugeLoadVariance.ScaleNumbersStepScaleLines = 2;
            this.gaugeLoadVariance.Size = new System.Drawing.Size(144, 78);
            this.gaugeLoadVariance.TabIndex = 19;
            this.gaugeLoadVariance.Tag = "Load Variance";
            this.gaugeLoadVariance.Text = "aGauge2";
            this.gaugeLoadVariance.Value = 22F;
            this.gaugeLoadVariance.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDoubleClick);
            // 
            // gaugeOutputVariance
            // 
            this.gaugeOutputVariance.BackColor = System.Drawing.SystemColors.Control;
            this.gaugeOutputVariance.BaseArcColor = System.Drawing.Color.Gray;
            this.gaugeOutputVariance.BaseArcRadius = 40;
            this.gaugeOutputVariance.BaseArcStart = 180;
            this.gaugeOutputVariance.BaseArcSweep = 180;
            this.gaugeOutputVariance.BaseArcWidth = 2;
            this.gaugeOutputVariance.Cap_Idx = ((byte)(1));
            this.gaugeOutputVariance.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gaugeOutputVariance.CapPosition = new System.Drawing.Point(10, 10);
            this.gaugeOutputVariance.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gaugeOutputVariance.CapsText = new string[] {
        "",
        "",
        "",
        "",
        ""};
            this.gaugeOutputVariance.CapText = "";
            this.gaugeOutputVariance.Center = new System.Drawing.Point(70, 70);
            this.gaugeOutputVariance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gaugeOutputVariance.Location = new System.Drawing.Point(-1, 0);
            this.gaugeOutputVariance.MaxValue = 50F;
            this.gaugeOutputVariance.MinValue = -50F;
            this.gaugeOutputVariance.Name = "gaugeOutputVariance";
            this.gaugeOutputVariance.NeedleColor1 = AJBauer.AGauge.NeedleColorEnum.Blue;
            this.gaugeOutputVariance.NeedleColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gaugeOutputVariance.NeedleRadius = 40;
            this.gaugeOutputVariance.NeedleType = 0;
            this.gaugeOutputVariance.NeedleWidth = 3;
            this.gaugeOutputVariance.Range_Idx = ((byte)(1));
            this.gaugeOutputVariance.RangeColor = System.Drawing.Color.Green;
            this.gaugeOutputVariance.RangeEnabled = true;
            this.gaugeOutputVariance.RangeEndValue = 15F;
            this.gaugeOutputVariance.RangeInnerRadius = 40;
            this.gaugeOutputVariance.RangeOuterRadius = 50;
            this.gaugeOutputVariance.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Green,
        System.Drawing.Color.Red,
        System.Drawing.Color.Yellow,
        System.Drawing.Color.Yellow};
            this.gaugeOutputVariance.RangesEnabled = new bool[] {
        true,
        true,
        true,
        true,
        true};
            this.gaugeOutputVariance.RangesEndValue = new float[] {
        -30F,
        15F,
        50F,
        -15F,
        30F};
            this.gaugeOutputVariance.RangesInnerRadius = new int[] {
        40,
        40,
        40,
        40,
        40};
            this.gaugeOutputVariance.RangesOuterRadius = new int[] {
        50,
        50,
        50,
        50,
        50};
            this.gaugeOutputVariance.RangesStartValue = new float[] {
        -50F,
        -15F,
        30F,
        -30F,
        15F};
            this.gaugeOutputVariance.RangeStartValue = -15F;
            this.gaugeOutputVariance.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gaugeOutputVariance.ScaleLinesInterColor = System.Drawing.Color.RosyBrown;
            this.gaugeOutputVariance.ScaleLinesInterInnerRadius = 42;
            this.gaugeOutputVariance.ScaleLinesInterOuterRadius = 50;
            this.gaugeOutputVariance.ScaleLinesInterWidth = 1;
            this.gaugeOutputVariance.ScaleLinesMajorColor = System.Drawing.Color.RoyalBlue;
            this.gaugeOutputVariance.ScaleLinesMajorInnerRadius = 40;
            this.gaugeOutputVariance.ScaleLinesMajorOuterRadius = 50;
            this.gaugeOutputVariance.ScaleLinesMajorStepValue = 10F;
            this.gaugeOutputVariance.ScaleLinesMajorWidth = 2;
            this.gaugeOutputVariance.ScaleLinesMinorColor = System.Drawing.Color.DarkSalmon;
            this.gaugeOutputVariance.ScaleLinesMinorInnerRadius = 43;
            this.gaugeOutputVariance.ScaleLinesMinorNumOf = 1;
            this.gaugeOutputVariance.ScaleLinesMinorOuterRadius = 50;
            this.gaugeOutputVariance.ScaleLinesMinorWidth = 1;
            this.gaugeOutputVariance.ScaleNumbersColor = System.Drawing.Color.RoyalBlue;
            this.gaugeOutputVariance.ScaleNumbersFormat = null;
            this.gaugeOutputVariance.ScaleNumbersRadius = 62;
            this.gaugeOutputVariance.ScaleNumbersRotation = 90;
            this.gaugeOutputVariance.ScaleNumbersStartScaleLine = 1;
            this.gaugeOutputVariance.ScaleNumbersStepScaleLines = 2;
            this.gaugeOutputVariance.Size = new System.Drawing.Size(144, 78);
            this.gaugeOutputVariance.TabIndex = 27;
            this.gaugeOutputVariance.Tag = "Output Variance";
            this.gaugeOutputVariance.Text = "aGauge2";
            this.gaugeOutputVariance.Value = 22F;
            this.gaugeOutputVariance.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDoubleClick);
            // 
            // gaugeInputVariance
            // 
            this.gaugeInputVariance.BackColor = System.Drawing.SystemColors.Control;
            this.gaugeInputVariance.BaseArcColor = System.Drawing.Color.Gray;
            this.gaugeInputVariance.BaseArcRadius = 40;
            this.gaugeInputVariance.BaseArcStart = 180;
            this.gaugeInputVariance.BaseArcSweep = 180;
            this.gaugeInputVariance.BaseArcWidth = 2;
            this.gaugeInputVariance.Cap_Idx = ((byte)(1));
            this.gaugeInputVariance.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gaugeInputVariance.CapPosition = new System.Drawing.Point(10, 10);
            this.gaugeInputVariance.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gaugeInputVariance.CapsText = new string[] {
        "",
        "",
        "",
        "",
        ""};
            this.gaugeInputVariance.CapText = "";
            this.gaugeInputVariance.Center = new System.Drawing.Point(70, 70);
            this.gaugeInputVariance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gaugeInputVariance.Location = new System.Drawing.Point(1, 1);
            this.gaugeInputVariance.MaxValue = 50F;
            this.gaugeInputVariance.MinValue = -50F;
            this.gaugeInputVariance.Name = "gaugeInputVariance";
            this.gaugeInputVariance.NeedleColor1 = AJBauer.AGauge.NeedleColorEnum.Blue;
            this.gaugeInputVariance.NeedleColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.gaugeInputVariance.NeedleRadius = 40;
            this.gaugeInputVariance.NeedleType = 0;
            this.gaugeInputVariance.NeedleWidth = 3;
            this.gaugeInputVariance.Range_Idx = ((byte)(1));
            this.gaugeInputVariance.RangeColor = System.Drawing.Color.Green;
            this.gaugeInputVariance.RangeEnabled = true;
            this.gaugeInputVariance.RangeEndValue = 10F;
            this.gaugeInputVariance.RangeInnerRadius = 40;
            this.gaugeInputVariance.RangeOuterRadius = 50;
            this.gaugeInputVariance.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Green,
        System.Drawing.Color.Red,
        System.Drawing.Color.Yellow,
        System.Drawing.Color.Yellow};
            this.gaugeInputVariance.RangesEnabled = new bool[] {
        true,
        true,
        true,
        true,
        true};
            this.gaugeInputVariance.RangesEndValue = new float[] {
        -25F,
        10F,
        50F,
        -10F,
        25F};
            this.gaugeInputVariance.RangesInnerRadius = new int[] {
        40,
        40,
        40,
        40,
        40};
            this.gaugeInputVariance.RangesOuterRadius = new int[] {
        50,
        50,
        50,
        50,
        50};
            this.gaugeInputVariance.RangesStartValue = new float[] {
        -50F,
        -10F,
        25F,
        -25F,
        10F};
            this.gaugeInputVariance.RangeStartValue = -10F;
            this.gaugeInputVariance.ScaleLinesInterColor = System.Drawing.Color.RosyBrown;
            this.gaugeInputVariance.ScaleLinesInterInnerRadius = 42;
            this.gaugeInputVariance.ScaleLinesInterOuterRadius = 50;
            this.gaugeInputVariance.ScaleLinesInterWidth = 1;
            this.gaugeInputVariance.ScaleLinesMajorColor = System.Drawing.Color.RoyalBlue;
            this.gaugeInputVariance.ScaleLinesMajorInnerRadius = 40;
            this.gaugeInputVariance.ScaleLinesMajorOuterRadius = 50;
            this.gaugeInputVariance.ScaleLinesMajorStepValue = 10F;
            this.gaugeInputVariance.ScaleLinesMajorWidth = 2;
            this.gaugeInputVariance.ScaleLinesMinorColor = System.Drawing.Color.DarkSalmon;
            this.gaugeInputVariance.ScaleLinesMinorInnerRadius = 43;
            this.gaugeInputVariance.ScaleLinesMinorNumOf = 1;
            this.gaugeInputVariance.ScaleLinesMinorOuterRadius = 50;
            this.gaugeInputVariance.ScaleLinesMinorWidth = 1;
            this.gaugeInputVariance.ScaleNumbersColor = System.Drawing.Color.RoyalBlue;
            this.gaugeInputVariance.ScaleNumbersFormat = null;
            this.gaugeInputVariance.ScaleNumbersRadius = 62;
            this.gaugeInputVariance.ScaleNumbersRotation = 90;
            this.gaugeInputVariance.ScaleNumbersStartScaleLine = 1;
            this.gaugeInputVariance.ScaleNumbersStepScaleLines = 2;
            this.gaugeInputVariance.Size = new System.Drawing.Size(144, 78);
            this.gaugeInputVariance.TabIndex = 26;
            this.gaugeInputVariance.Tag = "Input Variance";
            this.gaugeInputVariance.Text = "aGauge2";
            this.gaugeInputVariance.Value = 22F;
            this.gaugeInputVariance.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDoubleClick);
            // 
            // gaugeCapacityUtilization
            // 
            this.gaugeCapacityUtilization.BaseArcColor = System.Drawing.Color.Gray;
            this.gaugeCapacityUtilization.BaseArcRadius = 80;
            this.gaugeCapacityUtilization.BaseArcStart = 135;
            this.gaugeCapacityUtilization.BaseArcSweep = 270;
            this.gaugeCapacityUtilization.BaseArcWidth = 2;
            this.gaugeCapacityUtilization.Cap_Idx = ((byte)(1));
            this.gaugeCapacityUtilization.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gaugeCapacityUtilization.CapPosition = new System.Drawing.Point(10, 10);
            this.gaugeCapacityUtilization.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gaugeCapacityUtilization.CapsText = new string[] {
        "",
        "",
        "",
        "",
        ""};
            this.gaugeCapacityUtilization.CapText = "";
            this.gaugeCapacityUtilization.CausesValidation = false;
            this.gaugeCapacityUtilization.Center = new System.Drawing.Point(100, 100);
            this.gaugeCapacityUtilization.Location = new System.Drawing.Point(2, 2);
            this.gaugeCapacityUtilization.MaxValue = 100F;
            this.gaugeCapacityUtilization.MinValue = 0F;
            this.gaugeCapacityUtilization.Name = "gaugeCapacityUtilization";
            this.gaugeCapacityUtilization.NeedleColor1 = AJBauer.AGauge.NeedleColorEnum.Blue;
            this.gaugeCapacityUtilization.NeedleColor2 = System.Drawing.Color.DimGray;
            this.gaugeCapacityUtilization.NeedleRadius = 80;
            this.gaugeCapacityUtilization.NeedleType = 0;
            this.gaugeCapacityUtilization.NeedleWidth = 4;
            this.gaugeCapacityUtilization.Range_Idx = ((byte)(1));
            this.gaugeCapacityUtilization.RangeColor = System.Drawing.Color.Yellow;
            this.gaugeCapacityUtilization.RangeEnabled = true;
            this.gaugeCapacityUtilization.RangeEndValue = 60F;
            this.gaugeCapacityUtilization.RangeInnerRadius = 70;
            this.gaugeCapacityUtilization.RangeOuterRadius = 80;
            this.gaugeCapacityUtilization.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Yellow,
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.Green,
        System.Drawing.Color.Crimson};
            this.gaugeCapacityUtilization.RangesEnabled = new bool[] {
        true,
        true,
        true,
        true,
        true};
            this.gaugeCapacityUtilization.RangesEndValue = new float[] {
        40F,
        60F,
        70F,
        90F,
        100F};
            this.gaugeCapacityUtilization.RangesInnerRadius = new int[] {
        70,
        70,
        70,
        70,
        70};
            this.gaugeCapacityUtilization.RangesOuterRadius = new int[] {
        80,
        80,
        80,
        80,
        80};
            this.gaugeCapacityUtilization.RangesStartValue = new float[] {
        0F,
        40F,
        60F,
        70F,
        90F};
            this.gaugeCapacityUtilization.RangeStartValue = 40F;
            this.gaugeCapacityUtilization.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.gaugeCapacityUtilization.ScaleLinesInterInnerRadius = 73;
            this.gaugeCapacityUtilization.ScaleLinesInterOuterRadius = 80;
            this.gaugeCapacityUtilization.ScaleLinesInterWidth = 1;
            this.gaugeCapacityUtilization.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.gaugeCapacityUtilization.ScaleLinesMajorInnerRadius = 70;
            this.gaugeCapacityUtilization.ScaleLinesMajorOuterRadius = 80;
            this.gaugeCapacityUtilization.ScaleLinesMajorStepValue = 5F;
            this.gaugeCapacityUtilization.ScaleLinesMajorWidth = 2;
            this.gaugeCapacityUtilization.ScaleLinesMinorColor = System.Drawing.Color.Gray;
            this.gaugeCapacityUtilization.ScaleLinesMinorInnerRadius = 75;
            this.gaugeCapacityUtilization.ScaleLinesMinorNumOf = 9;
            this.gaugeCapacityUtilization.ScaleLinesMinorOuterRadius = 80;
            this.gaugeCapacityUtilization.ScaleLinesMinorWidth = 1;
            this.gaugeCapacityUtilization.ScaleNumbersColor = System.Drawing.Color.Black;
            this.gaugeCapacityUtilization.ScaleNumbersFormat = null;
            this.gaugeCapacityUtilization.ScaleNumbersRadius = 95;
            this.gaugeCapacityUtilization.ScaleNumbersRotation = 0;
            this.gaugeCapacityUtilization.ScaleNumbersStartScaleLine = 0;
            this.gaugeCapacityUtilization.ScaleNumbersStepScaleLines = 1;
            this.gaugeCapacityUtilization.Size = new System.Drawing.Size(207, 183);
            this.gaugeCapacityUtilization.TabIndex = 0;
            this.gaugeCapacityUtilization.Tag = "Capacity Utilization";
            this.gaugeCapacityUtilization.Text = "aGauge1";
            this.gaugeCapacityUtilization.Value = 0F;
            this.gaugeCapacityUtilization.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDoubleClick);
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 632);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(675, 671);
            this.MinimumSize = new System.Drawing.Size(675, 671);
            this.Name = "DashboardForm";
            this.Text = "Dashboard Demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViamDashboardForm_FormClosing);
            this.Load += new System.EventHandler(this.DashboardForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AJBauer.AGauge gaugeCapacityUtilization;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelCapacityUtilization;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private AJBauer.AGauge gaugeLoadVariance;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelInputVariance;
        private System.Windows.Forms.Label labelOutputVariance;
        private System.Windows.Forms.Label labelLoadVariance;
        private AJBauer.AGauge gaugeInputVariance;
        private AJBauer.AGauge gaugeOutputVariance;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label labelLoad;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labelCapacity;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelOutput;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelInput;
        private System.Windows.Forms.Label labelCapacityUtilitzationGraph;
        private System.Windows.Forms.Label labelInputVarianceGraph;
        private System.Windows.Forms.Label labelOutputVarianceGraph;
        private System.Windows.Forms.Label labelLoadVarianceGraph;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}