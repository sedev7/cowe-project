using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.DomainClasses;

namespace COWE.BusinessLayer
{
    public class MeansHypothesisTest : ICalculateHypothesisTest
    {
        private bool _TrimZeroPacketIntervals = false;
        public MeansHypothesisTest(bool trimZeroPacketIntervals)
        {
            this._TrimZeroPacketIntervals = trimZeroPacketIntervals;
        }
        public HypothesisTest GetHypothesisTestResult()
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
            if (markedStatistics != null)
            {
                _MeansTestStatistic.MarkedMean = markedStatistics.MeanOfMeans;
                _MeansTestStatistic.MarkedStdDev = markedStatistics.MeanOfMeansStandardDeviation;
                _MeansTestStatistic.MarkedIntervalCount = _TrimZeroPacketIntervals == true ? markedStatistics.TrimmedIntervalCount : markedStatistics.IntervalCount;
            }
            if (unmarkedStatistics != null)
            {
                _MeansTestStatistic.UnmarkedMean = unmarkedStatistics.MeanOfMeans;
                _MeansTestStatistic.UnmarkedStdDev = unmarkedStatistics.MeanOfMeansStandardDeviation;
                _MeansTestStatistic.UnmarkedIntervalCount = _TrimZeroPacketIntervals == true ? unmarkedStatistics.TrimmedIntervalCount : unmarkedStatistics.IntervalCount;
            }

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
    }
}
