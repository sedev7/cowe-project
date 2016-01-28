using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using COWE.BusinessLayer;
using COWE.DomainClasses;
using COWE.Enumerations;

namespace COWE_ClientTests
{
    [TestClass]
    public class AnalysisEngineTests
    {
        [TestMethod]
        public void CalculateBaseStatisticsTest()
        {
            // Arrange

            // Create a sequence of 10 intervals with packet counts 100, 200, 300, ..., 1000
            BindingList<BatchIntervalMarked> batchIntervals = new BindingList<BatchIntervalMarked>();
            for (int i = 0; i < 10; i++)
            {
                BatchIntervalMarked bim = new BatchIntervalMarked();
                bim.BatchIntervalId = i;
                bim.CaptureBatchId = 0;
                bim.IntervalNumber = i + 1;
                bim.PacketCount = (i + 1) * 100;
                bim.Marked = CaptureState.Unknown;
                batchIntervals.Add(bim);
            }

            int expectedCount = 10;
            int expectedMean = 550;
            int expectedMin = 100;
            int expectedMax = 1000;
            decimal expectedVar = 91666.667M;
            decimal expectedStdDev = 302.765M;

            // Act
            BaseStatistics bs = new BaseStatistics(batchIntervals);

            // Assert
            Assert.AreEqual(bs.Count, expectedCount);
            Assert.AreEqual(Convert.ToDecimal(bs.Mean), expectedMean);
            Assert.AreEqual(bs.Minimum, expectedMin);
            Assert.AreEqual(bs.Maximum, expectedMax);
            Assert.AreEqual(Convert.ToDecimal(Math.Round(bs.Variance,3)), expectedVar);
            Assert.AreEqual(Convert.ToDecimal(Math.Round(bs.StdDev,3)), expectedStdDev);
        }
    }
}
