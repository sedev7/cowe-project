-- Create_CaptureBatchUpdate_for_CapturBatchId_Stored_Procedure.sql
--
-- J. Savage  09-08-2015
-- COWE Thesis/Research Project (CISE)
--
-- v1 09-08-2015:
--  - First version
--

USE Packets
GO

/******************************************************************************/
/*                                                                            */
/*                         Drop Procedure                                     */
/*                                                                            */
/******************************************************************************/

PRINT 'Dropping procedure [CaptureBatchUpdate_for_CaptureBatchId]...'

IF OBJECT_ID(N'[COWE].[CaptureBatchUpdate_for_CaptureBatchId]', N'P') IS NOT NULL
	BEGIN
		DROP PROCEDURE [COWE].[CaptureBatchUpdate_for_CaptureBatchId];
		PRINT '  -> Procedure [CaptureBatchUpdate_for_CaptureBatchId] dropped'
	END
ELSE
	PRINT '  -> Procedure [CaptureBatchUpdate_for_CaptureBatchId] not found!'
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

PRINT 'Creating procedure [CaptureBatchUpdate_for_CaptureBatchId]...'
GO
CREATE PROCEDURE [COWE].[CaptureBatchUpdate_for_CaptureBatchId]
	@CaptureBatchId int,
	@FileName varchar(200),
	@Marked bit,
	@Mean decimal(28,10),
	@TrimmedMean decimal(28,10),
	@Parsed bit,
	@SingleStatistics bit,
	@CumulativeStatistics bit

AS 

BEGIN

SET ROWCOUNT 0

	BEGIN TRY

		BEGIN TRANSACTION UpdateCaptureBatch
		UPDATE [COWE].[CaptureBatch]
		   SET [FileName] = @FileName,
		       Marked = @Marked,
			   Mean = @Mean,
			   TrimmedMean = @TrimmedMean,
			   Parsed = @Parsed,
			   SingleStatistics = @SingleStatistics,
			   CumulativeStatistics = @CumulativeStatistics
		WHERE CaptureBatchId = @CaptureBatchId;
		COMMIT;
	END TRY
	
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION

		DECLARE @ERROR_MESSAGE VARCHAR(4000)

		SET @ERROR_MESSAGE = 'Error inserting new record into CumulativeProbabilityDistribution table:' + CHAR(13) + CHAR(10) + ERROR_MESSAGE()
		RAISERROR (@ERROR_MESSAGE, 16, 1)

	END CATCH
END
GO

IF ERROR_MESSAGE() IS NULL
	PRINT '  -> Create procedure [CaptureBatchUpdate_for_CaptureBatchId] successfully completed'
GO