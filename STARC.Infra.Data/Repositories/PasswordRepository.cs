using Dapper;
using STARC.Domain.Interfaces.Repositories;
using STARC.Infra.Data.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace STARC.Infra.Data.Repositories
{
    public class PasswordRepository : RepositoryConfiguration, IPasswordRepository
    {
        public byte[] GetHashPassword(long userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("UserId", userId, DbType.Int64);

                    var hash = connection.Query<byte[]>(
                        sql: "Password_GetHashPassword",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return hash.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
