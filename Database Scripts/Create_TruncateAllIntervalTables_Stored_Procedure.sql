-- Create_TruncateAllIntervalTables_Stored_Procedure.sql
--
-- J. Savage  08-09-2015
-- COWE Thesis/Research Project (CISE)
--
-- Truncate the BatchInterval and CumulativeInterval tables.  Call this procedure
-- when a new interval size has been entered and the intervals must be recalculated.
--
-- v1 08-09-2015:
--  - Initial version
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

		BEGIN TRANSACTION InsertCaptureBatch

 			TRUNCATE TABLE BatchInterval;
			TRUNCATE TABLE CumulativeInterval;

		COMMIT;

	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION

		DECLARE @ERROR_MESSAGE VARCHAR(4000)

		SET @ERROR_MESSAGE = 'Error truncating all interval tables:' + CHAR(13) + CHAR(10) + ERROR_MESSAGE()
		RAISERROR (@ERROR_MESSAGE, 16, 1)

	END CATCH
END
GO
