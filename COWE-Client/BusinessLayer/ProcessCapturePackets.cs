using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.DataLayer;
using COWE.DomainClasses;
using COWE.Enumerations;

namespace COWE.BusinessLayer
{
    public class ProcessCapturePackets
    {
        #region Global Variables
        Decimal _PacketCount = 0;
        Decimal _ProcessedPacketCount = 0;
        #endregion

        #region Constructors
        public ProcessCapturePackets() { }
        #endregion

        #region Custom EventArgs
        public class LoadingCapturePacketsEventArgs : EventArgs
        {
            #region Automatic Properties
            public int PercentCompleted { get; set; }
            #endregion

            #region Constructor
            public LoadingCapturePacketsEventArgs() { }
            #endregion

        }
        #endregion

        #region Delegates
        public delegate void LoadCapturePacketsProgressEventHandler(object sender, LoadingCapturePacketsEventArgs e);
        #endregion

        #region Events
        public event LoadCapturePacketsProgressEventHandler LoadCapturePacketsProgress;


        #endregion

        #region Public Methods
        public BindingList<RawPacket> LoadPackets()
        {
            BindingList<RawPacket> packets = new BindingList<RawPacket>();

            try
            {
                //using (var context = new PacketCaptureContext())
                using (var context = new PacketAnalysisEntity())
                {
                    //var capturePackets = context.CapturePackets.ToList();
                    //var batch = context.BatchIntervals.ToList();

                    //foreach (var pkt in capturePackets)
                    foreach (var pkt in context.CapturePackets)
                    {
                        RawPacket packet = new RawPacket();
                        packet.CapturePacketId = pkt.CapturePacketId;
                        //packet.FileName = pkt.FileName;
                        packet.PacketNumber = pkt.PacketNumber;
                        packet.TimeStamp = pkt.TimeStamp;
                        packet.Marked = pkt.Marked == true? CaptureState.Marked : CaptureState.Unmarked;
                        packets.Add(packet);
                    }
                }
                return packets;
            }
            catch (Exception ex)
            {
                throw new Exception ("Error retrieving capture packet data: " + ex.Message);
            }
        }

        public BindingList<CapturePacket> GetCapturePackets(string fileName)
        {
            BindingList<CapturePacket> packets = new BindingList<CapturePacket>();

            try
            {
                //using (var context = new PacketCaptureContext())
                using (var context = new PacketAnalysisEntity())
                {
                    //var captureBatchId = (from c in context.CaptureBatches
                    //                      where c.FileName == fileName
                    //                      select c).FirstOrDefault();

                    //var capturePackets = context.CapturePackets.ToList();
                    //var batch = context.BatchIntervals.ToList();

                    //var capturePackets = (from c in context.CapturePackets
                    //                      where c.FileName == fileName
                    //                      select c).ToList();

                    //var capturePackets = (from c in context.CapturePackets
                    //                      where c.CaptureBatchId == captureBatchId
                    //                      select c).ToList();

                    var captureBatchId = (from b in context.CaptureBatches
                                          where b.FileName == fileName
                                          select b.CaptureBatchId).FirstOrDefault();

                    var capturePackets = (from c in context.CapturePackets
                                          where c.CaptureBatchId == captureBatchId
                                          select c).ToList();


                    foreach (var pkt in capturePackets)
                    {
                        CapturePacket packet = new CapturePacket();
                        packet.CapturePacketId = pkt.CapturePacketId;
                        packet.CaptureBatchId = pkt.CaptureBatchId;
                        packet.PacketNumber = pkt.PacketNumber;
                        packet.TimeStamp = pkt.TimeStamp;
                        packet.Marked = pkt.Marked;
                        packets.Add(packet);
                    }
                }
                return packets;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving capture packet data for file [" + fileName + "]: " + ex.Message);
            }
        }

        public BindingList<RawPacket> LoadPackets(string fileName)
        {
            BindingList<RawPacket> packets = new BindingList<RawPacket>();

            try
            {
                //using (var context = new PacketCaptureContext())
                using (var context = new PacketAnalysisEntity())
                {
                    //var captureBatchId = (from c in context.CaptureBatches
                    //                      where c.FileName == fileName
                    //                      select c).FirstOrDefault();

                    //var capturePackets = context.CapturePackets.ToList();
                    //var batch = context.BatchIntervals.ToList();

                    //var capturePackets = (from c in context.CapturePackets
                    //                      where c.FileName == fileName
                    //                      select c).ToList();

                    //var capturePackets = (from c in context.CapturePackets
                    //                      where c.CaptureBatchId == captureBatchId
                    //                      select c).ToList();

                    var captureBatchId = (from b in context.CaptureBatches
                                          where b.FileName == fileName
                                          select b.CaptureBatchId).FirstOrDefault();

                    var capturePackets = (from c in context.CapturePackets
                                          where c.CaptureBatchId == captureBatchId
                                          select c).ToList();


                    foreach (var pkt in capturePackets)
                    {
                        RawPacket packet = new RawPacket();
                        packet.CapturePacketId = pkt.CapturePacketId;
                        //packet.FileName = pkt.FileName;
                        packet.CaptureBatchId = pkt.CaptureBatchId;
                        packet.PacketNumber = pkt.PacketNumber;
                        packet.TimeStamp = pkt.TimeStamp;
                        packet.Marked = pkt.Marked == true? CaptureState.Marked : CaptureState.Unmarked;
                        packets.Add(packet);
                    }
                }
                return packets;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving capture packet data for file [" + fileName + "]: " + ex.Message);
            }
        }

        public void ProcessPacketFile(string fileName)
        {
            BindingList<RawPacket> rawPackets = new BindingList<RawPacket>();
            BindingList<CapturePacket> capturePackets = new BindingList<CapturePacket>();

            try
            {
                ////using (var context = new PacketCaptureContext())
                //using (var context = new PacketAnalysisEntity())
                //{
                //var capturePackets = context.CapturePackets.ToList();
                //var batch = context.BatchIntervals.ToList();

                CaptureFileData cfd = new CaptureFileData();
                capturePackets = cfd.GetRawPacketData();

                //foreach (var pkt in capturePackets)
                //foreach (var pkt in context.CapturePackets)
                foreach (var pkt in capturePackets)
                {
                    RawPacket packet = new RawPacket();
                    packet.CapturePacketId = pkt.CapturePacketId;
                    //packet.FileName = pkt.FileName;
                    packet.PacketNumber = pkt.PacketNumber;
                    packet.TimeStamp = pkt.TimeStamp;
                    packet.Marked = pkt.Marked == true? CaptureState.Marked : CaptureState.Unmarked;
                    rawPackets.Add(packet);

                    _PacketCount = rawPackets.Count;
                }
                //}
                //_PacketCount = rawPackets.Count;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving capture packet data: " + ex.Message);
            }
        }

        public CurrentCaptureFile GetCurrentCaptureFile(string fileName)
        {
            CurrentCaptureFile ccf = null;

            try
            {
                CaptureFileData cfd = new CaptureFileData();
                ccf = cfd.GetCurrentCaptureFile(fileName);

                return ccf;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving current capture file information: " + ex.Message);
            }
        }

        public BindingList<PacketInterval> CalculateIntervalCounts(BindingList<RawPacket> packets, int intervalSizeMs)
        {
            // Iterate through the packets and determine the number of packet arrivals for each interval
            // Interval size is in milliseconds

            BindingList<PacketInterval> intervalCounts = new BindingList<PacketInterval>();

            bool IsOpenInterval = false;

            DateTime intervalStartTime = packets[0].TimeStamp;
            CaptureState packetState = packets[0].Marked;
            int previousPacketNumber = -1;
            int interval = 0;
            int intervalCount = 0;
            int captureBatchId = packets[0].CaptureBatchId;

            foreach (RawPacket pkt in packets)
            {
                // Verify that the packet number is the next sequential number
                if (pkt.PacketNumber == ++previousPacketNumber)
                {
                    if (pkt.TimeStamp < intervalStartTime.AddMilliseconds(intervalSizeMs))
                    {
                        // We are within the current interval
                        intervalCount++;
                        previousPacketNumber = pkt.PacketNumber;
                        IsOpenInterval = true;
                    }
                    else if ((pkt.TimeStamp.Millisecond - intervalStartTime.Millisecond) > intervalSizeMs)
                    {
                        // This packet arrived after the next interval - so we skip an interval

                        // Add the current interval to the list (add another interval)
                        PacketInterval pi = new PacketInterval(captureBatchId, interval, intervalCount, packetState);
                        intervalCounts.Add(pi);

                        // Add the skipped (empty) interval to the list (reset the count, add an interval)
                        intervalCount = 0;
                        PacketInterval pi2 = new PacketInterval(captureBatchId, interval++, intervalCount, packetState);

                        // Create a new interval
                        intervalStartTime = intervalStartTime.AddMilliseconds(intervalSizeMs);
                        previousPacketNumber = pkt.PacketNumber;
                        // If this is the last packet, don't add another interval because the packet count is zero
                        IsOpenInterval = false;
                    }
                    else
                    {
                        // This packet arrived during the next interval

                        // Add the current interval to the list (add another interval)
                        PacketInterval pi = new PacketInterval(captureBatchId, interval++, intervalCount, packetState);
                        intervalCounts.Add(pi);
                        
                        // Create a new interval
                        intervalStartTime = intervalStartTime.AddMilliseconds(intervalSizeMs);
                        intervalCount = 1;
                        previousPacketNumber = pkt.PacketNumber;
                        IsOpenInterval = true;
                    }
                }
                else
                {
                    throw new Exception ("Packet numbers are out of sequence!");
                }
            }

            // Add the last interval if it is still open
            if(IsOpenInterval)
            {
                PacketInterval pi = new PacketInterval(captureBatchId, interval, intervalCount, packetState);
                intervalCounts.Add(pi);
            }
            return intervalCounts;
        }
        //public bool SaveBatchIntervals(string dbConnectionString, BindingList<PacketInterval> intervals, string fileName, int captureBatchId, CaptureState marked)
        public bool SaveBatchIntervals(string dbConnectionString, BindingList<PacketInterval> intervals)
        {
            bool success = false;

            DataTable BatchIntervalsDataTable = null;

            // Create a data table for bulk-loading the data
            DataTable dt = CreateBatchIntervalDataTable();

            try
            {
                // Load the interval counts into the data table
                BatchIntervalsDataTable = new DataTable();
                //BatchIntervalsDataTable = LoadBatchIntervalDataTable(dt, intervals, captureBatchId);
                BatchIntervalsDataTable = LoadBatchIntervalDataTable(dt, intervals);
                success = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading batch intervals into DataTable: " + ex.Message);
            }

            try
            {
                if (success)
                {
                    // Save the batch intervals to the database
                    BatchIntervalData bid = new BatchIntervalData(dbConnectionString);
                    bid.LoadBatchIntervals(BatchIntervalsDataTable);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error bulk-loading batch intervals into database: " + ex.Message);
            }

            return success;
        }

        //// No longer needed - done by ParseCaptureFilesService
        //public int CreateIntervalBatchIdentifier(string fileName, bool marked)
        //{
        //    int captureBatchId = 0;
        //    try
        //    {
        //        BatchIntervalData bi = new BatchIntervalData();
        //        captureBatchId = bi.CreateBatchInterval(fileName, marked);
        //        return captureBatchId;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error creating interval batch identifier: " + ex.Message + "\r\nInner Exception: " + ex.InnerException);
        //    }
        //}

        public bool UpdateCumulativeIntervals(string dbConnectionString, BindingList<PacketInterval> intervalCounts, CaptureState marked)
        {
            bool success = false;

            //BindingList<BatchIntervalMarked> markedBatchIntervals = new BindingList<BatchIntervalMarked>();
            BindingList<CumulativeInterval> cumulativeIntervals = new BindingList<CumulativeInterval>();
            BatchIntervalData bid = new BatchIntervalData();
            CumulativeIntervalData cid = new CumulativeIntervalData();

            cumulativeIntervals = cid.GetCumulativeIntervals();
            
            if(cumulativeIntervals.Count == 0)
            {
                // No cumulative intervals yet - use the last batch intervals
                foreach (PacketInterval ic in intervalCounts)
                {
                    CumulativeInterval ci = new CumulativeInterval();
                    ci.CumulativeIntervalNumber = ic.Interval;
                    ci.PacketCount = ic.PacketCount;
                    ci.Marked = ic.PacketState == CaptureState.Marked? true : false;
                    cumulativeIntervals.Add(ci);
                }
            }
            else
            {
                // Add the new batch intervals to the cumulative intervals
                // Make sure they are for the correct marked set!

                // Get the cumulative intervals for the marked type
                cumulativeIntervals = cid.GetMarkedCumulativeIntervals(marked);

                for (int i = 0; i < cumulativeIntervals.Count; i++)
                {
                    cumulativeIntervals[i].PacketCount += intervalCounts[i].PacketCount;
                }

                if (intervalCounts.Count >= cumulativeIntervals.Count)
                {
                    int lastIntervalNumber = cid.LastIntervalNumber;

                    // There are additional intervals in this batch
                    // Add them to the cumulative intervals
                    foreach (PacketInterval pi in intervalCounts)
                    {
                        if (pi.Interval >= cumulativeIntervals.Count)
                        {
                            CumulativeInterval newci = new CumulativeInterval();
                            // Get the last cumulative interval number
                            //var cin = (from c in cumulativeIntervals select c.CumulativeIntervalNumber).Max();
                            //newci.CumulativeIntervalNumber = pi.Interval;
                            //newci.CumulativeIntervalNumber = cin + 1;
                            newci.CumulativeIntervalNumber = lastIntervalNumber++;
                            newci.PacketCount = pi.PacketCount;
                            newci.Marked = pi.PacketState == CaptureState.Marked ? true : false;
                            cumulativeIntervals.Add(newci);
                        }
                    }
                }

            }

            // Save the cumulative interval counts
            // Delete the old counts first
            cid.DeleteCumulativeIntervals(marked);
            cid.DbConnectionString = dbConnectionString;

            // Create a DataTable to hold the cumulative intervals
            DataTable CumulativeIntervalsDataTable = null;

            // Create a data table for bulk-loading the data
            DataTable dt = CreateCumulativeIntervalDataTable();

            try
            {
                // Load the cumulative interval counts into the data table
                CumulativeIntervalsDataTable = new DataTable();
                CumulativeIntervalsDataTable = LoadCumulativeIntervalDataTable(dt, cumulativeIntervals);
                success = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading cumulative intervals into DataTable: " + ex.Message);
            }

            if (success)
            {
                try
                {
                    // Load the cumulative interval counts into the database
                    success = cid.LoadCumulativeIntervals(CumulativeIntervalsDataTable);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error loading cumulative intervals into database: " + ex.Message);
                }
            }

            return success;
        }
        public BindingList<BatchIntervalMarked> GetMarkedBatchIntervals(int captureBatchId)
        {
            BindingList<BatchIntervalMarked> markedIntervals = new BindingList<BatchIntervalMarked>();
            BatchIntervalData bid = new BatchIntervalData();
            markedIntervals = bid.GetMarkedBatchIntervals(captureBatchId);
            return markedIntervals;
        }

        public BindingList<CurrentCaptureFile> GetLastCaptureBatchIds()
        {
            BindingList<CurrentCaptureFile> lastBatchIds = new BindingList<CurrentCaptureFile>();

            CaptureFileData cfd = new CaptureFileData();
            var batch = cfd.GetBatchIds();

            var lastMarkedBatchId = (from m in batch
                                     where m.Marked == CaptureState.Marked
                                     orderby m.CaptureBatchId descending
                                     select m).First();

            var lastUnmarkedBatchId = (from m in batch
                                     where m.Marked == CaptureState.Unmarked
                                     orderby m.CaptureBatchId descending
                                     select m).First();

            lastBatchIds.Add(lastMarkedBatchId);
            lastBatchIds.Add(lastUnmarkedBatchId);

            return lastBatchIds;
        }

        public BindingList<CumulativeInterval> GetCumulativeIntervals()
        {
            BindingList<CumulativeInterval> intervals = new BindingList<CumulativeInterval>();

            CumulativeIntervalData cid = new CumulativeIntervalData();
            intervals = cid.GetCumulativeIntervals();

            return intervals;
        }
 
        #endregion

        #region Private Methods
        private DataTable CreateBatchIntervalDataTable()
        {
            // Create the data columns (same as the BatchInterval database table)
            DataColumn BatchIntervalIdDataColumn = new DataColumn("BatchIntervalId", typeof(int));
            BatchIntervalIdDataColumn.Caption = "BatchIntervalId";
            BatchIntervalIdDataColumn.ReadOnly = false;
            BatchIntervalIdDataColumn.AllowDBNull = false;
            BatchIntervalIdDataColumn.Unique = true;
            BatchIntervalIdDataColumn.AutoIncrement = true;
            BatchIntervalIdDataColumn.AutoIncrementSeed = 1;
            BatchIntervalIdDataColumn.AutoIncrementStep = 1;

            DataColumn CaptureBatchIdDataColumn = new DataColumn("CaptureBatchId", typeof(int));
            CaptureBatchIdDataColumn.Caption = "CaptureBatchId";
            CaptureBatchIdDataColumn.ReadOnly = false;
            CaptureBatchIdDataColumn.AllowDBNull = false;
            CaptureBatchIdDataColumn.Unique = false;

            DataColumn IntervalNumberDataColumn = new DataColumn("IntervalNumber", typeof(int));
            IntervalNumberDataColumn.Caption = "IntervalNumber";
            IntervalNumberDataColumn.ReadOnly = false;
            IntervalNumberDataColumn.AllowDBNull = false;
            IntervalNumberDataColumn.Unique = true;

            DataColumn PacketCountDataColumn = new DataColumn("PacketCount", typeof(int));
            PacketCountDataColumn.Caption = "PacketCount";
            PacketCountDataColumn.ReadOnly = false;
            PacketCountDataColumn.AllowDBNull = false;
            PacketCountDataColumn.Unique = false;

            // Add the columns to the DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { BatchIntervalIdDataColumn, CaptureBatchIdDataColumn, IntervalNumberDataColumn, PacketCountDataColumn });

            // return the empty DataTable
            return dt;
        }

        //private DataTable LoadBatchIntervalDataTable(DataTable dt, BindingList<PacketInterval> intervals, int batchId)
        private DataTable LoadBatchIntervalDataTable(DataTable dt, BindingList<PacketInterval> intervals)
        {
            // Load packet count interval data into the DataTable
            foreach (PacketInterval pi in intervals)
            {
                DataRow dr = dt.NewRow();
                dr["CaptureBatchId"] = pi.CaptureBatchId;
                dr["IntervalNumber"] = pi.Interval;
                dr["PacketCount"] = pi.PacketCount;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private DataTable CreateCumulativeIntervalDataTable()
        {
            // Note: we are not using an identity column in this table
            DataColumn CumulativeIntervalNumberDataColumn = new DataColumn("CumulativeIntervalNumber", typeof(int));
            CumulativeIntervalNumberDataColumn.Caption = "CumulativeIntervalNumber";
            CumulativeIntervalNumberDataColumn.ReadOnly = false;
            CumulativeIntervalNumberDataColumn.AllowDBNull = false;
            CumulativeIntervalNumberDataColumn.Unique = true;

            DataColumn PacketCountDataColumn = new DataColumn("PacketCount", typeof(int));
            PacketCountDataColumn.Caption = "PacketCount";
            PacketCountDataColumn.ReadOnly = false;
            PacketCountDataColumn.AllowDBNull = false;
            PacketCountDataColumn.Unique = false;

            DataColumn MarkedDataColumn = new DataColumn("Marked", typeof(int));
            MarkedDataColumn.Caption = "Marked";
            MarkedDataColumn.ReadOnly = false;
            MarkedDataColumn.AllowDBNull = false;
            MarkedDataColumn.Unique = false;

            // Add the columns to the DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { CumulativeIntervalNumberDataColumn, PacketCountDataColumn, MarkedDataColumn });

            // return the empty DataTable
            return dt;
        }

        private DataTable LoadCumulativeIntervalDataTable(DataTable dt, BindingList<CumulativeInterval> intervals)
        {
            // Load packet count interval data into the DataTable
            foreach (CumulativeInterval ci in intervals)
            {
                DataRow dr = dt.NewRow();
                //dr["CumulativeIntervalId"] = ci.CumulativeIntervalId;
                dr["CumulativeIntervalNumber"] = ci.CumulativeIntervalNumber;
                dr["PacketCount"] = ci.PacketCount;
                dr["Marked"] = ci.Marked;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion
    }
}

