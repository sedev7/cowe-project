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
select * from COWE.SingleHistogram;
select * from COWE.CumulativeHistogram;
select * from COWE.CumulativeProbabilityDistribution;
select * from COWE.HypothesisTest;

--select * from cowe.SingleHistogram order by BatchType;

/********************************************************************************/
/*                                                                              */
/* For testing: do not delete data from CaptureBatch and CapturePacket tables   */
/*                                                                              */
/********************************************************************************/

/*
-- Clean out tables
delete from COWE.BatchInterval;
delete from COWE.CumulativeInterval;
delete from COWE.DisplayStatistic;
delete from COWE.CaptureBatch;
delete from COWE.CapturePacket;
delete from COWE.SingleHistogram;
delete from COWE.CumulativeHistogram;
delete from COWE.CumulativeProbabilityDistribution;
delete from COWE.HypothesisTest;
*/

/*
-- Clean out all tables except COWE.CaptureBatch and COWE.CapturePacket
TRUNCATE TABLE COWE.BatchInterval;
TRUNCATE TABLE COWE.CumulativeInterval;
TRUNCATE TABLE COWE.DisplayStatistic;
TRUNCATE TABLE COWE.SingleHistogram;
TRUNCATE TABLE COWE.CumulativeHistogram;
TRUNCATE TABLE COWE.CumulativeProbabilityDistribution;
TRUNCATE TABLE COWE.HypothesisTest;
UPDATE COWE.CaptureBatch SET Mean = 0, TrimmedMean = 0, SingleStatistics = 0, CumulativeStatistics = 0 WHERE CaptureBatchId in (5,6,7,8);
--TRUNCATE TABLE COWE.CapturePacket;
--TRUNCATE TABLE COWE.CaptureBatch;
*/

--exec COWE.TruncateAllTables

---- Test CaptureBatchInsert sproc
--declare @CaptureBatchId int
--exec [COWE].[CaptureBatchInsert] @FileName = 'CaptureFile635674934042778582d.pcap', @Marked = 1, @NewCaptureBatchId = @CaptureBatchId output
--select @CaptureBatchId
--delete from COWE.SingleHistogram where CaptureBatchId = 7 and SingleHistogramId >= 72
--delete from COWE.SingleHistogram where CaptureBatchId = 8 and SingleHistogramId >= 59
