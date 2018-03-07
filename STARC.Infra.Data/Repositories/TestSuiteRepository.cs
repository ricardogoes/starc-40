using System;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.Repositories;
using STARC.Infra.Data.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Linq;
using STARC.Domain.ViewModels.TestSuite;
using System.Collections.Generic;

namespace STARC.Infra.Data.Repositories
{
    public class TestSuiteRepository : RepositoryConfiguration, ITestSuiteRepository
    {
        public long Add(TestSuite testSuite)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("TestPlanId", testSuite.TestPlanId, DbType.Int64);
                    parameters.Add("ParentTestSuiteId", testSuite.ParentTestSuiteId, DbType.Int64);
                    parameters.Add("Name", testSuite.Name, DbType.String);
                    parameters.Add("Description", testSuite.Description, DbType.String);
                    parameters.Add("CreatedBy", testSuite.CreatedBy, DbType.Int64);
                    parameters.Add("CreatedDate", testSuite.CreatedDate, DbType.DateTime);
                    
                    var testSuiteInserted = connection.Query(
                        sql: "TestSuite_Add",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return (testSuiteInserted.FirstOrDefault()).TestSuiteId;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(TestSuite testSuite)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("TestSuiteId", testSuite.TestSuiteId, DbType.Int64);
                    parameters.Add("TestPlanId", testSuite.TestPlanId, DbType.Int64);
                    parameters.Add("ParentTestSuiteId", testSuite.ParentTestSuiteId, DbType.Int64);
                    parameters.Add("Name", testSuite.Name, DbType.String);
                    parameters.Add("Description", testSuite.Description, DbType.String);
                    
                    connection.Query(
                        sql: "TestSuite_Update",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(long testSuiteId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("TestSuiteId", testSuiteId, DbType.Int64);

                    connection.Query(
                        sql: "TestSuite_Delete",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TestSuiteToQueryViewModel GetById(long testSuiteId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("TestSuiteId", testSuiteId, DbType.Int64);

                    var testSuite = connection.Query<TestSuiteToQueryViewModel>(
                        sql: "TestSuite_GetById",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return testSuite.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<TestSuiteToQueryViewModel> GetByTestPlan(long testPlanId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("TestPlanId", testPlanId, DbType.Int64);

                    var testSuites = connection.Query<TestSuiteToQueryViewModel>(
                        sql: "TestSuite_GetByTestPlan",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return testSuites;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<TestSuiteToQueryViewModel> GetByParentTestSuite(long parentTestSuiteId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("ParentTestSuiteId", parentTestSuiteId, DbType.Int64);

                    var testSuites = connection.Query<TestSuiteToQueryViewModel>(
                        sql: "TestSuite_GetByParentTestSuite",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return testSuites;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
