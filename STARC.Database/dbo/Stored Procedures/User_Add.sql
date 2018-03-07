
CREATE PROCEDURE [dbo].[User_Add]
(
	@FirstName VARCHAR(50),
	@LastName VARCHAR(50),
	@Username VARCHAR(50),
	@Password VARCHAR(50),
	@PasswordHash  VARBINARY(MAX),
	@Email VARCHAR(100),
	@Status BIT,
	@UserProfileId INT,
	@CreatedBy BIGINT,
	@CreatedDate DATETIME,
	@LastUpdatedBy BIGINT,
	@LastUpdatedDate DATETIME,
	@CustomerId BIGINT
)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY

		DECLARE @ReturnTable TABLE(UserId BIGINT)

		INSERT INTO [User](FirstName, LastName, UserName, Password, PasswordHash, Email, Status, UserProfileId, CreatedBy, CreatedDate, LastUpdatedBy, LastUpdatedDate, CustomerId)
		OUTPUT inserted.UserId INTO @ReturnTable
		VALUES(@FirstName, @LastName, @Username, @Password, @PasswordHash, @Email, @Status, @UserProfileId, @CreatedBy, @CreatedDate, @LastUpdatedBy, @LastUpdatedDate, @CustomerId)

		SELECT UserId FROM @ReturnTable
	END TRY
	BEGIN CATCH
		SELECT
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END
