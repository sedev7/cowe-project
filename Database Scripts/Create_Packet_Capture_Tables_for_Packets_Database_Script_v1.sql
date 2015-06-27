-- Create_Packet_Capture_Tables_for_Packets_Database_Script.sql
--
-- J. Savage  05-05-2015
--
-- COWE Thesis/Research Project (CISE)
--
-- These tables contain raw packet capture data that will be used to
-- calculate flooder interval counts in the analysis subsystem.
--
-- v1 06-11-2015:
--  - Initial script
--

USE [Packets]
GO


/******  DROP THE TABLE IF IT EXISTS ******/

--IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CapturePackets]') AND type in (N'U'))
--DROP TABLE [dbo].[CapturePackets]
--GO

-- Note: alternate DROP TABLE syntax:
IF  OBJECT_ID(N'[COWE].[CapturePacket]',N'U') IS NOT NULL
  DROP TABLE [COWE].[CapturePacket]
GO



/******  CREATE THE TABLE  ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [COWE].[CapturePacket](
	[CapturePacketId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [varchar](200) NOT NULL,
	[PacketNumber] [int] NOT NULL,
	[TimeStamp] [DateTime] NOT NULL,
	[Marked] [bit] NOT NULL,
 CONSTRAINT [PK_CapturePacket_CapturePacketId] PRIMARY KEY CLUSTERED 
(
	[CapturePacketId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

