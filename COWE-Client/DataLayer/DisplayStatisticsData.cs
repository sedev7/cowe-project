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
            }
            return statistics;
        }

        public BindingList<DisplayStatistic> GetCumulativeMarkedDisplayStatistics()
        {
            BindingList<DisplayStatistic> statistics = new BindingList<DisplayStatistic>();

            using (var context = new PacketAnalysisEntity())
            {
                var data = (from d in context.DisplayStatistics
                            where d.Marked == true && d.BatchType == 2
                            select d).ToList();
            }
            return statistics;
        }

        public BindingList<DisplayStatistic> GetCumulativeUnmarkedDisplayStatistics()
        {
            BindingList<DisplayStatistic> statistics = new BindingList<DisplayStatistic>();

            using (var context = new PacketAnalysisEntity())
            {
                var data = (from d in context.DisplayStatistics
                            where d.Marked == false && d.BatchType == 2
                            select d).ToList();
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
        #endregion

    }
}
