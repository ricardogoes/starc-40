using Dapper;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.Repositories;
using STARC.Domain.ViewModels.TestPlan;
using STARC.Infra.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace STARC.Infra.Data.Repositories
{
    public class TestPlanRepository : RepositoryConfiguration, ITestPlanRepository
    {
        public long Add(TestPlan testPlan)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("Name", testPlan.Name, DbType.String);
                    parameters.Add("Description", testPlan.Description, DbType.String);
                    parameters.Add("StartDate", testPlan.StartDate, DbType.DateTime);
                    parameters.Add("FinishDate", testPlan.FinishDate, DbType.DateTime);
                    parameters.Add("OwnerId", testPlan.OwnerId, DbType.Int64);
                    parameters.Add("ProjectId", testPlan.ProjectId, DbType.Int64);
                    parameters.Add("Status", testPlan.Status, DbType.Boolean);
                    parameters.Add("CreatedBy", testPlan.CreatedBy, DbType.Int64);
                    parameters.Add("CreatedDate", testPlan.CreatedDate, DbType.DateTime);
                    parameters.Add("LastUpdatedBy", testPlan.LastUpdatedBy, DbType.Int64);
                    parameters.Add("LastUpdatedDate", testPlan.LastUpdatedDate, DbType.DateTime);

                    var testPlanInserted = connection.Query(
                        sql: "TestPlan_Add",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return (testPlanInserted.FirstOrDefault()).TestPlanId;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(TestPlan testPlan)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("TestPlanId", testPlan.TestPlanId, DbType.Int64);
                    parameters.Add("Name", testPlan.Name, DbType.String);
                    parameters.Add("Description", testPlan.Description, DbType.String);
                    parameters.Add("StartDate", testPlan.StartDate, DbType.DateTime);
                    parameters.Add("FinishDate", testPlan.FinishDate, DbType.DateTime);
                    parameters.Add("OwnerId", testPlan.OwnerId, DbType.Int64);
                    parameters.Add("ProjectId", testPlan.ProjectId, DbType.Int64);
                    parameters.Add("LastUpdatedBy", testPlan.LastUpdatedBy, DbType.Int64);
                    parameters.Add("LastUpdatedDate", testPlan.LastUpdatedDate, DbType.DateTime);

                    connection.Execute(
                       sql: "TestPlan_Update",
                       param: parameters,
                       commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ChangeStatus(TestPlan testPlan)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("TestPlanId", testPlan.TestPlanId, DbType.Int64);
                    parameters.Add("Status", testPlan.Status, DbType.Boolean);
                    parameters.Add("LastUpdatedBy", testPlan.LastUpdatedBy, DbType.Int64);
                    parameters.Add("LastUpdatedDate", testPlan.LastUpdatedDate, DbType.DateTime);

                    connection.Execute(
                       sql: "TestPlan_ChangeStatus",
                       param: parameters,
                       commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<TestPlanToQueryViewModel> GetByProject(long projectId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("ProjectId", projectId, DbType.Int64);

                    var testPlans = connection.Query<TestPlanToQueryViewModel>(
                        sql: "TestPlan_GetByProject",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return testPlans;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TestPlanToQueryViewModel GetById(long testPlanId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("TestPlanId", testPlanId, DbType.Int64);

                    var testPlan = connection.Query<TestPlanToQueryViewModel>(
                        sql: "TestPlan_GetById",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return testPlan.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<TestPlanToQueryViewModel> GetActiveByProject(long projectId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("ProjectId", projectId, DbType.Int64);

                    var testPlans = connection.Query<TestPlanToQueryViewModel>(
                        sql: "TestPlan_GetActiveByProject",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return testPlans;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<TestPlanStructureViewModel> GetStructure(long testPlanId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("TestPlanId", testPlanId, DbType.Int64);

                    var structure = connection.Query<TestPlanStructureViewModel>(
                        sql: "TestPlan_GetStructure",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return structure;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
