-- Truncate_COWE_Schema_Tables.sql
--
-- J. Savage  05-31-2015
-- COWE Thesis/Research Project (CISE)
--

USE Packets;
GO	

PRINT 'Starting Script...';
PRINT 'Truncating BatchIntervals table...';
-- Truncate BatchIntervals table first (references CaptureBatch FK)
TRUNCATE TABLE COWE.BatchIntervals;

-- Drop the FK constraint
PRINT 'Dropping CaptureBatch foreign key...';
IF OBJECT_ID(N'[COWE].[FK_CaptureBatch$CaptureBatchId]',N'F') IS NOT NULL
  BEGIN
    PRINT 'Found foreign key - dropping the FK constraint...';
    ALTER TABLE [COWE].[BatchIntervals] DROP CONSTRAINT [FK_CaptureBatch$CaptureBatchId];
	PRINT 'Done dropping foreign keys';
  END
ELSE
  BEGIN
    PRINT 'Foreign key [FK_CaptureBatch$CaptureBatchId] not found!';
  END
GO

-- Truncate the remaining tables
PRINT 'Truncating CaptureBatch table...';
TRUNCATE TABLE COWE.CaptureBatch;

PRINT 'Truncating CapturePacket table...';
TRUNCATE TABLE COWE.CapturePacket;

PRINT 'Truncating Cumulative table...';
TRUNCATE TABLE COWE.CumulativeInterval;
GO

PRINT 'Done truncating tables';

-- Recreate the foreign key
PRINT 'Recreating the CaptureBatch foreign key...';
ALTER TABLE [COWE].[BatchIntervals]  WITH CHECK ADD CONSTRAINT [FK_CaptureBatch$CaptureBatchId] FOREIGN KEY([CaptureBatchId]) REFERENCES [COWE].[CaptureBatch] ([CaptureBatchId])
GO
ALTER TABLE [COWE].[BatchIntervals] CHECK CONSTRAINT [FK_CaptureBatch$CaptureBatchId]
GO

-- Reseed the identity columns on each table
PRINT 'Reseeding the Identity columns:';

PRINT 'Reseeding the BatchIntervals Table...';
DBCC CHECKIDENT ('COWE.BatchIntervals', RESEED, 1);

PRINT 'Reseeding the CaptureBatch Table...';
DBCC CHECKIDENT ('COWE.CaptureBatch', RESEED, 1);

PRINT 'Reseeding the CapturePacket Table...';
DBCC CHECKIDENT ('COWE.CapturePacket', RESEED, 1);

PRINT 'Reseeding the CumulativeInterval Table...';
DBCC CHECKIDENT ('COWE.CumulativeInterval', RESEED, 1);
GO
PRINT 'Done reseeding the Identity columns';

PRINT 'Script completed';