-- Create_DisplayStatistic_Table_for_Packets_Database_Script.sql
--
-- J. Savage  08-13-2015
--
-- COWE Thesis/Research Project (CISE)
--
-- Create statistics tables for COWE project.
--
-- NOTE: this is a temporary script - add this script to the 
--       Create_Tables_for_Packets_Database_Script.sql script when done.
--
-- v1 08-13-2015:
--  - Initial version.
--

USE [Packets]
GO

PRINT 'Starting Script...';

/************************************************************************/
/*                                                                      */
/*               DROP THE TABLES IF THEY EXIST                          */
/*                                                                      */
/************************************************************************/

PRINT 'Dropping tables:';

PRINT '  Dropping DisplayStatistic table...';
IF  OBJECT_ID(N'[COWE].[DisplayStatistic]',N'U') IS NOT NULL
  DROP TABLE [COWE].[DisplayStatistic]
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
/*                  CREATE DisplayStatistic TABLE                       */
/*                                                                      */
/************************************************************************/

PRINT '  Creating DisplayStatistic table...';
CREATE TABLE [COWE].[DisplayStatistic](
	[DisplayStatisticId] [int] IDENTITY(1,1) NOT NULL,
	[IntervalCount] [int] NOT NULL,
	[TrimmedIntervalCount] [int] NOT NULL,
	[MeanPacketsPerInterval] [decimal] NOT NULL,
	[StandardDeviation] [decimal] NOT NULL,
	[MinPacketsPerInterval] [int] NOT NULL,
	[MaxPacketsPerInterval] [int] NOT NULL,
	[MeanOfMeans] [decimal] NOT NULL,
	[Marked] [bit] NOT NULL,
	[StatisticsType] [int] NOT NULL
 CONSTRAINT [PK_DisplayStatistic_DisplayStatisticId] PRIMARY KEY CLUSTERED 
(
	[DisplayStatisticId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
GO

PRINT 'Done creating tables';

SET ANSI_PADDING OFF
GO

PRINT 'Script completed';

