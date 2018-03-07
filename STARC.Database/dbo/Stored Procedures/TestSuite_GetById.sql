CREATE PROCEDURE [dbo].[TestSuite_GetById](@TestSuiteId BIGINT)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		SELECT	TS.TestSuiteId, 
				TS.TestPlanId, 
				TP.Name AS TestPlan, 
				TS.ParentTestSuiteId, 
				PTS.Name AS ParentTestSuite, 
				TS.Name,
				TS.Description,
				TS.CreatedBy,
				Cre.FirstName + ' ' + Cre.LastName as CreatedName,
				TS.CreatedDate
		FROM TestSuite TS (NOLOCK)
		INNER JOIN [User] Cre (NOLOCK)
			ON TS.CreatedBy = Cre.UserId
		LEFT JOIN TestPlan TP (NOLOCK)
			ON TS.TestPlanId = TP.TestPlanId
		LEFT JOIN TestSuite PTS (NOLOCK)
			ON TS.ParentTestSuiteId = PTS.TestPlanId
		WHERE TS.TestSuiteId = @TestSuiteId
	END TRY
	BEGIN CATCH
		SELECT
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END