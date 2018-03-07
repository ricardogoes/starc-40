using Dapper;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.Repositories;
using STARC.Domain.ViewModels.Projects;
using STARC.Infra.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace STARC.Infra.Data.Repositories
{
    public class ProjectRepository : RepositoryConfiguration, IProjectRepository
    {
        public  long Add(Project project)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                     connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("Name", project.Name, DbType.String);
                    parameters.Add("Description", project.Description, DbType.String);
                    parameters.Add("StartDate", project.StartDate, DbType.DateTime);
                    parameters.Add("FinishDate", project.FinishDate, DbType.DateTime);
                    parameters.Add("OwnerId", project.OwnerId, DbType.Int64);
                    parameters.Add("CustomerId", project.CustomerId, DbType.Int64);
                    parameters.Add("Status", project.Status, DbType.Boolean);
                    parameters.Add("CreatedBy", project.CreatedBy, DbType.Int64);
                    parameters.Add("CreatedDate", project.CreatedDate, DbType.DateTime);
                    parameters.Add("LastUpdatedBy", project.LastUpdatedBy, DbType.Int64);
                    parameters.Add("LastUpdatedDate", project.LastUpdatedDate, DbType.DateTime);

                    var projectInserted = connection.Query(
                        sql: "Project_Add",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return (projectInserted.FirstOrDefault()).ProjectId;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public  void Update(Project project)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                     connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("ProjectId", project.ProjectId, DbType.Int64);
                    parameters.Add("Name", project.Name, DbType.String);
                    parameters.Add("Description", project.Description, DbType.String);
                    parameters.Add("StartDate", project.StartDate, DbType.DateTime);
                    parameters.Add("FinishDate", project.FinishDate, DbType.DateTime);
                    parameters.Add("OwnerId", project.OwnerId, DbType.Int64);
                    parameters.Add("CustomerId", project.CustomerId, DbType.Int64);
                    parameters.Add("LastUpdatedBy", project.LastUpdatedBy, DbType.Int64);
                    parameters.Add("LastUpdatedDate", project.LastUpdatedDate, DbType.DateTime);

                     connection.Execute(
                        sql: "Project_Update",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public  void ChangeStatus(Project project)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                     connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("ProjectId", project.ProjectId, DbType.Int64);
                    parameters.Add("Status", project.Status, DbType.Boolean);
                    parameters.Add("LastUpdatedBy", project.LastUpdatedBy, DbType.Int64);
                    parameters.Add("LastUpdatedDate", project.LastUpdatedDate, DbType.DateTime);

                     connection.Execute(
                        sql: "Project_ChangeStatus",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ProjectToQueryViewModel> GetByCustomer(long customerId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("CustomerId", customerId, DbType.Int64);

                    var projects = connection.Query<ProjectToQueryViewModel>(
                        sql: "Project_GetByCustomer",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return projects;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProjectToQueryViewModel GetById(long projectId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                     connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("ProjectId", projectId, DbType.Int64);

                    var project = connection.Query<ProjectToQueryViewModel>(
                        sql: "Project_GetById",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return project.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ProjectToQueryViewModel> GetActiveByCustomer(long customerId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("CustomerId", customerId, DbType.Int64);

                    var projects = connection.Query<ProjectToQueryViewModel>(
                        sql: "Project_GetActiveByCustomer",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return projects;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }        
    }
}
