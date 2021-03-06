-- Create_Histogram_Delete_Stored_Procedure.sql
--
-- J. Savage  08-27-2015
-- COWE Thesis/Research Project (CISE)
--
-- v1 08-27-2015:
--  - First version
--

USE Packets
GO

/******************************************************************************/
/*                                                                            */
/*                         Drop Procedure                                     */
/*                                                                            */
/******************************************************************************/

IF OBJECT_ID(N'[COWE].[HistogramDelete]', N'P') IS NOT NULL
	DROP PROCEDURE [COWE].[HistogramDelete];
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

CREATE PROCEDURE [COWE].[HistogramDelete]
	@CaptureState int,
	@BatchType int

AS 

BEGIN

SET ROWCOUNT 0

	BEGIN TRY

		BEGIN TRANSACTION DeleteHistogramData
		DELETE FROM [COWE].[Histogram] WHERE CaptureState = @CaptureState AND BatchType = @BatchType;
		COMMIT;

	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION

		DECLARE @ERROR_MESSAGE VARCHAR(4000)

		SET @ERROR_MESSAGE = 'Error deleting records in Histogram:' + CHAR(13) + CHAR(10) + ERROR_MESSAGE()
		RAISERROR (@ERROR_MESSAGE, 16, 1)

	END CATCH
END
GO
