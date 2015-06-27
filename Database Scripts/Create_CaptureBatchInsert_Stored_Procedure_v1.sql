-- Create_CaptureBatchInsert_Stored_Procedure.sql
--
-- J. Savage  06-07-2015
-- COWE Thesis/Research Project (CISE)
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
	@Marked bit
	--@NewCaptureBatchId int output

AS 

BEGIN

SET ROWCOUNT 0

	BEGIN TRY
		BEGIN TRANSACTION InsertCaptureBatch
		INSERT INTO [COWE].[CaptureBatch] (FileName, Marked)
		VALUES (@FileName, @Marked);
		COMMIT;

		-- Get the new CaptureBatchId
		--RETURN( SELECT SCOPE_IDENTITY() AS CaptureBatchId);
		--RETURN SCOPE_IDENTITY()
		--SET @NewCaptureBatchId = SCOPE_IDENTITY();
		--RETURN @NewCaptureBatchId
		--SELECT SCOPE_IDENTITY() AS CaptureBatchId
		--SELECT SCOPE_IDENTITY();
		--SELECT MAX(CaptureBatchId) AS CaptureBatchId from COWE.CaptureBatch;
		--COMMIT;
		--DECLARE @NewCaptureBatchId INT = SCOPE_IDENTITY();
		--SELECT @NewCaptureBatchId as CaptureBatchId;

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
