CREATE PROCEDURE [dbo].[TestPlan_GetActiveByProject](@ProjectId AS BIGINT)
AS
BEGIN
	SET NOCOUNT ON
	BEGIN TRY

		SELECT	TP.TestPlanId, 
				TP.Name, 
				TP.Description, 
				TP.StartDate, 
				TP.FinishDate, 
				TP.Status, 
				TP.OwnerId, 
				U.FirstName + ' ' + U.LastName AS Owner, 
				TP.ProjectId, 
				P.Name as Project,
				U.CreatedBy,
				Cre.FirstName + ' ' + Cre.LastName as CreatedName,
				U.CreatedDate,
				U.LastUpdatedBy,
				Upd.FirstName + ' ' + Upd.LastName AS LastUpdatedName,
				U.LastUpdatedDate
		FROM TestPlan TP(NOLOCK)
		INNER JOIN [User] Cre (NOLOCK)
			ON TP.CreatedBy = Cre.UserId
		INNER JOIN [User] Upd (NOLOCK)
			ON TP.LastUpdatedBy = Upd.UserId
		INNER JOIN Project P(NOLOCK)
			ON TP.ProjectId = P.ProjectId
		LEFT JOIN [User] U (NOLOCK)
			ON P.OwnerId = U.UserId
		WHERE TP.ProjectId = @ProjectId 
		  AND TP.Status = 1
	
	END TRY
	BEGIN CATCH
		SELECT 
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END