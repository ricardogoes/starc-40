CREATE PROCEDURE [dbo].[TestCase_GetByTestPlan](@TestPlanId BIGINT)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		
		;WITH TestSuites(ParentTestSuiteId, TestSuiteId, Name, Level)
		AS
		(
			SELECT	TS.ParentTestSuiteId, TS.TestSuiteId, CAST(TS.Name AS VARCHAR(MAX)) Name, 0 AS Level
			FROM TestSuite TS (NOLOCK)
			WHERE TS.ParentTestSuiteId IS NULL
			  AND TS.TestPlanId = @TestPlanId

			UNION ALL

			SELECT	TS.ParentTestSuiteId, TS.TestSuiteId, CAST(T.Name + ' / ' +TS.Name AS VARCHAR(MAX)), Level+1
			FROM TestSuite TS (NOLOCK)
			INNER JOIN TestSuites AS T
				ON TS.ParentTestSuiteId = T.TestSuiteId
		)

		SELECT	TC.TestCaseId, 
				TC.Name,
				TC.Type,
				TC.Description,
				TC.PreConditions,
				TC.PosConditions,
				TC.ExpectedResult,
				TC.Status,
				TS.TestSuiteId,
				TS.Name AS TestSuite,
				TC.CreatedBy,
				Cre.FirstName + ' ' + Cre.LastName as CreatedName,
				TC.CreatedDate,
				TC.LastUpdatedBy,
				Upd.FirstName + ' ' + Upd.LastName AS LastUpdatedName,
				TC.LastUpdatedDate
		FROM TestSuites TS 
		LEFT JOIN TestCase TC(NOLOCK)
			ON TS.TestSuiteId = TC.TestSuiteId
		INNER JOIN [User] Cre (NOLOCK)
			ON TC.CreatedBy = Cre.UserId
		INNER JOIN [User] Upd (NOLOCK)
			ON TC.LastUpdatedBy = Upd.UserId
		WHERE TC.TestCaseId IS NOT NULL

	END TRY
	BEGIN CATCH
		SELECT 
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END