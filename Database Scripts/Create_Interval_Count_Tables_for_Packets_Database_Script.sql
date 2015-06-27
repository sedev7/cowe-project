-- Create_Interval_Count_Tables_for_Packets_Database_Script.sql
--
-- J. Savage  05-16-2015
--
-- COWE Thesis/Research Project (CISE)
--
-- These tables hold packet counts per interval for flooder batches and
-- cumulative counts that will be used by the analysis subsystem.
--
-- v2 06-11-2015:
--  - Add table name to FK constraint
--

USE [Packets]
GO

PRINT 'Starting Script...';

/************************************************************************/
/*                                                                      */
/*                DROP FOREIGN KEY CONSTRAINTS                          */
/*                                                                      */
/************************************************************************/

PRINT 'Dropping foreign keys...';

IF OBJECT_ID(N'[COWE].[FK_BatchInterval$CaptureBatch.CaptureBatchId]',N'F') IS NOT NULL
  ALTER TABLE [COWE].[BatchInterval] DROP CONSTRAINT [FK_BatchInterval$CaptureBatch.CaptureBatchId];
GO

PRINT 'Done dropping foreign keys';

/************************************************************************/
/*                                                                      */
/*               DROP THE TABLES IF THEY EXIST                          */
/*                                                                      */
/************************************************************************/

PRINT 'Dropping tables:';

PRINT '  Dropping BatchInterval table...';
IF  OBJECT_ID(N'[COWE].[BatchInterval]',N'U') IS NOT NULL
  DROP TABLE [COWE].[BatchInterval];
GO

PRINT '  Dropping CaptureBatch table...';
IF  OBJECT_ID(N'[COWE].[CaptureBatch]',N'U') IS NOT NULL
  DROP TABLE [COWE].[CaptureBatch];
GO

PRINT '  Dropping CumulativeIntervals table...';
IF  OBJECT_ID(N'[COWE].[CumulativeInterval]',N'U') IS NOT NULL
  DROP TABLE [COWE].[CumulativeInterval];
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
/*                    CREATE FlooderBatch TABLE                         */
/*                                                                      */
/************************************************************************/

PRINT '  Creating CaptureBatch table...';
CREATE TABLE [COWE].[CaptureBatch](
	[CaptureBatchId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [varchar](200) NOT NULL,
	[Marked] [bit] NOT NULL,
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
 ,
 CONSTRAINT [UC_CumulativeIntervalNumber] UNIQUE (CumulativeIntervalNumber)
) ON [PRIMARY]
;
GO


PRINT 'Done creating tables';

SET ANSI_PADDING OFF
GO

PRINT 'Script completed';

