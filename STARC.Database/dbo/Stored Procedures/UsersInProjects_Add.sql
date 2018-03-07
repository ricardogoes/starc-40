CREATE PROCEDURE [dbo].[UsersInProjects_Add]
(
	@UserId BIGINT,
	@ProjectId BIGINT,
	@CreatedBy BIGINT,
	@CreatedDate DATETIME	
)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY

		DECLARE @ReturnTable TABLE(UserInProjectId BIGINT)

		INSERT INTO UsersInProjects(UserId, ProjectId, CreatedBy, CreatedDate)
		OUTPUT inserted.UserInProjectId INTO @ReturnTable
		VALUES(@UserId, @ProjectId, @CreatedBy, @CreatedDate)

		SELECT UserInProjectId FROM @ReturnTable
	END TRY
	BEGIN CATCH
		SELECT
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END

