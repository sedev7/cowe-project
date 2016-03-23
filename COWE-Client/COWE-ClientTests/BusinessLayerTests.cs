using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
//using System.Data.EntityModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using COWE.BusinessLayer;
using COWE.DomainClasses;
using COWE.Enumerations;

namespace COWE_ClientTests
{
    [TestClass]
    public class BusinessLayerTests
    {
        [TestMethod]
        public void NewFileNotifierReturnsNewFile()
        {
            // Arrange
            string inputFolderPath = @"C:\Temp\InputFileTest";
            string outputFolderPath = @"C:\Temp\OutputFileTest";
            string inputFileName = "TestFile.txt";

            if (!Directory.Exists(inputFolderPath))
            {
                Directory.CreateDirectory(inputFolderPath);
            }

            NewFileNotifier nfn = new NewFileNotifier(inputFolderPath);
            if (File.Exists(inputFolderPath + '\\' + inputFileName))
            {
                File.Delete(inputFolderPath + '\\' + inputFileName);
            }

            StreamWriter sw = File.CreateText(inputFolderPath + '\\' + inputFileName);
            sw.Close();

            if(!Directory.Exists(outputFolderPath))
            {
                Directory.CreateDirectory(outputFolderPath);
            }

            // Act
            BindingList<CurrentCaptureFile> actual = new BindingList<CurrentCaptureFile>();
            actual = nfn.CheckForNewFiles(inputFolderPath, outputFolderPath);

            bool fileIsPresent = File.Exists(outputFolderPath + '\\' + inputFileName);
            Assert.IsTrue(fileIsPresent);
            // Assert
            //Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void CalculateHistogramValuesBatchIntervalsTest()
        {
            // Arrange
            CalculateHistogram ch = new CalculateHistogram();
            BindingList<BatchIntervalMarked> batchIntervals = new BindingList<BatchIntervalMarked>();

            // We will create 10 bins with values from 1 to 10
            for (int i = 0; i < 100; i++)
            {
                BatchIntervalMarked bim = new BatchIntervalMarked();
                bim.BatchIntervalId = i + 1;
                bim.CaptureBatchId = 9999;
                bim.IntervalNumber = i;
                bim.Marked = CaptureState.Marked;
                bim.PacketCount = i * 10;
                batchIntervals.Add(bim);
            }

            // Act
            Dictionary<int, int> histogram = ch.CalculateHistogramValues(batchIntervals);

            bool result = false;
            int j = 0;

            // We are counting the number of packet counts per interval so we should get ten bins each with incrementing values 
            // of 450, each bin count adding 1000 (e.g., (bin,count) = (0,450),(1,1450),(2,2450),...,(9,9450):
            // bin 0 with packet counts 0,10,20,30,...90 sum to 450; bin 1 with packet counts 100,110,120,130,... 190 sum
            // to 1450, and so on)
            foreach (KeyValuePair<int, int> pair in histogram)
            {
                if (pair.Key == j && pair.Value == j * 1000 + 450)
                {
                    result = true;
                }
            }

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetProbabilityValuesForHistogramTest()
        {
            // We will pass in a sorted sequence of 10 pairs of values, each pair has an iterator 
            // numbered 0-9, and a corresponding value between 0-4, which repeats after each increment 
            // of five by the iterator, so each pair has one duplicate value in the sequence
            // (e.g., (0,0),(1,1),(2,2),...(5,0),(6,1),(7,2),...(9,4) ).
            // The GetProbabilityValues method calculates the number of packet counts (the pair values),
            // then calculates the probability for each pair value, and returns a sorted sequence of
            // pairs with the probability for each pair value.
            // So we should get back a sequence of five pairs with the probabilities for the original
            // pair values.  For example, since the pair values total 20, the probability for a value 
            // of 2 is 2/20 = 0.1.  So the returned sequence should be (0,0),(1,0.1),(2,0.2),(3,0.3),(4,0.4).

            // Arrange
            Dictionary<int, int> histogram = new Dictionary<int, int>();
            for (int i = 0; i < 10; i++)
            {
                histogram.Add(i,i % 5);
            }

            CalculateProbability cp = new CalculateProbability(histogram);

            // Act
            Dictionary<int, decimal> sortedProbabilities = new Dictionary<int, decimal>();
            sortedProbabilities = cp.GetProbabilityValues();

            bool results = true;
            if(sortedProbabilities.Count != 5)
            {
                results = false;
            }

            decimal expectedProbability = 0M;

            foreach (var pair in sortedProbabilities)
            {
                if(pair.Value != expectedProbability)
                {
                    results = false;
                }
                expectedProbability = expectedProbability + 0.1M;
            }

            // Assert
            Assert.IsTrue(results);
        }
    }
}
