CREATE PROCEDURE [dbo].[UsersInProjects_GetByUser](@UserId BIGINT)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY

		DECLARE @UserProfileId INT
		SELECT @UserProfileId = UserProfileId
		FROM [User] (NOLOCK)
		WHERE UserId = @UserId

		IF @UserProfileId = 1
		BEGIN
			SELECT	NULL AS UserInProjectId,
					NULL AS UserId,
					NULL AS Username,
					P.ProjectId,
					C.Name + ' - ' +  P.Name AS Project,
					NULL AS CreatedBy,
					NULL AS CreatedName,
					NULL AS CreatedDate
			FROM Project P (NOLOCK)
			INNER JOIN Customer C (NOLOCK)
				ON P.CustomerId = C.CustomerId
			WHERE C.Status = 1 
			  AND P.Status = 1
			ORDER BY C.Name
		END
		ELSE IF @UserProfileId = 2
		BEGIN
			DECLARE @CustomerId BIGINT
			SELECT @CustomerId = CustomerId 
			FROM [User] (NOLOCK)
			WHERE UserId = @UserId

			SELECT	NULL AS UserInProjectId,
					NULL AS UserId,
					NULL AS Username,
					P.ProjectId,
					P.Name AS Project,
					NULL AS CreatedBy,
					NULL AS CreatedName,
					NULL AS CreatedDate
			FROM Project P (NOLOCK)			
			WHERE P.Status = 1
			  AND P.CustomerId = @CustomerId
			ORDER BY P.Name
		END
		ELSE
		BEGIN
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
			WHERE UP.UserId = @UserId
		END
	END TRY
	BEGIN CATCH
		SELECT
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END