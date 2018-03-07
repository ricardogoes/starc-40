CREATE PROCEDURE [dbo].[UsersInProjects_GetById](@UserInProjectId BIGINT)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY

		SELECT	UP.UserInProjectId,
				UP.UserId,
				U.FirstName + ' ' + U.LastName AS [User],
				UP.ProjectId,
				P.Name AS Project,
				UP.CreatedBy,
				Cre.FirstName + ' ' + Cre.LastName AS CreatedName,
				UP.CreatedDate
		FROM UsersInProjects UP (NOLOCK)
		INNER JOIN [User] U (NOLOCK)
			ON UP.UserId = U.UserId
			AND U.UserProfileId <> 1
		INNER JOIN Project P (NOLOCK)
			ON UP.ProjectId = P.ProjectId
		INNER JOIN [User] Cre (NOLOCK)
			ON UP.CreatedBy = Cre.UserId
		WHERE UP.UserInProjectId = @UserInProjectId

	END TRY
	BEGIN CATCH
		SELECT
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END


