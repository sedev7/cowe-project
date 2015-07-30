namespace COWE.Client
{
    partial class AnalysisControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.AnalysisDataGridView = new System.Windows.Forms.DataGridView();
            this.SingleChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.CumulativeChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.CdfChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AnalysisMetricsGroupBox = new System.Windows.Forms.GroupBox();
            this.HistogramBinSizeTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ChartTypeComboBox = new System.Windows.Forms.ComboBox();
            this.TrimIntervalsCheckBox = new System.Windows.Forms.CheckBox();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.AnalysisIntervalSizeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.TrimSmallestBinsToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.AnalysisDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SingleChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CumulativeChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CdfChart)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.AnalysisMetricsGroupBox.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // AnalysisDataGridView
            // 
            this.AnalysisDataGridView.AllowUserToAddRows = false;
            this.AnalysisDataGridView.AllowUserToDeleteRows = false;
            this.AnalysisDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.AnalysisDataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.AnalysisDataGridView.Location = new System.Drawing.Point(2, 3);
            this.AnalysisDataGridView.Name = "AnalysisDataGridView";
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(2);
            this.AnalysisDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.AnalysisDataGridView.Size = new System.Drawing.Size(410, 146);
            this.AnalysisDataGridView.TabIndex = 0;
            // 
            // SingleChart
            // 
            chartArea1.Name = "ChartArea1";
            this.SingleChart.ChartAreas.Add(chartArea1);
            this.SingleChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.SingleChart.Legends.Add(legend1);
            this.SingleChart.Location = new System.Drawing.Point(0, 0);
            this.SingleChart.Name = "SingleChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.SingleChart.Series.Add(series1);
            this.SingleChart.Size = new System.Drawing.Size(242, 219);
            this.SingleChart.TabIndex = 1;
            this.SingleChart.Text = "chart1";
            // 
            // CumulativeChart
            // 
            chartArea2.Name = "ChartArea1";
            this.CumulativeChart.ChartAreas.Add(chartArea2);
            this.CumulativeChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.CumulativeChart.Legends.Add(legend2);
            this.CumulativeChart.Location = new System.Drawing.Point(0, 0);
            this.CumulativeChart.Name = "CumulativeChart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.CumulativeChart.Series.Add(series2);
            this.CumulativeChart.Size = new System.Drawing.Size(242, 219);
            this.CumulativeChart.TabIndex = 2;
            this.CumulativeChart.Text = "chart2";
            // 
            // CdfChart
            // 
            chartArea3.Name = "ChartArea1";
            this.CdfChart.ChartAreas.Add(chartArea3);
            this.CdfChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.CdfChart.Legends.Add(legend3);
            this.CdfChart.Location = new System.Drawing.Point(0, 0);
            this.CdfChart.Name = "CdfChart";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.CdfChart.Series.Add(series3);
            this.CdfChart.Size = new System.Drawing.Size(242, 219);
            this.CdfChart.TabIndex = 3;
            this.CdfChart.Text = "chart3";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(3, 212);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(743, 243);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.SingleChart);
            this.panel3.Location = new System.Drawing.Point(5, 14);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(242, 219);
            this.panel3.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.CumulativeChart);
            this.panel2.Location = new System.Drawing.Point(250, 14);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(242, 219);
            this.panel2.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CdfChart);
            this.panel1.Location = new System.Drawing.Point(495, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(242, 219);
            this.panel1.TabIndex = 4;
            // 
            // AnalysisMetricsGroupBox
            // 
            this.AnalysisMetricsGroupBox.Controls.Add(this.HistogramBinSizeTextBox);
            this.AnalysisMetricsGroupBox.Controls.Add(this.label3);
            this.AnalysisMetricsGroupBox.Controls.Add(this.label2);
            this.AnalysisMetricsGroupBox.Controls.Add(this.ChartTypeComboBox);
            this.AnalysisMetricsGroupBox.Controls.Add(this.TrimIntervalsCheckBox);
            this.AnalysisMetricsGroupBox.Controls.Add(this.RefreshButton);
            this.AnalysisMetricsGroupBox.Controls.Add(this.AnalysisIntervalSizeTextBox);
            this.AnalysisMetricsGroupBox.Controls.Add(this.label1);
            this.AnalysisMetricsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnalysisMetricsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.AnalysisMetricsGroupBox.Name = "AnalysisMetricsGroupBox";
            this.AnalysisMetricsGroupBox.Size = new System.Drawing.Size(117, 205);
            this.AnalysisMetricsGroupBox.TabIndex = 1;
            this.AnalysisMetricsGroupBox.TabStop = false;
            this.AnalysisMetricsGroupBox.Text = "Analysis Metrics";
            // 
            // HistogramBinSizeTextBox
            // 
            this.HistogramBinSizeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HistogramBinSizeTextBox.Location = new System.Drawing.Point(87, 45);
            this.HistogramBinSizeTextBox.Name = "HistogramBinSizeTextBox";
            this.HistogramBinSizeTextBox.Size = new System.Drawing.Size(27, 22);
            this.HistogramBinSizeTextBox.TabIndex = 7;
            this.HistogramBinSizeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 35);
            this.label3.TabIndex = 6;
            this.label3.Text = "Hist Bin Size\r\n(packets/interval)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Chart Type:";
            // 
            // ChartTypeComboBox
            // 
            this.ChartTypeComboBox.FormattingEnabled = true;
            this.ChartTypeComboBox.Location = new System.Drawing.Point(8, 132);
            this.ChartTypeComboBox.Name = "ChartTypeComboBox";
            this.ChartTypeComboBox.Size = new System.Drawing.Size(103, 21);
            this.ChartTypeComboBox.TabIndex = 4;
            // 
            // TrimIntervalsCheckBox
            // 
            this.TrimIntervalsCheckBox.AutoSize = true;
            this.TrimIntervalsCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TrimIntervalsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TrimIntervalsCheckBox.Location = new System.Drawing.Point(4, 88);
            this.TrimIntervalsCheckBox.Name = "TrimIntervalsCheckBox";
            this.TrimIntervalsCheckBox.Size = new System.Drawing.Size(138, 20);
            this.TrimIntervalsCheckBox.TabIndex = 3;
            this.TrimIntervalsCheckBox.Text = "Trim Smallest Bins";
            this.TrimIntervalsCheckBox.UseVisualStyleBackColor = true;
            this.TrimIntervalsCheckBox.CheckedChanged += new System.EventHandler(this.TrimIntervalsCheckBox_CheckedChanged);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshButton.Location = new System.Drawing.Point(25, 166);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(68, 24);
            this.RefreshButton.TabIndex = 2;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // AnalysisIntervalSizeTextBox
            // 
            this.AnalysisIntervalSizeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AnalysisIntervalSizeTextBox.Location = new System.Drawing.Point(86, 17);
            this.AnalysisIntervalSizeTextBox.Name = "AnalysisIntervalSizeTextBox";
            this.AnalysisIntervalSizeTextBox.Size = new System.Drawing.Size(27, 22);
            this.AnalysisIntervalSizeTextBox.TabIndex = 1;
            this.AnalysisIntervalSizeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Interval Size (ms)";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.AnalysisMetricsGroupBox);
            this.panel4.Location = new System.Drawing.Point(627, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(117, 205);
            this.panel4.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.AnalysisDataGridView);
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(620, 205);
            this.panel5.TabIndex = 3;
            // 
            // AnalysisControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.groupBox1);
            this.MaximumSize = new System.Drawing.Size(996, 536);
            this.Name = "AnalysisControl";
            this.Size = new System.Drawing.Size(755, 460);
            this.Load += new System.EventHandler(this.AnalysisControl_Load);
            this.VisibleChanged += new System.EventHandler(this.AnalysisControl_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.AnalysisDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SingleChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CumulativeChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CdfChart)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.AnalysisMetricsGroupBox.ResumeLayout(false);
            this.AnalysisMetricsGroupBox.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView AnalysisDataGridView;
        private System.Windows.Forms.DataVisualization.Charting.Chart SingleChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart CumulativeChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart CdfChart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox AnalysisMetricsGroupBox;
        private System.Windows.Forms.TextBox AnalysisIntervalSizeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox TrimIntervalsCheckBox;
        private System.Windows.Forms.TextBox HistogramBinSizeTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ChartTypeComboBox;
        private System.Windows.Forms.ToolTip TrimSmallestBinsToolTip;

    }
}
