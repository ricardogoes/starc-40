CREATE PROCEDURE [dbo].[TestPlan_Add](
	@Name VARCHAR(200),
	@Description VARCHAR(1000),
	@StartDate DATETIME,
	@FinishDate DATETIME,
	@Status BIT,
	@OwnerId BIGINT,
	@ProjectId BIGINT,
	@CreatedBy BIGINT,
	@CreatedDate DATETIME,
	@LastUpdatedBy BIGINT,
	@LastUpdatedDate DATETIME
)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		
		DECLARE @ReturnTable TABLE(TestPlanId BIGINT)

		INSERT INTO TestPlan (Name, Description, StartDate, FinishDate, Status, OwnerId, ProjectId, CreatedBy, CreatedDate, LastUpdatedBy, LastUpdatedDate)
		OUTPUT Inserted.TestPlanId INTO @ReturnTable
		VALUES(@Name, @Description, @StartDate, @FinishDate, @Status, @OwnerId, @ProjectId, @CreatedBy, @CreatedDate, @LastUpdatedBy, @LastUpdatedDate)
	
		SELECT TestPlanId FROM @ReturnTable
	END TRY
	BEGIN CATCH
		SELECT 
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END