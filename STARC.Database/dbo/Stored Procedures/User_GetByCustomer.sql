CREATE PROCEDURE [dbo].[User_GetByCustomer](@CustomerId BIGINT)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		SELECT	U.UserId, 
				U.FirstName, 
				U.LastName, 
				U.UserName,
				U.Email, 
				U.Status, 
				UP.UserProfileId, 
				UP.ProfileName, 
				U.CustomerId,
				C.Name AS Customer,
				U.CreatedBy,
				Cre.FirstName + ' ' + Cre.LastName as CreatedName,
				U.CreatedDate,
				U.LastUpdatedBy,
				Upd.FirstName + ' ' + Upd.LastName AS LastUpdatedName,
				U.LastUpdatedDate
		FROM [User] U (NOLOCK)
		LEFT JOIN [User] Cre (NOLOCK)
			ON U.CreatedBy = Cre.UserId
		LEFT JOIN [User] Upd (NOLOCK)
			ON U.LastUpdatedBy = Upd.UserId
		INNER JOIN UserProfile UP (NOLOCK)
			ON U.UserProfileId = UP.UserProfileId
		LEFT JOIN Customer C (NOLOCK)
			ON U.CustomerId = C.CustomerId
		WHERE U.CustomerId = @CustomerId
	END TRY
	BEGIN CATCH
		SELECT
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END
