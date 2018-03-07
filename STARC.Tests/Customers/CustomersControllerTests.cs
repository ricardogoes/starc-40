using STARC.Infra.CrossCutting.AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using STARC.Api.Models;
using STARC.Domain.Entities;
using STARC.Domain.ViewModels.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using STARC.Tests.Users;
using STARC.Domain.Models;

namespace STARC.Tests.Customers
{
    [Collection(nameof(CustomersCollection))]
    public class CustomersControllerTests
    {
        public CustomersTestsFixture Fixture { get; set; }

        public CustomersControllerTests(CustomersTestsFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact(DisplayName = "Should get all customers")]
        [Trait("Category", "Customers Controller Tests")]
        public void CustomersController_GetAllToGrid_ShouldReturnsAllCustomers()
        {
            // Arrange
            var controller = Fixture.GetCustomersController();
            Fixture.CustomerAppServiceMock.Setup(c => c.GetAll()).Returns(Fixture.GetMixedCustomers());

            // Act
            var response = controller.GetAll() as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var customers = data.Data as IEnumerable<CustomerToQueryViewModel>;
            customers.Should().HaveCount(c => c == 100);
        }

        [Fact(DisplayName = "Should get all active customers")]
        [Trait("Category", "Customers Controller Tests")]
        public void CustomersController_GetActive_ShouldReturnsActiveCustomers()
        {
            // Arrange
            var controller = Fixture.GetCustomersController();
            Fixture.CustomerAppServiceMock.Setup(c => c.GetActive()).Returns(Fixture.GetActiveCustomers());

            // Act
            var response = controller.GetActive() as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var customers = data.Data as IEnumerable<CustomerToQueryViewModel>;
            customers.Should().HaveCount(c => c == 50);
            customers.Should().NotContain(c => !c.Status);
        }

        [Fact(DisplayName = "Should get customer by customerId")]
        [Trait("Category", "Customers Controller Tests")]
        public void CustomersController_GetById_ShouldReturnCustomer()
        {
            // Arrange
            var controller = Fixture.GetCustomersController();
            Fixture.CustomerAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidCustomerToQuery());

            // Act
            var response = controller.Get(1) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var customer = data.Data as CustomerToQueryViewModel;
            customer.Should().NotBeNull();
            customer.CustomerId.Should().Be(1);
        }

        [Fact(DisplayName = "Should not get customer by invalid customerId")]
        [Trait("Category", "Customers Controller Tests")]
        public void CustomersController_GetById_ShouldNotReturnCustomer()
        {
            // Arrange
            var controller = Fixture.GetCustomersController();
            Fixture.CustomerAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidCustomerToQuery());

            // Act
            var response = controller.Get(2) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Failed);
            data.Message.Should().Be("Customer not Found");
        }

        [Fact(DisplayName = "Should insert new customer")]
        [Trait("Category", "Customers Controller Tests")]
        public void CustomersController_Insert_ShouldAddCustomer()
        {
            var UserFixture = new UsersTestsFixture();
            // Arrange
            var controller = Fixture.GetCustomersController();
            var customerToInsert = Fixture.GenerateValidCustomerToInsert();

            Fixture.CustomerAppServiceMock.Setup(c => c.Add(It.IsAny<Customer>())).Returns(2);
            Fixture.CustomerAppServiceMock.Setup(c => c.IsValid(It.IsAny<Customer>()))
                .Returns(new EntityValidationResult
                {
                    Status = true
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());
            
            // Act
            var response = controller.Insert(customerToInsert) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(201);
        }

        [Fact(DisplayName = "Should not insert when customer is null")]
        [Trait("Category", "Customers Controller Tests")]
        public void CustomersController_Insert_ShouldReturnBadRequestCustomerNull()
        {
            // Arrange
            var controller = Fixture.GetCustomersController();
            
            // Act
            var response = controller.Insert(null) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Customer is null");
        }

        [Fact(DisplayName = "Should not insert when customer is not valid")]
        [Trait("Category", "Customers Controller Tests")]
        public void CustomersController_Insert_ShouldReturnBadRequestNotValid()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetCustomersController();
            var customerToInsert = Fixture.GenerateValidCustomerToInsert();

            Fixture.CustomerAppServiceMock.Setup(c => c.Add(It.IsAny<Customer>())).Returns(2);
            Fixture.CustomerAppServiceMock.Setup(c => c.IsValid(It.IsAny<Customer>()))
                .Returns(new EntityValidationResult
                {
                    Status = false
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Insert(customerToInsert) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Validation failed, please see details");
        }

        [Fact(DisplayName = "Should update customer")]
        [Trait("Category", "Customers Controller Tests")]
        public void CustomersController_Update_ShouldUpdateCustomer()
        {
            var UserFixture = new UsersTestsFixture();

            // Arrange
            var controller = Fixture.GetCustomersController();
            var customerToUpdate = Fixture.GenerateValidCustomerToUpdate();

            Fixture.CustomerAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidCustomerToQuery());
            Fixture.CustomerAppServiceMock.Setup(c => c.IsValid(It.IsAny<Customer>()))
                .Returns(new EntityValidationResult
                {
                    Status = true
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Update(1, customerToUpdate) as NoContentResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(204);
        }

        [Fact(DisplayName = "Should not update when customerId <> id")]
        [Trait("Category", "Customers Controller Tests")]
        public void CustomersController_Update_ShouldReturnBadRequestInvalidRequest()
        {
            // Arrange
            var controller = Fixture.GetCustomersController();
            var customerToUpdate = Fixture.GenerateValidCustomerToUpdate();

            // Act
            var response = controller.Update(2, customerToUpdate) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Invalid request");
        }

        [Fact(DisplayName = "Should not update when customer is null")]
        [Trait("Category", "Customers Controller Tests")]
        public void CustomersController_Update_ShouldReturnBadRequestInvalidRequestCustomerNull()
        {
            // Arrange
            var controller = Fixture.GetCustomersController();
            
            // Act
            var response = controller.Update(2, null) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Invalid request");
        }

        [Fact(DisplayName = "Should not update when customer not found")]
        [Trait("Category", "Customers Controller Tests")]
        public void CustomersController_Update_ShouldReturnCustomerNotFound()
        {
            
            // Arrange
            var controller = Fixture.GetCustomersController();
            var customerToUpdate = Fixture.GenerateValidCustomerToUpdate();
            Fixture.CustomerAppServiceMock.Setup(c => c.GetById(2)).Returns(Fixture.GenerateValidCustomerToQuery());

            // Act
            var response = controller.Update(1, customerToUpdate) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(404);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Customer Not Found");
        }

        [Fact(DisplayName = "Should not update when customer is not valid")]
        [Trait("Category", "Customers Controller Tests")]
        public void CustomersController_Update_ShouldReturnBadRequestNotValid()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetCustomersController();
            var customerToUpdate = Fixture.GenerateValidCustomerToUpdate();

            Fixture.CustomerAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidCustomerToQuery());
            Fixture.CustomerAppServiceMock.Setup(c => c.IsValid(It.IsAny<Customer>()))
                .Returns(new EntityValidationResult
                {
                    Status = false
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Update(1, customerToUpdate) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Validation failed, please see details");
        }

        [Fact(DisplayName = "Should change customer status")]
        [Trait("Category", "Customers Controller Tests")]
        public void CustomersController_ChangeStatus_ShouldUpdateCustomer()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetCustomersController();

            Fixture.CustomerAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidCustomerToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.ChangeStatus(1) as NoContentResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(204);
        }

        [Fact(DisplayName = "Should not change status when customer not found")]
        [Trait("Category", "Customers Controller Tests")]
        public void CustomersController_ChangeStatus_ShouldReturnNotFound()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetCustomersController();

            Fixture.CustomerAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidCustomerToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.ChangeStatus(2) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(404);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Customer Not Found");
        }
    }
}
