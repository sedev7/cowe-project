//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Data.Entity.Infrastructure.MappingViews;

[assembly: DbMappingViewCacheTypeAttribute(
    typeof(COWE.DataLayer.PacketAnalysisEntity),
    typeof(Edm_EntityMappingGeneratedViews.ViewsForBaseEntitySets67e3307eef0583d5afb158d92b3316677fc151d891cb2c439b5549f9ad424896))]

namespace Edm_EntityMappingGeneratedViews
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Core.Metadata.Edm;

    /// <summary>
    /// Implements a mapping view cache.
    /// </summary>
    [GeneratedCode("Entity Framework Power Tools", "0.9.0.0")]
    internal sealed class ViewsForBaseEntitySets67e3307eef0583d5afb158d92b3316677fc151d891cb2c439b5549f9ad424896 : DbMappingViewCache
    {
        /// <summary>
        /// Gets a hash value computed over the mapping closure.
        /// </summary>
        public override string MappingHashValue
        {
            get { return "67e3307eef0583d5afb158d92b3316677fc151d891cb2c439b5549f9ad424896"; }
        }

        /// <summary>
        /// Gets a view corresponding to the specified extent.
        /// </summary>
        /// <param name="extent">The extent.</param>
        /// <returns>The mapping view, or null if the extent is not associated with a mapping view.</returns>
        public override DbMappingView GetView(EntitySetBase extent)
        {
            if (extent == null)
            {
                throw new ArgumentNullException("extent");
            }

            var extentName = extent.EntityContainer.Name + "." + extent.Name;

            if (extentName == "PacketAnalysisModelStoreContainer.CapturePacket")
            {
                return GetView0();
            }

            if (extentName == "PacketAnalysisModelStoreContainer.BatchInterval")
            {
                return GetView1();
            }

            if (extentName == "PacketAnalysisModelStoreContainer.CaptureBatch")
            {
                return GetView2();
            }

            if (extentName == "PacketAnalysisModelStoreContainer.SingleHistogram")
            {
                return GetView3();
            }

            if (extentName == "PacketAnalysisEntity.CapturePackets")
            {
                return GetView4();
            }

            if (extentName == "PacketAnalysisEntity.BatchIntervals")
            {
                return GetView5();
            }

            if (extentName == "PacketAnalysisEntity.CaptureBatches")
            {
                return GetView6();
            }

            if (extentName == "PacketAnalysisEntity.SingleHistograms")
            {
                return GetView7();
            }

            if (extentName == "PacketAnalysisModelStoreContainer.CumulativeInterval")
            {
                return GetView8();
            }

            if (extentName == "PacketAnalysisEntity.CumulativeIntervals")
            {
                return GetView9();
            }

            if (extentName == "PacketAnalysisModelStoreContainer.DisplayStatistic")
            {
                return GetView10();
            }

            if (extentName == "PacketAnalysisEntity.DisplayStatistics")
            {
                return GetView11();
            }

            if (extentName == "PacketAnalysisModelStoreContainer.CumulativeHistogram")
            {
                return GetView12();
            }

            if (extentName == "PacketAnalysisEntity.CumulativeHistograms")
            {
                return GetView13();
            }

            if (extentName == "PacketAnalysisModelStoreContainer.CumulativeProbabilityDistribution")
            {
                return GetView14();
            }

            if (extentName == "PacketAnalysisEntity.CumulativeProbabilityDistributions")
            {
                return GetView15();
            }

            if (extentName == "PacketAnalysisModelStoreContainer.HypothesisTest")
            {
                return GetView16();
            }

            if (extentName == "PacketAnalysisEntity.HypothesisTests")
            {
                return GetView17();
            }

            return null;
        }

        /// <summary>
        /// Gets the view for PacketAnalysisModelStoreContainer.CapturePacket.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView0()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing CapturePacket
        [PacketAnalysisModel.Store.CapturePacket](T1.CapturePacket_CapturePacketId, T1.CapturePacket_CaptureBatchId, T1.CapturePacket_PacketNumber, T1.CapturePacket_TimeStamp, T1.CapturePacket_Marked)
    FROM (
        SELECT 
            T.CapturePacketId AS CapturePacket_CapturePacketId, 
            T.CaptureBatchId AS CapturePacket_CaptureBatchId, 
            T.PacketNumber AS CapturePacket_PacketNumber, 
            T.TimeStamp AS CapturePacket_TimeStamp, 
            T.Marked AS CapturePacket_Marked, 
            True AS _from0
        FROM PacketAnalysisEntity.CapturePackets AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for PacketAnalysisModelStoreContainer.BatchInterval.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView1()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing BatchInterval
        [PacketAnalysisModel.Store.BatchInterval](T1.BatchInterval_BatchIntervalId, T1.BatchInterval_CaptureBatchId, T1.BatchInterval_IntervalNumber, T1.BatchInterval_PacketCount)
    FROM (
        SELECT 
            T.BatchIntervalId AS BatchInterval_BatchIntervalId, 
            T.CaptureBatchId AS BatchInterval_CaptureBatchId, 
            T.IntervalNumber AS BatchInterval_IntervalNumber, 
            T.PacketCount AS BatchInterval_PacketCount, 
            True AS _from0
        FROM PacketAnalysisEntity.BatchIntervals AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for PacketAnalysisModelStoreContainer.CaptureBatch.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView2()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing CaptureBatch
        [PacketAnalysisModel.Store.CaptureBatch](T1.CaptureBatch_CaptureBatchId, T1.CaptureBatch_FileName, T1.CaptureBatch_Marked, T1.CaptureBatch_Mean, T1.CaptureBatch_TrimmedMean, T1.CaptureBatch_Parsed, T1.CaptureBatch_SingleStatistics, T1.CaptureBatch_CumulativeStatistics)
    FROM (
        SELECT 
            T.CaptureBatchId AS CaptureBatch_CaptureBatchId, 
            T.FileName AS CaptureBatch_FileName, 
            T.Marked AS CaptureBatch_Marked, 
            T.Mean AS CaptureBatch_Mean, 
            T.TrimmedMean AS CaptureBatch_TrimmedMean, 
            T.Parsed AS CaptureBatch_Parsed, 
            T.SingleStatistics AS CaptureBatch_SingleStatistics, 
            T.CumulativeStatistics AS CaptureBatch_CumulativeStatistics, 
            True AS _from0
        FROM PacketAnalysisEntity.CaptureBatches AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for PacketAnalysisModelStoreContainer.SingleHistogram.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView3()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing SingleHistogram
        [PacketAnalysisModel.Store.SingleHistogram](T1.SingleHistogram_SingleHistogramId, T1.SingleHistogram_CaptureBatchId, T1.SingleHistogram_Interval, T1.SingleHistogram_Probability, T1.SingleHistogram_CaptureState)
    FROM (
        SELECT 
            T.SingleHistogramId AS SingleHistogram_SingleHistogramId, 
            T.CaptureBatchId AS SingleHistogram_CaptureBatchId, 
            T.Interval AS SingleHistogram_Interval, 
            T.Probability AS SingleHistogram_Probability, 
            T.CaptureState AS SingleHistogram_CaptureState, 
            True AS _from0
        FROM PacketAnalysisEntity.SingleHistograms AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for PacketAnalysisEntity.CapturePackets.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView4()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing CapturePackets
        [COWE.PacketAnalysisModel.CapturePacket](T1.CapturePacket_CapturePacketId, T1.CapturePacket_PacketNumber, T1.CapturePacket_TimeStamp, T1.CapturePacket_Marked, T1.CapturePacket_CaptureBatchId)
    FROM (
        SELECT 
            T.CapturePacketId AS CapturePacket_CapturePacketId, 
            T.PacketNumber AS CapturePacket_PacketNumber, 
            T.TimeStamp AS CapturePacket_TimeStamp, 
            T.Marked AS CapturePacket_Marked, 
            T.CaptureBatchId AS CapturePacket_CaptureBatchId, 
            True AS _from0
        FROM PacketAnalysisModelStoreContainer.CapturePacket AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for PacketAnalysisEntity.BatchIntervals.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView5()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing BatchIntervals
        [COWE.PacketAnalysisModel.BatchInterval](T1.BatchInterval_BatchIntervalId, T1.BatchInterval_CaptureBatchId, T1.BatchInterval_IntervalNumber, T1.BatchInterval_PacketCount)
    FROM (
        SELECT 
            T.BatchIntervalId AS BatchInterval_BatchIntervalId, 
            T.CaptureBatchId AS BatchInterval_CaptureBatchId, 
            T.IntervalNumber AS BatchInterval_IntervalNumber, 
            T.PacketCount AS BatchInterval_PacketCount, 
            True AS _from0
        FROM PacketAnalysisModelStoreContainer.BatchInterval AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for PacketAnalysisEntity.CaptureBatches.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView6()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing CaptureBatches
        [COWE.PacketAnalysisModel.CaptureBatch](T1.CaptureBatch_CaptureBatchId, T1.CaptureBatch_FileName, T1.CaptureBatch_Marked, T1.CaptureBatch_Mean, T1.CaptureBatch_TrimmedMean, T1.CaptureBatch_Parsed, T1.CaptureBatch_SingleStatistics, T1.CaptureBatch_CumulativeStatistics)
    FROM (
        SELECT 
            T.CaptureBatchId AS CaptureBatch_CaptureBatchId, 
            T.FileName AS CaptureBatch_FileName, 
            T.Marked AS CaptureBatch_Marked, 
            T.Mean AS CaptureBatch_Mean, 
            T.TrimmedMean AS CaptureBatch_TrimmedMean, 
            T.Parsed AS CaptureBatch_Parsed, 
            T.SingleStatistics AS CaptureBatch_SingleStatistics, 
            T.CumulativeStatistics AS CaptureBatch_CumulativeStatistics, 
            True AS _from0
        FROM PacketAnalysisModelStoreContainer.CaptureBatch AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for PacketAnalysisEntity.SingleHistograms.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView7()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing SingleHistograms
        [COWE.PacketAnalysisModel.SingleHistogram](T1.SingleHistogram_SingleHistogramId, T1.SingleHistogram_CaptureBatchId, T1.SingleHistogram_Interval, T1.SingleHistogram_Probability, T1.SingleHistogram_CaptureState)
    FROM (
        SELECT 
            T.SingleHistogramId AS SingleHistogram_SingleHistogramId, 
            T.CaptureBatchId AS SingleHistogram_CaptureBatchId, 
            T.Interval AS SingleHistogram_Interval, 
            T.Probability AS SingleHistogram_Probability, 
            T.CaptureState AS SingleHistogram_CaptureState, 
            True AS _from0
        FROM PacketAnalysisModelStoreContainer.SingleHistogram AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for PacketAnalysisModelStoreContainer.CumulativeInterval.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView8()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing CumulativeInterval
        [PacketAnalysisModel.Store.CumulativeInterval](T1.CumulativeInterval_CumulativeIntervalId, T1.CumulativeInterval_CumulativeIntervalNumber, T1.CumulativeInterval_PacketCount, T1.CumulativeInterval_Marked)
    FROM (
        SELECT 
            T.CumulativeIntervalId AS CumulativeInterval_CumulativeIntervalId, 
            T.CumulativeIntervalNumber AS CumulativeInterval_CumulativeIntervalNumber, 
            T.PacketCount AS CumulativeInterval_PacketCount, 
            T.Marked AS CumulativeInterval_Marked, 
            True AS _from0
        FROM PacketAnalysisEntity.CumulativeIntervals AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for PacketAnalysisEntity.CumulativeIntervals.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView9()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing CumulativeIntervals
        [COWE.PacketAnalysisModel.CumulativeInterval](T1.CumulativeInterval_CumulativeIntervalId, T1.CumulativeInterval_CumulativeIntervalNumber, T1.CumulativeInterval_PacketCount, T1.CumulativeInterval_Marked)
    FROM (
        SELECT 
            T.CumulativeIntervalId AS CumulativeInterval_CumulativeIntervalId, 
            T.CumulativeIntervalNumber AS CumulativeInterval_CumulativeIntervalNumber, 
            T.PacketCount AS CumulativeInterval_PacketCount, 
            T.Marked AS CumulativeInterval_Marked, 
            True AS _from0
        FROM PacketAnalysisModelStoreContainer.CumulativeInterval AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for PacketAnalysisModelStoreContainer.DisplayStatistic.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView10()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing DisplayStatistic
        [PacketAnalysisModel.Store.DisplayStatistic](T1.DisplayStatistic_DisplayStatisticId, T1.DisplayStatistic_IntervalCount, T1.DisplayStatistic_TrimmedIntervalCount, T1.DisplayStatistic_MeanPacketsPerInterval, T1.DisplayStatistic_StandardDeviation, T1.DisplayStatistic_MinPacketsPerInterval, T1.DisplayStatistic_MaxPacketsPerInterval, T1.DisplayStatistic_MeanOfMeans, T1.DisplayStatistic_MeanOfMeansStandardDeviation, T1.DisplayStatistic_Marked, T1.DisplayStatistic_BatchType)
    FROM (
        SELECT 
            T.DisplayStatisticId AS DisplayStatistic_DisplayStatisticId, 
            T.IntervalCount AS DisplayStatistic_IntervalCount, 
            T.TrimmedIntervalCount AS DisplayStatistic_TrimmedIntervalCount, 
            T.MeanPacketsPerInterval AS DisplayStatistic_MeanPacketsPerInterval, 
            T.StandardDeviation AS DisplayStatistic_StandardDeviation, 
            T.MinPacketsPerInterval AS DisplayStatistic_MinPacketsPerInterval, 
            T.MaxPacketsPerInterval AS DisplayStatistic_MaxPacketsPerInterval, 
            T.MeanOfMeans AS DisplayStatistic_MeanOfMeans, 
            T.MeanOfMeansStandardDeviation AS DisplayStatistic_MeanOfMeansStandardDeviation, 
            T.Marked AS DisplayStatistic_Marked, 
            T.BatchType AS DisplayStatistic_BatchType, 
            True AS _from0
        FROM PacketAnalysisEntity.DisplayStatistics AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for PacketAnalysisEntity.DisplayStatistics.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView11()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing DisplayStatistics
        [COWE.PacketAnalysisModel.DisplayStatistic](T1.DisplayStatistic_DisplayStatisticId, T1.DisplayStatistic_IntervalCount, T1.DisplayStatistic_TrimmedIntervalCount, T1.DisplayStatistic_MeanPacketsPerInterval, T1.DisplayStatistic_StandardDeviation, T1.DisplayStatistic_MinPacketsPerInterval, T1.DisplayStatistic_MaxPacketsPerInterval, T1.DisplayStatistic_MeanOfMeans, T1.DisplayStatistic_MeanOfMeansStandardDeviation, T1.DisplayStatistic_Marked, T1.DisplayStatistic_BatchType)
    FROM (
        SELECT 
            T.DisplayStatisticId AS DisplayStatistic_DisplayStatisticId, 
            T.IntervalCount AS DisplayStatistic_IntervalCount, 
            T.TrimmedIntervalCount AS DisplayStatistic_TrimmedIntervalCount, 
            T.MeanPacketsPerInterval AS DisplayStatistic_MeanPacketsPerInterval, 
            T.StandardDeviation AS DisplayStatistic_StandardDeviation, 
            T.MinPacketsPerInterval AS DisplayStatistic_MinPacketsPerInterval, 
            T.MaxPacketsPerInterval AS DisplayStatistic_MaxPacketsPerInterval, 
            T.MeanOfMeans AS DisplayStatistic_MeanOfMeans, 
            T.MeanOfMeansStandardDeviation AS DisplayStatistic_MeanOfMeansStandardDeviation, 
            T.Marked AS DisplayStatistic_Marked, 
            T.BatchType AS DisplayStatistic_BatchType, 
            True AS _from0
        FROM PacketAnalysisModelStoreContainer.DisplayStatistic AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for PacketAnalysisModelStoreContainer.CumulativeHistogram.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView12()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing CumulativeHistogram
        [PacketAnalysisModel.Store.CumulativeHistogram](T1.CumulativeHistogram_CumulativeHistogramId, T1.CumulativeHistogram_Interval, T1.CumulativeHistogram_Probability, T1.CumulativeHistogram_CaptureState)
    FROM (
        SELECT 
            T.CumulativeHistogramId AS CumulativeHistogram_CumulativeHistogramId, 
            T.Interval AS CumulativeHistogram_Interval, 
            T.Probability AS CumulativeHistogram_Probability, 
            T.CaptureState AS CumulativeHistogram_CaptureState, 
            True AS _from0
        FROM PacketAnalysisEntity.CumulativeHistograms AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for PacketAnalysisEntity.CumulativeHistograms.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView13()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing CumulativeHistograms
        [COWE.PacketAnalysisModel.CumulativeHistogram](T1.CumulativeHistogram_CumulativeHistogramId, T1.CumulativeHistogram_Interval, T1.CumulativeHistogram_Probability, T1.CumulativeHistogram_CaptureState)
    FROM (
        SELECT 
            T.CumulativeHistogramId AS CumulativeHistogram_CumulativeHistogramId, 
            T.Interval AS CumulativeHistogram_Interval, 
            T.Probability AS CumulativeHistogram_Probability, 
            T.CaptureState AS CumulativeHistogram_CaptureState, 
            True AS _from0
        FROM PacketAnalysisModelStoreContainer.CumulativeHistogram AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for PacketAnalysisModelStoreContainer.CumulativeProbabilityDistribution.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView14()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing CumulativeProbabilityDistribution
        [PacketAnalysisModel.Store.CumulativeProbabilityDistribution](T1.CumulativeProbabilityDistribution_CumulativeProbabilityDistributionId, T1.CumulativeProbabilityDistribution_Interval, T1.CumulativeProbabilityDistribution_Probability, T1.CumulativeProbabilityDistribution_CaptureState)
    FROM (
        SELECT 
            T.CumulativeProbabilityDistributionId AS CumulativeProbabilityDistribution_CumulativeProbabilityDistributionId, 
            T.Interval AS CumulativeProbabilityDistribution_Interval, 
            T.Probability AS CumulativeProbabilityDistribution_Probability, 
            T.CaptureState AS CumulativeProbabilityDistribution_CaptureState, 
            True AS _from0
        FROM PacketAnalysisEntity.CumulativeProbabilityDistributions AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for PacketAnalysisEntity.CumulativeProbabilityDistributions.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView15()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing CumulativeProbabilityDistributions
        [COWE.PacketAnalysisModel.CumulativeProbabilityDistribution](T1.CumulativeProbabilityDistribution_CumulativeProbabilityDistributionId, T1.CumulativeProbabilityDistribution_Interval, T1.CumulativeProbabilityDistribution_Probability, T1.CumulativeProbabilityDistribution_CaptureState)
    FROM (
        SELECT 
            T.CumulativeProbabilityDistributionId AS CumulativeProbabilityDistribution_CumulativeProbabilityDistributionId, 
            T.Interval AS CumulativeProbabilityDistribution_Interval, 
            T.Probability AS CumulativeProbabilityDistribution_Probability, 
            T.CaptureState AS CumulativeProbabilityDistribution_CaptureState, 
            True AS _from0
        FROM PacketAnalysisModelStoreContainer.CumulativeProbabilityDistribution AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for PacketAnalysisModelStoreContainer.HypothesisTest.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView16()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing HypothesisTest
        [PacketAnalysisModel.Store.HypothesisTest](T1.HypothesisTest_HypothesisTestId, T1.HypothesisTest_MeanOfMeansVariance, T1.HypothesisTest_MeansVarianceStandardDeviation, T1.HypothesisTest_KsStatistic, T1.HypothesisTest_MaxCpdVariance, T1.HypothesisTest_MeansTestResult, T1.HypothesisTest_KsTestResult, T1.HypothesisTest_HasValues)
    FROM (
        SELECT 
            T.HypothesisTestId AS HypothesisTest_HypothesisTestId, 
            T.MeanOfMeansVariance AS HypothesisTest_MeanOfMeansVariance, 
            T.MeansVarianceStandardDeviation AS HypothesisTest_MeansVarianceStandardDeviation, 
            T.KsStatistic AS HypothesisTest_KsStatistic, 
            T.MaxCpdVariance AS HypothesisTest_MaxCpdVariance, 
            T.MeansTestResult AS HypothesisTest_MeansTestResult, 
            T.KsTestResult AS HypothesisTest_KsTestResult, 
            T.HasValues AS HypothesisTest_HasValues, 
            True AS _from0
        FROM PacketAnalysisEntity.HypothesisTests AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for PacketAnalysisEntity.HypothesisTests.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView17()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing HypothesisTests
        [COWE.PacketAnalysisModel.HypothesisTest](T1.HypothesisTest_HypothesisTestId, T1.HypothesisTest_MeanOfMeansVariance, T1.HypothesisTest_MeansVarianceStandardDeviation, T1.HypothesisTest_KsStatistic, T1.HypothesisTest_MaxCpdVariance, T1.HypothesisTest_MeansTestResult, T1.HypothesisTest_KsTestResult, T1.HypothesisTest_HasValues)
    FROM (
        SELECT 
            T.HypothesisTestId AS HypothesisTest_HypothesisTestId, 
            T.MeanOfMeansVariance AS HypothesisTest_MeanOfMeansVariance, 
            T.MeansVarianceStandardDeviation AS HypothesisTest_MeansVarianceStandardDeviation, 
            T.KsStatistic AS HypothesisTest_KsStatistic, 
            T.MaxCpdVariance AS HypothesisTest_MaxCpdVariance, 
            T.MeansTestResult AS HypothesisTest_MeansTestResult, 
            T.KsTestResult AS HypothesisTest_KsTestResult, 
            T.HasValues AS HypothesisTest_HasValues, 
            True AS _from0
        FROM PacketAnalysisModelStoreContainer.HypothesisTest AS T
    ) AS T1");
        }
    }
}
