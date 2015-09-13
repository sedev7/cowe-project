-- Create_DisplayStatistics_Insert_Stored_Procedure.sql
--
-- J. Savage  08-15-2015
-- COWE Thesis/Research Project (CISE)
--
-- v1 08-15-2015:
--  - First version
--

USE Packets
GO

/******************************************************************************/
/*                                                                            */
/*                         Drop Procedure                                     */
/*                                                                            */
/******************************************************************************/

IF OBJECT_ID(N'[COWE].[DisplayStatisticsInsert]', N'P') IS NOT NULL
	DROP PROCEDURE [COWE].[DisplayStatisticsInsert];
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

CREATE PROCEDURE [COWE].[DisplayStatisticsInsert]
	@IntervalCount int,
	@TrimmedIntervalCount int,
	@MeanPacketsPerInterval decimal(28,10),
	@StandardDeviation decimal(28,10),
	@MinPacketsPerInterval int,
	@MaxPacketsPerInterval int,
	@MeanOfMeans decimal(28,10), 
	@MeanOfMeansStandardDeviation decimal(28,10), 
	@Marked bit,
	@BatchType int

AS 

BEGIN

SET ROWCOUNT 0

	BEGIN TRY

		BEGIN TRANSACTION InsertDisplayStatistics
		INSERT INTO [COWE].[DisplayStatistic] (IntervalCount, TrimmedIntervalCount, MeanPacketsPerInterval, [StandardDeviation], MinPacketsPerInterval, MaxPacketsPerInterval, MeanOfMeans, MeanOfMeansStandardDeviation, Marked, BatchType)
		VALUES (@IntervalCount,	@TrimmedIntervalCount, @MeanPacketsPerInterval, @StandardDeviation,	@MinPacketsPerInterval,	@MaxPacketsPerInterval,	@MeanOfMeans, @MeanOfMeansStandardDeviation, @Marked, @BatchType);
		COMMIT;

	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION

		DECLARE @ERROR_MESSAGE VARCHAR(4000)

		SET @ERROR_MESSAGE = 'Error inserting new record in DisplayStatistics:' + CHAR(13) + CHAR(10) + ERROR_MESSAGE()
		RAISERROR (@ERROR_MESSAGE, 16, 1)

	END CATCH
END
GO
