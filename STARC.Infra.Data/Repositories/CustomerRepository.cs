using Dapper;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.Repositories;
using STARC.Domain.ViewModels.Customers;
using STARC.Infra.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace STARC.Infra.Data.Repositories
{
    public class CustomerRepository : RepositoryConfiguration, ICustomerRepository
    {
        public long Add(Customer customer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("Name", customer.Name, DbType.String);
                    parameters.Add("DocumentId", customer.DocumentId, DbType.String);
                    parameters.Add("Address", customer.Address, DbType.String);
                    parameters.Add("Status", customer.Status, DbType.Boolean);
                    parameters.Add("CreatedBy", customer.CreatedBy, DbType.Int64);
                    parameters.Add("CreatedDate", customer.CreatedDate, DbType.DateTime);
                    parameters.Add("LastUpdatedBy", customer.LastUpdatedBy, DbType.Int64);
                    parameters.Add("LastUpdatedDate", customer.LastUpdatedDate, DbType.DateTime);

                    var customerInserted = connection.Query(
                        sql: "Customer_Add",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return (customerInserted.FirstOrDefault()).CustomerId;
                }                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Customer customer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                     connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("CustomerId", customer.CustomerId, DbType.Int64);
                    parameters.Add("Name", customer.Name, DbType.String);
                    parameters.Add("DocumentId", customer.DocumentId, DbType.String);
                    parameters.Add("Address", customer.Address, DbType.String);
                    parameters.Add("LastUpdatedBy", customer.LastUpdatedBy, DbType.Int64);
                    parameters.Add("LastUpdatedDate", customer.LastUpdatedDate, DbType.DateTime);

                     connection.Execute(
                        sql: "Customer_Update",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ChangeStatus(Customer customer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                     connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("CustomerId", customer.CustomerId, DbType.Int64);
                    parameters.Add("Status", customer.Status, DbType.Boolean);
                    parameters.Add("LastUpdatedBy", customer.LastUpdatedBy, DbType.Int64);
                    parameters.Add("LastUpdatedDate", customer.LastUpdatedDate, DbType.DateTime);

                     connection.Execute(
                        sql: "Customer_ChangeStatus",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<CustomerToQueryViewModel> GetAll()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                     connection.Open();

                    var customers = connection.Query<CustomerToQueryViewModel>(
                        sql: "Customer_GetAll",
                        commandType: CommandType.StoredProcedure);

                    return  customers;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CustomerToQueryViewModel GetById(long customerId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                     connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("CustomerId", customerId, DbType.Int64);

                    var customer = connection.Query<CustomerToQueryViewModel>(
                        sql: "Customer_GetById",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return customer.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<CustomerToQueryViewModel> GetActive()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var customers = connection.Query<CustomerToQueryViewModel>(
                        sql: "Customer_GetActive",
                        commandType: CommandType.StoredProcedure);

                    return customers;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CustomerToQueryViewModel GetByDocumentId(string documentId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(__connectionString))
                {
                    connection.Open();

                    var parameters = new DynamicParameters();
                    parameters.Add("DocumentId", documentId, DbType.String);

                    var customer = connection.Query<CustomerToQueryViewModel>(
                        sql: "Customer_GetByDocumentId",
                        param: parameters,
                        commandType: CommandType.StoredProcedure);

                    return customer.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
