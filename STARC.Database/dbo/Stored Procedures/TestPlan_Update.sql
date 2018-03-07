CREATE PROCEDURE [dbo].[TestPlan_Update](
	@TestPlanId BIGINT,
	@Name VARCHAR(200),
	@Description VARCHAR(1000),
	@StartDate DATETIME,
	@FinishDate DATETIME,
	@OwnerId BIGINT,
	@ProjectId BIGINT,
	@LastUpdatedBy BIGINT,
	@LastUpdatedDate DATETIME
)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		UPDATE TestPlan
			SET Name = @Name,
				Description = @Description,
				StartDate = @StartDate,
				FinishDate = @FinishDate,
				OwnerId = @OwnerId,
				ProjectId = @ProjectId,
				LastUpdatedBy = @LastUpdatedBy,
				LastUpdatedDate = @LastUpdatedDate
		WHERE TestPlanId = @TestPlanId
	END TRY
	BEGIN CATCH
		SELECT
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH

END