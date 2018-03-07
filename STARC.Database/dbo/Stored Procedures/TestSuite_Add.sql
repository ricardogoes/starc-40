CREATE PROCEDURE [dbo].[TestSuite_Add](
	@TestPlanId BIGINT,
	@ParentTestSuiteId BIGINT,
	@Name VARCHAR(200),
	@Description VARCHAR(1000),
	@CreatedBy BIGINT,
	@CreatedDate DATETIME
)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		
		DECLARE @ReturnTable TABLE(TestSuiteId BIGINT)

		INSERT INTO TestSuite (TestPlanId, ParentTestSuiteId, Name, Description, CreatedBy, CreatedDate)
		OUTPUT Inserted.TestSuiteId INTO @ReturnTable
		VALUES(@TEstPlanId, @ParentTestSuiteId, @Name, @Description, @CreatedBy, @CreatedDate)
	
		SELECT TestSuiteId FROM @ReturnTable
	END TRY
	BEGIN CATCH
		SELECT 
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END