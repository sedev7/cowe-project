-- Select_Data_From_All_COWE_Schema_Tables.sql
--
-- J. Savage  05-31-2015
-- COWE Thesis/Research Project (CISE)
--

USE Packets;
GO	

select * from COWE.BatchInterval;
select * from COWE.CaptureBatch;
select * from COWE.CapturePacket;
select * from COWE.CumulativeInterval;
select * from COWE.DisplayStatistic;
select * from COWE.Histogram;
select * from COWE.CumulativeProbabilityDistribution;

/*
-- Clean out tables
delete from COWE.BatchInterval;
delete from COWE.CumulativeInterval;
delete from COWE.CaptureBatch;
delete from COWE.CapturePacket;
*/

/*
-- Clean out all tables except COWE.CaptureBatch
TRUNCATE TABLE COWE.BatchInterval;
TRUNCATE TABLE COWE.CumulativeInterval;
TRUNCATE TABLE COWE.DisplayStatistic;
TRUNCATE TABLE COWE.Histogram;
TRUNCATE TABLE COWE.CumulativeProbabilityDistribution;
UPDATE COWE.CaptureBatch SET Mean = 0, TrimmedMean = 0, SingleStatistics = 0, CumulativeStatistics = 0 WHERE CaptureBatchId in (6,7,8,9);
--TRUNCATE TABLE COWE.CapturePacket;
*/


---- Test CaptureBatchInsert sproc
--declare @CaptureBatchId int
--exec [COWE].[CaptureBatchInsert] @FileName = 'CaptureFile635674934042778582d.pcap', @Marked = 1, @NewCaptureBatchId = @CaptureBatchId output
--select @CaptureBatchId

