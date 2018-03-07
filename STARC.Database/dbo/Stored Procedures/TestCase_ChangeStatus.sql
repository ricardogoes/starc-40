CREATE PROCEDURE [dbo].[TestCase_ChangeStatus](
	@TestCaseId BIGINT,
	@Status BIT,
	@LastUpdatedBy BIGINT,
	@LastUpdatedDate DATETIME
)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		UPDATE TestCase
			SET Status = @Status,
				LastUpdatedBy = @LastUpdatedBy,
				LastUpdatedDate = @LastUpdatedDate
		WHERE TestCaseId = @TestCaseId
	END TRY
	BEGIN CATCH
		SELECT
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END
