using FluentAssertions;
using STARC.Domain.Models;
using STARC.Tests.Customers;
using STARC.Tests.UserProfiles;
using System;
using System.Linq;
using Xunit;

namespace STARC.Tests.Users
{
    [Collection(nameof(UsersCollection))]
    public class UserApplicationTests
    {
        public UsersTestsFixture Fixture { get; set; }

        public UserApplicationTests(UsersTestsFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact(DisplayName = "Should get users by Customer")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_GetByCustomer_ShouldReturnsOnlyUsersFromCustomer1()
        {
            // Arrange
            var userApp = Fixture.GetUserAppService();
            Fixture.UserRepositoryMock.Setup(c => c.GetByCustomer(1)).Returns(Fixture.GetMixedUsersByCustomer());

            // Act
            var users = userApp.GetByCustomer(1).ToList();

            // Assert Fluent Assertions
            users.Should().HaveCount(c => c == 50).And.OnlyHaveUniqueItems();
        }

        [Fact(DisplayName = "Should not get users by invalid customer")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_GetByCustomer_ShouldNotReturnUser()
        {
            // Arrange
            var userApp = Fixture.GetUserAppService();
            Fixture.UserRepositoryMock.Setup(c => c.GetByCustomer(1)).Returns(Fixture.GetMixedUsersByCustomer());

            // Act
            var users = userApp.GetByCustomer(2).ToList();

            // Assert Fluent Assertions
            users.Should().HaveCount(0);
        }

        [Fact(DisplayName = "Should not get users by customer when customerId = 0")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_GetByCustomer_ShouldThrowsArgumentException()
        {
            // Arrange
            var userApp = Fixture.GetUserAppService();

            // Act
            var ex = Assert.Throws<ArgumentException>(() => userApp.GetByCustomer(0).ToList());

            // Assert Fluent Assertions
            ex.Message.Should().Be("CustomerId invalid");
        }

        [Fact(DisplayName = "Should get user by userId")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_GetById_ShouldReturnUser()
        {
            // Arrange
            var userApp = Fixture.GetUserAppService();
            Fixture.UserRepositoryMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidUserToQuery());

            // Act
            var user = userApp.GetById(1);

            // Assert Fluent Assertions
            user.UserId.Should().Be(1);
        }

        [Fact(DisplayName = "Should not get user by invalid userId")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_GetByIdInvalid_ShouldNotReturnUser()
        {
            // Arrange
            var userApp = Fixture.GetUserAppService();
            Fixture.UserRepositoryMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidUserToQuery());

            // Act
            var user = userApp.GetById(2);

            // Assert Fluent Assertions
            user.Should().BeNull();
        }

        [Fact(DisplayName = "Should get users that are not associated with project")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_GetByNotInProject_ShouldReturnsOnlyUsersNotInProject1()
        {
            // Arrange
            var userApp = Fixture.GetUserAppService();
            Fixture.UserRepositoryMock.Setup(c => c.GetByNotInProject(1)).Returns(Fixture.GetUsers());

            // Act
            var users = userApp.GetByNotInProject(1).ToList();

            // Assert Fluent Assertions
            users.Should().HaveCount(c => c == 50).And.OnlyHaveUniqueItems();
        }

        [Fact(DisplayName = "Should not get users that are not associated with project when project is invalid")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_GetByNotInProject_ShouldNotReturnUser()
        {
            // Arrange
            var userApp = Fixture.GetUserAppService();
            Fixture.UserRepositoryMock.Setup(c => c.GetByNotInProject(1)).Returns(Fixture.GetUsers());

            // Act
            var users = userApp.GetByNotInProject(2).ToList();

            // Assert Fluent Assertions
            users.Should().HaveCount(0);
        }

        [Fact(DisplayName = "Should not get users that are not associated with project when projectId = 0")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_GetByNotInProject_ShouldThrowsArgumentException()
        {
            // Arrange
            var userApp = Fixture.GetUserAppService();

            // Act
            var ex = Assert.Throws<ArgumentException>(() => userApp.GetByNotInProject(0).ToList());

            // Assert Fluent Assertions
            ex.Message.Should().Be("ProjectId invalid");
        }

        [Fact(DisplayName = "Should get user by username")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_GetByUsername_ShouldReturnUser()
        {
            // Arrange
            var userApp = Fixture.GetUserAppService();
            Fixture.UserRepositoryMock.Setup(c => c.GetByUsername("ricardo.goes")).Returns(Fixture.GenerateValidUserToQuery());

            // Act
            var customer = userApp.GetByUsername("ricardo.goes");

            // Assert Fluent Assertions
            customer.Should().NotBeNull();
            customer.Username.Should().Be("ricardo.goes");

        }

        [Fact(DisplayName = "Should not get user with invalid username")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_GetByUsername_ShouldNotReturnUser()
        {
            // Arrange
            var userApp = Fixture.GetUserAppService();
            Fixture.UserRepositoryMock.Setup(c => c.GetByUsername("ricardo.goes")).Returns(Fixture.GenerateValidUserToQuery());

            // Act
            var customer = userApp.GetByUsername("admin");

            // Assert Fluent Assertions
            customer.Should().BeNull();
        }

        [Fact(DisplayName = "Should get user by username and password")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_GetByUsernameAndPassword_ShouldReturnUser()
        {
            // Arrange
            var userApp = Fixture.GetUserAppService();
            Fixture.UserRepositoryMock.Setup(c => c.GetByUsernameAndPassword("ricardo.goes", "admin")).Returns(Fixture.GenerateValidUserToQuery());

            // Act
            var customer = userApp.GetByUsernameAndPassword("ricardo.goes", "admin");

            // Assert Fluent Assertions
            customer.Should().NotBeNull();
            customer.Username.Should().Be("ricardo.goes");

        }

        [Fact(DisplayName = "Should not get user by username and password with invalid username")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_GetByUsernameAndPassword_InvalidUsername_ShouldNotReturnUser()
        {
            // Arrange
            var userApp = Fixture.GetUserAppService();
            Fixture.UserRepositoryMock.Setup(c => c.GetByUsernameAndPassword("ricardo.goes", "admin")).Returns(Fixture.GenerateValidUserToQuery());

            // Act
            var customer = userApp.GetByUsernameAndPassword("ricardo.goes1","admin");

            // Assert Fluent Assertions
            customer.Should().BeNull();
        }

        [Fact(DisplayName = "Should not get user by username and password with invalid password")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_GetByUsernameAndPassword_InvalidPassword_ShouldNotReturnUser()
        {
            // Arrange
            var userApp = Fixture.GetUserAppService();
            Fixture.UserRepositoryMock.Setup(c => c.GetByUsernameAndPassword("ricardo.goes", "admin")).Returns(Fixture.GenerateValidUserToQuery());

            // Act
            var customer = userApp.GetByUsernameAndPassword("ricardo.goes","admin1");

            // Assert Fluent Assertions
            customer.Should().BeNull();
        }

        [Fact(DisplayName = "Should username not be unique with duplicated data")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_IsUsernameUnique_ShouldReturnFalse()
        {
            // Arrange
            var userApp = Fixture.GetUserAppService();
            Fixture.UserRepositoryMock.Setup(c => c.GetByUsername("ricardo.goes")).Returns(Fixture.GenerateValidUserToQuery());

            // Act
            var isUsernameUnique = userApp.IsUsernameUnique("ricardo.goes");

            // Assert Fluent Assertions
            isUsernameUnique.Should().BeFalse();
        }

        [Fact(DisplayName = "Should username be unique")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_IsUsernameUnique_ShouldReturnTrue()
        {
            // Arrange
            var userApp = Fixture.GetUserAppService();
            Fixture.UserRepositoryMock.Setup(c => c.GetByUsername("34538886858")).Returns(Fixture.GenerateValidUserToQuery());

            // Act
            var isUsernameUnique = userApp.IsUsernameUnique("ricardo.goes1");

            // Assert Fluent Assertions
            isUsernameUnique.Should().BeTrue();
        }

        [Fact(DisplayName = "Should username not be unique when blank")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_IsUsernameUniqueBlank_ShouldThrowsArgumentException()
        {
            // Arrange
            var userApp = Fixture.GetUserAppService();

            // Act
            Exception ex = Assert.Throws<ArgumentException>(() => userApp.IsUsernameUnique(string.Empty));

            // Assert Fluent Assertions
            ex.Message.Should().Be("username invalid");
        }

        [Fact(DisplayName = "Should user be valid")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_IsValid_ShouldReturnTrue()
        {
            // Arrange
            var CustomerFixture = new CustomersTestsFixture();
            var UserProfileFixture = new UserProfilesTestsFixture();

            var userApp = Fixture.GetUserAppService();
            var user = Fixture.GenerateValidUserToAdd();

            Fixture.CustomerAppServiceMock.Setup(c => c.GetById(1)).Returns(CustomerFixture.GenerateValidCustomerToQuery());
            Fixture.UserProfileAppServiceMock.Setup(c => c.GetById(3)).Returns(UserProfileFixture.GenerateValidUserProfile());
            
            // Act
            EntityValidationResult validation = userApp.IsValid(user);

            // Assert Fluent Assertions
            validation.Status.Should().BeTrue();
            validation.ValidationMessages.Should().HaveCount(c => c == 0);
        }

        [Fact(DisplayName = "Should user not be valid when username already exists")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_IsValid_ShouldReturnFalseAndMessageUsernameAlreadyExists()
        {
            // Arrange
            var CustomerFixture = new CustomersTestsFixture();
            var UserProfileFixture = new UserProfilesTestsFixture();

            var userApp = Fixture.GetUserAppService();
            var user = Fixture.GenerateValidUserWithUserId0();

            Fixture.UserRepositoryMock.Setup(c => c.GetByUsername("ricardo.goes")).Returns(Fixture.GenerateValidUserToQuery());
            Fixture.CustomerAppServiceMock.Setup(c => c.GetById(1)).Returns(CustomerFixture.GenerateValidCustomerToQuery());
            Fixture.UserProfileAppServiceMock.Setup(c => c.GetById(3)).Returns(UserProfileFixture.GenerateValidUserProfile());

            // Act
            EntityValidationResult validation = userApp.IsValid(user);

            // Assert Fluent Assertions
            validation.Status.Should().BeFalse();
            validation.ValidationMessages.Should().HaveCount(c => c == 1);
            validation.ValidationMessages[0].Should().Be("Username already exists");
        }

        [Fact(DisplayName = "Should user not be valid with invalid customer")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_IsValid_ShouldReturnFalseAndMessageInvalidCustomer()
        {
            var CustomerFixture = new CustomersTestsFixture();
            var UserProfileFixture = new UserProfilesTestsFixture();

            // Arrange
            var userApp = Fixture.GetUserAppService();
            var user = Fixture.GenerateUserWithInvalidCustomer();

            Fixture.CustomerAppServiceMock.Setup(c => c.GetById(1)).Returns(CustomerFixture.GenerateValidCustomerToQuery());
            Fixture.UserProfileAppServiceMock.Setup(c => c.GetById(3)).Returns(UserProfileFixture.GenerateValidUserProfile());

            // Act
            EntityValidationResult validation = userApp.IsValid(user);

            // Assert Fluent Assertions
            validation.Status.Should().BeFalse();
            validation.ValidationMessages.Should().HaveCount(c => c == 1);
            validation.ValidationMessages[0].Should().Be("Customer Not Found");
        }

        [Fact(DisplayName = "Should user not be valid with invalid user profile")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_IsValid_ShouldReturnFalseAndMessageInvalidProfile()
        {
            var CustomerFixture = new CustomersTestsFixture();
            var UserProfileFixture = new UserProfilesTestsFixture();

            // Arrange
            var userApp = Fixture.GetUserAppService();
            var user = Fixture.GenerateUserWithInvalidProfile();

            Fixture.CustomerAppServiceMock.Setup(c => c.GetById(1)).Returns(CustomerFixture.GenerateValidCustomerToQuery());
            Fixture.UserProfileAppServiceMock.Setup(c => c.GetById(1)).Returns(UserProfileFixture.GenerateValidUserProfile());

            // Act
            EntityValidationResult validation = userApp.IsValid(user);

            // Assert Fluent Assertions
            validation.Status.Should().BeFalse();
            validation.ValidationMessages.Should().HaveCount(c => c == 1);
            validation.ValidationMessages[0].Should().Be("Profile Not Found");
        }

        [Fact(DisplayName = "Should add new user")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_Add_ShouldAddUser()
        {
            // Arrange
            var userApp = Fixture.GetUserAppService();
            var user = Fixture.GenerateValidUserToAdd();

            // Act
            userApp.Add(user);

            // Assert Fluent Assertions
            Fixture.UserRepositoryMock.Verify(repo => repo.Add(user));
        }

        [Fact(DisplayName = "Should update user")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_Update_ShouldUpdateUser()
        {
            // Arrange
            var userApp = Fixture.GetUserAppService();
            var user = Fixture.GenerateValidUserToAdd();

            // Act
            user.FirstName = "aaaaa";
            userApp.Update(user);

            // Assert Fluent Assertions
            Fixture.UserRepositoryMock.Verify(repo => repo.Update(user));
        }

        [Fact(DisplayName = "Should change user status")]
        [Trait("Category", "Users Application Tests")]
        public void UserApplication_ChangeStatus_ShouldUpdateStatus()
        {
            // Arrange
            var userApp = Fixture.GetUserAppService();
            var user = Fixture.GenerateValidUserToAdd();

            // Act
            user.ChangeStatus();
            userApp.ChangeStatus(user);

            // Assert Fluent Assertions
            Fixture.UserRepositoryMock.Verify(repo => repo.ChangeStatus(user));
        }
    }
}
