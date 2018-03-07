using Dapper;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.Repositories;
using STARC.Domain.ViewModels.UsersInProjects;
using STARC.Infra.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace STARC.Infra.Data.Repositories
{
    public class UsersInProjectsRepository : RepositoryConfiguration, IUsersInProjectsRepository
    {
        public long Add(UsersInProjects userInProject)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("UserId", userInProject.UserId, DbType.Int64);
                    parameters.Add("ProjectId", userInProject.ProjectId, DbType.Int64);
                    parameters.Add("CreatedBy", userInProject.CreatedBy, DbType.Int64);
                    parameters.Add("CreatedDate", userInProject.CreatedDate, DbType.DateTime);

                    var userInProjectInserted = connection.Query(
                        sql: "UsersInProjects_Add",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return (userInProjectInserted.FirstOrDefault()).UserInProjectId;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(long id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("UserInProjectId", id, DbType.Int64);

                    connection.Execute(
                        sql: "UsersInProjects_Delete",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UsersInProjectsToQueryViewModel GetById(long id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("UserInProjectId", id, DbType.Int64);

                    var userInProject = connection.Query<UsersInProjectsToQueryViewModel>(
                                            sql: "UsersInProjects_GetById",
                                            param: parameters,
                                            commandType: CommandType.StoredProcedure);

                    return userInProject.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<UsersInProjectsToQueryViewModel> GetByCustomer(long customerId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("CustomerId", customerId, DbType.Int64);

                    var usersInProjects = connection.Query<UsersInProjectsToQueryViewModel>(
                        sql: "UsersInProjects_GetByCustomer",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return usersInProjects;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<UsersInProjectsToQueryViewModel> GetByProject(long projectId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("ProjectId", projectId, DbType.Int64);

                    var usersInProjects = connection.Query<UsersInProjectsToQueryViewModel>(
                        sql: "UsersInProjects_GetByProject",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return usersInProjects;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<UsersInProjectsToQueryViewModel> GetByUser(long userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("UserId", userId, DbType.Int64);

                    var usersInProjects = connection.Query<UsersInProjectsToQueryViewModel>(
                        sql: "UsersInProjects_GetByUser",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return usersInProjects;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UsersInProjectsToQueryViewModel GetByUserAndProject(long userId, long projectId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("UserId", userId, DbType.Int64);
                    parameters.Add("ProjectId", projectId, DbType.Int64);

                    var userInProject = connection.Query<UsersInProjectsToQueryViewModel>(
                                            sql: "UsersInProjects_GetByUserAndProject",
                                            param: parameters,
                                            commandType: CommandType.StoredProcedure);

                    return userInProject.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
