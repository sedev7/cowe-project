-- Truncate_COWE_Schema_Tables.sql
--
-- J. Savage  05-31-2015
-- COWE Thesis/Research Project (CISE)
--
-- v1 07-14-2015:
--  - Revised foreign key references for BatchInterval and CaptureBatch tables.
--  - Removed identity reseed for CumulativeInterval table.
--

USE Packets;
GO	

PRINT 'Starting Script...';
PRINT 'Truncating BatchInterval table...';
-- Truncate BatchInterval table first (references CaptureBatch FK)
TRUNCATE TABLE COWE.BatchInterval;

-- Truncate CapturePacket table first (references CaptureBatch FK)
PRINT 'Truncating CapturePacket table...';
TRUNCATE TABLE COWE.CapturePacket;

-- Drop the FK constraints
PRINT 'Dropping BatchInterval.CaptureBatch foreign key...';
IF OBJECT_ID(N'[COWE].[FK_BatchInterval$CaptureBatch.CaptureBatchId]',N'F') IS NOT NULL
  BEGIN
    PRINT 'Found foreign key - dropping the FK constraint...';
    ALTER TABLE [COWE].[BatchInterval] DROP CONSTRAINT [FK_BatchInterval$CaptureBatch.CaptureBatchId];
	PRINT 'Done dropping foreign keys';
  END
ELSE
  BEGIN
    PRINT 'Foreign key [FK_BatchInterval$CaptureBatch.CaptureBatchId] not found!';
  END
GO

PRINT 'Dropping CapturePacket foreign key...';
IF OBJECT_ID(N'[COWE].[FK_CapturePacket$CaptureBatch.CaptureBatchId]',N'F') IS NOT NULL
  BEGIN
    PRINT 'Found foreign key - dropping the FK constraint...';
    ALTER TABLE [COWE].[CapturePacket] DROP CONSTRAINT [FK_CapturePacket$CaptureBatch.CaptureBatchId];
	PRINT 'Done dropping foreign keys';
  END
ELSE
  BEGIN
    PRINT 'Foreign key [FK_CapturePacket$CaptureBatch.CaptureBatchId] not found!';
  END
GO

-- Truncate the remaining tables
PRINT 'Truncating CaptureBatch table...';
TRUNCATE TABLE COWE.CaptureBatch;

PRINT 'Truncating Cumulative table...';
TRUNCATE TABLE COWE.CumulativeInterval;
GO

PRINT 'Done truncating tables';

-- Recreate the foreign keys
PRINT 'Recreating the BatchInterval CaptureBatch foreign key...';
ALTER TABLE [COWE].[BatchInterval]  WITH CHECK ADD CONSTRAINT [FK_BatchInterval$CaptureBatch.CaptureBatchId] FOREIGN KEY([CaptureBatchId]) REFERENCES [COWE].[CaptureBatch] ([CaptureBatchId])
GO
ALTER TABLE [COWE].[BatchInterval] CHECK CONSTRAINT [FK_BatchInterval$CaptureBatch.CaptureBatchId]
GO

PRINT 'Recreating the CapturePacket CaptureBatch foreign key...';
ALTER TABLE [COWE].[CapturePacket]  WITH CHECK ADD CONSTRAINT [FK_CapturePacket$CaptureBatch.CaptureBatchId] FOREIGN KEY([CaptureBatchId]) REFERENCES [COWE].[CaptureBatch] ([CaptureBatchId])
GO
ALTER TABLE [COWE].[CapturePacket] CHECK CONSTRAINT [FK_CapturePacket$CaptureBatch.CaptureBatchId]
GO

-- Reseed the identity columns on each table
PRINT 'Reseeding the Identity columns:';

PRINT 'Reseeding the BatchInterval Table...';
DBCC CHECKIDENT ('COWE.BatchInterval', RESEED, 1);

PRINT 'Reseeding the CaptureBatch Table...';
DBCC CHECKIDENT ('COWE.CaptureBatch', RESEED, 1);

PRINT 'Reseeding the CapturePacket Table...';
DBCC CHECKIDENT ('COWE.CapturePacket', RESEED, 1);

GO
PRINT 'Done reseeding the Identity columns';

PRINT 'Script completed';