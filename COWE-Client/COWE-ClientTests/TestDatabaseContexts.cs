using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using COWE.DataLayer;
//using COWE.DomainClasses;
using COWE.Enumerations;
using COWE.PacketsTestDatabaseInitialization;

namespace COWE_ClientTests
{
    [TestClass]
    public class TestDatabaseContexts
    {
        public TestDatabaseContexts()
        {
            // Truncate all tables and reseed the database before running any tests
            TruncateAllTablesPacketsTestDatabaseForTesting();
            ReSeedDataPacketsTestDatabaseForTesting();
        }

        public void TruncateAllTablesPacketsTestDatabaseForTesting()
        {
            using (var context = new PacketsTestEntities())
            {
                try
                {
                    context.TruncateAllTables();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void ReSeedDataPacketsTestDatabaseForTesting()
        {
            DatabaseSeedingInitializer dsi = new DatabaseSeedingInitializer();
            using (var context = new PacketsTestEntities())
            {
                dsi.SeedTestDatabase(context);
            }
        }

        [TestMethod]
        public void DatabaseTestCanInsertCaptureBatchDataAndRetrieve()
        {
            // Arrange
            string expectedFileName = "CaptureFileTest2u.pcap";

            // Act
            using (var context = new PacketsTestEntities())
            {
                var captureBatch = new CaptureBatch()
                {
                    FileName = expectedFileName,
                    Marked = false,
                    Mean = 0M,
                    TrimmedMean = 0M,
                    Parsed = false,
                    SingleStatistics = false,
                    CumulativeStatistics = false
                };
                context.CaptureBatches.Add(captureBatch);
                context.SaveChanges();
            }

            // Assert
            using (var context = new PacketsTestEntities())
            {
                var actualFileName =  (from c in context.CaptureBatches
                                       where c.CaptureBatchId == 2 
                                       select c.FileName).FirstOrDefault();

                Assert.AreEqual(expectedFileName, actualFileName);
            }
        }

        [TestMethod]
        public void DatabaseTestCanRetrieveCapturePacketData()
        {
            // Arrange
            int expectedRecordCount = 10;
            int actualRecordCount = 0;

            // Act
            using (var context = new PacketsTestEntities())
            {
                var count = context.CapturePackets.Count();
                actualRecordCount = Convert.ToInt32(count);
            }

            // Assert
            Assert.AreEqual(expectedRecordCount, actualRecordCount);
        }

        [TestMethod]
        public void DatabaseTestCanDeleteCapturePacketData()
        {
            // Arrange
            int expectedRecordCount = 9;
            int actualRecordCount = 0;

            // Act
            using (var context = new PacketsTestEntities())
            {
                var record = context.CapturePackets.Where(c => c.CapturePacketId == 9).FirstOrDefault();
                context.CapturePackets.Remove(record);
                context.SaveChanges();

                var count = context.CapturePackets.Count();
                actualRecordCount = Convert.ToInt32(count);
            }

            // Assert
            Assert.AreEqual(expectedRecordCount, actualRecordCount);
        }

        [TestMethod]
        public void DatabaseTestCanUpdateCaptureBatchData()
        {
            // Arrange
            bool expectedParsedValue = true;
            bool actualParsedValue = false;

            // Act
            using (var context = new PacketsTestEntities())
            {
                var record0 = context.CaptureBatches.Where(c => c.CaptureBatchId == 1).FirstOrDefault();
                record0.Parsed = true;
                context.SaveChanges();

                var record1 = context.CaptureBatches.Where(c => c.CaptureBatchId == 1).FirstOrDefault();
                actualParsedValue = record1.Parsed;
            }

            // Assert
            Assert.AreEqual(expectedParsedValue, actualParsedValue);
        }
    }
}
