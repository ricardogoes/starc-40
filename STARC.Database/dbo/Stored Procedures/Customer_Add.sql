CREATE PROCEDURE [dbo].[Customer_Add](
	@Name VARCHAR(200),
	@DocumentId VARCHAR(20),
	@Address VARCHAR(200),
	@Status BIT,
	@CreatedBy BIGINT,
	@CreatedDate DATETIME,
	@LastUpdatedBy BIGINT,
	@LastUpdatedDate DATETIME
)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		
		DECLARE @ReturnTable TABLE (CustomerId BIGINT)

		INSERT INTO Customer(Name, DocumentId, Address, Status, CreatedBy, CreatedDate, LastUpdatedBy, LastUpdatedDate)
		OUTPUT inserted.CustomerId INTO @ReturnTable
		VALUES(@Name, @DocumentId, @Address, @Status, @CreatedBy, @CreatedDate, @LastUpdatedBy, @LastUpdatedDate)

		SELECT CustomerId FROM @ReturnTable

	END TRY
	BEGIN CATCH
		SELECT 
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH

END
