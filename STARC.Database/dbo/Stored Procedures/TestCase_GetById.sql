CREATE PROCEDURE [dbo].[TestCase_GetById](@TestCaseId BIGINT)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRY
		
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
		FROM  TestCase TC(NOLOCK)
		INNER JOIN TestSuite TS (NOLOCK)
			ON TC.TestSuiteId = TS.TestSuiteId
		INNER JOIN [User] Cre (NOLOCK)
			ON TC.CreatedBy = Cre.UserId
		INNER JOIN [User] Upd (NOLOCK)
			ON TC.LastUpdatedBy = Upd.UserId
		WHERE TC.TestCaseId = @TestCaseId

	END TRY
	BEGIN CATCH
		SELECT 
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END