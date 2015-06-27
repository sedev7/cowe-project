-- Create_Packet_Capture_Tables_for_Packets_Database_Script.sql
--
-- J. Savage  05-05-2015
--
-- COWE Thesis/Research Project (CISE)
--
-- These tables contain raw packet capture data that will be used to
-- calculate flooder interval counts in the analysis subsystem.
--
-- v2 06-11-2015:
--  - Change FileName field to CaptureBatchId 
--
-- v1 06-11-2015:
--  - Initial script
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

IF OBJECT_ID(N'[COWE].[FK_CapturePacket$CaptureBatch.CaptureBatchId]',N'F') IS NOT NULL
  ALTER TABLE [COWE].[CapturePacket] DROP CONSTRAINT [FK_CapturePacket$CaptureBatch.CaptureBatchId];
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
  DROP TABLE [COWE].[CapturePacket]
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

PRINT 'Done creating tables';

SET ANSI_PADDING OFF
GO

PRINT 'Script completed';

