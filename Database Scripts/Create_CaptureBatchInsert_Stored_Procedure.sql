-- Create_CaptureBatchInsert_Stored_Procedure.sql
--
-- J. Savage  06-07-2015
-- COWE Thesis/Research Project (CISE)
--
-- v4 07-07-2015:
--  - Add Mean and TrimmedMean columns
--
-- v3 06-14-2015:
--  - First production version
--
-- v2 06-11-2015:
--  - Add output parameter (calling sproc from ParseCaptureFilesService)
--
-- v1 06-11-2015:
--  - No output parameter used
--

USE Packets
GO

/******************************************************************************/
/*                                                                            */
/*                         Drop Procedure                                     */
/*                                                                            */
/******************************************************************************/

IF OBJECT_ID(N'[COWE].[CaptureBatchInsert]', N'P') IS NOT NULL
	DROP PROCEDURE [COWE].[CaptureBatchInsert];
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

CREATE PROCEDURE [COWE].[CaptureBatchInsert]
	@FileName VARCHAR(200),
	@Marked bit,
	@Mean decimal = 0,  -- Default value
	@TrimmedMean decimal = 0,  -- Default value
	@NewCaptureBatchId int output

AS 

BEGIN

SET ROWCOUNT 0

	BEGIN TRY
		BEGIN TRANSACTION InsertCaptureBatch
		INSERT INTO [COWE].[CaptureBatch] (FileName, Marked, [Mean], TrimmedMean)
		VALUES (@FileName, @Marked, @Mean, @TrimmedMean);
		COMMIT;

		-- Get the new CaptureBatchId
		SET @NewCaptureBatchId = SCOPE_IDENTITY();
		RETURN @NewCaptureBatchId

	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION

		DECLARE @ERROR_MESSAGE VARCHAR(4000)

		SET @ERROR_MESSAGE = 'Error inserting new record in CaptureBatch:' + CHAR(13) + CHAR(10) + ERROR_MESSAGE()
		RAISERROR (@ERROR_MESSAGE, 16, 1)

	END CATCH
END
GO
