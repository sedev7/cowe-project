using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.DataLayer;
using COWE.DomainClasses;
using COWE.Enumerations;

namespace COWE.BusinessLayer
{
   
    public class AnalysisEngine
    {
        #region Global Variables
        bool _TrimZeroPacketIntervals = false;
        int _HistogramBinSize = 0;
        string _CaptureFileName = string.Empty;

        BatchType _BatchType = BatchType.Unknown;
        CaptureState _CaptureState = CaptureState.Unknown;
        HypothesisTest _HypothesisTest = HypothesisTest.Unknown;
        #endregion

        #region Constructors
        public AnalysisEngine() { }

        public AnalysisEngine(bool trimZeroPacketIntervals, int histogramBinSize)
        {
            this._HistogramBinSize = histogramBinSize;
            this._TrimZeroPacketIntervals = trimZeroPacketIntervals;
        }

        public AnalysisEngine(bool trimZeroPacketIntervals, int histogramBinSize, HypothesisTest hypothesisTest, string captureFileName, CaptureState captureState)
        {
            this._HistogramBinSize = histogramBinSize;
            this._TrimZeroPacketIntervals = trimZeroPacketIntervals;
            this._CaptureFileName = captureFileName;
            this._CaptureState = captureState;
            this._HypothesisTest = hypothesisTest;
        }
        #endregion

        #region Public Methods
        /*******************************************************************************************
         *
         * Need to update cumulative intervals, but need to know if file has already been parsed
         * first.
         * 
         * - Add a "Parsed" field to CaptureBatch table with default = 0, and update to 1 after file has been parsed.
         * - Add a "Cumulative" field to CaptureBatch table with default = 0, and update to 1 after file has been added to cumulative totals.
         * 
         * Note: may also need a ParseEngine - see Client.ProcessCaptureDataButton_Click event.
         * 
         *******************************************************************************************/

        public bool CalculateSingleBatchStatistics()
        {
            bool result = false;

            // Calculate single batch statistics
            //// Get the last marked and unmarked batches and add them to the graph
            //int lastBatchId = 0;
            ProcessCapturePackets pcp = new ProcessCapturePackets();
            //lastBatchId = pcp.GetLastCaptureBatchId();
            CurrentCaptureFile captureFile = new CurrentCaptureFile();
            CaptureFileData cfd = new CaptureFileData();
            //captureFile = cfd.GetLastCaptureBatchRecord();
            captureFile = cfd.GetCurrentCaptureFile(_CaptureFileName);

            // Set the global variable
            _CaptureState = captureFile.Marked;

            BindingList<BatchIntervalMarked> batchIntervals = new BindingList<BatchIntervalMarked>();

            // Calculate probabilities
            batchIntervals = pcp.GetMarkedBatchIntervals(captureFile.CaptureBatchId);
            int histogramBinSize = Convert.ToInt32(_HistogramBinSize);
            SortedDictionary<int, decimal> probabilities = new CalculateProbability(batchIntervals).GetProbabilityByPacketRange(_TrimZeroPacketIntervals, histogramBinSize);

            BatchStatistics markedSingleStats = new BatchStatistics();
            BatchStatistics unmarkedSingleStats = new BatchStatistics();

            // Add the results to the DisplayStatistics table
            DisplayStatisticsData dsd = new DisplayStatisticsData();
            if(captureFile.Marked == CaptureState.Marked)
            {
                markedSingleStats = CalculateBatchStatistics(batchIntervals, CaptureState.Marked, BatchType.Single);
            }
            else
            {
                unmarkedSingleStats = CalculateBatchStatistics(batchIntervals, CaptureState.Unmarked, BatchType.Single);
            }
            return result;
        }

        public bool CalculateSingleBatchStatisticsForLastTwoBatches()
        {
            bool result = false;

            // Calculate single batch statistics
            // Get the last marked and unmarked batches and add them to the graph
            BindingList<CurrentCaptureFile> lastBatchIds = new BindingList<CurrentCaptureFile>();
            ProcessCapturePackets pcp = new ProcessCapturePackets();
            lastBatchIds = pcp.GetLastCaptureBatchIds();

            foreach (CurrentCaptureFile file in lastBatchIds)
            {
                BindingList<BatchIntervalMarked> batchIntervals = new BindingList<BatchIntervalMarked>();

                // Calculate probabilities
                batchIntervals = pcp.GetMarkedBatchIntervals(file.CaptureBatchId);
                int histogramBinSize = Convert.ToInt32(_HistogramBinSize);
                SortedDictionary<int, decimal> probabilities = new CalculateProbability(batchIntervals).GetProbabilityByPacketRange(_TrimZeroPacketIntervals, histogramBinSize);
            }

            return result;
        }

        //public bool CalculateBatchStatistics()
        public bool CalculateCumulativeBatchStatistics()
        {
            bool result = false;

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
            //decimal markedMeanOfMeans = 0;
            //decimal markedStdDevMeanOfMeans = 0;
            //decimal unmarkedMeanOfMeans = 0;
            //decimal unmarkedStdDevMeanOfMeans = 0;

            /********************************************************************************************
             * 
             * Note: must have at least two samples to calculate mean of means and mean of means std dev...
             * 
             * ******************************************************************************************/

            /* TO DO: - need to retrieve std dev value for batch for cumulative stats when only one batch
             *          has been processed.
             *        - Have to calculate CumulativeStats first, then update MeanOfMeans and StdDev
             */

            if (_CaptureState == CaptureState.Marked && markedBatchIntervals.Count > 0)
            {
                markedCumulativeStats = CalculateBatchStatistics(markedBatchIntervals, CaptureState.Marked, BatchType.Cumulative);
            }

            if (_CaptureState == CaptureState.Unmarked && unmarkedBatchIntervals.Count > 0)
            {
                unmarkedCumulativeStats = CalculateBatchStatistics(unmarkedBatchIntervals, CaptureState.Unmarked, BatchType.Cumulative);
            }

            if (pcp.GetMeanCount() > 1)
            {
                if (_CaptureState == CaptureState.Marked && markedBatchIntervals.Count > 0)
                {
                    //markedMeanOfMeans = pcp.CalculateMeanOfMeans(CaptureState.Marked, AnalysisConfiguration.TrimSmallPackets ? true : false);
                    markedCumulativeStats.MeanOfMeans = pcp.CalculateMeanOfMeans(CaptureState.Marked, AnalysisConfiguration.TrimSmallPackets ? true : false);
                    //markedStdDevMeanOfMeans = pcp.CalculateStdDevForMeanOfMeans(CaptureState.Marked, AnalysisConfiguration.TrimSmallPackets ? true : false);
                    markedCumulativeStats.MeanOfMeansStandardDeviation = pcp.CalculateStdDevForMeanOfMeans(CaptureState.Marked, AnalysisConfiguration.TrimSmallPackets ? true : false);
                    SaveDisplayStatistics(markedCumulativeStats, pcp.GetCaptureBatchId(_CaptureFileName), CaptureState.Marked, BatchType.Cumulative, true);
                }

                if (_CaptureState == CaptureState.Unmarked && unmarkedBatchIntervals.Count > 0)
                {
                    //unmarkedMeanOfMeans = pcp.CalculateMeanOfMeans(CaptureState.Unmarked, AnalysisConfiguration.TrimSmallPackets ? true : false);
                    unmarkedCumulativeStats.MeanOfMeans = pcp.CalculateMeanOfMeans(CaptureState.Unmarked, AnalysisConfiguration.TrimSmallPackets ? true : false);
                    //unmarkedStdDevMeanOfMeans = pcp.CalculateStdDevForMeanOfMeans(CaptureState.Unmarked, AnalysisConfiguration.TrimSmallPackets ? true : false);
                    unmarkedCumulativeStats.MeanOfMeansStandardDeviation = pcp.CalculateStdDevForMeanOfMeans(CaptureState.Unmarked, AnalysisConfiguration.TrimSmallPackets ? true : false);
                    SaveDisplayStatistics(unmarkedCumulativeStats, pcp.GetCaptureBatchId(_CaptureFileName), CaptureState.Unmarked, BatchType.Cumulative, true);
                }
            }
            else
            {
                // Only one batch - use the mean and standard deviation from the first batch
                if (_CaptureState == CaptureState.Marked && markedBatchIntervals.Count > 0)
                {
                    //markedMeanOfMeans = pcp.GetMean(CaptureState.Marked, _TrimZeroPacketIntervals);
                    //markedMeanOfMeans = markedCumulativeStats.PacketCountMean;
                    markedCumulativeStats.MeanOfMeans = markedCumulativeStats.PacketCountMean;
                    //markedStdDevMeanOfMeans = markedCumulativeStats.PacketCountStandardDeviation;
                    markedCumulativeStats.MeanOfMeansStandardDeviation = markedCumulativeStats.PacketCountStandardDeviation;
                    SaveDisplayStatistics(markedCumulativeStats, pcp.GetCaptureBatchId(_CaptureFileName), CaptureState.Marked, BatchType.Cumulative, true);
                }

                if (_CaptureState == CaptureState.Unmarked && unmarkedBatchIntervals.Count > 0)
                {
                    //unmarkedMeanOfMeans = pcp.GetMean(CaptureState.Unmarked, _TrimZeroPacketIntervals);
                    //unmarkedMeanOfMeans = unmarkedCumulativeStats.PacketCountMean;
                    unmarkedCumulativeStats.MeanOfMeans = unmarkedCumulativeStats.PacketCountMean;
                    //unmarkedStdDevMeanOfMeans = unmarkedCumulativeStats.PacketCountStandardDeviation;
                    unmarkedCumulativeStats.MeanOfMeansStandardDeviation = unmarkedCumulativeStats.PacketCountStandardDeviation;
                    SaveDisplayStatistics(unmarkedCumulativeStats, pcp.GetCaptureBatchId(_CaptureFileName), CaptureState.Unmarked, BatchType.Cumulative, true);
                }
            }


            // public bool GetHypothesisTestResult()
            // NOTE: use _HypothesisTest variable to determine which test result to return
            //if (markedBatchIntervals.Count > 0 && unmarkedBatchIntervals.Count > 0)
            //{
            //    // Cumulative variance column

            //}

            //// Update the K-S statistics object
            //_KsStatistics.MarkedMean = markedMeanOfMeans;
            //_KsStatistics.MarkedStdDev = markedStdDevMeanOfMeans;
            ////_KsStatistics.MarkedMean = markedCumulativeStats.PacketCountMean;
            ////_KsStatistics.MarkedStdDev = markedCumulativeStats.PacketCountStandardDeviation;
            //_KsStatistics.MarkedIntervalCount = TrimIntervalsCheckBox.Checked == true ? markedCumulativeStats.IntervalCountTrimmed : markedCumulativeStats.IntervalCount;
            //_KsStatistics.UnmarkedMean = unmarkedMeanOfMeans;
            //_KsStatistics.UnmarkedStdDev = unmarkedStdDevMeanOfMeans;
            ////_KsStatistics.UnmarkedMean = unmarkedCumulativeStats.PacketCountMean;
            ////_KsStatistics.UnmarkedStdDev = unmarkedCumulativeStats.PacketCountStandardDeviation;
            //_KsStatistics.UnmarkedIntervalCount = TrimIntervalsCheckBox.Checked == true ? unmarkedCumulativeStats.IntervalCountTrimmed : unmarkedCumulativeStats.IntervalCount;

            return result;
        }

        public BatchStatistics CalculateBatchStatistics(BindingList<BatchIntervalMarked> batchIntervals, CaptureState captureState, BatchType batchType)
        {
            decimal batchIntervalsMean = 0;
            decimal batchIntervalsTrimmedMean = 0;

            // Trim zero packets from the batch interval
            BindingList<BatchIntervalMarked> batchIntervalsTrimmed = new BindingList<BatchIntervalMarked>();
            foreach (BatchIntervalMarked bim in batchIntervals)
            {
                if (bim.PacketCount > AnalysisConfiguration.HistogramBinSize)
                {
                    batchIntervalsTrimmed.Add(bim);
                }
            }

            // Calculate statistics for the batch
            BatchStatistics bs = new BatchStatistics();
            double varianceSum = 0;
            double variance = 0;

            if (AnalysisConfiguration.TrimSmallPackets)
            {
                bs.IntervalCountTrimmed = batchIntervalsTrimmed.Count;
                var maxValue = (from t in batchIntervalsTrimmed select t.PacketCount).Max();
                var minValue = (from t in batchIntervalsTrimmed select t.PacketCount).Min();
                var meanValue = (from t in batchIntervalsTrimmed select t.PacketCount).Average();

                // Calculate standard deviation
                var packets = (from t in batchIntervalsTrimmed select t.PacketCount).ToList();
                var packetAverage = packets.Sum() / (double)batchIntervalsTrimmed.Count;

                foreach (var item in packets)
                {
                    double packetVariance = Math.Pow((Convert.ToDouble(item) - Convert.ToDouble(packetAverage)), 2);
                    //double packetVariance = Math.Pow((Convert.ToDouble(item) - Convert.ToDouble(meanValue)), 2);
                    varianceSum += packetVariance;
                }
                variance = (Convert.ToDouble(varianceSum) / (bs.IntervalCountTrimmed - 1));
                var stdDevValue = Math.Sqrt(variance);

                bs.IntervalCount = (from t in batchIntervals select t).Count();
                bs.PacketCountMaximum = maxValue;
                bs.PacketCountMinimum = minValue;
                bs.PacketCountMean = Convert.ToDecimal(Convert.ToDouble(meanValue));
                bs.PacketCountStandardDeviation = Convert.ToDecimal(stdDevValue);

                // Calculate both means for updating the capture batch intervals
                batchIntervalsTrimmedMean = bs.PacketCountMean;
                var untrimmedMean = (from t in batchIntervals select t.PacketCount).Average();
                batchIntervalsMean = Convert.ToDecimal(untrimmedMean);
            }
            else
            {
                bs.IntervalCount = batchIntervals.Count;
                var maxValue = (from t in batchIntervals select t.PacketCount).Max();
                var minValue = (from t in batchIntervals select t.PacketCount).Min();
                var meanValue = (from t in batchIntervals select t.PacketCount).Average();

                // Calculate standard deviation
                var packets = (from t in batchIntervals select t.PacketCount).ToList();
                var packetAverage = packets.Sum() / (double)batchIntervals.Count;


                foreach (var item in packets)
                {
                    double packetVariance = Math.Pow((Convert.ToDouble(item) - Convert.ToDouble(packetAverage)), 2);
                    //double packetVariance = Math.Pow((Convert.ToDouble(item) - Convert.ToDouble(meanValue)), 2);
                    varianceSum += packetVariance;
                }
                variance = Convert.ToDouble(varianceSum) / (bs.IntervalCount - 1);
                var stdDevValue = Math.Sqrt(variance);

                bs.PacketCountMaximum = maxValue;
                bs.PacketCountMinimum = minValue;
                bs.PacketCountMean = Convert.ToDecimal(meanValue);
                bs.PacketCountStandardDeviation = Convert.ToDecimal(stdDevValue);

                // Calculate both means for updating the capture batch intervals
                batchIntervalsMean = bs.PacketCountMean;
                var trimmedMean = (from t in batchIntervalsTrimmed select t.PacketCount).Average();
                batchIntervalsTrimmedMean = Convert.ToDecimal(trimmedMean);
            }

            //// Update the batch mean - only for single batches, not cumulative batches
            //var captureBatchId = (from c in batchIntervals select c.CaptureBatchId).FirstOrDefault();
            CurrentCaptureFile captureFile = new CurrentCaptureFile();
            ProcessCapturePackets pcp = new ProcessCapturePackets();
            captureFile = pcp.GetCurrentCaptureFile(_CaptureFileName);
            int captureBatchId = captureFile.CaptureBatchId;

            if (batchType == BatchType.Single && captureBatchId != 0)
            {
                try
                {
                    //ProcessCapturePackets pcp = new ProcessCapturePackets();
                    //if (!pcp.UpdateBatchMean(Convert.ToInt32(captureBatchId), bs.PacketCountMean))
                    if (!pcp.UpdateBatchMean(Convert.ToInt32(captureBatchId), batchIntervalsMean, batchIntervalsTrimmedMean))
                    {
                        throw new Exception("Error updating batch mean for CaptureBatchId " + captureBatchId);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating batch mean for CaptureBatchId " + captureBatchId + ": " + ex.Message);
                }
            }

            // Save the statistics to the database for display on the Analysis tab (save to DisplayStatistics table)
            SaveDisplayStatistics(bs, captureBatchId, captureState, batchType, true);
            
            return bs;
        }
        public void SaveDisplayStatistics(BatchStatistics batchStatistics, int captureBatchId, CaptureState captureState, BatchType batchType, bool saveData)
        {
            // Save the statistics to the database for display on the Analysis tab (save to DisplayStatistics table)
            // Replace cumulative data with new data
            DisplayStatisticsData dsd = new DisplayStatisticsData();
            switch (captureState)
            {
                case CaptureState.Marked:
                    switch (batchType)
                    {
                        case BatchType.Single:
                            //dsd.DeleteSingleMarkedDisplayStatitics();
                            dsd.InsertSingleMarkedDisplayStatitics(batchStatistics);
                            dsd.UpdateDisplayStatsSavedFlag(captureBatchId, BatchType.Single, true);
                            break;
                        case BatchType.Cumulative:
                            dsd.DeleteCumulativeMarkedDisplayStatitics();
                            dsd.InsertCumulativeMarkedDisplayStatitics(batchStatistics);
                            dsd.UpdateDisplayStatsSavedFlag(captureBatchId, BatchType.Cumulative, true);
                            break;
                    }
                    break;
                case CaptureState.Unmarked:
                    switch (batchType)
                    {
                        case BatchType.Single:
                            //dsd.DeleteSingleUnmarkedDisplayStatitics();
                            dsd.InsertSingleUnarkedDisplayStatitics(batchStatistics);
                            dsd.UpdateDisplayStatsSavedFlag(captureBatchId, BatchType.Single, true);
                            break;
                        case BatchType.Cumulative:
                            dsd.DeleteCumulativeUnmarkedDisplayStatitics();
                            dsd.InsertCumulativeUnarkedDisplayStatitics(batchStatistics);
                            dsd.UpdateDisplayStatsSavedFlag(captureBatchId, BatchType.Cumulative, true);
                            break;
                    }
                    break;
            }
        }

        /*********************************************************************************************
         * 
         * Need to split this into two methods according to BatchType, then call the method below and
         * pass in BatchType and BatchIntervals (single or cumulative).  Are batch intervals the same type???
         * 
         *********************************************************************************************/

        public void CalculateSingleHistogramData()
        {
            ProcessCapturePackets pcp = new ProcessCapturePackets();
            BindingList<BatchIntervalMarked> batchIntervals = new BindingList<BatchIntervalMarked>();

            // Get the batch intervals
            CurrentCaptureFile captureFile = new CurrentCaptureFile();
            captureFile = pcp.GetCurrentCaptureFile(_CaptureFileName);
            batchIntervals = pcp.GetMarkedBatchIntervals(captureFile.CaptureBatchId);

            switch (_CaptureState)
            {
                case CaptureState.Marked:
                    CalculateHistogramDataByType(batchIntervals, BatchType.Single, CaptureState.Marked);
                    break;
                case CaptureState.Unmarked:
                    CalculateHistogramDataByType(batchIntervals, BatchType.Single, CaptureState.Unmarked);
                    break;
            }
        }

        public void CalculateCumulativeHistogramData()
        {
            // Delete existing cumulative histogram data - it will be replaced with new data
            HistogramData hd = new HistogramData(BatchType.Cumulative, _CaptureState);
            hd.DeleteCumulativeHistogramData();

            ProcessCapturePackets pcp = new ProcessCapturePackets();
            BindingList<BatchIntervalMarked> batchIntervals = new BindingList<BatchIntervalMarked>();

            // Get the batch intervals
            BindingList<BatchIntervalMarked> unmarkedBatchIntervals = new BindingList<BatchIntervalMarked>();
            BindingList<BatchIntervalMarked> markedBatchIntervals = new BindingList<BatchIntervalMarked>();
            BindingList<CumulativeInterval> cumulativeIntervals = new BindingList<CumulativeInterval>();
            cumulativeIntervals = pcp.GetCumulativeIntervals();

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

            switch (_CaptureState)
            {
                case CaptureState.Marked:
                    CalculateHistogramDataByType(markedBatchIntervals, BatchType.Cumulative, CaptureState.Marked);
                    break;
                case CaptureState.Unmarked:
                    CalculateHistogramDataByType(unmarkedBatchIntervals, BatchType.Cumulative, CaptureState.Unmarked);
                    break;
            }
        }
        #endregion

        #region Private Methods
        private void CalculateHistogramDataByType(BindingList<BatchIntervalMarked> batchIntervalsCollection, BatchType batchType, CaptureState captureState)
        {
            //ProcessCapturePackets pcp = new ProcessCapturePackets();
            BindingList<BatchIntervalMarked> batchIntervals = new BindingList<BatchIntervalMarked>();
            batchIntervals = batchIntervalsCollection;

            //CurrentCaptureFile captureFile = new CurrentCaptureFile();
            //captureFile = pcp.GetCurrentCaptureFile(_CaptureFileName);

            // Get batch intervals
            //batchIntervals = pcp.GetMarkedBatchIntervals(captureFile.CaptureBatchId);
            ////CalculateHistogram histogram = new CalculateHistogram();
            ////Dictionary<int, int> histValues = new Dictionary<int, int>();

            ////BindingList<CapturePacket> capturePackets = new BindingList<CapturePacket>();
            ////capturePackets = pcp.GetCapturePackets(_CaptureFileName);
            ////histValues = histogram.CalculateHistogramValues(capturePackets);

            //Dictionary<int, decimal> probabilities = new CalculateProbability(histValues).GetProbabilityValues();
            //SortedDictionary<int, decimal> probabilities = new CalculateProbability(markedIntervals).GetProbabilityByPacketRange();

            SortedDictionary<int, decimal> histogramProbabilities = new SortedDictionary<int, decimal>();
            //SortedDictionary<int, decimal> _CumulativeUnmarkedProbabilities = new SortedDictionary<int, decimal>();

            int histogramBinSize = AnalysisConfiguration.HistogramBinSize;
            //SortedDictionary<int, decimal> markedProbabilities = new CalculateProbability(markedBatchIntervals).GetProbabilityByPacketRange(_trimZeroPacketIntervals, histogramBinSize);
            //SortedDictionary<int, decimal> unmarkedProbabilities = new CalculateProbability(unmarkedBatchIntervals).GetProbabilityByPacketRange(_trimZeroPacketIntervals, histogramBinSize);
            histogramProbabilities = new CalculateProbability(batchIntervals).GetProbabilityByPacketRange(_TrimZeroPacketIntervals, histogramBinSize);
            //_CumulativeUnmarkedProbabilities = new CalculateProbability(unmarkedBatchIntervals).GetProbabilityByPacketRange(_trimZeroPacketIntervals, histogramBinSize);

            // Convert histogram probabilities to Histogram type collection
            BindingList<Histogram> histogramProbabilityData = new BindingList<Histogram>();
            foreach (KeyValuePair<int, decimal> data in histogramProbabilities)
            {
                Histogram h = new Histogram();
                h.Interval = data.Key;
                h.Probability = data.Value;
                h.BatchType = Convert.ToInt32(batchType);
                h.CaptureState = Convert.ToInt32(captureState);
                histogramProbabilityData.Add(h);
            }

            // Save histogram data
            HistogramData hd = new HistogramData(histogramProbabilityData);
            hd.InsertHistogramData();
        }
        #endregion
    }
}
