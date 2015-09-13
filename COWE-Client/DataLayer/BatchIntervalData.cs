using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.DomainClasses;
using COWE.Enumerations;

namespace COWE.DataLayer
{
    public class BatchIntervalData
    {
        #region Constructors
        public BatchIntervalData() { }

        public BatchIntervalData(string dbConnectionString)
        {
            this.DbConnectionString = dbConnectionString;
        }
        #endregion

        #region Properties
        public string DbConnectionString { get; set; }
        #endregion

        #region Public Methods
        public int CreateBatchInterval(string fileName, CaptureState marked)
        {
            //bool success = false;
            int newCaptureBatchId = 0;

            //using (var context = new PacketCaptureContext())
            using (var context = new PacketAnalysisEntity())
            {
                // Check to see if the batch already exists (it shouldn't - but we will use this for testing)
                var captureBatchId = (from c in context.CaptureBatches
                             where c.FileName == fileName
                             select c.CaptureBatchId).FirstOrDefault();

                if (captureBatchId > 0)
                {
                    // CaptureBatch already exists - just use the current CaptureBatchId
                    newCaptureBatchId = captureBatchId;
                }
                else
                {
                    // We need to add a new capture batch and get the CaptureBatchId

                    var newBatch = context.CaptureBatches.Add(new CaptureBatch());
                    newBatch.FileName = fileName;
                    newBatch.Marked = marked == CaptureState.Marked? true : false;
                    //newCaptureBatchId = context.SaveChanges();
                    context.SaveChanges();
                    newCaptureBatchId = context.CaptureBatches.Select(c => c.CaptureBatchId).Max();
                }
            }

            // success;
            return newCaptureBatchId;
        }
        public BindingList<BatchIntervalMarked> GetMarkedBatchIntervals(int captureBatchId)
        {
            BindingList<BatchIntervalMarked> intervals = new BindingList<BatchIntervalMarked>();

            using (var context = new PacketAnalysisEntity())
            {
                var batchIntervals = from b in context.BatchIntervals
                                     where b.CaptureBatchId == captureBatchId
                                     select new
                                     {
                                         b.BatchIntervalId,
                                         b.CaptureBatchId,
                                         b.IntervalNumber,
                                         b.PacketCount,
                                         b.CaptureBatch.Marked
                                     };

                foreach (var bi in batchIntervals)
                {
                    BatchIntervalMarked bim = new BatchIntervalMarked();
                    bim.BatchIntervalId = bi.BatchIntervalId;
                    bim.CaptureBatchId = bi.CaptureBatchId;
                    bim.IntervalNumber = bi.IntervalNumber;
                    bim.PacketCount = bi.PacketCount;
                    bim.Marked = bi.Marked == true ? CaptureState.Marked : CaptureState.Unmarked;
                    intervals.Add(bim);
                }
            }
            return intervals;
        }

        public bool LoadBatchIntervals(DataTable dataTable)
        {
            // Bulk-copy flooder batch interval packet counts
            bool success = false;
            //string DbConnectionString = dbConnectionString;

            // Bulk load the DataTable into the database
            using(SqlConnection conn = new SqlConnection(DbConnectionString))
            {
                conn.Open();

                using(SqlBulkCopy bc = new SqlBulkCopy(conn))
                {
                    bc.DestinationTableName = "COWE.BatchInterval";

                    try
                    {
                        bc.WriteToServer(dataTable);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error bulk-inserting batchIntervals DataTable into database: " + ex.Message);
                    }
                }

                conn.Close();
            }

            return success;
        }

        public void RemoveAllBatchIntervals()
        {
            using(var context = new PacketAnalysisEntity())
            {
                context.TruncateAllIntervalTables();
            }
        }
        #endregion
    }
}

