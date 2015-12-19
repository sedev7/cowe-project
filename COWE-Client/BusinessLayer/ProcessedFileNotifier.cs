using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using COWE.DataLayer;
using COWE.DomainClasses;

namespace COWE.BusinessLayer
{
    public class ProcessedFileNotifier
    {
        bool IsRunning = false;
        string _processedFilesPath = string.Empty;

        Dictionary<string, int> updatedFiles = new Dictionary<string, int>();
        public ProcessedFileNotifier(string processedFilesPath)
        {
            this._processedFilesPath = processedFilesPath;
        }

        public void Start()
        {
            IsRunning = true;

            FileInfo[] fi = null;

            while (IsRunning)
            {
                try
                {
                    if (_processedFilesPath != "")
                    {
                        CaptureFileData cfd = new CaptureFileData();

                        DirectoryInfo di = new DirectoryInfo(_processedFilesPath);
                        fi = di.GetFiles("*.pcap");
                        if (fi.Length > 0)
                        {
                            foreach (var file in fi)
                            {
                                if (!updatedFiles.ContainsKey(file.Name))
                                {
                                    int batchId = cfd.GetBatchId(file.Name);
                                    if (batchId > 0)
                                    {
                                        cfd.UpdateCaptureBatchParseStatus(batchId);
                                        updatedFiles.Add(file.Name, batchId);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Thread.Sleep(3000);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("ParsedFileNotifier unable to find parsed pcap files: " + ex.Message);
                }
            }
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}
