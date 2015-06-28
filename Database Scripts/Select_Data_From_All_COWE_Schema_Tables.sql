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

/*
-- Clean out tables
delete from COWE.BatchInterval;
delete from COWE.CumulativeInterval;
delete from COWE.CaptureBatch;
delete from COWE.CapturePacket;
*/

---- Test CaptureBatchInsert sproc
--declare @CaptureBatchId int
--exec [COWE].[CaptureBatchInsert] @FileName = 'CaptureFile635674934042778582d.pcap', @Marked = 1, @NewCaptureBatchId = @CaptureBatchId output
--select @CaptureBatchId
