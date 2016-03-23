using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.DomainClasses;
using COWE.DataLayer;
using COWE.Enumerations;

namespace COWE.PacketsTestDatabaseInitialization
{
    public class DatabaseSeedingInitializer
    {
        public void SeedTestDatabase(PacketsTestEntities context)
        {
            var captureBatch = new CaptureBatch
            {
                FileName = "CaptureFileTest1d.pcap",
                Marked = true,
                Mean = 0M,
                TrimmedMean = 0M,
                Parsed = false,
                SingleStatistics = false,
                CumulativeStatistics = false
            };
            context.CaptureBatches.Add(captureBatch);

            int msec = 067;

            for (int i = 0; i < 10; i++)
            {
                var capturePacket = new CapturePacket
                {
                    PacketNumber = i,
                    CaptureBatchId = 1,
                    TimeStamp = Convert.ToDateTime(string.Format("2016-01-31 12:13:58." + msec++)),
                    Marked = true
                };
                context.CapturePackets.Add(capturePacket);
            }

            context.SaveChanges();

            /* Test Data
            CapturePacketId	CaptureBatchId	PacketNumber	TimeStamp	Marked
            1	1	0	2016-01-31 12:13:58.067	1
            2	1	1	2016-01-31 12:13:58.067	1
            3	1	2	2016-01-31 12:13:58.067	1
            4	1	3	2016-01-31 12:13:58.070	1
            5	1	4	2016-01-31 12:13:58.070	1
            6	1	5	2016-01-31 12:13:58.070	1
            7	1	6	2016-01-31 12:13:58.073	1
            8	1	7	2016-01-31 12:13:58.073	1



            CaptureBatchId	FileName	        Marked	Mean	        TrimmedMean	Parsed	SingleStatistics	CumulativeStatistics
            1	CaptureFile635898392258793796d.pcap	1	105.5416666667	115.0454545455	1	1	1
            2	CaptureFile635898392480034634u.pcap	0	9.8230616302	125.6944444444	1	1	1
            3	CaptureFile635898392680134473d.pcap	1	7.7898989899	132.3846153846	1	1	1
            4	CaptureFile635898392880258123u.pcap	0	9.6271844660	106.0930232558	1	1	1
            5	CaptureFile635898393080301245d.pcap	1	9.7991967871	110.2682926829	1	1	1
            6	CaptureFile635898393280407635u.pcap	0	9.5570866142	120.8648648649	1	1	1
            7	CaptureFile635898393480605337d.pcap	1	9.1496062992	109.3076923077	1	1	1
            8	CaptureFile635898393680732945u.pcap	0	10.3278008299	131.9714285714	1	1	1
            */

        }
    }
}
