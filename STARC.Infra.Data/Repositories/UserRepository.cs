using Dapper;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.Repositories;
using STARC.Domain.ViewModels.Users;
using STARC.Infra.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace STARC.Infra.Data.Repositories
{
    public class UserRepository : RepositoryConfiguration, IUserRepository
    {
        public long Add(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                     connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("FirstName", user.FirstName, DbType.String);
                    parameters.Add("LastName", user.LastName, DbType.String);
                    parameters.Add("Username", user.Username, DbType.String);
                    parameters.Add("Password", user.Password, DbType.String);
                    parameters.Add("PasswordHash", user.PasswordHash, DbType.Binary);
                    parameters.Add("Email", user.Email, DbType.String);
                    parameters.Add("UserProfileId", user.UserProfileId, DbType.Int32);
                    parameters.Add("CustomerId", user.CustomerId, DbType.Int64);
                    parameters.Add("Status", user.Status, DbType.Boolean);
                    parameters.Add("CreatedBy", user.CreatedBy, DbType.Int64);
                    parameters.Add("CreatedDate", user.CreatedDate, DbType.DateTime);
                    parameters.Add("LastUpdatedBy", user.LastUpdatedBy, DbType.Int64);
                    parameters.Add("LastUpdatedDate", user.LastUpdatedDate, DbType.DateTime);

                    var userInserted = connection.Query(
                        sql: "User_Add",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return (userInserted.FirstOrDefault()).UserId;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                     connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("UserId", user.UserId, DbType.Int64);
                    parameters.Add("FirstName", user.FirstName, DbType.String);
                    parameters.Add("LastName", user.LastName, DbType.String);
                    parameters.Add("Username", user.Username, DbType.String);
                    parameters.Add("Email", user.Email, DbType.String);
                    parameters.Add("UserProfileId", user.UserProfileId, DbType.Int32);
                    parameters.Add("CustomerId", user.CustomerId, DbType.Int64);
                    parameters.Add("LastUpdatedBy", user.LastUpdatedBy, DbType.Int64);
                    parameters.Add("LastUpdatedDate", user.LastUpdatedDate, DbType.DateTime);

                    connection.Execute(
                        sql: "User_Update",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ChangeStatus(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                     connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("UserId", user.UserId, DbType.Int64);
                    parameters.Add("Status", user.Status, DbType.Boolean);
                    parameters.Add("LastUpdatedBy", user.LastUpdatedBy, DbType.Int64);
                    parameters.Add("LastUpdatedDate", user.LastUpdatedDate, DbType.DateTime);

                    connection.Execute(
                        sql: "User_ChangeStatus",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<UserToQueryViewModel> GetByCustomer(long customerId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                     connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("CustomerId", customerId, DbType.Int64);

                    var users = connection.Query<UserToQueryViewModel>(
                        sql: "User_GetByCustomer",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return users;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserToQueryViewModel GetById(long userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                     connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("UserId", userId, DbType.Int64);

                    var user = connection.Query<UserToQueryViewModel>(
                        sql: "User_GetById",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return user.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserToQueryViewModel GetByUsernameAndPassword(string username, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                     connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("Username", username, DbType.String);
                    parameters.Add("Password", password, DbType.String);

                    var user = connection.Query<UserToQueryViewModel>(
                        sql: "User_GetByUsernameAndPassword",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return user.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserToQueryViewModel GetByUsername(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("Username", username, DbType.String);

                    var user = connection.Query<UserToQueryViewModel>(
                        sql: "User_GetByUsername",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return user.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<UserToQueryViewModel> GetByNotInProject(long projectId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("ProjectId", projectId, DbType.Int64);

                    var users = connection.Query<UserToQueryViewModel>(
                        sql: "User_GetByNotInProject",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return users;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }        
    }
}
