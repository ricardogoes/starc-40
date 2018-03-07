CREATE PROCEDURE [dbo].[User_Update](
	@UserId BIGINT,
	@FirstName VARCHAR(50),
	@LastName VARCHAR(50),
	@Username VARCHAR(50),
	@Email VARCHAR(100),
	@CustomerId BIGINT,
	@UserProfileId INT,
	@LastUpdatedBy BIGINT,
	@LastUpdatedDate DATETIME
)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		UPDATE [User]
			SET FirstName = @FirstName,
				LastName = @LastName,
				UserName = @Username,
				Email = @Email,
				CustomerId = @CustomerId,
				UserProfileId = @UserProfileId,
				LastUpdatedBy = @LastUpdatedBy,
				LastUpdatedDate = @LastUpdatedDate
		WHERE UserId = @UserId
	END TRY
	BEGIN CATCH
		SELECT
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END
