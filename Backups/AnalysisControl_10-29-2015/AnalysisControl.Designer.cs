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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.AnalysisDataGridView = new System.Windows.Forms.DataGridView();
            this.SingleChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.CumulativeChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.CdfChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AnalysisMetricsGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ChartTypeComboBox = new System.Windows.Forms.ComboBox();
            this.RefreshButton = new System.Windows.Forms.Button();
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.AnalysisDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.AnalysisDataGridView.Location = new System.Drawing.Point(2, 3);
            this.AnalysisDataGridView.Name = "AnalysisDataGridView";
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(2);
            this.AnalysisDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.AnalysisDataGridView.Size = new System.Drawing.Size(410, 146);
            this.AnalysisDataGridView.TabIndex = 0;
            // 
            // SingleChart
            // 
            chartArea4.Name = "ChartArea1";
            this.SingleChart.ChartAreas.Add(chartArea4);
            this.SingleChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Name = "Legend1";
            this.SingleChart.Legends.Add(legend4);
            this.SingleChart.Location = new System.Drawing.Point(0, 0);
            this.SingleChart.Name = "SingleChart";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.SingleChart.Series.Add(series4);
            this.SingleChart.Size = new System.Drawing.Size(242, 219);
            this.SingleChart.TabIndex = 1;
            this.SingleChart.Text = "chart1";
            // 
            // CumulativeChart
            // 
            chartArea5.Name = "ChartArea1";
            this.CumulativeChart.ChartAreas.Add(chartArea5);
            this.CumulativeChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend5.Name = "Legend1";
            this.CumulativeChart.Legends.Add(legend5);
            this.CumulativeChart.Location = new System.Drawing.Point(0, 0);
            this.CumulativeChart.Name = "CumulativeChart";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.CumulativeChart.Series.Add(series5);
            this.CumulativeChart.Size = new System.Drawing.Size(242, 219);
            this.CumulativeChart.TabIndex = 2;
            this.CumulativeChart.Text = "chart2";
            // 
            // CdfChart
            // 
            chartArea6.Name = "ChartArea1";
            this.CdfChart.ChartAreas.Add(chartArea6);
            this.CdfChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend6.Name = "Legend1";
            this.CdfChart.Legends.Add(legend6);
            this.CdfChart.Location = new System.Drawing.Point(0, 0);
            this.CdfChart.Name = "CdfChart";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.CdfChart.Series.Add(series6);
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
            this.AnalysisMetricsGroupBox.Controls.Add(this.label2);
            this.AnalysisMetricsGroupBox.Controls.Add(this.ChartTypeComboBox);
            this.AnalysisMetricsGroupBox.Controls.Add(this.RefreshButton);
            this.AnalysisMetricsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnalysisMetricsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.AnalysisMetricsGroupBox.Name = "AnalysisMetricsGroupBox";
            this.AnalysisMetricsGroupBox.Size = new System.Drawing.Size(117, 205);
            this.AnalysisMetricsGroupBox.TabIndex = 1;
            this.AnalysisMetricsGroupBox.TabStop = false;
            this.AnalysisMetricsGroupBox.Text = "Chart Controls";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Chart Type:";
            // 
            // ChartTypeComboBox
            // 
            this.ChartTypeComboBox.FormattingEnabled = true;
            this.ChartTypeComboBox.Location = new System.Drawing.Point(7, 57);
            this.ChartTypeComboBox.Name = "ChartTypeComboBox";
            this.ChartTypeComboBox.Size = new System.Drawing.Size(103, 21);
            this.ChartTypeComboBox.TabIndex = 4;
            // 
            // RefreshButton
            // 
            this.RefreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshButton.Location = new System.Drawing.Point(27, 104);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(68, 24);
            this.RefreshButton.TabIndex = 2;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
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
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ChartTypeComboBox;
        private System.Windows.Forms.ToolTip TrimSmallestBinsToolTip;

    }
}
