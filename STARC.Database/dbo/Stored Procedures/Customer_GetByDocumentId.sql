CREATE PROCEDURE [dbo].[Customer_GetByDocumentId](@DocumentId VARCHAR(20))
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		SELECT	C.CustomerId, 
				C.Name, 
				C.DocumentId, 
				C.Address, 
				C.Status, 
				C.CreatedBy, 
				Cre.FirstName + ' ' + Cre.LastName AS CreatedName, 
				C.CreatedDate, 
				C.LastUpdatedBy, 
				Upd.FirstName + ' ' + Upd.LastName AS LastUpdatedName,
				C.LastUpdatedDate
		FROM Customer C (NOLOCK)
		INNER JOIN [User] Cre (NOLOCK)
			ON C.CreatedBy = Cre.UserId
		INNER JOIN [User] Upd (NOLOCK)
			ON C.LastUpdatedBy = Upd.UserId
		WHERE DocumentId = @DocumentId

	END TRY
	BEGIN CATCH
		SELECT
			ERROR_NUMBER() as ErrorNumber,
			ERROR_MESSAGE() as ErrorMessage
	END CATCH
END
