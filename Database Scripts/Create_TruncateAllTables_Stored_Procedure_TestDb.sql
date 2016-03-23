-- Create_TruncateAllTables_Stored_Procedure_TestDb.sql
--
-- J. Savage  01-31-2016
-- COWE Thesis/Research Project (CISE)
--
-- Truncate all tables.  Call this procedure
-- when a new interval parameters have been entered and the intervals must be recalculated.
--
-- v1 01-31-2016:
--  - Initial version
--

USE PacketsTest
GO

/******************************************************************************/
/*                                                                            */
/*                         Drop Procedure                                     */
/*                                                                            */
/******************************************************************************/

PRINT 'Dropping procedure [Test].[TruncateAllTables]...';

IF OBJECT_ID(N'[Test].[TruncateAllTables]', N'P') IS NOT NULL
  BEGIN
	DROP PROCEDURE [Test].[TruncateAllTables];
    PRINT '  Procedure successfully dropped'
  END
ELSE
  PRINT '   => Procedure not found!'
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET NOCOUNT ON
GO

/******************************************************************************/
/*                                                                            */
/*                       Create Procedure                                     */
/*                                                                            */
/******************************************************************************/

PRINT 'Creating procedure [Test].[TruncateAllTables]...';
GO

CREATE PROCEDURE [Test].[TruncateAllTables]

AS 

BEGIN

SET ROWCOUNT 0

	BEGIN TRY

		BEGIN TRANSACTION TruncateTablesTransaction

			--Drop foreign key constraints
			ALTER TABLE [Test].[BatchInterval] DROP CONSTRAINT [FK_BatchInterval$CaptureBatch.CaptureBatchId];
			ALTER TABLE [Test].[CapturePacket] DROP CONSTRAINT [FK_CapturePacket$CaptureBatch.CaptureBatchId];
			ALTER TABLE [Test].[SingleHistogram] DROP CONSTRAINT [FK_SingleHistogram$CaptureBatch.CaptureBatchId];

			-- Truncate tables
 			TRUNCATE TABLE Test.BatchInterval;
			TRUNCATE TABLE Test.CumulativeInterval;
			TRUNCATE TABLE Test.DisplayStatistic;
			TRUNCATE TABLE Test.SingleHistogram;
			TRUNCATE TABLE Test.CumulativeHistogram;
			TRUNCATE TABLE Test.CumulativeProbabilityDistribution;
			TRUNCATE TABLE Test.HypothesisTest;
			TRUNCATE TABLE Test.CaptureBatch;
			TRUNCATE TABLE Test.CapturePacket;

			-- Add the foreign key constraints
			ALTER TABLE [Test].[BatchInterval] ADD CONSTRAINT [FK_BatchInterval$CaptureBatch.CaptureBatchId] FOREIGN KEY (CaptureBatchId) REFERENCES [Test].[CaptureBatch] (CaptureBatchId);
			ALTER TABLE [Test].[CapturePacket] ADD CONSTRAINT [FK_CapturePacket$CaptureBatch.CaptureBatchId] FOREIGN KEY (CaptureBatchId) REFERENCES [Test].[CaptureBatch] (CaptureBatchId);
			ALTER TABLE [Test].[SingleHistogram] ADD CONSTRAINT [FK_SingleHistogram$CaptureBatch.CaptureBatchId] FOREIGN KEY (CaptureBatchId) REFERENCES [Test].[CaptureBatch] (CaptureBatchId);

		COMMIT;

	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION

		DECLARE @ERROR_MESSAGE VARCHAR(4000)

		SET @ERROR_MESSAGE = 'Error truncating all tables:' + CHAR(13) + CHAR(10) + ERROR_MESSAGE()
		RAISERROR (@ERROR_MESSAGE, 16, 1)

	END CATCH
END
GO

PRINT '  Procedure [Test].[TruncateAllTables] successfully created';
