CREATE TABLE [TestSuite](
	TestSuiteId BIGINT IDENTITY,
	TestPlanId BIGINT NULL,
	[ParentTestSuiteId] BIGINT NULL,
	Name VARCHAR(200) NOT NULL,	
	Description VARCHAR(1000) NULL,
	CreatedBy BIGINT NOT NULL,
	CreatedDate DATETIME NOT NULL,
	CONSTRAINT pkTestSuite PRIMARY KEY(TestSuiteId),
	CONSTRAINT fkTestSuiteTestPlan FOREIGN KEY(TestPlanId) REFERENCES TestPlan(TestPlanId),
	CONSTRAINT fkTestSuiteTestSuite FOREIGN KEY([ParentTestSuiteId]) REFERENCES TestSuite(TestSuiteId),
	CONSTRAINT fkTestSuiteUser FOREIGN KEY(CreatedBy) REFERENCES [User](UserId)
)

