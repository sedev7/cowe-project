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
            this.KsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.AnalysisIntervalSizeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AnalysisDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SingleChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CumulativeChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KsChart)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // AnalysisDataGridView
            // 
            this.AnalysisDataGridView.AllowUserToAddRows = false;
            this.AnalysisDataGridView.AllowUserToDeleteRows = false;
            this.AnalysisDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AnalysisDataGridView.Location = new System.Drawing.Point(15, 36);
            this.AnalysisDataGridView.Name = "AnalysisDataGridView";
            this.AnalysisDataGridView.Size = new System.Drawing.Size(558, 215);
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
            this.SingleChart.Size = new System.Drawing.Size(244, 172);
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
            this.CumulativeChart.Size = new System.Drawing.Size(244, 172);
            this.CumulativeChart.TabIndex = 2;
            this.CumulativeChart.Text = "chart2";
            // 
            // KsChart
            // 
            chartArea3.Name = "ChartArea1";
            this.KsChart.ChartAreas.Add(chartArea3);
            this.KsChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.KsChart.Legends.Add(legend3);
            this.KsChart.Location = new System.Drawing.Point(0, 0);
            this.KsChart.Name = "KsChart";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.KsChart.Series.Add(series3);
            this.KsChart.Size = new System.Drawing.Size(244, 172);
            this.KsChart.TabIndex = 3;
            this.KsChart.Text = "chart3";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 344);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(755, 192);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.SingleChart);
            this.panel3.Location = new System.Drawing.Point(5, 14);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(244, 172);
            this.panel3.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.CumulativeChart);
            this.panel2.Location = new System.Drawing.Point(252, 14);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(244, 172);
            this.panel2.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.KsChart);
            this.panel1.Location = new System.Drawing.Point(499, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 172);
            this.panel1.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.AnalysisDataGridView);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(755, 344);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.RefreshButton);
            this.groupBox3.Controls.Add(this.AnalysisIntervalSizeTextBox);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(642, 29);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(90, 183);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Analysis";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshButton.Location = new System.Drawing.Point(8, 72);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(75, 27);
            this.RefreshButton.TabIndex = 2;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // AnalysisIntervalSizeTextBox
            // 
            this.AnalysisIntervalSizeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AnalysisIntervalSizeTextBox.Location = new System.Drawing.Point(9, 39);
            this.AnalysisIntervalSizeTextBox.Name = "AnalysisIntervalSizeTextBox";
            this.AnalysisIntervalSizeTextBox.Size = new System.Drawing.Size(73, 22);
            this.AnalysisIntervalSizeTextBox.TabIndex = 1;
            this.AnalysisIntervalSizeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Interval Size";
            // 
            // AnalysisControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximumSize = new System.Drawing.Size(996, 536);
            this.Name = "AnalysisControl";
            this.Size = new System.Drawing.Size(755, 536);
            this.Load += new System.EventHandler(this.AnalysisControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AnalysisDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SingleChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CumulativeChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KsChart)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView AnalysisDataGridView;
        private System.Windows.Forms.DataVisualization.Charting.Chart SingleChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart CumulativeChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart KsChart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox AnalysisIntervalSizeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;

    }
}
