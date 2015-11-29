-- Create_Tables_for_Packets_Database_Script.sql
--
-- J. Savage  06-11-2015
--
-- COWE Thesis/Research Project (CISE)
--
-- Create all tables and database structures for COWE project.
--
-- v6 10-24-2015:
--  - Rename Histogram table to SingleHistogram.
--  - Add CaptureBatchId FK to SingleHistogram table.
--  - Add CumulativeHistogram table.
--  - Remove BatchState column from both histogram tables - no longer needed.
--  - Remove BatchState column from CumulativeProbabilityDistribution table - no longer needed.
--  - Add HypothesisTest table.
--
-- v5 09-07-2015:
--  - Add columns for parse and statistics (single and cumulative) flags to CaptureBatch table.
--  - Add column for MeanOfMeansStandardDeviation in DisplayStatistic table.
--
-- v4 08-27-2015:
--  - Add Histogram table.
--  - Add CumulativeProbabilityDistribution table.
--  - Add identity columns to CumulativeInterval and DisplayStatistic tables (for EntityFramework).
--
-- v3 07-06-2015:
--  - Add "mean" column to CaptureBatch table.
--  - Add DisplayStatistic table.
--
-- v2 06-14-2015:
--  - Remove identity column for CumulativeInterval table (not needed 
--    because we are replacing the data each time an capture file is
--    processed).
--
-- v1 06-11-2015:
--  - Combining all scripts into one.
--

USE [Packets]
GO

PRINT 'Starting Script...';

/************************************************************************/
/*                                                                      */
/*                DROP FOREIGN KEY CONSTRAINTS                          */
/*                                                                      */
/************************************************************************/

PRINT 'Dropping foreign keys:';

PRINT 'Dropping [COWE].[FK_BatchInterval$CaptureBatch.CaptureBatchId]...';
IF OBJECT_ID(N'[COWE].[FK_BatchInterval$CaptureBatch.CaptureBatchId]',N'F') IS NOT NULL
  BEGIN
    ALTER TABLE [COWE].[BatchInterval] DROP CONSTRAINT [FK_BatchInterval$CaptureBatch.CaptureBatchId];
	PRINT '  Constraint successfully dropped'
  END
ELSE
  PRINT '   => Constraint not found!'
GO

PRINT 'Dropping [COWE].[FK_CapturePacket$CaptureBatch.CaptureBatchId]...';
IF OBJECT_ID(N'[COWE].[FK_CapturePacket$CaptureBatch.CaptureBatchId]',N'F') IS NOT NULL
  BEGIN
    ALTER TABLE [COWE].[CapturePacket] DROP CONSTRAINT [FK_CapturePacket$CaptureBatch.CaptureBatchId];
	PRINT '  Constraint successfully dropped'
  END
ELSE
  PRINT '   => Constraint not found!'
GO

PRINT 'Dropping [COWE].[FK_SingleHistogram$CaptureBatch.CaptureBatchId]...';
IF OBJECT_ID(N'[COWE].[FK_SingleHistogram$CaptureBatch.CaptureBatchId]',N'F') IS NOT NULL
  BEGIN
    ALTER TABLE [COWE].[SingleHistogram] DROP CONSTRAINT [FK_SingleHistogram$CaptureBatch.CaptureBatchId];
	PRINT '  Constraint successfully dropped'
  END
ELSE
  PRINT '   => Constraint not found!'
GO

PRINT 'Done dropping foreign keys';

/************************************************************************/
/*                                                                      */
/*               DROP THE TABLES IF THEY EXIST                          */
/*                                                                      */
/************************************************************************/

PRINT 'Dropping tables:';

PRINT '  Dropping CapturePacket table...';
IF  OBJECT_ID(N'[COWE].[CapturePacket]',N'U') IS NOT NULL
  BEGIN
    DROP TABLE [COWE].[CapturePacket]
	PRINT '  Table successfully dropped'
  END
ELSE
  PRINT '   => Table not found!'
GO

PRINT '  Dropping BatchInterval table...';
IF  OBJECT_ID(N'[COWE].[BatchInterval]',N'U') IS NOT NULL
  BEGIN
    DROP TABLE [COWE].[BatchInterval];
	PRINT '  Table successfully dropped'
  END
ELSE
  PRINT '   => Table not found!'
GO

PRINT '  Dropping CaptureBatch table...';
IF  OBJECT_ID(N'[COWE].[CaptureBatch]',N'U') IS NOT NULL
  BEGIN
    DROP TABLE [COWE].[CaptureBatch];
	PRINT '  Table successfully dropped'
  END
ELSE
  PRINT '   => Table not found!'
GO

PRINT '  Dropping CumulativeIntervals table...';
IF  OBJECT_ID(N'[COWE].[CumulativeInterval]',N'U') IS NOT NULL
  BEGIN
    DROP TABLE [COWE].[CumulativeInterval];
	PRINT '  Table successfully dropped'
  END
ELSE
  PRINT '   => Table not found!'
GO

PRINT '  Dropping DisplayStatistic table...';
IF  OBJECT_ID(N'[COWE].[DisplayStatistic]',N'U') IS NOT NULL
  BEGIN
    DROP TABLE [COWE].[DisplayStatistic]
	PRINT '  Table successfully dropped'
  END
ELSE
  PRINT '   => Table not found!'
GO

PRINT '  Dropping SingleHistogram table...';
IF  OBJECT_ID(N'[COWE].[SingleHistogram]',N'U') IS NOT NULL
  BEGIN
    DROP TABLE [COWE].[SingleHistogram]
	PRINT '  Table successfully dropped'
  END
ELSE
  PRINT '   => Table not found!'
GO

PRINT '  Dropping CumulativeHistogram table...';
IF  OBJECT_ID(N'[COWE].[CumulativeHistogram]',N'U') IS NOT NULL
  BEGIN
    DROP TABLE [COWE].[CumulativeHistogram]
	PRINT '  Table successfully dropped'
  END
ELSE
  PRINT '   => Table not found!'
GO

PRINT '  Dropping CumulativeProbabilityDistribution table...';
IF  OBJECT_ID(N'[COWE].[CumulativeProbabilityDistribution]',N'U') IS NOT NULL
  BEGIN
    DROP TABLE [COWE].[CumulativeProbabilityDistribution]
	PRINT '  Table successfully dropped'
  END
ELSE
  PRINT '   => Table not found!'
GO

PRINT '  Dropping HypothesisTest table...';
IF  OBJECT_ID(N'[COWE].[HypothesisTest]',N'U') IS NOT NULL
  BEGIN
    DROP TABLE [COWE].[HypothesisTest]
	PRINT '  Table successfully dropped'
  END
ELSE
  PRINT '   => Table not found!'
GO


PRINT 'Done dropping tables';

/************************************************************************/
/*                                                                      */
/*                     CREATE TABLES SECTION                            */
/*                                                                      */
/************************************************************************/

PRINT 'Creating tables:';

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO


/************************************************************************/
/*                                                                      */
/*                    CREATE CaptureBatch TABLE                         */
/*                                                                      */
/************************************************************************/

PRINT '  Creating CaptureBatch table...';
CREATE TABLE [COWE].[CaptureBatch](
	[CaptureBatchId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [varchar](200) NOT NULL,
	[Marked] [bit] NOT NULL,
	[Mean] [decimal](28,10) NOT NULL,
	[TrimmedMean] [decimal](28,10) NOT NULL DEFAULT 0,
	[Parsed] [bit] NOT NULL DEFAULT 0,
	[SingleStatistics] [bit] NOT NULL DEFAULT 0,
	[CumulativeStatistics] [bit] NOT NULL DEFAULT 0,
 CONSTRAINT [AK_CaptureBatch_FileName] UNIQUE ([FileName]),
 CONSTRAINT [PK_CaptureBatch_CaptureBatchId] PRIMARY KEY CLUSTERED
 (
	[CaptureBatchId] ASC
 ) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
GO


/************************************************************************/
/*                                                                      */
/*                   CREATE BatchIntervals TABLE                         */
/*                                                                      */
/************************************************************************/

PRINT '  Creating BatchIntervals table...';
CREATE TABLE [COWE].[BatchInterval](
	[BatchIntervalId] [int] IDENTITY(1,1) NOT NULL,
	[CaptureBatchId] [int] NOT NULL,
	[IntervalNumber] [int] NOT NULL,
	[PacketCount] [int] NOT NULL,
 CONSTRAINT [PK_BatchInterval_PacketId] PRIMARY KEY CLUSTERED 
 (
	[BatchIntervalId] ASC
 )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
 ) ON [PRIMARY]
;
GO

ALTER TABLE [COWE].[BatchInterval] ADD CONSTRAINT [FK_BatchInterval$CaptureBatch.CaptureBatchId] FOREIGN KEY (CaptureBatchId) REFERENCES [COWE].[CaptureBatch] (CaptureBatchId);
GO

/************************************************************************/
/*                                                                      */
/*                CREATE CumulativeIntervals TABLE                      */
/*                                                                      */
/************************************************************************/

PRINT '  Creating CumulativeInterval table...';
CREATE TABLE [COWE].[CumulativeInterval](
	[CumulativeIntervalId] [int] IDENTITY(1,1) NOT NULL,
	[CumulativeIntervalNumber] [int] NOT NULL,
	[PacketCount] [int] NOT NULL,
	[Marked] [bit] NOT NULL,
 CONSTRAINT [PK_CumulativeInterval_CumulativeIntervalId] PRIMARY KEY CLUSTERED 
(
	[CumulativeIntervalId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
GO

/************************************************************************/
/*                                                                      */
/*                   CREATE CapturePacket TABLE                         */
/*                                                                      */
/************************************************************************/

PRINT '  Creating CaptureBatch table...';
CREATE TABLE [COWE].[CapturePacket](
	[CapturePacketId] [int] IDENTITY(1,1) NOT NULL,
	[CaptureBatchId] [int] NOT NULL,
	[PacketNumber] [int] NOT NULL,
	[TimeStamp] [DateTime] NOT NULL,
	[Marked] [bit] NOT NULL,
 CONSTRAINT [PK_CapturePacket_CapturePacketId] PRIMARY KEY CLUSTERED 
(
	[CapturePacketId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [COWE].[CapturePacket] ADD CONSTRAINT [FK_CapturePacket$CaptureBatch.CaptureBatchId] FOREIGN KEY (CaptureBatchId) REFERENCES [COWE].[CaptureBatch] (CaptureBatchId);
GO

/************************************************************************/
/*                                                                      */
/*                  CREATE DisplayStatistic TABLE                       */
/*                                                                      */
/************************************************************************/

PRINT '  Creating DisplayStatistic table...';
CREATE TABLE [COWE].[DisplayStatistic](
	[DisplayStatisticId] [int] IDENTITY(1,1) NOT NULL,
	[IntervalCount] [int] NOT NULL,
	[TrimmedIntervalCount] [int] NOT NULL,
	[MeanPacketsPerInterval] [decimal](28,10) NOT NULL,
	[StandardDeviation] [decimal](28,10) NOT NULL,
	[MinPacketsPerInterval] [int] NOT NULL,
	[MaxPacketsPerInterval] [int] NOT NULL,
	[MeanOfMeans] [decimal](28,10) NOT NULL,
	[MeanOfMeansStandardDeviation] [decimal](28,10) NOT NULL,
	[Marked] [bit] NOT NULL,
	[BatchType] [int] NOT NULL		-- Single, Cumulative
 CONSTRAINT [PK_DisplayStatistic_DisplayStatisticId] PRIMARY KEY CLUSTERED 
(
	[DisplayStatisticId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
GO

/************************************************************************/
/*                                                                      */
/*                    CREATE SingleHistogram TABLE                      */
/*                                                                      */
/************************************************************************/

PRINT '  Creating SingleHistogram table...';
CREATE TABLE [COWE].[SingleHistogram](
	[SingleHistogramId] [int] IDENTITY(1,1) NOT NULL,
	[CaptureBatchId] [int] NOT NULL,
	[Interval] [int] NOT NULL,
	[Probability] [decimal](28,10) NOT NULL,
	[CaptureState] [int] NOT NULL,		-- Marked, Unmarked
 CONSTRAINT [PK_SingleHistogram_HistogramId] PRIMARY KEY CLUSTERED 
(
	[SingleHistogramId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];
GO
ALTER TABLE [COWE].[SingleHistogram] ADD CONSTRAINT [FK_SingleHistogram$CaptureBatch.CaptureBatchId] FOREIGN KEY (CaptureBatchId) REFERENCES [COWE].[CaptureBatch] (CaptureBatchId);
GO

/************************************************************************/
/*                                                                      */
/*                 CREATE CumulativeHistogram TABLE                     */
/*                                                                      */
/************************************************************************/

PRINT '  Creating CumulativeHistogram table...';
CREATE TABLE [COWE].[CumulativeHistogram](
	[CumulativeHistogramId] [int] IDENTITY(1,1) NOT NULL,
	[Interval] [int] NOT NULL,
	[Probability] [decimal](28,10) NOT NULL,
	[CaptureState] [int] NOT NULL,		-- Marked, Unmarked
 CONSTRAINT [PK_CumulativeHistogram_HistogramId] PRIMARY KEY CLUSTERED 
(
	[CumulativeHistogramId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

/************************************************************************/
/*                                                                      */
/*           CREATE CumulativeProbabilityDistribution TABLE             */
/*                                                                      */
/************************************************************************/

PRINT '  Creating CumulativeProbabilityDistribution table...';
CREATE TABLE [COWE].[CumulativeProbabilityDistribution](
	[CumulativeProbabilityDistributionId] [int] IDENTITY(1,1) NOT NULL,
	[Interval] [int] NOT NULL,
	[Probability] [decimal](28,10) NOT NULL,
	[CaptureState] [int] NOT NULL,		-- Marked, Unmarked
 CONSTRAINT [PK_CumulativeProbabilityDistribution_CumulativeProbabilityDistributionId] PRIMARY KEY CLUSTERED 
(
	[CumulativeProbabilityDistributionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
GO

/************************************************************************/
/*                                                                      */
/*                 CREATE HypothesisTest TABLE                          */
/*                                                                      */
/************************************************************************/

PRINT '  Creating HypothesisTest table...';
CREATE TABLE [COWE].[HypothesisTest](
	[HypothesisTestId] [int] IDENTITY(1,1) NOT NULL,
	[MeanOfMeansVariance] [decimal](28,10) NOT NULL,
	[MeansVarianceStandardDeviation] [decimal](28,10) NOT NULL,
	[KsStatistic] [decimal](28,10) NOT NULL,
	[MaxCpdVariance] [decimal](28,10) NOT NULL,
	[MeansTestResult] [bit] NOT NULL,
	[KsTestResult] [bit] NOT NULL,
	[HasValues] [bit] NOT NULL
 CONSTRAINT [PK_Hypothesis_HypothesisTestId] PRIMARY KEY CLUSTERED 
(
	[HypothesisTestId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];
GO


PRINT 'Done creating tables';

SET ANSI_PADDING OFF
GO

PRINT 'Script completed';

