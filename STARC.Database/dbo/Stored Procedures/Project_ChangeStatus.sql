CREATE PROCEDURE [dbo].[Project_ChangeStatus](
	@ProjectId BIGINT,
	@Status BIT,
	@LastUpdatedBy BIGINT,
	@LastUpdatedDate DATETIME
)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		UPDATE Project
			SET Status = @Status,
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
