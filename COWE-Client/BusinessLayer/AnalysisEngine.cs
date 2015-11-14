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
        //const double _alpha = 0.05;       // Hypothesis test significance level
        //const decimal _zvalue = 1.65M;    // Z value for (1-_alpha), from standard normal distribution table
        //                                  // (note: one-tailed test because we are looking at the distribution 
        //                                  // for the difference of the means)
        bool _TrimZeroPacketIntervals = false;
        int _HistogramBinSize = 0;
        string _CaptureFileName = string.Empty;

        BatchType _BatchType = BatchType.Unknown;
        CaptureState _CaptureState = CaptureState.Unknown;
        CurrentCaptureFile _File = null;
        HypothesisTestType _HypothesisTestType = HypothesisTestType.Unknown;
        MeansTestStatistic _MeansTestStatistic = new MeansTestStatistic(AnalysisConfiguration.Alpha, AnalysisConfiguration.Zvalue);
        SortedDictionary<int, decimal> _CumulativeMarkedProbabilities = new SortedDictionary<int, decimal>();
        SortedDictionary<int, decimal> _CumulativeUnmarkedProbabilities = new SortedDictionary<int, decimal>();
        #endregion

        #region Constructors
        public AnalysisEngine() { }

        public AnalysisEngine(bool trimZeroPacketIntervals, int histogramBinSize)
        {
            this._HistogramBinSize = histogramBinSize;
            this._TrimZeroPacketIntervals = trimZeroPacketIntervals;
        }

        //public AnalysisEngine(bool trimZeroPacketIntervals, int histogramBinSize, HypothesisTest hypothesisTest, string captureFileName, CaptureState captureState)
        public AnalysisEngine(bool trimZeroPacketIntervals, int histogramBinSize, HypothesisTestType hypothesisTestType, CurrentCaptureFile file)
        {
            this._HistogramBinSize = histogramBinSize;
            this._TrimZeroPacketIntervals = trimZeroPacketIntervals;
            this._CaptureFileName = file.FileName;
            //this._CaptureState = captureState;
            this._CaptureState = file.CaptureState;
            this._HypothesisTestType = hypothesisTestType;
            this._File = new CurrentCaptureFile();
            this._File = file;
        }
        #endregion

        #region Public Methods

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
            _CaptureState = captureFile.CaptureState;

            BindingList<BatchIntervalMarked> batchIntervals = new BindingList<BatchIntervalMarked>();

            // Calculate probabilities
            batchIntervals = pcp.GetMarkedBatchIntervals(captureFile.CaptureBatchId);
            int histogramBinSize = Convert.ToInt32(_HistogramBinSize);
            SortedDictionary<int, decimal> probabilities = new CalculateProbability(batchIntervals).GetProbabilityByPacketRange(_TrimZeroPacketIntervals, histogramBinSize);

            BatchStatistics markedSingleStats = new BatchStatistics();
            BatchStatistics unmarkedSingleStats = new BatchStatistics();

            // Add the results to the DisplayStatistics table
            DisplayStatisticsData dsd = new DisplayStatisticsData();
            if(captureFile.CaptureState == CaptureState.Marked)
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

            //// Update the K-S statistics object - for display
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
         * ==> add CaptureBatchId field when inserting histogram data for single batches
         * 
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

            CalculateSingleHistogramProbability(batchIntervals, BatchType.Single, captureFile);

            //switch (_CaptureState)
            //{
            //    case CaptureState.Marked:
            //        CalculateHistogramDataByType(batchIntervals, BatchType.Single, CaptureState.Marked);
            //        break;
            //    case CaptureState.Unmarked:
            //        CalculateHistogramDataByType(batchIntervals, BatchType.Single, CaptureState.Unmarked);
            //        break;
            //}
        }

        public void CalculateCumulativeHistogramData()
        {
            // Delete existing cumulative histogram data - it will be replaced with new data
            CumulativeHistogramData chd = new CumulativeHistogramData(_CaptureState);
            chd.DeleteCumulativeHistogramData();

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
                    CalculateCumulativeHistogramProbability(markedBatchIntervals, BatchType.Cumulative, CaptureState.Marked);
                    break;
                case CaptureState.Unmarked:
                    CalculateCumulativeHistogramProbability(unmarkedBatchIntervals, BatchType.Cumulative, CaptureState.Unmarked);
                    break;
            }
        }
        public void CalculateCumulativeProbabilityDistribution(CaptureState captureState)
        {
            // Note: cumulative histogram intervals must have previously been calculated in order to calculate cumulative probabilities

            //SortedDictionary<int, decimal> _CumulativeMarkedProbabilities = new SortedDictionary<int, decimal>();
            //SortedDictionary<int, decimal> _CumulativeUnmarkedProbabilities = new SortedDictionary<int, decimal>();

            //_CumulativeMarkedProbabilities = new CalculateProbability(batchIntervals).GetProbabilityByPacketRange(_TrimZeroPacketIntervals, histogramBinSize);

            // Get the batch intervals
            BindingList<BatchIntervalMarked> unmarkedBatchIntervals = new BindingList<BatchIntervalMarked>();
            BindingList<BatchIntervalMarked> markedBatchIntervals = new BindingList<BatchIntervalMarked>();
            BindingList<CumulativeProbabilityDistribution> distribution = new BindingList<CumulativeProbabilityDistribution>();
            CumulativeProbabilityDistributionData cumProbDistData = new CumulativeProbabilityDistributionData();

            // Delete any existing cumulative probability distribution data for the captureState
            cumProbDistData.DeleteCumulativeProbabilityDistribution(captureState);

            // Add the newly calculated cumulative probability distribution
            switch (captureState)
            {
                case CaptureState.Marked:
                    if (_CumulativeMarkedProbabilities != null)
                    {
                        SortedDictionary<int, decimal> markedProbabilities = new CalculateProbability(markedBatchIntervals).GetCumulativeProbabilityDistribution(_CumulativeMarkedProbabilities);

                        // Convert to CumulativeProbabilityDistribution type
                        foreach (KeyValuePair<int, decimal> item in markedProbabilities)
                        {
                            CumulativeProbabilityDistribution cpd = new CumulativeProbabilityDistribution();
                            cpd.CaptureState = (int)CaptureState.Marked;
                            cpd.Interval = item.Key;
                            cpd.Probability = Math.Round(item.Value, 10);
                            distribution.Add(cpd);
                        }
                    }
                    // Save to database
                    cumProbDistData.InsertCumulativeProbabilityDistribution(distribution);
                    break;

                case CaptureState.Unmarked:
                    if (_CumulativeUnmarkedProbabilities != null)
                    {
                        SortedDictionary<int, decimal> unmarkedProbabilities = new CalculateProbability(unmarkedBatchIntervals).GetCumulativeProbabilityDistribution(_CumulativeUnmarkedProbabilities);

                        // Convert to CumulativeProbabilityDistribution type
                        foreach (KeyValuePair<int, decimal> item in unmarkedProbabilities)
                        {
                            CumulativeProbabilityDistribution cpd = new CumulativeProbabilityDistribution();
                            cpd.CaptureState = (int)CaptureState.Unmarked;
                            cpd.Interval = item.Key;
                            //cpd.Probability = Convert.ToDecimal(String.Format("{0,10}", item.Value.ToString("D")));
                            cpd.Probability = Math.Round(item.Value,10);
                            distribution.Add(cpd);
                        }
                    }
                    // Save to database
                    cumProbDistData.InsertCumulativeProbabilityDistribution(distribution);
                    break;
            }
        }
        public void CalculateHypothesisTestResults()
        {
            // Only perform these calculations if files have been processed and a pair of files (marked and unmarked) are available

            bool IsDirty = false;
            bool HasValues = false;
            HypothesisTest ht = new HypothesisTest();
            ProcessCapturePackets pcp = new ProcessCapturePackets();
            int markedFileCount = pcp.GetProcessedFilesCountMarked();
            int unmarkedFileCount = pcp.GetProcessedFilesCountUnmarked();

            ht = pcp.GetHypothesisTestResults();
            if (ht != null)
            {
                HasValues = ht.HasValues;
            }

            if (markedFileCount >= 1 && unmarkedFileCount >= 1 && (markedFileCount + unmarkedFileCount) % 2 == 0)
            {
                // Get mean of means test results
                HypothesisTest htMeans = new HypothesisTest();
                htMeans = GetMeansHypothesisTestResult();
                ht.MeansTestResult = htMeans.MeansTestResult;
                ht.MeanOfMeansVariance = htMeans.MeanOfMeansVariance;
                ht.MeansVarianceStandardDeviation = htMeans.MeansVarianceStandardDeviation;

                // Get the K-S test results
                ht.KsTestResult = GetKsHypothesisTestResult();
                ht.HasValues = true;
                IsDirty = true;
            }
            else if(!HasValues)
            {
                // Default values - only if we haven't previously calculated hypothesis test results
                ht.MeanOfMeansVariance = 0;
                ht.MeansVarianceStandardDeviation = 0;
                ht.MeansTestResult = false;
                ht.KsTestResult = false;
                ht.HasValues = false;
                IsDirty = true;
            }

            if (IsDirty)
            {
                // Save the test results
                pcp.DeleteHypothesisTestResults();
                pcp.InsertHypothesisTestResults(ht);
            }
        }
        #endregion

        #region Private Methods
        //private void CalculateHistogramDataByType(BindingList<BatchIntervalMarked> batchIntervalsCollection, BatchType batchType, CaptureState captureState)
        private void CalculateSingleHistogramProbability(BindingList<BatchIntervalMarked> batchIntervalsCollection, BatchType batchType, CurrentCaptureFile captureFile)
        {
            BindingList<BatchIntervalMarked> batchIntervals = new BindingList<BatchIntervalMarked>();
            batchIntervals = batchIntervalsCollection;

            SortedDictionary<int, decimal> histogramProbabilities = new SortedDictionary<int, decimal>();

            int histogramBinSize = AnalysisConfiguration.HistogramBinSize;
            histogramProbabilities = new CalculateProbability(batchIntervals).GetProbabilityByPacketRange(_TrimZeroPacketIntervals, histogramBinSize);

            // Convert histogram probabilities to Histogram type collection
            BindingList<SingleHistogram> singleHistogramProbabilityData = new BindingList<SingleHistogram>();
            foreach (KeyValuePair<int, decimal> data in histogramProbabilities)
            {
                SingleHistogram h = new SingleHistogram();
                h.CaptureBatchId = captureFile.CaptureBatchId;
                h.Interval = data.Key;
                h.Probability = data.Value;
                h.BatchType = Convert.ToInt32(batchType);
                h.CaptureState = Convert.ToInt32(captureFile.CaptureState);
                singleHistogramProbabilityData.Add(h);
            }

            // Save histogram data
            SingleHistogramData shd = new SingleHistogramData(singleHistogramProbabilityData);
            shd.InsertSingleHistogramData();
        }

        private void CalculateCumulativeHistogramProbability(BindingList<BatchIntervalMarked> batchIntervalsCollection, BatchType batchType, CaptureState captureState)
        {
            BindingList<BatchIntervalMarked> batchIntervals = new BindingList<BatchIntervalMarked>();
            batchIntervals = batchIntervalsCollection;

            SortedDictionary<int, decimal> histogramProbabilities = new SortedDictionary<int, decimal>();

            int histogramBinSize = AnalysisConfiguration.HistogramBinSize;
            histogramProbabilities = new CalculateProbability(batchIntervals).GetProbabilityByPacketRange(_TrimZeroPacketIntervals, histogramBinSize);
            
            // Update the cumulative intervals for calculating cumulative probability distributions
            switch(captureState)
            {
                case CaptureState.Marked:
                    _CumulativeMarkedProbabilities = histogramProbabilities;
                    break;

                case CaptureState.Unmarked:
                    _CumulativeUnmarkedProbabilities = histogramProbabilities;
                    break;
            }

            // Convert histogram probabilities to Histogram type collection
            BindingList<CumulativeHistogram> cumulativeHistogramProbabilityData = new BindingList<CumulativeHistogram>();
            foreach (KeyValuePair<int, decimal> data in histogramProbabilities)
            {
                CumulativeHistogram h = new CumulativeHistogram();
                h.Interval = data.Key;
                h.Probability = data.Value;
                h.BatchType = Convert.ToInt32(batchType);
                h.CaptureState = Convert.ToInt32(captureState);
                cumulativeHistogramProbabilityData.Add(h);
            }

            // Save histogram data
            CumulativeHistogramData chd = new CumulativeHistogramData(cumulativeHistogramProbabilityData);
            chd.InsertCumulativeHistogramData();
        }
        //private bool GetMeansHypothesisTestResult(decimal unmarkedMean, decimal markedMean, decimal unmarkedStdDev, decimal markedStdDev, int unmarkedPacketCount, int markedPacketCount)
        private HypothesisTest GetMeansHypothesisTestResult()
        {
            // We are comparing the mean of sample means for a significant difference between the distributions

            // H0: there is no difference in the distribution of packets between marked and unmarked batches
            // H1: there is a difference between the batches

            /*******************************************************************************************
             * 
             * Note: we need a test to verify that the following data (DisplayStatistic) has been updated
             * 
             *******************************************************************************************/
            HypothesisTest ht = new HypothesisTest();
            //// Update the K-S statistics object
            //_KsStatistics.MarkedMean = markedMeanOfMeans;
            //_KsStatistics.MarkedStdDev = markedStdDevMeanOfMeans;
            ////_KsStatistics.MarkedMean = markedCumulativeStats.PacketCountMean;
            ////_KsStatistics.MarkedStdDev = markedCumulativeStats.PacketCountStandardDeviation;
            //_KsStatistics.MarkedIntervalCount = TrimIntervals == true ? markedCumulativeStats.IntervalCountTrimmed : markedCumulativeStats.IntervalCount;
            //_KsStatistics.UnmarkedMean = unmarkedMeanOfMeans;
            //_KsStatistics.UnmarkedStdDev = unmarkedStdDevMeanOfMeans;
            ////_KsStatistics.UnmarkedMean = unmarkedCumulativeStats.PacketCountMean;
            ////_KsStatistics.UnmarkedStdDev = unmarkedCumulativeStats.PacketCountStandardDeviation;
            //_KsStatistics.UnmarkedIntervalCount = TrimIntervals == true ? unmarkedCumulativeStats.IntervalCountTrimmed : unmarkedCumulativeStats.IntervalCount;

            ProcessCapturePackets pcp = new ProcessCapturePackets();
            DisplayStatistic markedStatistics = new DisplayStatistic();
            DisplayStatistic unmarkedStatistics = new DisplayStatistic();
            markedStatistics = pcp.GetCumulativeMarkedDisplayStatistics();
            unmarkedStatistics = pcp.GetCumulativeUnmarkedDisplayStatistics();

            MeansTestStatistic _MeansTestStatistic = new MeansTestStatistic(AnalysisConfiguration.Alpha, AnalysisConfiguration.Zvalue);
            _MeansTestStatistic.MarkedMean = markedStatistics.MeanOfMeans;
            _MeansTestStatistic.MarkedStdDev = markedStatistics.MeanOfMeansStandardDeviation;
            _MeansTestStatistic.MarkedIntervalCount = _TrimZeroPacketIntervals == true ? markedStatistics.TrimmedIntervalCount : markedStatistics.IntervalCount;
            _MeansTestStatistic.UnmarkedMean = unmarkedStatistics.MeanOfMeans;
            _MeansTestStatistic.UnmarkedStdDev = unmarkedStatistics.MeanOfMeansStandardDeviation;
            _MeansTestStatistic.UnmarkedIntervalCount = _TrimZeroPacketIntervals == true ? unmarkedStatistics.TrimmedIntervalCount : unmarkedStatistics.IntervalCount;

            // Test the difference in the distribution means
            decimal meanDifference = _MeansTestStatistic.MeanDifference;
            decimal sigmaDifference = _MeansTestStatistic.SigmaDifference;

            // Single-tail test (if there is a difference in the means it will be a positive value)
            // Z value for alpha = 5% significance level:

            // Test result: true = reject H0 - difference of means has only 5% probability of occurring if H0 is true
            // Note: standard deviation = SigmaDifference * Zvalue
            ht.MeansTestResult = _MeansTestStatistic.MeanDifference > _MeansTestStatistic.StandardDeviation ? true : false;
            ht.MeansVarianceStandardDeviation = _MeansTestStatistic.StandardDeviation;
            ht.MeanOfMeansVariance = _MeansTestStatistic.MeanDifference;

            return ht;
        }

        private bool GetKsHypothesisTestResult()
        {
            bool result = false;

            // Get cumulative probability distribution data and find the max difference between marked and unmarked distributions
            ProcessCapturePackets pcp = new ProcessCapturePackets();
            BindingList<CumulativeProbabilityDistribution> markedCPD = new BindingList<CumulativeProbabilityDistribution>();
            BindingList<CumulativeProbabilityDistribution> unmarkedCPD = new BindingList<CumulativeProbabilityDistribution>();
            markedCPD = pcp.GetCumulativeProbabilityDistributionData(CaptureState.Marked);
            unmarkedCPD = pcp.GetCumulativeProbabilityDistributionData(CaptureState.Unmarked);

            decimal maxVariance = 0M;
            int intervalCount = 0;

            // Only compare intervals from each distribution with a corresponding interval in the other distribution 
            if (unmarkedCPD.Count > markedCPD.Count)
            {
                intervalCount = markedCPD.Count;
            }
            else
            {
                intervalCount = unmarkedCPD.Count;
            }

            // Expand each distribution into equal discrete steps for comparison of cumulative probabilities
            // First, find the largest cumulative packet count (= interval)
            int maxPacketCount = 0;
            if(markedCPD[markedCPD.Count - 1].Interval >= unmarkedCPD[unmarkedCPD.Count - 1].Interval)
            {
                maxPacketCount = markedCPD[markedCPD.Count - 1].Interval;
            }
            else
            {
                maxPacketCount = unmarkedCPD[unmarkedCPD.Count - 1].Interval;
            }

            // Second, expand the packet counts by interpolating between packet counts (intervals) using an average probability
            // for each packet count in the range and successively adding up to the next packet count (interval); add these 
            // interpolated packets to a dictionary; outcome is a dictionary for each distribution containing packet counts and
            // probabilities from packet count = 0 to packet count = largest packet count (interval) of both distributions and 
            // the associated probabilities for each packet count.  We are basically calculating a linear estimate of packet
            // counts and probabilities between each packet count and probability in the actual distributions.

            //int lastInterval = 0;
            //decimal newProbability = 0M;
            //SortedDictionary<int, decimal> markedCPDExpanded = new SortedDictionary<int, decimal>();

            //foreach (var item in markedCPD)
            //{
            //    if(item.Interval > lastInterval  && item.Interval <= maxPacketCount)
            //    {
            //        int numberOfIntervals = item.Interval - lastInterval - 1;
            //        decimal intervalProbability = (item.Probability - newProbability) / numberOfIntervals;

            //        // Probability of first expanded packet count will be zero
            //        if(markedCPD.IndexOf(item) == 0)
            //        {
            //            markedCPDExpanded.Add(0, 0M);
            //        }

            //        // Add each expanded packet count and probability in the range to the dictionary
            //        for (int i = 1; i <= numberOfIntervals; i++)
            //        {
            //            newProbability = newProbability + intervalProbability;
            //            markedCPDExpanded.Add(lastInterval + i, newProbability);
            //        }
            //        // Last packet count is the current interval value
            //        //markedCPDExpanded.Add(item.Interval, item.Probability);
            //        // Reset the last interval to the current interval
            //        lastInterval = item.Interval - 1;
            //    }
            //    // Move to next interval and reset the probability
            //    //lastInterval++;
            //    newProbability = item.Probability;
            //}

            //// Third, check for packet counts that are less than the maximum packet count and assign a probability of 1
            //// to any that are found
            //int maxMarkedPacketCount = markedCPD[markedCPD.Count - 1].Interval;
            //if(maxPacketCount > maxMarkedPacketCount)
            //{
            //    // We have fewer packet counts in this distribution than the max packet count, so add one to the packet
            //    // count with a probability of 1.0 for each incremental packet count, up to maxMarkedPacketCount
            //    for (int i = 0; i < maxPacketCount - maxMarkedPacketCount; i++)
            //    {
            //        markedCPDExpanded.Add(++lastInterval, 1.0M);
            //    }
            //}

            SortedDictionary<int, decimal> markedCPDExpanded = new SortedDictionary<int, decimal>();
            SortedDictionary<int, decimal> unmarkedCPDExpanded = new SortedDictionary<int, decimal>();
            markedCPDExpanded = ExpandPacketCount(markedCPD, maxPacketCount);
            unmarkedCPDExpanded = ExpandPacketCount(unmarkedCPD, maxPacketCount);

            //// Find the maximum variance between the cumulative probabilities in each distribution
            //for (int i = 0; i < intervalCount; i++ )
            //{
            //    if(Math.Abs(unmarkedCPD[i].Probability - markedCPD[i].Probability) > maxVariance)
            //    {
            //        maxVariance = Math.Abs(unmarkedCPD[i].Probability - markedCPD[i].Probability);
            //    }
            //}

            for (int i = 0; i < maxPacketCount; i++)
            {
                #region Debug
#if(DEBUG)
                System.Diagnostics.Debug.WriteLine("unmarkedCPDExpanded[{0}]:[{1}] - markedCPDExpanded[{2}]:[{3}] = {4}", i, unmarkedCPDExpanded[i], i, markedCPDExpanded[i], Math.Abs(unmarkedCPDExpanded[i] - markedCPDExpanded[i]));
#endif
                #endregion
                if (Math.Abs(unmarkedCPDExpanded[i] - markedCPDExpanded[i]) > maxVariance)
                {
                    maxVariance = Math.Abs(unmarkedCPDExpanded[i] - markedCPDExpanded[i]);
                }
            }

            // Compare the maximum variance with the hypothesis test threshold
            // For significance level alpha = 0.05, the K-S statistic is computed as 1.36/N^(1/2), where N is the number of samples
            decimal ksStatistic = Convert.ToDecimal(1.36 / Math.Pow(intervalCount, 0.5));
            if(maxVariance > ksStatistic)
            {
                // Reject the null hypothesis
                result = true;
            }
            return result;
        }

        private SortedDictionary<int, decimal> ExpandPacketCount(BindingList<CumulativeProbabilityDistribution> cpdPackets, int maxPacketCount)
        {
            int lastInterval = 0;
            decimal newProbability = 0M;
            SortedDictionary<int, decimal> expandedPacketCount = new SortedDictionary<int, decimal>();

            foreach (var item in cpdPackets)
            {
                if (item.Interval > lastInterval && item.Interval <= maxPacketCount)
                {
                    int numberOfIntervals = item.Interval - lastInterval - 1;
                    decimal intervalProbability = (item.Probability - newProbability) / numberOfIntervals;

                    // Probability of first expanded packet count will be zero
                    if (cpdPackets.IndexOf(item) == 0)
                    {
                        expandedPacketCount.Add(0, 0M);
                    }

                    // Add each expanded packet count and probability in the range to the dictionary
                    for (int i = 1; i <= numberOfIntervals; i++)
                    {
                        newProbability = newProbability + intervalProbability;
                        expandedPacketCount.Add(lastInterval + i, newProbability);
                    }
                    // Last packet count is the current interval value
                    // Reset the last interval to the current interval
                    lastInterval = item.Interval - 1;
                }
                // Move to next interval and reset the probability
                newProbability = item.Probability;
            }

            // Check for packet counts that are less than the maximum packet count and assign a probability of 1
            // to any that are found
            int maxMarkedPacketCount = cpdPackets[cpdPackets.Count - 1].Interval;
            if (maxPacketCount > maxMarkedPacketCount)
            {
                // We have fewer packet counts in this distribution than the max packet count, so add one to the packet
                // count with a probability of 1.0 for each incremental packet count, up to maxMarkedPacketCount
                for (int i = 0; i < maxPacketCount - maxMarkedPacketCount; i++)
                {
                    expandedPacketCount.Add(++lastInterval, 1.0M);
                }
            }
            return expandedPacketCount;
        }
        #endregion
    }
}
