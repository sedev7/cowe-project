-- Select_Test_Data_From_All_COWE_Schema_Tables.sql
--
-- J. Savage  01-31-2016
-- COWE Thesis/Research Project (CISE)
--

USE PacketsTest;
GO	

select * from Test.BatchInterval;
select * from Test.CaptureBatch;
select * from Test.CapturePacket;
select * from Test.CumulativeInterval;
select * from Test.DisplayStatistic;
select * from Test.SingleHistogram;
select * from Test.CumulativeHistogram;
select * from Test.CumulativeProbabilityDistribution;
select * from Test.HypothesisTest;