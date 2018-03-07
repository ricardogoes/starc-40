
CREATE PROCEDURE UserProfile_GetById(@UserProfileId INT)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		SELECT UserProfileId, ProfileName 
		FROM UserProfile (NOLOCK)
		WHERE UserProfileId = @UserProfileId

	END TRY
	BEGIN CATCH
		SELECT
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END
