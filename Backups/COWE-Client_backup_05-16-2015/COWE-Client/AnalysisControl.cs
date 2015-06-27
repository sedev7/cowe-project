using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COWE.Client
{
    public partial class AnalysisControl : UserControl
    {
        #region Global Variables
        DataGridView _AnalysisDataGridView = new DataGridView();
        int _MaxGridDisplayRows = 9;
        int _MaxGridHeight = 320;
        #endregion
        #region Constructor
        public AnalysisControl()
        {
            InitializeComponent();
            //InitializeAnalysisDataGridView();
        }
        #endregion

        #region Event Handlers
        private void AnalysisControl_Load(object sender, EventArgs e)
        {
            InitializeAnalysisDataGridView();
        }
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
            statisticName.Width = 120;
            statisticName.ReadOnly = true;
            _AnalysisDataGridView.Columns.Insert(col++, statisticName);

            // Create single baseline column
            DataGridViewTextBoxColumn baselineSingle = new DataGridViewTextBoxColumn();
            baselineSingle.Name = "BaselineSingle";
            baselineSingle.DataPropertyName = "BaselineSingle";
            baselineSingle.HeaderText = "Single Baseline";
            baselineSingle.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            baselineSingle.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            baselineSingle.Width = 100;
            baselineSingle.ReadOnly = true;
            _AnalysisDataGridView.Columns.Insert(col++, baselineSingle);

            // Create single marked column
            DataGridViewTextBoxColumn markedSingle = new DataGridViewTextBoxColumn();
            markedSingle.Name = "MarkedSingle";
            markedSingle.DataPropertyName = "MarkedSingle";
            markedSingle.HeaderText = "Single Marked";
            markedSingle.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            markedSingle.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            markedSingle.Width = 100;
            markedSingle.ReadOnly = true;
            _AnalysisDataGridView.Columns.Insert(col++, markedSingle);

            // Create single variance column
            DataGridViewTextBoxColumn varianceSingle = new DataGridViewTextBoxColumn();
            varianceSingle.Name = "VarianceSingle";
            varianceSingle.DataPropertyName = "VarianceSingle";
            varianceSingle.HeaderText = "Single Variance";
            varianceSingle.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            varianceSingle.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            varianceSingle.Width = 100;
            varianceSingle.ReadOnly = true;
            _AnalysisDataGridView.Columns.Insert(col++, varianceSingle);

            // Create cumulative baseline column
            DataGridViewTextBoxColumn baselineCumulative = new DataGridViewTextBoxColumn();
            baselineCumulative.Name = "BaselineCumulative";
            baselineCumulative.DataPropertyName = "BaselineCumulative";
            baselineCumulative.HeaderText = "Cumulative Baseline";
            baselineCumulative.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            baselineCumulative.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            baselineCumulative.Width = 100;
            baselineCumulative.ReadOnly = true;
            _AnalysisDataGridView.Columns.Insert(col++, baselineCumulative);

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
            ksStatistic.Width = 100;
            ksStatistic.ReadOnly = true;
            _AnalysisDataGridView.Columns.Insert(col++, ksStatistic);

            // Initialize the row
            //DataGridViewRow countGridRow = new DataGridViewRow();
            //_AnalysisDataGridView.Rows.Add();
            //_AnalysisDataGridView.Rows[row].Height = 30;

            DataTable dt = new DataTable();
            dt.Columns.Add("StatisticName");
            dt.Columns.Add("BaselineSingle");
            dt.Columns.Add("MarkedSingle");
            dt.Columns.Add("VarianceSingle");
            dt.Columns.Add("BaselineCumulative");
            dt.Columns.Add("MarkedCumulative");
            dt.Columns.Add("VarianceCumulative");
            dt.Columns.Add("KSStatistic");
            dt.Rows.Add("Count", "0", "0", "0", "0", "0", "0", "0");
            dt.Rows.Add("Mean", "0", "0", "0", "0", "0", "0", "0");
            dt.Rows.Add("Std Dev", "0", "0", "0", "0", "0", "0", "0");
            dt.Rows.Add("Min", "0", "0", "0", "0", "0", "0", "0");
            dt.Rows.Add("Max", "0", "0", "0", "0", "0", "0", "0");
            dt.Rows.Add("Mean of Means", "0", "0", "0", "0", "0", "0", "0");
            dt.Rows.Add("Alpha", "0", "0", "0", "0", "0", "0", "0");
            dt.Rows.Add("Reject H0?", "0", "0", "0", "0", "0", "0", "0");
            
            // Add the DataTable to the grid
            _AnalysisDataGridView.AutoGenerateColumns = false;
            _AnalysisDataGridView.DataSource = dt;
            
            //// Adjust the row height
            //foreach (DataGridViewRow r in _AnalysisDataGridView.Rows)
            //{
            //    r.Height = 30;
            //}
            
            _AnalysisDataGridView.Refresh();



            //// Add data to the first row
            ////row = _AnalysisDataGridView.Rows.Count - 1; // Next row
            //col = 0;
            //row++;
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "Count";           // Statistic Name
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Baseline
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Marked
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Variance
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Baseline
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Marked
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Variance
            //_AnalysisDataGridView.Rows[row].Cells[col].Value = "0";                 // K-S Statistic
            ////_AnalysisDataGridView.Rows[row].Cells[col].Style.BackColor = NotConnected;

            //row++;
            //col = 0;
            //DataGridViewRow meanGridRow = new DataGridViewRow();
            //_AnalysisDataGridView.Rows.Add();
            //_AnalysisDataGridView.Rows[row].Height = 30;
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "Mean";           // Statistic Name
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Baseline
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Marked
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Variance
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Baseline
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Marked
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Variance
            //_AnalysisDataGridView.Rows[row].Cells[col].Value = "0";                 // K-S Statistic

            //row++;
            //col = 0;
            //DataGridViewRow stdDevGridRow = new DataGridViewRow();
            //_AnalysisDataGridView.Rows.Add();
            //_AnalysisDataGridView.Rows[row].Height = 30;
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "Std Dev";           // Statistic Name
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Baseline
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Marked
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Variance
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Baseline
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Marked
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Variance
            //_AnalysisDataGridView.Rows[row].Cells[col].Value = "0";                 // K-S Statistic

            //row++;
            //col = 0;
            //DataGridViewRow minGridRow = new DataGridViewRow();
            //_AnalysisDataGridView.Rows.Add();
            //_AnalysisDataGridView.Rows[row].Height = 30;
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "Min";           // Statistic Name
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Baseline
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Marked
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Variance
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Baseline
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Marked
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Variance
            //_AnalysisDataGridView.Rows[row].Cells[col].Value = "0";                 // K-S Statistic

            //row++;
            //col = 0;
            //DataGridViewRow maxGridRow = new DataGridViewRow();
            //_AnalysisDataGridView.Rows.Add();
            //_AnalysisDataGridView.Rows[row].Height = 30;
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "Max";           // Statistic Name
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Baseline
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Marked
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Variance
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Baseline
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Marked
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Variance
            //_AnalysisDataGridView.Rows[row].Cells[col].Value = "0";                 // K-S Statistic

            //row++;
            //col = 0;
            //DataGridViewRow meanOfMeansGridRow = new DataGridViewRow();
            //_AnalysisDataGridView.Rows.Add();
            //_AnalysisDataGridView.Rows[row].Height = 30;
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "Mean of Means";           // Statistic Name
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Baseline
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Marked
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Variance
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Baseline
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Marked
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Variance
            //_AnalysisDataGridView.Rows[row].Cells[col].Value = "0";                 // K-S Statistic

            //row++;
            //col = 0;
            //DataGridViewRow alphaGridRow = new DataGridViewRow();
            //_AnalysisDataGridView.Rows.Add();
            //_AnalysisDataGridView.Rows[row].Height = 30;
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "Alpha";           // Statistic Name
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Baseline
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Marked
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Variance
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Baseline
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Marked
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Variance
            //_AnalysisDataGridView.Rows[row].Cells[col].Value = "0";                 // K-S Statistic

            //row++;
            //col = 0;
            //DataGridViewRow rejectH0GridRow = new DataGridViewRow();
            //_AnalysisDataGridView.Rows.Add();
            //_AnalysisDataGridView.Rows[row].Height = 30;
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "Reject H0?";           // Statistic Name
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Baseline
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Marked
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Single Variance
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Baseline
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Marked
            //_AnalysisDataGridView.Rows[row].Cells[col++].Value = "0";               // Cumulative Variance
            //_AnalysisDataGridView.Rows[row].Cells[col].Value = "0";                 // K-S Statistic

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
        #endregion


    }
}
