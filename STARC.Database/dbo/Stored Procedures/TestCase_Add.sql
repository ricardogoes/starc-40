CREATE PROCEDURE [dbo].[TestCase_Add](
	@TestSuiteId BIGINT,
	@Name VARCHAR(200),
	@Type VARCHAR(30),
	@Description VARCHAR(1000),
	@PreConditions VARCHAR(1000),
	@PosConditions VARCHAR(1000),
	@ExpectedResult VARCHAR(1000),
	@Status BIT,
	@CreatedBy BIGINT,
	@CreatedDate DATETIME,
	@LastUpdatedBy BIGINT,
	@LastUpdatedDate DATETIME
)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		
		DECLARE @ReturnTable TABLE(TestCaseId BIGINT)

		INSERT INTO TestCase (TestSuiteId, Name, Type, Description, PreConditions, PosConditions, ExpectedResult, Status, CreatedBy, CreatedDate, LastUpdatedBy, LastUpdatedDate)
		OUTPUT Inserted.TestCaseId INTO @ReturnTable
		VALUES(@TestSuiteId, @Name, @Type, @Description, @PreConditions, @PosConditions, @ExpectedResult, @Status, @CreatedBy, @CreatedDate, @LastUpdatedBy, @LastUpdatedDate)
	
		SELECT TestCaseId FROM @ReturnTable
	END TRY
	BEGIN CATCH
		SELECT 
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END