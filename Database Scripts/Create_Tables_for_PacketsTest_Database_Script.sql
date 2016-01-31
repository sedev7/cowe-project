-- Create_Tables_for_PacketsTest_Database_Script.sql
--
-- J. Savage  01-28-2016
--
-- Test Thesis/Research Project (CISE)
--
-- Create all tables and database structures for Test project.
--
-- v1 01-28-2016:
--  - Adapted from Create_Tables_for_Packets_Database_Script.sql.
--

USE [PacketsTest]
GO

PRINT 'Starting Script...';

/************************************************************************/
/*                                                                      */
/*                DROP FOREIGN KEY CONSTRAINTS                          */
/*                                                                      */
/************************************************************************/

PRINT 'Dropping foreign keys:';

PRINT 'Dropping [Test].[FK_BatchInterval$CaptureBatch.CaptureBatchId]...';
IF OBJECT_ID(N'[Test].[FK_BatchInterval$CaptureBatch.CaptureBatchId]',N'F') IS NOT NULL
  BEGIN
    ALTER TABLE [Test].[BatchInterval] DROP CONSTRAINT [FK_BatchInterval$CaptureBatch.CaptureBatchId];
	PRINT '  Constraint successfully dropped'
  END
ELSE
  PRINT '   => Constraint not found!'
GO

PRINT 'Dropping [Test].[FK_CapturePacket$CaptureBatch.CaptureBatchId]...';
IF OBJECT_ID(N'[Test].[FK_CapturePacket$CaptureBatch.CaptureBatchId]',N'F') IS NOT NULL
  BEGIN
    ALTER TABLE [Test].[CapturePacket] DROP CONSTRAINT [FK_CapturePacket$CaptureBatch.CaptureBatchId];
	PRINT '  Constraint successfully dropped'
  END
ELSE
  PRINT '   => Constraint not found!'
GO

PRINT 'Dropping [Test].[FK_SingleHistogram$CaptureBatch.CaptureBatchId]...';
IF OBJECT_ID(N'[Test].[FK_SingleHistogram$CaptureBatch.CaptureBatchId]',N'F') IS NOT NULL
  BEGIN
    ALTER TABLE [Test].[SingleHistogram] DROP CONSTRAINT [FK_SingleHistogram$CaptureBatch.CaptureBatchId];
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
IF  OBJECT_ID(N'[Test].[CapturePacket]',N'U') IS NOT NULL
  BEGIN
    DROP TABLE [Test].[CapturePacket]
	PRINT '  Table successfully dropped'
  END
ELSE
  PRINT '   => Table not found!'
GO

PRINT '  Dropping BatchInterval table...';
IF  OBJECT_ID(N'[Test].[BatchInterval]',N'U') IS NOT NULL
  BEGIN
    DROP TABLE [Test].[BatchInterval];
	PRINT '  Table successfully dropped'
  END
ELSE
  PRINT '   => Table not found!'
GO

PRINT '  Dropping CaptureBatch table...';
IF  OBJECT_ID(N'[Test].[CaptureBatch]',N'U') IS NOT NULL
  BEGIN
    DROP TABLE [Test].[CaptureBatch];
	PRINT '  Table successfully dropped'
  END
ELSE
  PRINT '   => Table not found!'
GO

PRINT '  Dropping CumulativeIntervals table...';
IF  OBJECT_ID(N'[Test].[CumulativeInterval]',N'U') IS NOT NULL
  BEGIN
    DROP TABLE [Test].[CumulativeInterval];
	PRINT '  Table successfully dropped'
  END
ELSE
  PRINT '   => Table not found!'
GO

PRINT '  Dropping DisplayStatistic table...';
IF  OBJECT_ID(N'[Test].[DisplayStatistic]',N'U') IS NOT NULL
  BEGIN
    DROP TABLE [Test].[DisplayStatistic]
	PRINT '  Table successfully dropped'
  END
ELSE
  PRINT '   => Table not found!'
GO

PRINT '  Dropping SingleHistogram table...';
IF  OBJECT_ID(N'[Test].[SingleHistogram]',N'U') IS NOT NULL
  BEGIN
    DROP TABLE [Test].[SingleHistogram]
	PRINT '  Table successfully dropped'
  END
ELSE
  PRINT '   => Table not found!'
GO

PRINT '  Dropping CumulativeHistogram table...';
IF  OBJECT_ID(N'[Test].[CumulativeHistogram]',N'U') IS NOT NULL
  BEGIN
    DROP TABLE [Test].[CumulativeHistogram]
	PRINT '  Table successfully dropped'
  END
ELSE
  PRINT '   => Table not found!'
GO

PRINT '  Dropping CumulativeProbabilityDistribution table...';
IF  OBJECT_ID(N'[Test].[CumulativeProbabilityDistribution]',N'U') IS NOT NULL
  BEGIN
    DROP TABLE [Test].[CumulativeProbabilityDistribution]
	PRINT '  Table successfully dropped'
  END
ELSE
  PRINT '   => Table not found!'
GO

PRINT '  Dropping HypothesisTest table...';
IF  OBJECT_ID(N'[Test].[HypothesisTest]',N'U') IS NOT NULL
  BEGIN
    DROP TABLE [Test].[HypothesisTest]
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
CREATE TABLE [Test].[CaptureBatch](
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
CREATE TABLE [Test].[BatchInterval](
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

ALTER TABLE [Test].[BatchInterval] ADD CONSTRAINT [FK_BatchInterval$CaptureBatch.CaptureBatchId] FOREIGN KEY (CaptureBatchId) REFERENCES [Test].[CaptureBatch] (CaptureBatchId);
GO

/************************************************************************/
/*                                                                      */
/*                CREATE CumulativeIntervals TABLE                      */
/*                                                                      */
/************************************************************************/

PRINT '  Creating CumulativeInterval table...';
CREATE TABLE [Test].[CumulativeInterval](
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
CREATE TABLE [Test].[CapturePacket](
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
ALTER TABLE [Test].[CapturePacket] ADD CONSTRAINT [FK_CapturePacket$CaptureBatch.CaptureBatchId] FOREIGN KEY (CaptureBatchId) REFERENCES [Test].[CaptureBatch] (CaptureBatchId);
GO

/************************************************************************/
/*                                                                      */
/*                  CREATE DisplayStatistic TABLE                       */
/*                                                                      */
/************************************************************************/

PRINT '  Creating DisplayStatistic table...';
CREATE TABLE [Test].[DisplayStatistic](
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
CREATE TABLE [Test].[SingleHistogram](
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
ALTER TABLE [Test].[SingleHistogram] ADD CONSTRAINT [FK_SingleHistogram$CaptureBatch.CaptureBatchId] FOREIGN KEY (CaptureBatchId) REFERENCES [Test].[CaptureBatch] (CaptureBatchId);
GO

/************************************************************************/
/*                                                                      */
/*                 CREATE CumulativeHistogram TABLE                     */
/*                                                                      */
/************************************************************************/

PRINT '  Creating CumulativeHistogram table...';
CREATE TABLE [Test].[CumulativeHistogram](
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
CREATE TABLE [Test].[CumulativeProbabilityDistribution](
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
CREATE TABLE [Test].[HypothesisTest](
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

