using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using COWE.BusinessLayer;
using COWE.DomainClasses;
using COWE.Enumerations;

namespace COWE.Client
{
    public partial class AnalysisControl : UserControl
    {
        #region Global Variables
        bool _trimZeroPacketIntervals = true;
        
        CurrentCaptureFile _CaptureFile = new CurrentCaptureFile();

        DataGridView _AnalysisDataGridView = new DataGridView();

        const double _alpha = 0.05;       // Hypothesis test significance level
        const decimal _zvalue = 1.65M;    // Z value for (1-_alpha), from standard normal distribution table
                                          // (note: one-tailed test because we are looking at the distribution 
                                          // for the difference of the means)
        
        int _MaxGridDisplayRows = 8;
        int _MaxGridHeight = 300;

        KsStatistics _KsStatistics = new KsStatistics(_alpha, _zvalue);    // Alpha, Z value
        
        SortedDictionary<int, decimal> _CumulativeMarkedProbabilities = new SortedDictionary<int, decimal>();
        SortedDictionary<int, decimal> _CumulativeUnmarkedProbabilities = new SortedDictionary<int, decimal>();

        #endregion

        #region Constructor
        public AnalysisControl()
        {
            InitializeComponent();
            //_CaptureFile.RegisterWithCaptureFile(new CurrentCaptureFile.ReceivedParsedFileEventHandler(OnReceivedFileEvent));
        }
        #endregion

        #region Event Handlers
        private void AnalysisControl_Load(object sender, EventArgs e)
        {
            InitializeAnalysisDataGridView();
            InitializeChartTypeComboBox();
            //InitializeAnalysisMetricsGroupBox();
            //AnalysisIntervalSizeTextBox.Text = InterarrivalInterval.GetIntervalMilliSeconds().ToString();
            //HistogramBinSizeTextBox.Text = "5";
            //TrimIntervalsCheckBox.Checked = true;
            //TrimSmallestBinsToolTip.SetToolTip(TrimIntervalsCheckBox, "Trim any intervals with a packet count less then or equal to the histogram bin size");
            //RefreshData();
        }
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshData();
        }
        private void TrimIntervalsCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void AnalysisControl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                RefreshData();
            }
        }
        //private void COWE.Client.Client.ReceivedParsedFileEventHandler(object sender, ReceivedParsedFileEventArgs e)
        //{
        //    //COWE.Client.Client.ReceivedParsedFile += new COWE.Client.Client.ReceivedParsedFileEventHandler(COWE.Client.Client.ReceivedParsedFile)
        //}
        //COWE.Client.ReceivedParsedFile += new COWE.Client.ReceivedParsedFileEventHandler(ReceivedParsedFile);
        //COWE.Client.ReceivedParsedFileEventHandler rp = new Client.ReceivedParsedFileEventHandler(ReceivedParsedFile);
        //COWE.Client.ReceivedFile += rp;
        //CurrentCaptureFile ccf = new CurrentCaptureFile();
        //ccf.ReceivedParsedFile += new CurrentCaptureFile.ReceivedParsedFileEventHandler(ReceivedParsedFile);
        //CurrentCaptureFile.ReceivedParsedFileEventHandler rpfh = new CurrentCaptureFile.ReceivedParsedFileEventHandler(ReceivedParsedFile);
        //ccf.ReceivedParsedFile += rpfh;
        

        //public static void ReceivedParsedFile(string msg)
        public static void OnReceivedFileEvent(string msg)
        {
            MessageBox.Show("[AnalysisControl] - received file event notification");
        }
        #endregion

        #region Properties
        public bool TrimIntervals { get; set; }
        public string HistogramBinSize { get; set; }
        #endregion

        #region Private Methods
        private void InitializeAnalysisDataGridView()
        {
            _AnalysisDataGridView = AnalysisDataGridView;
            _AnalysisDataGridView.Columns.Clear();
            // User cannot change data in the grid
            _AnalysisDataGridView.ReadOnly = true;

            int col = 0; 
            int row = 0;

            // Create statistic name column
            DataGridViewTextBoxColumn statisticName = new DataGridViewTextBoxColumn();
            statisticName.Name = "StatisticName";
            statisticName.DataPropertyName = "StatisticName";
            statisticName.HeaderText = "";
            statisticName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            statisticName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            statisticName.Width = 180;
            statisticName.ReadOnly = true;
            _AnalysisDataGridView.Columns.Insert(col++, statisticName);

            // Create single baseline column
            DataGridViewTextBoxColumn UnmarkedSingle = new DataGridViewTextBoxColumn();
            UnmarkedSingle.Name = "UnmarkedSingle";
            UnmarkedSingle.DataPropertyName = "UnmarkedSingle";
            UnmarkedSingle.HeaderText = "Single Unmarked";
            UnmarkedSingle.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            UnmarkedSingle.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            UnmarkedSingle.Width = 90;
            UnmarkedSingle.ReadOnly = true;
            _AnalysisDataGridView.Columns.Insert(col++, UnmarkedSingle);

            // Create single marked column
            DataGridViewTextBoxColumn markedSingle = new DataGridViewTextBoxColumn();
            markedSingle.Name = "MarkedSingle";
            markedSingle.DataPropertyName = "MarkedSingle";
            markedSingle.HeaderText = "Single Marked";
            markedSingle.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            markedSingle.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            markedSingle.Width = 90;
            markedSingle.ReadOnly = true;
            _AnalysisDataGridView.Columns.Insert(col++, markedSingle);

            // Create single variance column
            DataGridViewTextBoxColumn varianceSingle = new DataGridViewTextBoxColumn();
            varianceSingle.Name = "VarianceSingle";
            varianceSingle.DataPropertyName = "VarianceSingle";
            varianceSingle.HeaderText = "Single Variance";
            varianceSingle.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            varianceSingle.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            varianceSingle.Width = 90;
            varianceSingle.ReadOnly = true;
            _AnalysisDataGridView.Columns.Insert(col++, varianceSingle);

            // Create cumulative baseline column
            DataGridViewTextBoxColumn UnmarkedCumulative = new DataGridViewTextBoxColumn();
            UnmarkedCumulative.Name = "UnmarkedCumulative";
            UnmarkedCumulative.DataPropertyName = "UnmarkedCumulative";
            UnmarkedCumulative.HeaderText = "Cumulative Unmarked";
            UnmarkedCumulative.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            UnmarkedCumulative.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            UnmarkedCumulative.Width = 100;
            UnmarkedCumulative.ReadOnly = true;
            _AnalysisDataGridView.Columns.Insert(col++, UnmarkedCumulative);

            // Create cumulative marked column
            DataGridViewTextBoxColumn markedCumulative = new DataGridViewTextBoxColumn();
            markedCumulative.Name = "MarkedCumulative";
            markedCumulative.DataPropertyName = "MarkedCumulative";
            markedCumulative.HeaderText = "Cumulative Marked";
            markedCumulative.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            markedCumulative.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            markedCumulative.Width = 100;
            markedCumulative.ReadOnly = true;
            _AnalysisDataGridView.Columns.Insert(col++, markedCumulative);

            // Create cumulative variance column
            DataGridViewTextBoxColumn varianceCumulative = new DataGridViewTextBoxColumn();
            varianceCumulative.Name = "VarianceCumulative";
            varianceCumulative.DataPropertyName = "VarianceCumulative";
            varianceCumulative.HeaderText = "Cumulative Variance";
            varianceCumulative.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            varianceCumulative.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            varianceCumulative.Width = 100;
            varianceCumulative.ReadOnly = true;
            _AnalysisDataGridView.Columns.Insert(col++, varianceCumulative);

            // Create K-S statistic column
            DataGridViewTextBoxColumn ksStatistic = new DataGridViewTextBoxColumn();
            ksStatistic.Name = "KSStatistic";
            ksStatistic.DataPropertyName = "KSStatistic";
            ksStatistic.HeaderText = "K-S";
            ksStatistic.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ksStatistic.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ksStatistic.Width = 70;
            ksStatistic.ReadOnly = true;
            _AnalysisDataGridView.Columns.Insert(col++, ksStatistic);

            // Initialize the row
            //DataGridViewRow countGridRow = new DataGridViewRow();
            //_AnalysisDataGridView.Rows.Add();
            //_AnalysisDataGridView.Rows[row].Height = 30;

            DataTable dt = new DataTable();
            dt.Columns.Add("StatisticName");
            dt.Columns.Add("UnmarkedSingle");
            dt.Columns.Add("MarkedSingle");
            dt.Columns.Add("VarianceSingle");
            dt.Columns.Add("UnmarkedCumulative");
            dt.Columns.Add("MarkedCumulative");
            dt.Columns.Add("VarianceCumulative");
            dt.Columns.Add("KSStatistic");
            dt.Rows.Add("Interval Count", "0", "0", "0", "0", "0", "0", "0");
            dt.Rows.Add("Interval Count (Trimmed)", "0", "0", "0", "0", "0", "0", "0");
            dt.Rows.Add("Mean Packets/Interval", "0", "0", "0", "0", "0", "0", "0");
            dt.Rows.Add("Standard Deviation", "0", "0", "0", "0", "0", "0", "0");
            dt.Rows.Add("Minimum Packets/Interval", "0", "0", "0", "0", "0", "0", "0");
            dt.Rows.Add("Maximum Packets/Interval", "0", "0", "0", "0", "0", "0", "0");
            dt.Rows.Add("Mean of Means", "0", "0", "0", "0", "0", "0", "0");
            dt.Rows.Add("Alpha", "0", "0", "5%", "0", "0", "5%", "5%");
            dt.Rows.Add("Reject H0?", "0", "0", "0", "0", "0", "0", "0");
            
            // Add the DataTable to the grid
            _AnalysisDataGridView.AutoGenerateColumns = false;
            _AnalysisDataGridView.DataSource = dt;

            // Adjust the row height
            //foreach (DataGridViewRow r in _AnalysisDataGridView.Rows)
            //{
            //    r.Height = 60;
            //}
            //for (int i = 0; i <_AnalysisDataGridView.Rows.Count; i++ )
            //{
            //    _AnalysisDataGridView.Rows[i].Height = 60;
            //}

            _AnalysisDataGridView.Refresh();

            // Hide the row header column
            _AnalysisDataGridView.RowHeadersVisible = false;

            ResizeAnalysisGrid();
        }
        private void ResizeAnalysisGrid()
        {
            int height = 0;
            int width = 0;

            foreach (DataGridViewRow row in _AnalysisDataGridView.Rows)
            {
                height += row.Height;
            }
            height += _AnalysisDataGridView.ColumnHeadersHeight;

            foreach (DataGridViewColumn col in _AnalysisDataGridView.Columns)
            {
                width += col.Width;
            }

            if (_AnalysisDataGridView.Rows.Count == _MaxGridDisplayRows)
            {
                _MaxGridHeight = height;
            }

            //this._AnalysisDataGridView.ClientSize = new Size(width + 3, height + 3);

            if (_AnalysisDataGridView.Rows.Count > _MaxGridDisplayRows)
            {
                // Allow additional width for scroll bar
                _AnalysisDataGridView.ClientSize = new Size(width + 20, _MaxGridHeight + 4);
            }
            else
            {
                this._AnalysisDataGridView.ClientSize = new Size(width + 3, height + 3);
            }

        }
        //private void RefreshSingleDataChart(int captureBatchId)
        private void InitializeChartTypeComboBox()
        {
            ChartTypeComboBox.Items.Add("Bar");
            ChartTypeComboBox.Items.Add("Line");

            // Make the control read-only
            ChartTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ChartTypeComboBox.FlatStyle = FlatStyle.Flat;
            ChartTypeComboBox.SelectedIndex = 0;
            ChartTypeComboBox.Font = new Font("Microsoft Sans Serif", 10);
        }
        //private void InitializeAnalysisMetricsGroupBox()
        //{
        //    // Set the font size for the GroupBox (but not for the controls it contains)
        //    Font analysisMetricsFont = new Font("Microsoft Sans Serif", 8);
        //    AnalysisMetricsGroupBox.Font = analysisMetricsFont;
        //}
        private void RefreshSingleDataChart()
        {
            // Get the last marked and unmarked batches and add them to the graph
            BindingList<CurrentCaptureFile> lastBatchIds = new BindingList<CurrentCaptureFile>();
            ProcessCapturePackets pcp = new ProcessCapturePackets();
            lastBatchIds = pcp.GetLastCaptureBatchIds();

            // Format the chart
            //standardSeries.ChartType = SeriesChartType.RangeColumn;
            //standardSeries.BorderWidth = 1;
            //standardSeries.BorderDashStyle = ChartDashStyle.Solid;
            //standardSeries.BorderColor = Color.Black;
            //standardSeries.Color = Color.Blue;
            SingleChart.Series.Clear();
            SingleChart.Titles.Clear();
            SingleChart.Titles.Add("Current Capture Packet Probability Distribution");
            //SingleChart.Legends[0].Position.Auto = true; //ElementPosition
            SingleChart.Legends[0].IsDockedInsideChartArea = true;
            SingleChart.Legends[0].Docking = Docking.Bottom;
            SingleChart.Legends[0].Alignment = StringAlignment.Center;
            SingleChart.ChartAreas[0].AxisX.Title = "Packets per Interval";
            SingleChart.ChartAreas[0].AxisX.Minimum = 0;
            //SingleChart.ChartAreas[0].AxisX.Maximum = 

            // Get the type of chart to display
            string chartType = ChartTypeComboBox.Items[ChartTypeComboBox.SelectedIndex].ToString();

            // Marked probabilities series
            SingleChart.Series.Add("MarkedProbabilities");
            //SingleChart.Series["MarkedProbabilities"].ChartType = SeriesChartType.Line;
            SingleChart.Series["MarkedProbabilities"].ChartType = chartType == "Bar" ? SeriesChartType.Column : SeriesChartType.Line;
            SingleChart.Series["MarkedProbabilities"].IsVisibleInLegend = true;
            SingleChart.Series["MarkedProbabilities"].LegendText = "Marked";

            // Unmarked probabilities series
            SingleChart.Series.Add("UnmarkedProbabilities");
            //SingleChart.Series["UnmarkedProbabilities"].ChartType = SeriesChartType.Line;
            SingleChart.Series["UnmarkedProbabilities"].ChartType = chartType == "Bar" ? SeriesChartType.Column : SeriesChartType.Line;
            SingleChart.Series["UnmarkedProbabilities"].IsVisibleInLegend = true;
            SingleChart.Series["UnmarkedProbabilities"].LegendText = "Unmarked";
            
            foreach (CurrentCaptureFile file in lastBatchIds)
            {
                BindingList<BatchIntervalMarked> batchIntervals = new BindingList<BatchIntervalMarked>();
                
                // Calculate probabilities
                batchIntervals = pcp.GetMarkedBatchIntervals(file.CaptureBatchId);
                int histogramBinSize = Convert.ToInt32(HistogramBinSize);
                //SortedDictionary<int, decimal> probabilities = new CalculateProbability(batchIntervals).GetProbabilityByPacketRange(_trimZeroPacketIntervals, histogramBinSize);
                SortedDictionary<int, decimal> probabilities = new CalculateProbability(batchIntervals).GetProbabilityByPacketRange(_trimZeroPacketIntervals, histogramBinSize);

                if (file.CaptureState == CaptureState.Marked)
                {
                    SingleChart.Series["MarkedProbabilities"].Color = Color.CornflowerBlue;

                    foreach (KeyValuePair<int, decimal> pair in probabilities)
                    {
                        SingleChart.Series["MarkedProbabilities"].Points.AddXY(Convert.ToDouble(pair.Key), Convert.ToDouble(pair.Value));
                    }
                }
                else
                {
                    SingleChart.Series["UnmarkedProbabilities"].Color = Color.Red;

                    foreach (KeyValuePair<int, decimal> pair in probabilities)
                    {
                        SingleChart.Series["UnmarkedProbabilities"].Points.AddXY(Convert.ToDouble(pair.Key), Convert.ToDouble(pair.Value));
                    }
                }
            }
        }
        private void RefreshSingleBatchStatistics()
        {
            // Get the last marked and unmarked batches
            BindingList<CurrentCaptureFile> lastBatchIds = new BindingList<CurrentCaptureFile>();
            ProcessCapturePackets pcp = new ProcessCapturePackets();
            lastBatchIds = pcp.GetLastCaptureBatchIds();

            // Get the batch intervals
            BindingList<BatchIntervalMarked> unmarkedBatchIntervals = new BindingList<BatchIntervalMarked>();
            BindingList<BatchIntervalMarked> markedBatchIntervals = new BindingList<BatchIntervalMarked>();

            foreach (CurrentCaptureFile file in lastBatchIds)
            {
                if (file.CaptureState == CaptureState.Marked)
                {
                    markedBatchIntervals = pcp.GetMarkedBatchIntervals(file.CaptureBatchId);
                }
                else if(file.CaptureState == CaptureState.Unmarked)
                {
                    unmarkedBatchIntervals = pcp.GetMarkedBatchIntervals(file.CaptureBatchId);
                }
                else
                {
                    MessageBox.Show("Error retrieving batch intervals: capture state is unknown!", "GetMarkedBatchIntervals by CaptureBatchId", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            BatchStatistics markedSingleStats = new BatchStatistics();
            BatchStatistics unmarkedSingleStats = new BatchStatistics();

            // Get this data from DisplayStatistics table; except on refresh...
            //AnalysisEngine ae = new AnalysisEngine();
            //markedSingleStats = ae.GetBatchStatistics(markedBatchIntervals, CaptureState.Marked, BatchType.Single);
            //unmarkedSingleStats = ae.GetBatchStatistics(unmarkedBatchIntervals, CaptureState.Unmarked, BatchType.Single);

            // Load up the table
            // Single unmarked column
            int row = 0;
            _AnalysisDataGridView.Rows[row++].Cells[1].Value = unmarkedSingleStats.IntervalCount;
            _AnalysisDataGridView.Rows[row++].Cells[1].Value = TrimIntervals == true ? unmarkedSingleStats.IntervalCountTrimmed.ToString() : "N/A";
            _AnalysisDataGridView.Rows[row++].Cells[1].Value = string.Format("{0:N2}", unmarkedSingleStats.PacketCountMean);
            _AnalysisDataGridView.Rows[row++].Cells[1].Value = string.Format("{0:N2}", unmarkedSingleStats.PacketCountStandardDeviation);
            _AnalysisDataGridView.Rows[row++].Cells[1].Value = unmarkedSingleStats.PacketCountMinimum;
            _AnalysisDataGridView.Rows[row++].Cells[1].Value = unmarkedSingleStats.PacketCountMaximum;
            _AnalysisDataGridView.Rows[row++].Cells[1].Value = "N/A";
            _AnalysisDataGridView.Rows[row++].Cells[1].Value = "N/A";
            _AnalysisDataGridView.Rows[row++].Cells[1].Value = "N/A";

            // Single marked column
            row = 0;
            _AnalysisDataGridView.Rows[row++].Cells[2].Value = markedSingleStats.IntervalCount;
            _AnalysisDataGridView.Rows[row++].Cells[2].Value = TrimIntervals == true ? markedSingleStats.IntervalCountTrimmed.ToString() : "N/A";
            _AnalysisDataGridView.Rows[row++].Cells[2].Value = string.Format("{0:N2}", markedSingleStats.PacketCountMean);
            _AnalysisDataGridView.Rows[row++].Cells[2].Value = string.Format("{0:N2}", markedSingleStats.PacketCountStandardDeviation);
            _AnalysisDataGridView.Rows[row++].Cells[2].Value = markedSingleStats.PacketCountMinimum;
            _AnalysisDataGridView.Rows[row++].Cells[2].Value = markedSingleStats.PacketCountMaximum;
            _AnalysisDataGridView.Rows[row++].Cells[2].Value = "N/A";
            _AnalysisDataGridView.Rows[row++].Cells[2].Value = "N/A";
            _AnalysisDataGridView.Rows[row++].Cells[2].Value = "N/A";

            // Single variance column
            row = 0;
            _AnalysisDataGridView.Rows[row++].Cells[3].Value = unmarkedSingleStats.IntervalCount - markedSingleStats.IntervalCount;
            _AnalysisDataGridView.Rows[row++].Cells[3].Value = TrimIntervals == true ? (unmarkedSingleStats.IntervalCountTrimmed - markedSingleStats.IntervalCountTrimmed).ToString() : "N/A";
            _AnalysisDataGridView.Rows[row++].Cells[3].Value = string.Format("{0:N2}", (unmarkedSingleStats.PacketCountMean - markedSingleStats.PacketCountMean));
            _AnalysisDataGridView.Rows[row++].Cells[3].Value = string.Format("{0:N2}",(unmarkedSingleStats.PacketCountStandardDeviation - markedSingleStats.PacketCountStandardDeviation));
            _AnalysisDataGridView.Rows[row++].Cells[3].Value = unmarkedSingleStats.PacketCountMinimum - markedSingleStats.PacketCountMinimum;
            _AnalysisDataGridView.Rows[row++].Cells[3].Value = unmarkedSingleStats.PacketCountMaximum - markedSingleStats.PacketCountMaximum;
            _AnalysisDataGridView.Rows[row++].Cells[3].Value = "N/A";
            _AnalysisDataGridView.Rows[row++].Cells[3].Value = "N/A";
            _AnalysisDataGridView.Rows[row++].Cells[3].Value = "N/A";
        }

        private void RefreshCumulativeDataChart()
        {
            // Get the cumulative marked and unmarked batches and add them to the graph
           

            // Format the chart
            //standardSeries.ChartType = SeriesChartType.RangeColumn;
            //standardSeries.BorderWidth = 1;
            //standardSeries.BorderDashStyle = ChartDashStyle.Solid;
            //standardSeries.BorderColor = Color.Black;
            //standardSeries.Color = Color.Blue;
            CumulativeChart.Series.Clear();
            CumulativeChart.Titles.Clear();
            CumulativeChart.Titles.Add("Cumulative Capture Packet Probability Distribution");
            //CumulativeChart.Legends[0].Position.Auto = true; //ElementPosition
            CumulativeChart.Legends[0].IsDockedInsideChartArea = true;
            CumulativeChart.Legends[0].Docking = Docking.Bottom;
            CumulativeChart.Legends[0].Alignment = StringAlignment.Center;
            CumulativeChart.ChartAreas[0].AxisX.Title = "Packets per Interval";
            CumulativeChart.ChartAreas[0].AxisX.Minimum = 0;
            //CumulativeChart.ChartAreas[0].AxisX.Maximum = 

            // Get the type of chart to display
            string chartType = ChartTypeComboBox.Items[ChartTypeComboBox.SelectedIndex].ToString();

            // Marked probabilities series
            CumulativeChart.Series.Add("MarkedProbabilities");
            //CumulativeChart.Series["MarkedProbabilities"].ChartType = SeriesChartType.Line;
            CumulativeChart.Series["MarkedProbabilities"].ChartType = chartType == "Bar" ? SeriesChartType.Column : SeriesChartType.Line;
            CumulativeChart.Series["MarkedProbabilities"].IsVisibleInLegend = true;
            CumulativeChart.Series["MarkedProbabilities"].LegendText = "Marked";

            // Unmarked probabilities series
            CumulativeChart.Series.Add("UnmarkedProbabilities");
            //CumulativeChart.Series["UnmarkedProbabilities"].ChartType = SeriesChartType.Line;
            CumulativeChart.Series["UnmarkedProbabilities"].ChartType = chartType == "Bar" ? SeriesChartType.Column : SeriesChartType.Line;
            CumulativeChart.Series["UnmarkedProbabilities"].IsVisibleInLegend = true;
            CumulativeChart.Series["UnmarkedProbabilities"].LegendText = "Unmarked";

            // Get the cumulative interval counts
            ProcessCapturePackets pcp = new ProcessCapturePackets();
            BindingList<CumulativeInterval> cumulativeIntervals = new BindingList<CumulativeInterval>();
            cumulativeIntervals = pcp.GetCumulativeIntervals();

            // Get the batch intervals
            BindingList<BatchIntervalMarked> unmarkedBatchIntervals = new BindingList<BatchIntervalMarked>();
            BindingList<BatchIntervalMarked> markedBatchIntervals = new BindingList<BatchIntervalMarked>();

            foreach (CumulativeInterval ci in cumulativeIntervals)
            {
                if (ci.Marked)
                {
                    BatchIntervalMarked bim = new BatchIntervalMarked();
                    bim.BatchIntervalId = 0;
                    bim.CaptureBatchId = 0;
                    bim.IntervalNumber = ci.CumulativeIntervalNumber;
                    bim.Marked = CaptureState.Marked;
                    bim.PacketCount = ci.PacketCount;
                    markedBatchIntervals.Add(bim);
                }
                else
                {
                    BatchIntervalMarked bim = new BatchIntervalMarked();
                    bim.BatchIntervalId = 0;
                    bim.CaptureBatchId = 0;
                    bim.IntervalNumber = ci.CumulativeIntervalNumber;
                    bim.Marked = CaptureState.Unmarked;
                    bim.PacketCount = ci.PacketCount;
                    unmarkedBatchIntervals.Add(bim);
                }
            }

            int histogramBinSize = Convert.ToInt32(HistogramBinSize);
            //SortedDictionary<int, decimal> markedProbabilities = new CalculateProbability(markedBatchIntervals).GetProbabilityByPacketRange(_trimZeroPacketIntervals, histogramBinSize);
            //SortedDictionary<int, decimal> unmarkedProbabilities = new CalculateProbability(unmarkedBatchIntervals).GetProbabilityByPacketRange(_trimZeroPacketIntervals, histogramBinSize);
            _CumulativeMarkedProbabilities = new CalculateProbability(markedBatchIntervals).GetProbabilityByPacketRange(_trimZeroPacketIntervals, histogramBinSize);
            _CumulativeUnmarkedProbabilities = new CalculateProbability(unmarkedBatchIntervals).GetProbabilityByPacketRange(_trimZeroPacketIntervals, histogramBinSize);

            CumulativeChart.Series["MarkedProbabilities"].Color = Color.CornflowerBlue;

            foreach (KeyValuePair<int, decimal> pair in _CumulativeMarkedProbabilities)
            {
                CumulativeChart.Series["MarkedProbabilities"].Points.AddXY(Convert.ToDouble(pair.Key), Convert.ToDouble(pair.Value));
            }

            CumulativeChart.Series["UnmarkedProbabilities"].Color = Color.Red;

            foreach (KeyValuePair<int, decimal> pair in _CumulativeUnmarkedProbabilities)
            {
                CumulativeChart.Series["UnmarkedProbabilities"].Points.AddXY(Convert.ToDouble(pair.Key), Convert.ToDouble(pair.Value));
            }
        }
        private void RefreshCumulativeBatchStatistics()
        {
            // Get the cumulative interval counts
            ProcessCapturePackets pcp = new ProcessCapturePackets();
            BindingList<CumulativeInterval> cumulativeIntervals = new BindingList<CumulativeInterval>();
            cumulativeIntervals = pcp.GetCumulativeIntervals();

            // Get the batch intervals
            BindingList<BatchIntervalMarked> unmarkedBatchIntervals = new BindingList<BatchIntervalMarked>();
            BindingList<BatchIntervalMarked> markedBatchIntervals = new BindingList<BatchIntervalMarked>();

            foreach (CumulativeInterval ci in cumulativeIntervals)
            {
                if (ci.Marked)
                {
                    BatchIntervalMarked bim = new BatchIntervalMarked();
                    bim.BatchIntervalId = 0;
                    bim.CaptureBatchId = 0;
                    bim.IntervalNumber = ci.CumulativeIntervalNumber;
                    bim.Marked = CaptureState.Marked;
                    bim.PacketCount = ci.PacketCount;
                    markedBatchIntervals.Add(bim);
                }
                else
                {
                    BatchIntervalMarked bim = new BatchIntervalMarked();
                    bim.BatchIntervalId = 0;
                    bim.CaptureBatchId = 0;
                    bim.IntervalNumber = ci.CumulativeIntervalNumber;
                    bim.Marked = CaptureState.Unmarked;
                    bim.PacketCount = ci.PacketCount;
                    unmarkedBatchIntervals.Add(bim);
                }
            }

            BatchStatistics markedCumulativeStats = new BatchStatistics();
            BatchStatistics unmarkedCumulativeStats = new BatchStatistics();
            decimal markedMeanOfMeans = 0;
            decimal markedStdDevMeanOfMeans = 0;
            decimal unmarkedMeanOfMeans = 0;
            decimal unmarkedStdDevMeanOfMeans = 0;

            AnalysisEngine ae = new AnalysisEngine();

            if(markedBatchIntervals.Count > 0)
            {
                markedCumulativeStats = ae.CalculateBatchStatistics(markedBatchIntervals,CaptureState.Marked, BatchType.Cumulative);
                markedMeanOfMeans = pcp.CalculateMeanOfMeans(CaptureState.Marked, TrimIntervals ? true : false);
                markedStdDevMeanOfMeans = pcp.CalculateStdDevForMeanOfMeans(CaptureState.Marked, TrimIntervals ? true : false);

                // Load up the table
                // Cumulative marked column
                int row = 0;
                _AnalysisDataGridView.Rows[row++].Cells[5].Value = markedCumulativeStats.IntervalCount;
                _AnalysisDataGridView.Rows[row++].Cells[5].Value = TrimIntervals == true ? markedCumulativeStats.IntervalCountTrimmed.ToString() : "N/A";
                _AnalysisDataGridView.Rows[row++].Cells[5].Value = string.Format("{0:N2}", markedCumulativeStats.PacketCountMean);
                _AnalysisDataGridView.Rows[row++].Cells[5].Value = string.Format("{0:N2}", markedCumulativeStats.PacketCountStandardDeviation);
                _AnalysisDataGridView.Rows[row++].Cells[5].Value = markedCumulativeStats.PacketCountMinimum;
                _AnalysisDataGridView.Rows[row++].Cells[5].Value = markedCumulativeStats.PacketCountMaximum;
                _AnalysisDataGridView.Rows[row++].Cells[5].Value = string.Format("{0:N2}", markedMeanOfMeans);
                _AnalysisDataGridView.Rows[row++].Cells[5].Value = "N/A";
                _AnalysisDataGridView.Rows[row++].Cells[5].Value = "N/A";
            }

            if (unmarkedBatchIntervals.Count > 0)
            {
                unmarkedCumulativeStats = ae.CalculateBatchStatistics(unmarkedBatchIntervals, CaptureState.Marked, BatchType.Cumulative);
                unmarkedMeanOfMeans = pcp.CalculateMeanOfMeans(CaptureState.Unmarked, TrimIntervals ? true : false);
                unmarkedStdDevMeanOfMeans = pcp.CalculateStdDevForMeanOfMeans(CaptureState.Unmarked, TrimIntervals ? true : false);

                // Load up the table
                // Cumulative unmarked column
                int row = 0;
                _AnalysisDataGridView.Rows[row++].Cells[4].Value = unmarkedCumulativeStats.IntervalCount;
                _AnalysisDataGridView.Rows[row++].Cells[4].Value = TrimIntervals == true ? unmarkedCumulativeStats.IntervalCountTrimmed.ToString() : "N/A";
                _AnalysisDataGridView.Rows[row++].Cells[4].Value = string.Format("{0:N2}", unmarkedCumulativeStats.PacketCountMean);
                _AnalysisDataGridView.Rows[row++].Cells[4].Value = string.Format("{0:N2}", unmarkedCumulativeStats.PacketCountStandardDeviation);
                _AnalysisDataGridView.Rows[row++].Cells[4].Value = unmarkedCumulativeStats.PacketCountMinimum;
                _AnalysisDataGridView.Rows[row++].Cells[4].Value = unmarkedCumulativeStats.PacketCountMaximum;
                _AnalysisDataGridView.Rows[row++].Cells[4].Value = string.Format("{0:N2}", unmarkedMeanOfMeans);
                _AnalysisDataGridView.Rows[row++].Cells[4].Value = "N/A";
                _AnalysisDataGridView.Rows[row++].Cells[4].Value = "N/A";
            }


            if (markedBatchIntervals.Count > 0 && unmarkedBatchIntervals.Count > 0)
            {
                // Cumulative variance column
                int row = 0;
                _AnalysisDataGridView.Rows[row++].Cells[6].Value = unmarkedCumulativeStats.IntervalCount - markedCumulativeStats.IntervalCount;
                _AnalysisDataGridView.Rows[row++].Cells[6].Value = TrimIntervals == true ? (unmarkedCumulativeStats.IntervalCountTrimmed - markedCumulativeStats.IntervalCountTrimmed).ToString() : "N/A";
                _AnalysisDataGridView.Rows[row++].Cells[6].Value = string.Format("{0:N2}", (unmarkedCumulativeStats.PacketCountMean - markedCumulativeStats.PacketCountMean));
                _AnalysisDataGridView.Rows[row++].Cells[6].Value = string.Format("{0:N2}", (unmarkedCumulativeStats.PacketCountStandardDeviation - markedCumulativeStats.PacketCountStandardDeviation));
                _AnalysisDataGridView.Rows[row++].Cells[6].Value = unmarkedCumulativeStats.PacketCountMinimum - markedCumulativeStats.PacketCountMinimum;
                _AnalysisDataGridView.Rows[row++].Cells[6].Value = unmarkedCumulativeStats.PacketCountMaximum - markedCumulativeStats.PacketCountMaximum;
                _AnalysisDataGridView.Rows[row++].Cells[6].Value = string.Format("{0:N2}", (unmarkedMeanOfMeans - markedMeanOfMeans));
                _AnalysisDataGridView.Rows[row++].Cells[6].Value = "N/A";
                _AnalysisDataGridView.Rows[row++].Cells[6].Value = "N/A";
            }

            // Update the K-S statistics object
            _KsStatistics.MarkedMean = markedMeanOfMeans;
            _KsStatistics.MarkedStdDev = markedStdDevMeanOfMeans;
            //_KsStatistics.MarkedMean = markedCumulativeStats.PacketCountMean;
            //_KsStatistics.MarkedStdDev = markedCumulativeStats.PacketCountStandardDeviation;
            _KsStatistics.MarkedIntervalCount = TrimIntervals == true ? markedCumulativeStats.IntervalCountTrimmed : markedCumulativeStats.IntervalCount;
            _KsStatistics.UnmarkedMean = unmarkedMeanOfMeans;
            _KsStatistics.UnmarkedStdDev = unmarkedStdDevMeanOfMeans;
            //_KsStatistics.UnmarkedMean = unmarkedCumulativeStats.PacketCountMean;
            //_KsStatistics.UnmarkedStdDev = unmarkedCumulativeStats.PacketCountStandardDeviation;
            _KsStatistics.UnmarkedIntervalCount = TrimIntervals == true ? unmarkedCumulativeStats.IntervalCountTrimmed : unmarkedCumulativeStats.IntervalCount;
            
        }
        private void RefreshCumulativeProbabilityChart()
        {
            // Get the cumulative probabilities for marked and unmarked batches and add them to the graph

            // Format the chart
            //standardSeries.ChartType = SeriesChartType.RangeColumn;
            //standardSeries.BorderWidth = 1;
            //standardSeries.BorderDashStyle = ChartDashStyle.Solid;
            //standardSeries.BorderColor = Color.Black;
            //standardSeries.Color = Color.Blue;
            CdfChart.Series.Clear();
            CdfChart.Titles.Clear();
            CdfChart.Titles.Add("Cumulative Probability Distribution");
            //CdfChart.Legends[0].Position.Auto = true; //ElementPosition
            CdfChart.Legends[0].IsDockedInsideChartArea = true;
            CdfChart.Legends[0].Docking = Docking.Bottom;
            CdfChart.Legends[0].Alignment = StringAlignment.Center;
            CdfChart.ChartAreas[0].AxisX.Title = "Packets per Interval";
            CdfChart.ChartAreas[0].AxisX.Minimum = 0;
            //CumulativeChart.ChartAreas[0].AxisX.Maximum = 

            // Get the type of chart to display
            string chartType = ChartTypeComboBox.Items[ChartTypeComboBox.SelectedIndex].ToString();

            // Marked probabilities series
            CdfChart.Series.Add("MarkedProbabilities");
            CdfChart.Series["MarkedProbabilities"].ChartType = SeriesChartType.Line;
            //CdfChart.Series["MarkedProbabilities"].ChartType = chartType == "Bar" ? SeriesChartType.Column : SeriesChartType.Line;
            CdfChart.Series["MarkedProbabilities"].IsVisibleInLegend = true;
            CdfChart.Series["MarkedProbabilities"].LegendText = "Marked";

            // Unmarked probabilities series
            CdfChart.Series.Add("UnmarkedProbabilities");
            CdfChart.Series["UnmarkedProbabilities"].ChartType = SeriesChartType.Line;
            //CdfChart.Series["UnmarkedProbabilities"].ChartType = chartType == "Bar" ? SeriesChartType.Column : SeriesChartType.Line;
            CdfChart.Series["UnmarkedProbabilities"].IsVisibleInLegend = true;
            CdfChart.Series["UnmarkedProbabilities"].LegendText = "Unmarked";

            // Get the batch intervals
            BindingList<BatchIntervalMarked> unmarkedBatchIntervals = new BindingList<BatchIntervalMarked>();
            BindingList<BatchIntervalMarked> markedBatchIntervals = new BindingList<BatchIntervalMarked>();

            //int histogramBinSize = Convert.ToInt32(HistogramBinSizeTextBox.Text);
            SortedDictionary<int, decimal> markedProbabilities = new CalculateProbability(markedBatchIntervals).GetCumulativeProbabilityDistribution(_CumulativeMarkedProbabilities);
            SortedDictionary<int, decimal> unmarkedProbabilities = new CalculateProbability(unmarkedBatchIntervals).GetCumulativeProbabilityDistribution(_CumulativeUnmarkedProbabilities);

            CdfChart.Series["MarkedProbabilities"].Color = Color.CornflowerBlue;

            foreach (KeyValuePair<int, decimal> pair in markedProbabilities)
            {
                CdfChart.Series["MarkedProbabilities"].Points.AddXY(Convert.ToDouble(pair.Key), Convert.ToDouble(pair.Value));
            }

            CdfChart.Series["UnmarkedProbabilities"].Color = Color.Red;

            foreach (KeyValuePair<int, decimal> pair in unmarkedProbabilities)
            {
                CdfChart.Series["UnmarkedProbabilities"].Points.AddXY(Convert.ToDouble(pair.Key), Convert.ToDouble(pair.Value));
            }
        }
        private void RefreshKsStatistics()
        {
            // Reset the backcolor for Reject H0? cell in K-S column of grid
            _AnalysisDataGridView.Rows[8].Cells[7].Style.BackColor = Color.White;

            //bool KS_result = GetHypothesisTestResult(_KsStatistics.UnmarkedMean, _KsStatistics.MarkedMean, _KsStatistics.UnmarkedStdDev, _KsStatistics.MarkedStdDev, _KsStatistics.UnmarkedIntervalCount, _KsStatistics.MarkedIntervalCount);
            bool KS_result = GetHypothesisTestResult(_KsStatistics.UnmarkedMean, _KsStatistics.MarkedMean, _KsStatistics.UnmarkedStdDev, _KsStatistics.MarkedStdDev, _KsStatistics.UnmarkedIntervalCount, _KsStatistics.MarkedIntervalCount);
            int row = 0;
            _AnalysisDataGridView.Rows[row++].Cells[7].Value = "N/A";
            _AnalysisDataGridView.Rows[row++].Cells[7].Value = "N/A";
            _AnalysisDataGridView.Rows[row++].Cells[7].Value = string.Format("{0:N2}", _KsStatistics.MeanDifference);
            _AnalysisDataGridView.Rows[row++].Cells[7].Value = string.Format("{0:N2}",_KsStatistics.StandardDeviation);
            _AnalysisDataGridView.Rows[row++].Cells[7].Value = "N/A";
            _AnalysisDataGridView.Rows[row++].Cells[7].Value = "N/A";
            _AnalysisDataGridView.Rows[row++].Cells[7].Value = string.Format("{0:N2}",_KsStatistics.MeanDifference);
            _AnalysisDataGridView.Rows[row++].Cells[7].Value = string.Format("{0:P1}", _alpha);
            Font font = new Font(_AnalysisDataGridView.DefaultCellStyle.Font.FontFamily, _AnalysisDataGridView.Font.Size, FontStyle.Bold);
            _AnalysisDataGridView.Rows[row].Cells[7].Style.Font = font;
            _AnalysisDataGridView.Rows[row].Cells[7].Value = KS_result.ToString();
            _AnalysisDataGridView.Rows[row].Cells[7].Style.BackColor = KS_result == true ? Color.LightGreen : Color.LightCoral;
        }
        private bool GetHypothesisTestResult(decimal unmarkedMean, decimal markedMean, decimal unmarkedStdDev, decimal markedStdDev, int unmarkedPacketCount, int markedPacketCount)
        {
            bool result = false;

            // H0: there is no difference in the distribution of packets between marked and unmarked batches
            // H1: there is a difference between the batches

            // Test the difference in the distribution means
            decimal meanDifference = _KsStatistics.MeanDifference;
            decimal sigmaDifference = _KsStatistics.SigmaDifference;

            // Single-tail test (if there is a difference in the means it will be a positive value)
            // Z value for alpha = 5% significance level:

            // Test result: true = reject H0 - difference of means has only 5% probability of occurring if H0 is true
            result = _KsStatistics.MeanDifference > _KsStatistics.StandardDeviation ? true : false;

            return result;
        }
        #endregion

        #region Public Methods
        public void RefreshData()
        {
            RefreshSingleDataChart();
            RefreshCumulativeDataChart();
            RefreshSingleBatchStatistics();
            RefreshCumulativeBatchStatistics();
            RefreshCumulativeProbabilityChart();
            RefreshKsStatistics();
        }
        
        #endregion

    }
}
