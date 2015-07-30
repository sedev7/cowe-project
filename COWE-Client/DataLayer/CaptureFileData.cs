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
    public class CaptureFileData
    {
        public CaptureFileData() { }

        public CurrentCaptureFile GetCurrentCaptureFile(string fileName)
        {
            CurrentCaptureFile ccf = null;

            //using (var context = new PacketCaptureContext())
            using (var context = new PacketAnalysisEntity())
            {
                var captureBatchId = (from b in context.CaptureBatches
                                      where b.FileName == fileName
                                      select b.CaptureBatchId).FirstOrDefault();

                var marked = (from m in context.CapturePackets
                              where m.CaptureBatchId == Convert.ToInt32(captureBatchId) 
                              select m.Marked).FirstOrDefault();

                ccf = new CurrentCaptureFile(fileName, Convert.ToInt32(captureBatchId), marked == true? CaptureState.Marked : CaptureState.Unmarked);
            }
            return ccf;
        }

        public BindingList<CapturePacket> GetRawPacketData()
        {
            BindingList<CapturePacket> packets = new BindingList<CapturePacket>();

            using (var context = new PacketAnalysisEntity())
            {
                foreach (CapturePacket cp in context.CapturePackets)
                {
                    packets.Add(cp);
                }                
            }
            return packets;
        }

        public BindingList<CurrentCaptureFile> GetBatchIds()
        {
            BindingList<CurrentCaptureFile> batchIds = new BindingList<CurrentCaptureFile>();

            using (var context = new PacketAnalysisEntity())
            {
                var items = (from b in context.CaptureBatches
                             select new
                             {
                                 b.FileName,
                                 b.CaptureBatchId,
                                 b.Marked
                             }).ToList();

                foreach (var item in items)
                {
                    CurrentCaptureFile ccf = new CurrentCaptureFile();
                    ccf.FileName = item.FileName;
                    ccf.CaptureBatchId = item.CaptureBatchId;
                    ccf.Marked = item.Marked == true ? CaptureState.Marked : CaptureState.Unmarked;
                    batchIds.Add(ccf);
                }
            }

            return batchIds;
        }

        public bool UpdateBatchMean(int captureBatchId, decimal mean, decimal trimmedMean)
        {
            bool result = false;

            using (var context = new PacketAnalysisEntity())
            {
                var batch = context.CaptureBatches.Where(c => c.CaptureBatchId == captureBatchId).FirstOrDefault();
                batch.Mean = mean;
                batch.TrimmedMean = trimmedMean;
                context.SaveChanges();
                result = true;
            }
            return result;
        }

        public decimal CalculateMeanOfMeans(CaptureState captureState, bool trimmed)
        {
            bool marked = captureState == CaptureState.Marked ? true : false;
            bool _trimmed = trimmed;
            decimal meanOfMeans = 0;
            decimal sumOfMeans = 0;
            List<decimal> means = new List<decimal>();

            using (var context = new PacketAnalysisEntity())
            {
                if (_trimmed)
                {
                    means = (from m in context.CaptureBatches
                             where m.Marked == marked
                             select m.TrimmedMean).ToList();
                }
                else
                {
                    means = (from m in context.CaptureBatches
                             where m.Marked == marked
                             select m.Mean).ToList();
                }

                foreach (var mean in means)
                {
                    sumOfMeans += mean;
                }

                meanOfMeans = sumOfMeans / means.Count;
            }

            return meanOfMeans;
        }

        public decimal CalculateStdDevForMeanOfMeans(CaptureState captureState, bool trimmed)
        {
            decimal stdDev = 0;
            bool marked = captureState == CaptureState.Marked ? true : false;
            bool _trimmed = trimmed;
            decimal meanOfMeans = 0;
            decimal sumOfMeans = 0;
            decimal variance = 0;
            decimal varianceSum = 0;

            List<decimal> means = new List<decimal>();

            // Get the mean of means
            using (var context = new PacketAnalysisEntity())
            {
                if (_trimmed)
                {
                    means = (from m in context.CaptureBatches
                             where m.Marked == marked
                             select m.TrimmedMean).ToList();
                }
                else
                {
                    means = (from m in context.CaptureBatches
                             where m.Marked == marked
                             select m.Mean).ToList();
                }

                foreach (var mean in means)
                {
                    sumOfMeans += mean;
                }

                meanOfMeans = sumOfMeans / means.Count;

                // Calculate the standard deviation
                foreach (var mean in means)
                {
                    varianceSum += Convert.ToDecimal(Math.Pow(Convert.ToDouble(meanOfMeans - mean), 2));
                }
                variance = varianceSum / (means.Count - 1);
            }

            return stdDev = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(variance)));
        }
    }
}
