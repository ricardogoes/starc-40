
CREATE PROCEDURE [dbo].[Project_Add](
	@Name VARCHAR(200),
	@Description VARCHAR(1000),
	@StartDate DATETIME,
	@FinishDate DATETIME,
	@Status BIT,
	@OwnerId BIGINT,
	@CustomerId BIGINT,
	@CreatedBy BIGINT,
	@CreatedDate DATETIME,
	@LastUpdatedBy BIGINT,
	@LastUpdatedDate DATETIME
)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		
		DECLARE @ReturnTable TABLE(ProjectId BIGINT)

		INSERT INTO Project (Name, Description, StartDate, FinishDate, Status, OwnerId, CustomerId, CreatedBy, CreatedDate, LastUpdatedBy, LastUpdatedDate)
		OUTPUT Inserted.ProjectId INTO @ReturnTable
		VALUES(@Name, @Description, @StartDate, @FinishDate, @Status, @OwnerId, @CustomerId, @CreatedBy, @CreatedDate, @LastUpdatedBy, @LastUpdatedDate)
	
		SELECT ProjectId FROM @ReturnTable
	END TRY
	BEGIN CATCH
		SELECT 
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END
