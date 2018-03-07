CREATE PROCEDURE [dbo].[TestCase_Update](
	@TestCaseId BIGINT,
	@TestSuiteId BIGINT,
	@Name VARCHAR(200),
	@Type VARCHAR(30),
	@Description VARCHAR(1000),
	@PreConditions VARCHAR(1000),
	@PosConditions VARCHAR(1000),
	@ExpectedResult VARCHAR(1000),
	@LastUpdatedBy BIGINT,
	@LastUpdatedDate DATETIME
)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		
		UPDATE TestCase
			SET TestSuiteId = @TestSuiteId,
				Name = @Name,
				Type = @Type,
				Description = @Description,
				PreConditions = @PreConditions,
				PosConditions = @PosConditions,
				ExpectedResult = @ExpectedResult,
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