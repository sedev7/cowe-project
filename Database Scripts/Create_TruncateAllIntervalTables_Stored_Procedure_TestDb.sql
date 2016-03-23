-- Create_TruncateAllIntervalTables_Stored_Procedure_TestDb.sql
--
-- J. Savage  01-31-2016
-- COWE Thesis/Research Project (CISE)
--
-- Truncate BatchInterval, CumulativeInterval, statistic and test tables.  Call this procedure
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

IF OBJECT_ID(N'[Test].[TruncateAllIntervalTables]', N'P') IS NOT NULL
	DROP PROCEDURE [Test].[TruncateAllIntervalTables];
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

CREATE PROCEDURE [Test].[TruncateAllIntervalTables]

AS 

BEGIN

SET ROWCOUNT 0

	BEGIN TRY

		BEGIN TRANSACTION TruncateTablesTransaction

 			TRUNCATE TABLE Test.BatchInterval;
			TRUNCATE TABLE Test.CumulativeInterval;
			TRUNCATE TABLE Test.DisplayStatistic;
			TRUNCATE TABLE Test.SingleHistogram;
			TRUNCATE TABLE Test.CumulativeHistogram;
			TRUNCATE TABLE Test.CumulativeProbabilityDistribution;
			TRUNCATE TABLE Test.HypothesisTest;
			TRUNCATE TABLE Test.CaptureBatch;
			TRUNCATE TABLE Test.CapturePacket;

		COMMIT;

	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION

		DECLARE @ERROR_MESSAGE VARCHAR(4000)

		SET @ERROR_MESSAGE = 'Error truncating all interval, statistic and test tables:' + CHAR(13) + CHAR(10) + ERROR_MESSAGE()
		RAISERROR (@ERROR_MESSAGE, 16, 1)

	END CATCH
END
GO
