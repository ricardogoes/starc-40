using FluentAssertions;
using STARC.Domain.Models;
using System;
using System.Linq;
using Xunit;

namespace STARC.Tests.Customers
{
    [Collection(nameof(CustomersCollection))]
    public class CustomersApplicationTests
    {
        public CustomersTestsFixture Fixture { get; set; }

        public CustomersApplicationTests(CustomersTestsFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact(DisplayName = "Should get all customers")]
        [Trait("Category", "Customers Application Tests")]
        public void CustomerApplication_GetAll_ShouldReturnsAllCustomers()
        {
            // Arrange
            var customerApp = Fixture.GetCustomerAppService();
            Fixture.CustomerRepositoryMock.Setup(c => c.GetAll()).Returns(Fixture.GetMixedCustomers());

            // Act
            var customers = customerApp.GetAll().ToList();

            // Assert Fluent Assertions
            customers.Should().HaveCount(c => c > 1).And.OnlyHaveUniqueItems();
        }

        [Fact(DisplayName = "Should get all active customers")]
        [Trait("Category", "Customers Application Tests")]
        public void CustomerApplication_GetActive_ShouldReturnsOnlyActiveCustomers()
        {
            // Arrange
            var customerApp = Fixture.GetCustomerAppService();
            Fixture.CustomerRepositoryMock.Setup(c => c.GetActive()).Returns(Fixture.GetActiveCustomers());

            // Act
            var customers = customerApp.GetActive().ToList();

            // Assert Fluent Assertions
            customers.Should().HaveCount(c => c > 1).And.OnlyHaveUniqueItems();
            customers.Should().NotContain(c => !c.Status);
        }

        [Fact(DisplayName = "Should get customer by customerId")]
        [Trait("Category", "Customers Application Tests")]
        public void CustomerApplication_GetById_ShouldReturnCustomer()
        {
            // Arrange
            var customerApp = Fixture.GetCustomerAppService();
            Fixture.CustomerRepositoryMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidCustomerToQuery());

            // Act
            var customer = customerApp.GetById(1);

            // Assert Fluent Assertions
            customer.CustomerId.Should().Be(1);            
        }

        [Fact(DisplayName = "Should not get customer by invalid customerId")]
        [Trait("Category", "Customers Application Tests")]
        public void CustomerApplication_GetById_ShouldNotReturnCustomer()
        {
            // Arrange
            var customerApp = Fixture.GetCustomerAppService();
            Fixture.CustomerRepositoryMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidCustomerToQuery());

            // Act
            var customer = customerApp.GetById(2);

            // Assert Fluent Assertions
            customer.Should().BeNull();
        }

        [Fact(DisplayName = "Should get customer by documentId")]
        [Trait("Category", "Customers Application Tests")]
        public void CustomerApplication_GetByDocumentId_ShouldReturnCustomer()
        {
            // Arrange
            var customerApp = Fixture.GetCustomerAppService();
            Fixture.CustomerRepositoryMock.Setup(c => c.GetByDocumentId("34538886858")).Returns(Fixture.GenerateValidCustomerToQuery());

            // Act
            var customer = customerApp.GetByDocumentId("34538886858");

            // Assert Fluent Assertions
            customer.Should().NotBeNull();
            customer.DocumentId.Should().Be("34538886858");

        }

        [Fact(DisplayName = "Should not get customer by invalid documentId")]
        [Trait("Category", "Customers Application Tests")]
        public void CustomerApplication_GetByDocumentId_ShouldNotReturnCustomer()
        {
            // Arrange
            var customerApp = Fixture.GetCustomerAppService();
            Fixture.CustomerRepositoryMock.Setup(c => c.GetByDocumentId("34538886858")).Returns(Fixture.GenerateValidCustomerToQuery());

            // Act
            var customer = customerApp.GetByDocumentId("30663649846");

            // Assert Fluent Assertions
            customer.Should().BeNull();
        }

        [Fact(DisplayName = "Should not get customer by blank documentId")]
        [Trait("Category", "Customers Application Tests")]
        public void CustomerApplication_GetByDocumentIdBlank_ShouldThrowsArgumentException()
        {
            // Arrange
            var customerApp = Fixture.GetCustomerAppService();
            
            // Act
            Exception ex = Assert.Throws<ArgumentException>(() => customerApp.GetByDocumentId(string.Empty));

            // Assert Fluent Assertions
            ex.Message.Should().Be("documentId invalid");
        }

        [Fact(DisplayName = "Should documentId not be unique with duplicated data")]
        [Trait("Category", "Customers Application Tests")]
        public void CustomerApplication_IsDocumentIdUnique_ShouldReturnFalse()
        {
            // Arrange
            var customerApp = Fixture.GetCustomerAppService();
            Fixture.CustomerRepositoryMock.Setup(c => c.GetByDocumentId("34538886858")).Returns(Fixture.GenerateValidCustomerToQuery());

            // Act
            var isDocumentIdUnique = customerApp.IsDocumentIdUnique("34538886858");

            // Assert Fluent Assertions
            isDocumentIdUnique.Should().BeFalse();
        }

        [Fact(DisplayName = "Should documentId be unique")]
        [Trait("Category", "Customers Application Tests")]
        public void CustomerApplication_IsDocumentIdUnique_ShouldReturnTrue()
        {
            // Arrange
            var customerApp = Fixture.GetCustomerAppService();
            Fixture.CustomerRepositoryMock.Setup(c => c.GetByDocumentId("34538886858")).Returns(Fixture.GenerateValidCustomerToQuery());

            // Act
            var isDocumentIdUnique = customerApp.IsDocumentIdUnique("30663649846");

            // Assert Fluent Assertions
            isDocumentIdUnique.Should().BeTrue();
        }

        [Fact(DisplayName = "Should documentId not be unique with blank documentId")]
        [Trait("Category", "Customers Application Tests")]
        public void CustomerApplication_IsDocumentIdUniqueBlank_ShouldThrowsArgumentException()
        {
            // Arrange
            var customerApp = Fixture.GetCustomerAppService();

            // Act
            Exception ex = Assert.Throws<ArgumentException>(() => customerApp.IsDocumentIdUnique(string.Empty));

            // Assert Fluent Assertions
            ex.Message.Should().Be("documentId invalid");
        }

        [Fact(DisplayName = "Should customer be valid")]
        [Trait("Category", "Customers Application Tests")]
        public void CustomerApplication_IsValid_ShouldReturnTrue()
        {
            // Arrange
            var customerApp = Fixture.GetCustomerAppService();
            var customer = Fixture.GenerateValidCustomer();

            // Act
            EntityValidationResult validation = customerApp.IsValid(customer);

            // Assert Fluent Assertions
            validation.Status.Should().BeTrue();
            validation.ValidationMessages.Should().HaveCount(c => c == 0);
        }

        [Fact(DisplayName = "Should customer not be valid with invalid documentId")]
        [Trait("Category", "Customers Application Tests")]
        public void CustomerApplication_IsValid_ShouldReturnFalseAndMessageDocumentIdIsInvalid()
        {
            // Arrange
            var customerApp = Fixture.GetCustomerAppService();
            var customer = Fixture.GenerateCustomerWithDocumentIdInvalid();

            // Act
            EntityValidationResult validation = customerApp.IsValid(customer);

            // Assert Fluent Assertions
            validation.Status.Should().BeFalse();
            validation.ValidationMessages.Should().HaveCount(c => c == 1);
            validation.ValidationMessages[0].Should().Be("CPF/CNPJ invalid");
        }

        [Fact(DisplayName = "Should customer not be valid when documentId already exists")]
        [Trait("Category", "Customers Application Tests")]
        public void CustomerApplication_IsValid_ShouldReturnFalseAndMessageDocumentIdAlreadyExists()
        {
            // Arrange
            var customerApp = Fixture.GetCustomerAppService();
            var customer = Fixture.GenerateCustomerWithDocumentIdDuplicated();
            Fixture.CustomerRepositoryMock.Setup(c => c.GetByDocumentId("34538886858")).Returns(Fixture.GenerateValidCustomerToQuery());

            // Act
            EntityValidationResult validation = customerApp.IsValid(customer);

            // Assert Fluent Assertions
            validation.Status.Should().BeFalse();
            validation.ValidationMessages.Should().HaveCount(c => c == 1);
            validation.ValidationMessages[0].Should().Be("CPF/CNPJ already exists");
        }

        [Fact(DisplayName = "Should customer not be valid when customer is null")]
        [Trait("Category", "Customers Application Tests")]
        public void CustomerApplication_IsValid_ShouldThrowsArgumentException()
        {
            // Arrange
            var customerApp = Fixture.GetCustomerAppService();            

            // Act
            Exception ex = Assert.Throws<ArgumentException>(() => customerApp.IsValid(null));

            // Assert Fluent Assertions
            ex.Message.Should().Be("Customer invalid");
        }

        [Fact(DisplayName = "Should add new customer")]
        [Trait("Category", "Customers Application Tests")]
        public void CustomerApplication_Add_ShouldAddCustomer()
        {
            // Arrange
            var customerApp = Fixture.GetCustomerAppService();
            var customer = Fixture.GenerateValidCustomer();

            // Act
            customerApp.Add(customer);

            // Assert Fluent Assertions
            Fixture.CustomerRepositoryMock.Verify(repo => repo.Add(customer));
        }

        [Fact(DisplayName = "Should update customer")]
        [Trait("Category", "Customers Application Tests")]
        public void CustomerApplication_Update_ShouldUpdateCustomer()
        {
            // Arrange
            var customerApp = Fixture.GetCustomerAppService();
            var customer = Fixture.GenerateValidCustomer();
            
            // Act
            customer.Name = "aaaaa";
            customerApp.Update(customer);

            // Assert Fluent Assertions
            Fixture.CustomerRepositoryMock.Verify(repo => repo.Update(customer));
        }

        [Fact(DisplayName = "Should change customer status")]
        [Trait("Category", "Customers Application Tests")]
        public void CustomerApplication_ChangeStatus_ShouldUpdateStatus()
        {
            // Arrange
            var customerApp = Fixture.GetCustomerAppService();
            var customer = Fixture.GenerateValidCustomer();
           
            // Act
            customer.ChangeStatus();
            customerApp.ChangeStatus(customer);

            // Assert Fluent Assertions
            Fixture.CustomerRepositoryMock.Verify(repo => repo.ChangeStatus(customer));
        }
    }
}
