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
        #region Constructors
        public CaptureFileData() { }
        #endregion

        #region Public Methods
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
                              //where m.CaptureBatchId == Convert.ToInt32(captureBatchId) 
                              where m.CaptureBatchId == captureBatchId 
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
                    ccf.CaptureState = item.Marked == true ? CaptureState.Marked : CaptureState.Unmarked;
                    batchIds.Add(ccf);
                }
            }

            return batchIds;
        }

        public int GetLastBatchId()
        {
            int lastBatchId = 0;

            using (var context = new PacketAnalysisEntity())
            {
                var batchId = (from b in context.CaptureBatches
                               select b.CaptureBatchId).Max();

                lastBatchId = Convert.ToInt32(batchId);
            }
            return lastBatchId;
        }

        public int GetBatchId(string fileName)
        {
            // Get BatchId for the file name that was passed in
            int BatchId = 0;

            using(var context = new PacketAnalysisEntity())
            {
                var batchId = (from b in context.CaptureBatches
                               where b.FileName == fileName
                               select b.CaptureBatchId).FirstOrDefault();

                BatchId = Convert.ToInt32(batchId);
            }
            return BatchId;
        }
        public bool GetParsedFileStatus(int batchId)
        {
            bool parseStatus = false;

            using(var context = new PacketAnalysisEntity())
            {
                var status = (from s in context.CaptureBatches
                              where s.CaptureBatchId == batchId
                              select s.Parsed).FirstOrDefault();
                parseStatus = status;
            }
            return parseStatus;
        }
        public BindingList<CurrentCaptureFile> GetAllFiles()
        {
            BindingList<CurrentCaptureFile> files = new BindingList<CurrentCaptureFile>();

            using (var context = new PacketAnalysisEntity())
            {
                var fileList = (from f in context.CaptureBatches
                                select f).ToList();

                foreach (var record in fileList)
                {
                    CurrentCaptureFile ccf = new CurrentCaptureFile();
                    ccf.CaptureBatchId = record.CaptureBatchId;
                    ccf.CaptureState = record.Marked == true ? CaptureState.Marked : CaptureState.Unmarked;
                    ccf.FileName = record.FileName;
                    files.Add(ccf);
                }
            }
            return files;
        }
        public CurrentCaptureFile GetLastCaptureBatchRecord()
        {
            CurrentCaptureFile lastRecord = new CurrentCaptureFile();

            using (var context = new PacketAnalysisEntity())
            {
                var batchId = (from b in context.CaptureBatches
                               where b.Parsed == true
                               select b.CaptureBatchId).Max();

                var record = (from r in context.CaptureBatches
                                  where r.CaptureBatchId == batchId
                                  select new 
                                  {
                                      r.CaptureBatchId,
                                      r.FileName,
                                      r.Marked
                                  }).FirstOrDefault();

                lastRecord.CaptureBatchId = record.CaptureBatchId;
                lastRecord.FileName = record.FileName;
                lastRecord.CaptureState = record.Marked == true ? CaptureState.Marked : CaptureState.Unmarked;
            }

            return lastRecord;
        }
        public int GetProcessedFilesCountMarked()
        {
            // Gets the number of marked files that have been parsed and for which statistics have been calculated
            int fileCount = 0;

            using(var context = new PacketAnalysisEntity())
            {
                var count = (from c in context.CaptureBatches
                             where c.Marked == true && c.CumulativeStatistics == true
                             select c).Count();

                fileCount = Convert.ToInt32(count);
            }
            return fileCount;
        }
        public int GetProcessedFilesCountUnmarked()
        {
            // Gets the number of unmarked files that have been parsed and for which statistics have been calculated
            int fileCount = 0;

            using (var context = new PacketAnalysisEntity())
            {
                var count = (from c in context.CaptureBatches
                             where c.Marked == false && c.CumulativeStatistics == true
                             select c).Count();

                fileCount = Convert.ToInt32(count);
            }
            return fileCount;
        }
        public int GetRawFileCountMarked()
        {
            // Gets the total number of marked files
            int fileCount = 0;

            using (var context = new PacketAnalysisEntity())
            {
                var count = (from c in context.CaptureBatches
                             where c.Marked == true
                             select c).Count();

                fileCount = Convert.ToInt32(count);
            }
            return fileCount;
        }
        public int GetRawFileCountUnmarked()
        {
            // Gets the total number of marked files
            int fileCount = 0;

            using (var context = new PacketAnalysisEntity())
            {
                var count = (from c in context.CaptureBatches
                             where c.Marked == false
                             select c).Count();

                fileCount = Convert.ToInt32(count);
            }
            return fileCount;
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

        public void UpdateCaptureBatchParseStatus(int captureBatchId)
        {
            using (var context = new PacketAnalysisEntity())
            {
                var captureBatch = (from b in context.CaptureBatches
                                    where b.CaptureBatchId == captureBatchId
                                    select b).Single();

                captureBatch.Parsed = true;

                context.SaveChanges();
            }
        }

        public decimal CalculateMeanOfMeans(CaptureState captureState, bool trimmed)
        {
            bool marked = captureState == CaptureState.Marked ? true : false;
            bool _trimmed = trimmed;
            decimal meanOfMeans = 0;
            decimal sumOfMeans = 0;
            int meansCount = 0;

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

                meansCount = means.Count;

                foreach (var mean in means)
                {
                    // Only include means that are greater than zero (i.e., don't 
                    // include batch means for batches where mean has not yet  been
                    // calculated) - note: this should only happen when testing
                    // (i.e., multiple capture batch files, each being processed serially)
                    if (mean > 0)
                    {
                        sumOfMeans += mean;
                    }
                    else
                    {
                        // Mean is zero, don't include it
                        meansCount--;
                    }
                }

                meanOfMeans = sumOfMeans / (meansCount <= 1 ? 1 : meansCount);
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
                if (means.Count > 1)
                {
                    variance = varianceSum / (means.Count - 1);
                }
                else
                {
                    variance = varianceSum / (means.Count);
                }
            }

            return stdDev = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(variance)));
        }
        public int GetMeanCount()
        {
            int meanCount = 0;

            // Get count of files with mean already calculated (i.e., mean must be greater
            // than zero, as opposed to files in CaptureBatch for which a mean has not yet
            // been calculated) - note: this should only happen when testing (i.e., multiple
            // capture batch files, each being processed serially)
            using (var context = new PacketAnalysisEntity())
            {
                var count = (from c in context.CaptureBatches
                             where c.Mean > 0
                             select c.Mean).Count();

                meanCount = Convert.ToInt32(count);
            }
            return meanCount;
        }
        public decimal GetMean(CaptureState captureState, bool trimmed)
        {
            bool marked = captureState == CaptureState.Marked? true : false;
            decimal mean = 0;

            using (var context = new PacketAnalysisEntity())
            {
                if (trimmed)
                {
                    var x = (from m in context.CaptureBatches
                             where m.Marked == marked
                             select m.TrimmedMean).FirstOrDefault();

                    mean = Convert.ToDecimal(x);
                }
                else
                {
                    var x = (from m in context.CaptureBatches
                             where m.Marked == marked
                             select m.TrimmedMean).FirstOrDefault();

                    mean = Convert.ToDecimal(x);
                }
            }
            return mean;
        }
        public void TruncateAllTables()
        {
            using (var context = new PacketAnalysisEntity())
            {
                context.TruncateAllTables();
            }
        }
        #endregion
    }
}
