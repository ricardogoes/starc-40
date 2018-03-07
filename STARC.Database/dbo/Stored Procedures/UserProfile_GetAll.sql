CREATE PROCEDURE [dbo].[UserProfile_GetAll]
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		SELECT UserProfileId, ProfileName 
		FROM UserProfile (NOLOCK)
		WHERE UserProfileId <> 1

	END TRY
	BEGIN CATCH
		SELECT
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END
