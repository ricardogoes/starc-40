
CREATE PROCEDURE [dbo].[Project_Update](
	@ProjectId BIGINT,
	@Name VARCHAR(200),
	@Description VARCHAR(1000),
	@StartDate DATETIME,
	@FinishDate DATETIME,
	@OwnerId BIGINT,
	@CustomerId BIGINT,
	@LastUpdatedBy BIGINT,
	@LastUpdatedDate DATETIME
)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		UPDATE Project
			SET Name = @Name,
				Description = @Description,
				StartDate = @StartDate,
				FinishDate = @FinishDate,
				OwnerId = @OwnerId,
				CustomerId = @CustomerId,
				LastUpdatedBy = @LastUpdatedBy,
				LastUpdatedDate = @LastUpdatedDate
		WHERE ProjectId = @ProjectId
	END TRY
	BEGIN CATCH
		SELECT
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH

END
