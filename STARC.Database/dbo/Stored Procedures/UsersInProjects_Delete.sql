CREATE PROCEDURE UsersInProjects_Delete(@UserInProjectId BIGINT)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY

		DELETE FROM UsersInProjects
		WHERE UserInProjectId = @UserInProjectId

	END TRY
	BEGIN CATCH
		SELECT
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END

