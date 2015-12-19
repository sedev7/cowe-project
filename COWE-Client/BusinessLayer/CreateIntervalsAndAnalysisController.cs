﻿using System;
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
        //public CreateIntervalsAndAnalysisController() { }

        public void ProcessFiles()
        {
            IsRunning = true;

            while (IsRunning)
            {
                if (FileQueue.Count > 0)
                {
                    CurrentCaptureFile file = FileQueue.Dequeue();

                    CaptureFileData cfd = new CaptureFileData();
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
                else
                {
                    Thread.Sleep(3000);
                }
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
