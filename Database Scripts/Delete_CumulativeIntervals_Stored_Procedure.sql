-- Delete_CumulativeIntervals_Stored_Procedure.sql
--
-- J. Savage  06-14-2015
-- COWE Thesis/Research Project (CISE)
--
-- v1 06-14-2015:
--  - First version
--

USE Packets
GO

/******************************************************************************/
/*                                                                            */
/*                         Drop Procedure                                     */
/*                                                                            */
/******************************************************************************/

IF OBJECT_ID(N'[COWE].[CumulativeIntervalDelete]', N'P') IS NOT NULL
	DROP PROCEDURE [COWE].[CumulativeIntervalDelete];
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--SET NOCOUNT ON
--GO

/******************************************************************************/
/*                                                                            */
/*                       Create Procedure                                     */
/*                                                                            */
/******************************************************************************/

CREATE PROCEDURE [COWE].[CumulativeIntervalDelete]
	@Marked bit,
	@RowsDeleted int out
AS 

BEGIN

SET ROWCOUNT 0

	BEGIN TRY
		BEGIN TRANSACTION DeleteCumulativeIntervals
		DELETE FROM [COWE].[CumulativeInterval] WHERE Marked = @Marked;
		COMMIT;

		-- Get the number of rows deleted
		SET @RowsDeleted = @@ROWCOUNT;
		--RETURN @RowsDeleted;

	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION

		DECLARE @ERROR_MESSAGE VARCHAR(4000)

		SET @ERROR_MESSAGE = 'Error deleting CumulativeInterval rows:' + CHAR(13) + CHAR(10) + ERROR_MESSAGE()
		RAISERROR (@ERROR_MESSAGE, 16, 1)

	END CATCH
END
GO
