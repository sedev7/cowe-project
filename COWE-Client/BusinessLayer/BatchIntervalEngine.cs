using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using COWE.DataLayer;
using COWE.DomainClasses;
using COWE.Enumerations;

namespace COWE.BusinessLayer
{
    public class BatchIntervalEngine
    {
        #region Private Variables
        CalculateProbability _CalculateProbability = null;

        int _CurrentCaptureBatchId = 0;
        int _IntervalSize = 0;
        int _WaitSecondsLimit = 0;

        string _CaptureFileName = string.Empty;
        string _DbConnectionString = string.Empty;
        string _ProcessedCaptureFilesPath = string.Empty;
        #endregion

        #region Constructors
        public BatchIntervalEngine(string dbConnectionString, string processedCaptureFilesPath, string captureFileName, int waitSecondsTimeLimit, int intervalSize)
        {
            if (dbConnectionString != null)
            {
                this._DbConnectionString = dbConnectionString;
            }
            else
            {
                throw new Exception("BatchIntervalEngine instantiation error: DbConnectionString is null!");
            }

            if (captureFileName != null)
            {
                this._CaptureFileName = captureFileName;
            }
            else
            {
                throw new Exception("BatchIntervalEngine instantiation error: capture file name is null!");
            }

            if (processedCaptureFilesPath != null)
            {
                this._ProcessedCaptureFilesPath = processedCaptureFilesPath;
            }
            else
            {
                throw new Exception("BatchIntervalEngine instantiation error: processed capture files path is null!");
            }

            if (waitSecondsTimeLimit > 0 && waitSecondsTimeLimit < 20)
            {
                this._WaitSecondsLimit = waitSecondsTimeLimit;
            }
            else
            {
                throw new Exception("BatchIntervalEngine instantiation error: time limit seconds must be greater than zero and less than 20!");
            }

            if (intervalSize > 0 && intervalSize < 1200000)
            {
                this._IntervalSize = intervalSize;
            }
            else
            {
                throw new Exception("BatchIntervalEngine instantiation error: interval size (milliseconds) must be greater than zero and less than 120,000 (2 minutes)!");
            }
        }
        #endregion

        #region Public Methods
        public void ProcessNewBatchIntervals()
        {
            // Create new batch intervals from parsed capture data and add new intervals to cumulative intervals

            // Wait for capture file to be parsed by ParseCaptureFilesService service
            int waitSeconds = 0;

            CaptureFileData cfd = new CaptureFileData();
            _CurrentCaptureBatchId = cfd.GetBatchId(_CaptureFileName);

            while (waitSeconds < _WaitSecondsLimit)
            {
                if(File.Exists(_ProcessedCaptureFilesPath + "\\" + _CaptureFileName))
                {
                    //ClientStatusToolStripStatusLabel.Visible = true;
                    //ClientStatusToolStripProgressBar.Visible = true;
                    //ClientStatusToolStripStatusLabel.Text = "Loading capture packets into data store for file [" + file.FileName + "]...";

                    BindingList<PacketInterval> batchIntervals = new BindingList<PacketInterval>();
                    
                    batchIntervals = CreateBatchIntervals(_CaptureFileName);
                    UpdateCumulativeIntervals(batchIntervals);
                    UpdateCaptureBatchParseStatus();
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                    waitSeconds++;
                }
            }

            if(waitSeconds == _WaitSecondsLimit)
            {
                throw new Exception("BatchIntervalEngine error: time expired - cannot find parsed capture file!");
            }
        }

        public void RecalculateBatchIntervals()
        {
            // Recalculate intervals for all existing batch data using a new interval size

            // First delete all existing batch intervals and cumulative intervals
            BatchIntervalData bid = new BatchIntervalData();
            //bid.RemoveAllBatchIntervals();
            bid.TruncateAllIntervalStatisticAndTestTables();

            // Get a list of all capture files
            CaptureFileData cfd = new CaptureFileData();

            BindingList<CurrentCaptureFile> captureFiles = new BindingList<CurrentCaptureFile>();
            captureFiles = cfd.GetBatchIds();

            // Create new intervals for each capture file and update the cumulative intervals
            foreach (CurrentCaptureFile file in captureFiles)
            {
                BindingList<PacketInterval> intervals = new BindingList<PacketInterval>();
                intervals = CreateBatchIntervals(file.FileName);
                UpdateCumulativeIntervals(intervals);
                UpdateCaptureBatchParseStatusForRecalculation(file.CaptureBatchId);
            }
        }
        #endregion

        #region Private  Methods
        private BindingList<PacketInterval> CreateBatchIntervals(string fileName)
        {
            bool success = false;

            BindingList<RawPacket> rawPackets = new BindingList<RawPacket>();
            BindingList<PacketInterval> intervalCounts = new BindingList<PacketInterval>();
            ProcessCapturePackets pcp = new ProcessCapturePackets();

            try
            {
                rawPackets = pcp.LoadPackets(fileName);
                if (rawPackets.Count > 0) { success = true; }
            }
            catch (Exception ex)
            {
                success = false;
                throw new Exception("BatchIntervalEngine: Error loading raw packet data for file [" + fileName + "]: " + ex.Message);
            }
            try
            {
                if (success)
                {
                    intervalCounts = pcp.CalculateIntervalCounts(rawPackets, _IntervalSize);
                }
            }
            catch (Exception ex)
            {
                success = false;
                throw new Exception("BatchIntervalEngine: Error calculating interval counts for file [" + fileName + "]: " + ex.Message);
            }

            // Load the batch intervals into the database
            if (success)
            {
                try
                {
                    success = pcp.SaveBatchIntervals(_DbConnectionString, intervalCounts);
                }
                catch (Exception ex)
                {
                    success = false;
                    throw new Exception("BatchIntervalEngine: Error saving batch interval counts: " + ex.Message);
                }
            }
            return intervalCounts;
        }
        private void UpdateCumulativeIntervals(BindingList<PacketInterval> newIntervals)
        {
            ProcessCapturePackets pcp = new ProcessCapturePackets();

            // Add batch to cumulative totals
            if (newIntervals.Count > 0)
            {
                try
                {
                    pcp.UpdateCumulativeIntervals(_DbConnectionString, newIntervals);
                }
                catch (Exception ex)
                {
                    throw new Exception("BatchIntervalEngine: Error updating cumulative intervals for this file  [" + _CaptureFileName + "]: " + ex.Message);
                }
            }
            else
            {
                throw new Exception("BatchIntervalEngine: UpdateCumulativeIntervals - error updating cumulative intervals for this file  [" + _CaptureFileName + "]: new intervals list contains not data!");
            }
        }
        private void UpdateCaptureBatchParseStatus()
        {
            CaptureFileData cfd = new CaptureFileData();
            cfd.UpdateCaptureBatchParseStatus(_CurrentCaptureBatchId);
        }
        private void UpdateCaptureBatchParseStatusForRecalculation(int captureBatchId)
        {
            CaptureFileData cfd = new CaptureFileData();
            cfd.UpdateCaptureBatchParseStatus(captureBatchId);
        }

        private int GetLastCaptureBatchId()
        {
            BindingList<CurrentCaptureFile> currentCaptureFiles = new BindingList<CurrentCaptureFile>();
            CaptureFileData cfd = new CaptureFileData();
            currentCaptureFiles = cfd.GetBatchIds();
            int lastBatchId = 0;
            foreach (var item in currentCaptureFiles)
            {
                if (item.CaptureBatchId > lastBatchId)
                {
                    lastBatchId = item.CaptureBatchId;
                }
            }
            return lastBatchId;
        }
        #endregion
    }
}
