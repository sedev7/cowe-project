using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using COWE.DataLayer;
using COWE.DomainClasses;

namespace COWE.BusinessLayer
{
    public class CreateIntervalsAndAnalysisController
    {
        bool IsRunning = false;
        public CreateIntervalsAndAnalysisController() { }

        public void ProcessFiles()
        {
            IsRunning = true;

            while (IsRunning)
            {
                List<CurrentCaptureFile> files = new List<CurrentCaptureFile>();

                while (FileQueue.Count > 0)
                {
                    CurrentCaptureFile file = FileQueue.Dequeue();
                    files.Add(file);
                }

                CaptureFileData cfd = new CaptureFileData();

                foreach(CurrentCaptureFile file in files)
                {
                    file.CaptureBatchId = cfd.GetBatchId(file.FileName);
                    if (cfd.GetParsedFileStatus(file.CaptureBatchId))
                    {
                        BatchIntervalEngine intervalEngine = new BatchIntervalEngine(DatabaseConnections.SqlConnection, AnalysisConfiguration.ProcessedCaptureFilesPath, file.FileName, 5, AnalysisConfiguration.IntervalSize);
                        intervalEngine.ProcessNewBatchIntervals();
                        CreateAnalysisData(file);
                        intervalEngine = null;
                    }
                    else
                    {
                        FileQueue.Enqueue(file);
                    }
                }

                if(files.Count > 0)
                {
                    files.Clear();
                }
                Thread.Sleep(3000);
            }
        }

        private void CreateAnalysisData(CurrentCaptureFile file)
        {
            AnalysisEngine analysisEngine = new AnalysisEngine(AnalysisConfiguration.TrimSmallPackets, AnalysisConfiguration.HistogramBinSize, AnalysisConfiguration.HypothesisTestType, file);
            analysisEngine.CalculateSingleBatchStatistics();
            analysisEngine.CalculateCumulativeBatchStatistics();
            analysisEngine.CalculateSingleHistogramData();
            analysisEngine.CalculateCumulativeHistogramData();
            analysisEngine.CalculateCumulativeProbabilityDistribution(file.CaptureState);
            analysisEngine.CalculateHypothesisTestResults();
            analysisEngine = null;
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}
