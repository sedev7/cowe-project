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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.AnalysisDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SingleChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CumulativeChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KsChart)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // AnalysisDataGridView
            // 
            this.AnalysisDataGridView.AllowUserToAddRows = false;
            this.AnalysisDataGridView.AllowUserToDeleteRows = false;
            this.AnalysisDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AnalysisDataGridView.Location = new System.Drawing.Point(60, 39);
            this.AnalysisDataGridView.Name = "AnalysisDataGridView";
            this.AnalysisDataGridView.Size = new System.Drawing.Size(907, 215);
            this.AnalysisDataGridView.TabIndex = 0;
            // 
            // SingleChart
            // 
            chartArea1.Name = "ChartArea1";
            this.SingleChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.SingleChart.Legends.Add(legend1);
            this.SingleChart.Location = new System.Drawing.Point(15, 19);
            this.SingleChart.Name = "SingleChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.SingleChart.Series.Add(series1);
            this.SingleChart.Size = new System.Drawing.Size(217, 154);
            this.SingleChart.TabIndex = 1;
            this.SingleChart.Text = "chart1";
            // 
            // CumulativeChart
            // 
            chartArea2.Name = "ChartArea1";
            this.CumulativeChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.CumulativeChart.Legends.Add(legend2);
            this.CumulativeChart.Location = new System.Drawing.Point(258, 19);
            this.CumulativeChart.Name = "CumulativeChart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.CumulativeChart.Series.Add(series2);
            this.CumulativeChart.Size = new System.Drawing.Size(230, 154);
            this.CumulativeChart.TabIndex = 2;
            this.CumulativeChart.Text = "chart2";
            // 
            // KsChart
            // 
            chartArea3.Name = "ChartArea1";
            this.KsChart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.KsChart.Legends.Add(legend3);
            this.KsChart.Location = new System.Drawing.Point(514, 19);
            this.KsChart.Name = "KsChart";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.KsChart.Series.Add(series3);
            this.KsChart.Size = new System.Drawing.Size(211, 154);
            this.KsChart.TabIndex = 3;
            this.KsChart.Text = "chart3";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SingleChart);
            this.groupBox1.Controls.Add(this.KsChart);
            this.groupBox1.Controls.Add(this.CumulativeChart);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 344);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(996, 192);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.AnalysisDataGridView);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(996, 344);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
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
            this.Size = new System.Drawing.Size(996, 536);
            this.Load += new System.EventHandler(this.AnalysisControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AnalysisDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SingleChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CumulativeChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KsChart)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView AnalysisDataGridView;
        private System.Windows.Forms.DataVisualization.Charting.Chart SingleChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart CumulativeChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart KsChart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;

    }
}
