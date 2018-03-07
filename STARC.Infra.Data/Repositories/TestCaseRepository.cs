using System;
using System.Collections.Generic;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.Repositories;
using STARC.Infra.Data.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Linq;
using STARC.Domain.ViewModels.TestCase;

namespace STARC.Infra.Data.Repositories
{
    public class TestCaseRepository : RepositoryConfiguration, ITestCaseRepository
    {
        public long Add(TestCase testCase)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("TestSuiteId", testCase.TestSuiteId, DbType.Int64);
                    parameters.Add("Name", testCase.Name, DbType.String);
                    parameters.Add("Type", testCase.Type, DbType.String);
                    parameters.Add("Description", testCase.Description, DbType.String);
                    parameters.Add("PreConditions", testCase.PreConditions, DbType.String);
                    parameters.Add("PosConditions", testCase.PosConditions, DbType.String);
                    parameters.Add("ExpectedResult", testCase.ExpectedResult, DbType.String);
                    parameters.Add("Status", testCase.Status, DbType.Boolean);
                    parameters.Add("CreatedBy", testCase.CreatedBy, DbType.Int64);
                    parameters.Add("CreatedDate", testCase.CreatedDate, DbType.DateTime);
                    parameters.Add("LastUpdatedBy", testCase.LastUpdatedBy, DbType.Int64);
                    parameters.Add("LastUpdatedDate", testCase.LastUpdatedDate, DbType.DateTime);

                    var testCaseInserted = connection.Query(
                        sql: "TestCase_Add",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return (testCaseInserted.FirstOrDefault()).TestCaseId;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(TestCase testCase)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("TestCaseId", testCase.TestCaseId, DbType.Int64);
                    parameters.Add("TestSuiteId", testCase.TestSuiteId, DbType.Int64);
                    parameters.Add("Name", testCase.Name, DbType.String);
                    parameters.Add("Type", testCase.Type, DbType.String);
                    parameters.Add("Description", testCase.Description, DbType.String);
                    parameters.Add("PreConditions", testCase.PreConditions, DbType.String);
                    parameters.Add("PosConditions", testCase.PosConditions, DbType.String);
                    parameters.Add("ExpectedResult", testCase.ExpectedResult, DbType.String);
                    parameters.Add("LastUpdatedBy", testCase.LastUpdatedBy, DbType.Int64);
                    parameters.Add("LastUpdatedDate", testCase.LastUpdatedDate, DbType.DateTime);

                    connection.Execute(
                       sql: "TestCase_Update",
                       param: parameters,
                       commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ChangeStatus(TestCase testCase)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("TestCaseId", testCase.TestCaseId, DbType.Int64);
                    parameters.Add("Status", testCase.Status, DbType.Boolean);
                    parameters.Add("LastUpdatedBy", testCase.LastUpdatedBy, DbType.Int64);
                    parameters.Add("LastUpdatedDate", testCase.LastUpdatedDate, DbType.DateTime);

                    connection.Execute(
                       sql: "TestCase_Update",
                       param: parameters,
                       commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<TestCaseToQueryViewModel> GetByTestPlan(long testPlanId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("TestPlanId", testPlanId, DbType.Int64);

                    var testCases = connection.Query<TestCaseToQueryViewModel>(
                        sql: "TestCase_GetByTestPlan",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return testCases;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<TestCaseToQueryViewModel> GetByTestSuite(long testSuiteId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("TestSuiteId", testSuiteId, DbType.Int64);

                    var testCases = connection.Query<TestCaseToQueryViewModel>(
                        sql: "TestCase_GetByTestSuite",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return testCases;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TestCaseToQueryViewModel GetById(long testCaseId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("TestCaseId", testCaseId, DbType.Int64);

                    var testCases = connection.Query<TestCaseToQueryViewModel>(
                        sql: "TestCase_GetAll",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return testCases.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }        
    }
}
