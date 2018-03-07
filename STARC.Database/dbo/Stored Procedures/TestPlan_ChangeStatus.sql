CREATE PROCEDURE [dbo].[TestPlan_ChangeStatus](
	@TestPlanId BIGINT,
	@Status BIT,
	@LastUpdatedBy BIGINT,
	@LastUpdatedDate DATETIME
)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		UPDATE TestPlan
			SET Status = @Status,
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