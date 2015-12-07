-- Create_TruncateAllIntervalTables_Stored_Procedure.sql
--
-- J. Savage  08-09-2015
-- COWE Thesis/Research Project (CISE)
--
-- Truncate BatchInterval, CumulativeInterval, statistic and test tables.  Call this procedure
-- when a new interval parameters have been entered and the intervals must be recalculated.
--
-- v1 08-09-2015:
--  - Initial version
--
-- v2 11-17-2015:
--  - Add statistic and test tables.
--

USE Packets
GO

/******************************************************************************/
/*                                                                            */
/*                         Drop Procedure                                     */
/*                                                                            */
/******************************************************************************/

IF OBJECT_ID(N'[COWE].[TruncateAllIntervalTables]', N'P') IS NOT NULL
	DROP PROCEDURE [COWE].[TruncateAllIntervalTables];
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

CREATE PROCEDURE [COWE].[TruncateAllIntervalTables]

AS 

BEGIN

SET ROWCOUNT 0

	BEGIN TRY

		BEGIN TRANSACTION TruncateTablesTransaction

 			TRUNCATE TABLE COWE.BatchInterval;
			TRUNCATE TABLE COWE.CumulativeInterval;
			TRUNCATE TABLE COWE.DisplayStatistic;
			TRUNCATE TABLE COWE.SingleHistogram;
			TRUNCATE TABLE COWE.CumulativeHistogram;
			TRUNCATE TABLE COWE.CumulativeProbabilityDistribution;
			TRUNCATE TABLE COWE.HypothesisTest;
			TRUNCATE TABLE COWE.CaptureBatch;
			TRUNCATE TABLE COWE.CapturePacket;

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
