
CREATE PROCEDURE Customer_Update(
	@CustomerId BIGINT,
	@Name VARCHAR(200),
	@DocumentId VARCHAR(20),
	@Address VARCHAR(200),
	@LastUpdatedBy BIGINT,
	@LastUpdatedDate DATETIME
)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		UPDATE Customer
			SET Name = @Name,
				DocumentId = @DocumentId,
				Address = @Address,
				LastUpdatedBy = @LastUpdatedBy,
				LastUpdatedDate = @LastUpdatedDate
		WHERE CustomerId = @CustomerId
	END TRY
	BEGIN CATCH
		SELECT 
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END
