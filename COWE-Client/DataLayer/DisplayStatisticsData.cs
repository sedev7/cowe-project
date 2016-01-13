using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.DomainClasses;
using COWE.Enumerations;

namespace COWE.DataLayer
{
    public class DisplayStatisticsData
    {
        #region Constructors
        public DisplayStatisticsData() { }
        #endregion

        #region Public Methods
        public BindingList<DisplayStatistic> GetSingleMarkedDisplayStatistics()
        {
            BindingList<DisplayStatistic> statistics = new BindingList<DisplayStatistic>();

            using (var context = new PacketAnalysisEntity())
            {
                var data = (from d in context.DisplayStatistics
                            where d.Marked ==  true && d.BatchType == 1
                            select d).ToList();

                foreach (var stat in data)
                {
                    DisplayStatistic statistic = stat;
                    statistics.Add(stat);
                }
            }
            return statistics;
        }
        public DisplayStatistic GetLastSingleMarkedDisplayStatistics()
        {
            DisplayStatistic statistics = new DisplayStatistic();

            if (GetSingleMarkedDisplayStatistics().Count > 0)
            {
                using (var context = new PacketAnalysisEntity())
                {
                    // Get the last Id value for single marked statistic data
                    var maxid = (from m in context.DisplayStatistics
                                 where m.Marked == true && m.BatchType == 1
                                 select m.DisplayStatisticId).Max();

                    var data = (from d in context.DisplayStatistics
                                where d.DisplayStatisticId == maxid
                                select d).FirstOrDefault();

                    statistics = data;
                }
            }
            return statistics;
        }
        public BindingList<DisplayStatistic> GetSingleUnmarkedDisplayStatistics()
        {
            BindingList<DisplayStatistic> statistics = new BindingList<DisplayStatistic>();

            using (var context = new PacketAnalysisEntity())
            {
                var data = (from d in context.DisplayStatistics
                            where d.Marked == false && d.BatchType == 1
                            select d).ToList();

                foreach (var stat in data)
                {
                    DisplayStatistic statistic = stat;
                    statistics.Add(stat);
                }
            }
            return statistics;
        }
        public DisplayStatistic GetLastSingleUnmarkedDisplayStatistics()
        {
            DisplayStatistic statistics = new DisplayStatistic();

            if (GetSingleUnmarkedDisplayStatistics().Count > 0)
            {
                using (var context = new PacketAnalysisEntity())
                {
                    // Get the last Id value for single unmarked statistic data
                    var maxid = (from m in context.DisplayStatistics
                                 where m.Marked == false && m.BatchType == 1
                                 select m.DisplayStatisticId).Max();

                    var data = (from d in context.DisplayStatistics
                                where d.DisplayStatisticId == maxid
                                select d).FirstOrDefault();

                    statistics = data;
                }
            }
            return statistics;
        }

        

        public DisplayStatistic GetCumulativeMarkedDisplayStatistics()
        {
            DisplayStatistic statistics = new DisplayStatistic();

            using (var context = new PacketAnalysisEntity())
            {
                var data = (from d in context.DisplayStatistics
                            where d.Marked == true && d.BatchType == 2
                            select d).FirstOrDefault();

                statistics = data;
            }
            return statistics;
        }

        public DisplayStatistic GetCumulativeUnmarkedDisplayStatistics()
        {
            DisplayStatistic statistics = new DisplayStatistic();

            using (var context = new PacketAnalysisEntity())
            {
                var data = (from d in context.DisplayStatistics
                            where d.Marked == false && d.BatchType == 2
                            select d).FirstOrDefault();

                statistics = data;
            }
            return statistics;
        }

        public void DeleteSingleMarkedDisplayStatitics()
        {
            using (var context = new PacketAnalysisEntity())
            {
                context.DisplayStatisticsDelete(true, 1);
            }
        }

        public void DeleteSingleUnmarkedDisplayStatitics()
        {
            using (var context = new PacketAnalysisEntity())
            {
                context.DisplayStatisticsDelete(false, 1);
            }
        }
        public void DeleteCumulativeMarkedDisplayStatitics()
        {
            using (var context = new PacketAnalysisEntity())
            {
                context.DisplayStatisticsDelete(true, 2);
            }
        }

        public void DeleteCumulativeUnmarkedDisplayStatitics()
        {
            using (var context = new PacketAnalysisEntity())
            {
                context.DisplayStatisticsDelete(false, 2);
            }
        }
        public void InsertSingleMarkedDisplayStatitics(BatchStatistics batchStatistics)
        {
            using (var context = new PacketAnalysisEntity())
            {
                context.DisplayStatisticsInsert(
                    batchStatistics.IntervalCount,
                    batchStatistics.IntervalCountTrimmed,
                    batchStatistics.PacketCountMean,
                    batchStatistics.PacketCountStandardDeviation,
                    batchStatistics.PacketCountMinimum,
                    batchStatistics.PacketCountMaximum,
                    batchStatistics.MeanOfMeans,
                    batchStatistics.MeanOfMeansStandardDeviation,
                    true,   // Marked
                    Convert.ToInt32(BatchType.Single)
                );
                context.SaveChanges();
            }
        }
        public void InsertSingleUnarkedDisplayStatitics(BatchStatistics batchStatistics)
        {
            using (var context = new PacketAnalysisEntity())
            {
                context.DisplayStatisticsInsert(
                    batchStatistics.IntervalCount,
                    batchStatistics.IntervalCountTrimmed,
                    batchStatistics.PacketCountMean,
                    batchStatistics.PacketCountStandardDeviation,
                    batchStatistics.PacketCountMinimum,
                    batchStatistics.PacketCountMaximum,
                    batchStatistics.MeanOfMeans,
                    batchStatistics.MeanOfMeansStandardDeviation,
                    false,   // Marked
                    Convert.ToInt32(BatchType.Single)
                );
                context.SaveChanges();
            }
        }
        public void InsertCumulativeMarkedDisplayStatitics(BatchStatistics batchStatistics)
        {
            using (var context = new PacketAnalysisEntity())
            {
                context.DisplayStatisticsInsert(
                    batchStatistics.IntervalCount,
                    batchStatistics.IntervalCountTrimmed,
                    batchStatistics.PacketCountMean,
                    batchStatistics.PacketCountStandardDeviation,
                    batchStatistics.PacketCountMinimum,
                    batchStatistics.PacketCountMaximum,
                    batchStatistics.MeanOfMeans,
                    batchStatistics.MeanOfMeansStandardDeviation,
                    true,   // Marked
                    Convert.ToInt32(BatchType.Cumulative)
                );
                context.SaveChanges();
            }
        }
        public void InsertCumulativeUnarkedDisplayStatitics(BatchStatistics batchStatistics)
        {
            using (var context = new PacketAnalysisEntity())
            {
                context.DisplayStatisticsInsert(
                    batchStatistics.IntervalCount,
                    batchStatistics.IntervalCountTrimmed,
                    batchStatistics.PacketCountMean,
                    batchStatistics.PacketCountStandardDeviation,
                    batchStatistics.PacketCountMinimum,
                    batchStatistics.PacketCountMaximum,
                    batchStatistics.MeanOfMeans,
                    batchStatistics.MeanOfMeansStandardDeviation,
                    false,   // Marked
                    Convert.ToInt32(BatchType.Cumulative)
                );
                context.SaveChanges();
            }
        }
        public void UpdateDisplayStatsSavedFlag(int captureBatchId, BatchType batchType, bool saveData)
        {
            using (var context = new PacketAnalysisEntity())
            {
                var captureBatch = (from b in context.CaptureBatches
                                    where b.CaptureBatchId == captureBatchId
                                    select b).Single();

                if (batchType == BatchType.Single)
                {
                    captureBatch.SingleStatistics = saveData;
                }
                else if(batchType == BatchType.Cumulative)
                {
                    captureBatch.CumulativeStatistics = saveData;
                }
                else
                {
                    // no op - unknown batch type
                }

                context.SaveChanges();
            }
        }
        public void DeleteHypothesisTestResults()
        {
            using (var context = new PacketAnalysisEntity())
            {
                // Check to see if the table is empty
                var isEmpty = (from e in context.HypothesisTests select e).Count();

                // If not empty delete any rows
                if (isEmpty > 0)
                {
                    var deleteResults = (from t in context.HypothesisTests select t).ToList();

                    foreach (HypothesisTest ht in deleteResults)
                    {
                        context.HypothesisTests.Remove(ht);    
                    }
                    context.SaveChanges();
                }
            }
        }
        public HypothesisTest GetHypothesisTestResults()
        {
            HypothesisTest ht = new HypothesisTest();

            using (var context = new PacketAnalysisEntity())
            {
                var testResults = (from t in context.HypothesisTests select t).FirstOrDefault();
                ht = testResults as HypothesisTest;
            }
            return ht;
        }
        public void InsertHypothesisTestResults(HypothesisTest testResults)
        {
            using (var context = new PacketAnalysisEntity())
            {
                context.HypothesisTests.Add(testResults);
                context.SaveChanges();
            }
        }
        #endregion

    }
}
