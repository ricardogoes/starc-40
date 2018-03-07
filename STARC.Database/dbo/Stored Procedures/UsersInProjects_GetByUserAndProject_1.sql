CREATE PROCEDURE UsersInProjects_GetByUserAndProject(
	@UserId BIGINT,
	@ProjectId BIGINT
)
AS
BEGIN
	BEGIN TRY
		SET NOCOUNT ON

		SELECT	UP.UserInProjectId,
				UP.UserId,
				UP.ProjectId,
				UP.CreatedBy,
				UP.CreatedDate
		FROM UsersInProjects UP (NOLOCK)
		WHERE UP.ProjectId = @ProjectId 
		  AND UP.UserId = @UserId
	END TRY
	BEGIN CATCH
		SELECT
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END
GO

