-- Create_Histogram_Insert_Stored_Procedure.sql
--
-- J. Savage  08-29-2015
-- COWE Thesis/Research Project (CISE)
--
-- v1 08-29-2015:
--  - First version
--

USE Packets
GO

/******************************************************************************/
/*                                                                            */
/*                         Drop Procedure                                     */
/*                                                                            */
/******************************************************************************/

PRINT 'Dropping procedure [HistogramInsert]...'

IF OBJECT_ID(N'[COWE].[HistogramInsert]', N'P') IS NOT NULL
	BEGIN
		DROP PROCEDURE [COWE].[HistogramInsert];
		PRINT '  -> Procedure [HistogramInsert] dropped'
	END
ELSE
	PRINT '  -> Procedure [HistogramInsert] not found!'
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

PRINT 'Creating procedure [HistogramInsert]...'
GO

CREATE PROCEDURE [COWE].[HistogramInsert]
	@Interval int,
	@Probability decimal (28,10),
	@CaptureState int,
	@BatchType int

AS 

BEGIN

SET ROWCOUNT 0

	BEGIN TRY

		BEGIN TRANSACTION InsertHistogram
		INSERT INTO [COWE].[Histogram] (Interval, Probability, CaptureState, BatchType)
		VALUES (@Interval,	@Probability, @CaptureState, @BatchType);
		COMMIT;

	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION

		DECLARE @ERROR_MESSAGE VARCHAR(4000)

		SET @ERROR_MESSAGE = 'Error inserting new record into Histogram table:' + CHAR(13) + CHAR(10) + ERROR_MESSAGE()
		RAISERROR (@ERROR_MESSAGE, 16, 1)

	END CATCH
END
GO

IF ERROR_MESSAGE() IS NULL
	PRINT '  -> Create procedure [HistogramInsert] successfully completed'
GO