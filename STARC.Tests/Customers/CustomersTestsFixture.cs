using AutoMoq;
using Bogus;
using Bogus.DataSets;
using Moq;
using STARC.Api.Controllers;
using STARC.Application;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.Interfaces.Repositories;
using STARC.Domain.ViewModels.Customers;
using STARC.Infra.CrossCutting.AutoMapper;
using STARC.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace STARC.Tests.Customers
{
    [CollectionDefinition(nameof(CustomersCollection))]
    public class CustomersCollection : ICollectionFixture<CustomersTestsFixture>
    {
    }

    public class CustomersTestsFixture
    {
        public Mock<ICustomerRepository> CustomerRepositoryMock { get; set; }
        public Mock<ICustomerAppService> CustomerAppServiceMock { get; set; }
        public Mock<IUserAppService> UserAppServiceMock { get; set; }

        public CustomerAppService GetCustomerAppService()
        {
            var mocker = new AutoMoqer();
            mocker.Create<CustomerAppService>();

            var customerAppService = mocker.Resolve<CustomerAppService>();

            CustomerRepositoryMock = mocker.GetMock<ICustomerRepository>();    

            return customerAppService;
        }

        public CustomersController GetCustomersController()
        {
            var mocker = new AutoMoqer();
            mocker.Create<CustomersController>();

            MappingConfiguration.Configure();
            var customerController = mocker.Resolve<CustomersController>();

            CustomerAppServiceMock = mocker.GetMock<ICustomerAppService>();
            UserAppServiceMock = mocker.GetMock<IUserAppService>();

            return customerController;
        }

        private static IEnumerable<CustomerToQueryViewModel> GenerateCustomerToQuery(int number, bool isActive)
        {
            Random random = new Random();

            var customerTests = new Faker<CustomerToQueryViewModel>("pt-BR")
                .CustomInstantiator(f => new CustomerToQueryViewModel
                {
                    CustomerId = random.Next(1, 1000),
                    Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                    Address = f.Address.StreetAddress(true),
                    DocumentId = CpfCnpjGenerator.GenerateCnpj(),
                    Status = isActive,
                    CreatedBy = 1,
                    CreatedName = "Administrador Sistema",
                    CreatedDate = f.Date.Between(DateTime.Now.AddMonths(-3), DateTime.Now),
                    LastUpdatedBy = 1,
                    LastUpdatedName = "Administrador Sistema",
                    LastUpdatedDate = f.Date.Between(DateTime.Now.AddMonths(-3), DateTime.Now)
                });

            return customerTests.Generate(number);
        }

        public IEnumerable<CustomerToQueryViewModel> GetMixedCustomers()
        {
            var customers = new List<CustomerToQueryViewModel>();

            customers.AddRange(GenerateCustomerToQuery(50, true).ToList());
            customers.AddRange(GenerateCustomerToQuery(50, false).ToList());

            return customers;
        }

        public IEnumerable<CustomerToQueryViewModel> GetActiveCustomers()
        {
            var customers = new List<CustomerToQueryViewModel>();

            customers.AddRange(GenerateCustomerToQuery(50, true).ToList());            

            return customers;
        }

        public Customer GenerateValidCustomer()
        {
            var customerTests = new Faker<Customer>("pt-BR")
                .CustomInstantiator(f => new Customer
                {
                    CustomerId = 1,
                    Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                    Address = f.Address.StreetAddress(true),
                    DocumentId = "34538886858",
                    Status = true,
                    CreatedBy = 1,
                    CreatedDate = f.Date.Between(DateTime.Now.AddMonths(-3), DateTime.Now),
                    LastUpdatedBy = 1,
                    LastUpdatedDate = f.Date.Between(DateTime.Now.AddMonths(-3), DateTime.Now)
                });

            return customerTests.Generate(1).FirstOrDefault();
        }

        public CustomerToQueryViewModel GenerateValidCustomerToQuery()
        {
            var customerTests = new Faker<CustomerToQueryViewModel>("pt-BR")
                .CustomInstantiator(f => new CustomerToQueryViewModel
                {
                    CustomerId = 1,
                    Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                    Address = f.Address.StreetAddress(true),
                    DocumentId = "34538886858",
                    Status = true,
                    CreatedBy = 1,
                    CreatedDate = f.Date.Between(DateTime.Now.AddMonths(-3), DateTime.Now),
                    LastUpdatedBy = 1,
                    LastUpdatedDate = f.Date.Between(DateTime.Now.AddMonths(-3), DateTime.Now)
                });

            return customerTests.Generate(1).FirstOrDefault();
        }

        public CustomerToInsertViewModel GenerateValidCustomerToInsert()
        {
            var customerTests = new Faker<CustomerToInsertViewModel>("pt-BR")
                .CustomInstantiator(f => new CustomerToInsertViewModel
                {
                    Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                    Address = f.Address.StreetAddress(true),
                    DocumentId = "34538886858"                    
                });

            return customerTests.Generate(1).FirstOrDefault();
        }

        public CustomerToInsertViewModel GenerateValidCustomerToInsertWithoutName()
        {
            var customerTests = new Faker<CustomerToInsertViewModel>("pt-BR")
                .CustomInstantiator(f => new CustomerToInsertViewModel
                {
                    Address = f.Address.StreetAddress(true),
                    DocumentId = "34538886858"
                });

            return customerTests.Generate(1).FirstOrDefault();
        }

        public CustomerToUpdateViewModel GenerateValidCustomerToUpdate()
        {
            var customerTests = new Faker<CustomerToUpdateViewModel>("pt-BR")
                .CustomInstantiator(f => new CustomerToUpdateViewModel
                {
                    CustomerId = 1,
                    Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                    Address = f.Address.StreetAddress(true),
                    DocumentId = "34538886858"
                });

            return customerTests.Generate(1).FirstOrDefault();
        }

        public Customer GenerateCustomerWithDocumentIdInvalid()
        {
            var customerTests = new Faker<Customer>("pt-BR")
                .CustomInstantiator(f => new Customer
                {
                    CustomerId = 1,
                    Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                    Address = f.Address.StreetAddress(true),
                    DocumentId = "34538886858123",
                    Status = true,
                    CreatedBy = 1,
                    CreatedDate = f.Date.Between(DateTime.Now.AddMonths(-3), DateTime.Now),
                    LastUpdatedBy = 1,
                    LastUpdatedDate = f.Date.Between(DateTime.Now.AddMonths(-3), DateTime.Now)
                });

            return customerTests.Generate(1).FirstOrDefault();
        }

        public Customer GenerateCustomerWithDocumentIdDuplicated()
        {
            var customerTests = new Faker<Customer>("pt-BR")
                .CustomInstantiator(f => new Customer
                {
                    Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                    Address = f.Address.StreetAddress(true),
                    DocumentId = "34538886858",
                    Status = true,
                    CreatedBy = 1,
                    CreatedDate = f.Date.Between(DateTime.Now.AddMonths(-3), DateTime.Now),
                    LastUpdatedBy = 1,
                    LastUpdatedDate = f.Date.Between(DateTime.Now.AddMonths(-3), DateTime.Now)
                });

            return customerTests.Generate(1).FirstOrDefault();
        }
    }
}
