CREATE PROCEDURE [dbo].[TestSuite_Update](
	@TestSuiteId BIGINT,
	@TestPlanId BIGINT,
	@ParentTestSuiteId BIGINT,
	@Name VARCHAR(200),
	@Description VARCHAR(1000)	
)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		
		UPDATE TestSuite
			SET TestPlanId = @TestPlanId,
				ParentTestSuiteId = @ParentTestSuiteId,
				Name = @Name,
				Description = @Description				
		WHERE TestSuiteId = @TestSuiteId

	END TRY
	BEGIN CATCH
		SELECT 
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END