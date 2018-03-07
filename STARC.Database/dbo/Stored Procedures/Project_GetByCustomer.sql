CREATE PROCEDURE [dbo].[Project_GetByCustomer](@CustomerId BIGINT)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		SELECT	P.ProjectId,
				P.Name,
				P.Description,
				P.StartDate,
				P.FinishDate,
				P.Status,
				P.OwnerId,
				O.FirstName + ' ' + O.LastName as Owner,
				P.CustomerId,
				C.Name AS Customer,
				P.CreatedBy,
				Cre.FirstName + ' ' + Cre.LastName as CreatedName,
				P.CreatedDate,
				P.LastUpdatedBy,
				Upd.FirstName + ' ' + Upd.LastName AS LastUpdatedName,
				P.LastUpdatedDate
		FROM Project P (NOLOCK)
		INNER JOIN Customer C (NOLOCK)
			ON P.CustomerId = C.CustomerId
		LEFT JOIN [User] O (NOLOCK)
			ON P.OwnerId = O.UserId
		LEFT JOIN [User] Cre (NOLOCK)
			ON P.CreatedBy = Cre.UserId
		LEFT JOIN [User] Upd (NOLOCK)
			ON P.LastUpdatedBy = Upd.UserId
		WHERE P.CustomerId = @CustomerId
	END TRY
	BEGIN CATCH
		SELECT
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END
