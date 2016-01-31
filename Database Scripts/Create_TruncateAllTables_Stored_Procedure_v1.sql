-- Create_TruncateAllTables_Stored_Procedure.sql
--
-- J. Savage  11-29-2015
-- COWE Thesis/Research Project (CISE)
--
-- Truncate all tables.  Call this procedure
-- when a new interval parameters have been entered and the intervals must be recalculated.
--
-- v1 11-29-2015:
--  - Initial version
--

USE Packets
GO

/******************************************************************************/
/*                                                                            */
/*                         Drop Procedure                                     */
/*                                                                            */
/******************************************************************************/

PRINT 'Dropping procedure [COWE].[TruncateAllTables]...';

IF OBJECT_ID(N'[COWE].[TruncateAllTables]', N'P') IS NOT NULL
  BEGIN
	DROP PROCEDURE [COWE].[TruncateAllTables];
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

PRINT 'Creating procedure [COWE].[TruncateAllTables]...';
GO

CREATE PROCEDURE [COWE].[TruncateAllTables]

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

		SET @ERROR_MESSAGE = 'Error truncating all tables:' + CHAR(13) + CHAR(10) + ERROR_MESSAGE()
		RAISERROR (@ERROR_MESSAGE, 16, 1)

	END CATCH
END
GO

PRINT '  Procedure [COWE].[TruncateAllTables] successfully created';
