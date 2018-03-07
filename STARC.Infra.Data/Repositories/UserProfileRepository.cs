using Dapper;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.Repositories;
using STARC.Infra.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace STARC.Infra.Data.Repositories
{
    public class UserProfileRepository : RepositoryConfiguration, IUserProfileRepository
    {
        public IEnumerable<UserProfile> GetAll()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var profiles = connection.Query<UserProfile>(
                        sql: "UserProfile_GetAll",
                        commandType: CommandType.StoredProcedure);

                    return profiles;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserProfile GetById(int userProfileId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("UserProfileId", userProfileId, DbType.Int64);

                    var profile = connection.Query<UserProfile>(
                        sql: "UserProfile_GetById",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return profile.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
