using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using STARC.Api.Models;
using STARC.Domain.Entities;
using STARC.Domain.Models;
using STARC.Domain.ViewModels.Users;
using STARC.Tests.Password;
using System;
using System.Collections.Generic;
using Xunit;

namespace STARC.Tests.Users
{
    [Collection(nameof(UsersCollection))]
    public class UsersControllerTests
    {
        public UsersTestsFixture Fixture { get; set; }

        public UsersControllerTests(UsersTestsFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact(DisplayName = "Should get user by userId")]
        [Trait("Category", "Users Controller Tests")]
        public void UsersController_GetById_ShouldReturnUser()
        {
            // Arrange
            var controller = Fixture.GetUsersController();
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Get(1) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var user = data.Data as UserToQueryViewModel;
            user.Should().NotBeNull();
            user.UserId.Should().Be(1);
        }

        [Fact(DisplayName = "Should not get user by invalid userId")]
        [Trait("Category", "Users Controller Tests")]
        public void UsersController_GetById_ShouldNotReturnUser()
        {
            // Arrange
            var controller = Fixture.GetUsersController();
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Get(2) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Failed);
            data.Message.Should().Be("User Not Found");
        }

        [Fact(DisplayName = "Should get user by customer")]
        [Trait("Category", "Users Controller Tests")]
        public void UsersController_GetByCustomerId_ShouldReturnUser()
        {
            // Arrange
            var controller = Fixture.GetUsersController();
            Fixture.UserAppServiceMock.Setup(c => c.GetByCustomer(1)).Returns(Fixture.GetMixedUsersByCustomer());

            // Act
            var response = controller.GetByCustomer(1) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var users = data.Data as IEnumerable<UserToQueryViewModel>;
            users.Should().HaveCount(c => c == 50);
        }

        [Fact(DisplayName = "Should not get user by invalid customer")]
        [Trait("Category", "Users Controller Tests")]
        public void UsersController_GetByCustomerId_ShouldNotReturnUser()
        {
            // Arrange
            var controller = Fixture.GetUsersController();
            Fixture.UserAppServiceMock.Setup(c => c.GetByCustomer(1)).Returns(Fixture.GetMixedUsersByCustomer());

            // Act
            var response = controller.GetByCustomer(2) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var users = data.Data as IEnumerable<UserToQueryViewModel>;
            users.Should().HaveCount(c => c == 0);
        }

        [Fact(DisplayName = "Should get users that are not associated with project")]
        [Trait("Category", "Users Controller Tests")]
        public void UsersController_GetByNotInProject_ShouldReturnUser()
        {
            // Arrange
            var controller = Fixture.GetUsersController();
            Fixture.UserAppServiceMock.Setup(c => c.GetByNotInProject(1)).Returns(Fixture.GetUsers());

            // Act
            var response = controller.GetByNotInProject(1) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var users = data.Data as IEnumerable<UserToQueryViewModel>;
            users.Should().HaveCount(c => c == 50);
        }

        [Fact(DisplayName = "Should not get users that are not associated with project when project is invalid")]
        [Trait("Category", "Users Controller Tests")]
        public void UsersController_GetByNotInProject_ShouldNotReturnUser()
        {
            // Arrange
            var controller = Fixture.GetUsersController();
            Fixture.UserAppServiceMock.Setup(c => c.GetByNotInProject(1)).Returns(Fixture.GetUsers());

            // Act
            var response = controller.GetByNotInProject(2) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var users = data.Data as IEnumerable<UserToQueryViewModel>;
            users.Should().HaveCount(c => c == 0);
        }

        [Fact(DisplayName = "Should insert new user")]
        [Trait("Category", "Users Controller Tests")]
        public void UsersController_Insert_ShouldAddUser()
        {
            // Arrange
            var PasswordFixture = new PasswordTestsFixture();

            var controller = Fixture.GetUsersController();
            var userToInsert = Fixture.GenerateValidUserToInsert();
            var salt = PasswordFixture.GenerateSalt("0xF2AAE5AB737144A57EC78745C82F7433");
            var hashPassword = new HashPassword
            {
                Password = "/hR/l/WEyLUdT3dfp46+tI8mNfpwoAwtPrEyTZ2fz/I=",
                Salt = salt
            };

            Fixture.UserAppServiceMock.Setup(c => c.Add(It.IsAny<User>())).Returns(2);
            Fixture.UserAppServiceMock.Setup(c => c.IsValid(It.IsAny<User>()))
                .Returns(new EntityValidationResult
                {
                    Status = true
                });
            
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidUserToQuery());
            Fixture.PasswordAppServiceMock.Setup(c => c.SetCriptoPassword("admin")).Returns(hashPassword);

            // Act
            var response = controller.Insert(userToInsert) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(201);
        }

        [Fact(DisplayName = "Should not insert when user is null")]
        [Trait("Category", "Users Controller Tests")]
        public void UsersController_Insert_ShouldReturnBadRequestUserNull()
        {
            // Arrange
            var controller = Fixture.GetUsersController();

            // Act
            var response = controller.Insert(null) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("User is null");
        }

        [Fact(DisplayName = "Should not insert when userProfileId=1")]
        [Trait("Category", "Users Controller Tests")]
        public void UsersController_Insert_ShouldReturnBadRequestUserProfile1()
        {
            // Arrange
            var controller = Fixture.GetUsersController();
            var userToInsert = Fixture.GenerateValidUserToInsertWithUserProfileSystemAdmin();

            // Act
            var response = controller.Insert(userToInsert) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Invalid request");
        }

        [Fact(DisplayName = "Should not insert when user is not valid")]
        [Trait("Category", "Users Controller Tests")]
        public void UsersController_Insert_ShouldReturnBadRequestNotValid()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var PasswordFixture = new PasswordTestsFixture();

            var controller = Fixture.GetUsersController();
            var userToInsert = Fixture.GenerateValidUserToInsert();
            var salt = PasswordFixture.GenerateSalt("0xF2AAE5AB737144A57EC78745C82F7433");
            var hashPassword = new HashPassword
            {
                Password = "/hR/l/WEyLUdT3dfp46+tI8mNfpwoAwtPrEyTZ2fz/I=",
                Salt = salt
            };

            Fixture.UserAppServiceMock.Setup(c => c.Add(It.IsAny<User>())).Returns(2);
            Fixture.UserAppServiceMock.Setup(c => c.IsValid(It.IsAny<User>()))
                .Returns(new EntityValidationResult
                {
                    Status = false
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());
            Fixture.PasswordAppServiceMock.Setup(c => c.SetCriptoPassword("admin")).Returns(hashPassword);

            // Act
            var response = controller.Insert(userToInsert) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Validation failed, please see details");
        }

        [Fact(DisplayName = "Should update user")]
        [Trait("Category", "Users Controller Tests")]
        public void UsersController_Update_ShouldUpdateUser()
        {
            var UserFixture = new UsersTestsFixture();
            var PasswordFixture = new PasswordTestsFixture();

            // Arrange
            var controller = Fixture.GetUsersController();
            var userToUpdate = Fixture.GenerateValidUserToUpdate();
            var salt = PasswordFixture.GenerateSalt("0xF2AAE5AB737144A57EC78745C82F7433");
            var hashPassword = new HashPassword
            {
                Password = "/hR/l/WEyLUdT3dfp46+tI8mNfpwoAwtPrEyTZ2fz/I=",
                Salt = salt
            };

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidUserToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.IsValid(It.IsAny<User>()))
                .Returns(new EntityValidationResult
                {
                    Status = true
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());
            Fixture.PasswordAppServiceMock.Setup(c => c.SetCriptoPassword("admin")).Returns(hashPassword);

            // Act
            var response = controller.Update(1, userToUpdate) as NoContentResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(204);
        }

        [Fact(DisplayName = "Should not update when Id <> UserId")]
        [Trait("Category", "Users Controller Tests")]
        public void UsersController_Update_ShouldReturnBadRequestInvalidRequest()
        {
            // Arrange
            var controller = Fixture.GetUsersController();
            var userToUpdate = Fixture.GenerateValidUserToUpdate();

            // Act
            var response = controller.Update(2, userToUpdate) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Invalid request");
        }

        [Fact(DisplayName = "Should not update when user is null")]
        [Trait("Category", "Users Controller Tests")]
        public void UsersController_Update_ShouldReturnBadRequestInvalidRequestUserNull()
        {
            // Arrange
            var controller = Fixture.GetUsersController();

            // Act
            var response = controller.Update(2, null) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Invalid request");
        }

        [Fact(DisplayName = "Should not update when userProfileId = 1")]
        [Trait("Category", "Users Controller Tests")]
        public void UsersController_Update_ShouldReturnBadRequestInvalidRequestUserProfileId1()
        {
            // Arrange
            var controller = Fixture.GetUsersController();
            var userToUpdate = Fixture.GenerateValidUserToUpdateWithUserProfileSystemAdmin();
            
            // Act
            var response = controller.Update(1, userToUpdate) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Invalid request");
        }

        [Fact(DisplayName = "Should not update when user not found")]
        [Trait("Category", "Users Controller Tests")]
        public void UsersController_Update_ShouldReturnUserNotFound()
        {

            // Arrange
            var controller = Fixture.GetUsersController();
            var userToUpdate = Fixture.GenerateValidUserToUpdate();
            Fixture.UserAppServiceMock.Setup(c => c.GetById(2)).Returns(Fixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Update(1, userToUpdate) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(404);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("User Not Found");
        }

        [Fact(DisplayName = "Should not update when user is not valid")]
        [Trait("Category", "Users Controller Tests")]
        public void UsersController_Update_ShouldReturnBadRequestNotValid()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var PasswordFixture = new PasswordTestsFixture();

            var controller = Fixture.GetUsersController();
            var userToUpdate = Fixture.GenerateValidUserToUpdate();
            var salt = PasswordFixture.GenerateSalt("0xF2AAE5AB737144A57EC78745C82F7433");
            var hashPassword = new HashPassword
            {
                Password = "/hR/l/WEyLUdT3dfp46+tI8mNfpwoAwtPrEyTZ2fz/I=",
                Salt = salt
            };

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidUserToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.IsValid(It.IsAny<User>()))
                .Returns(new EntityValidationResult
                {
                    Status = false
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());
            Fixture.PasswordAppServiceMock.Setup(c => c.SetCriptoPassword("admin")).Returns(hashPassword);

            // Act
            var response = controller.Update(1, userToUpdate) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Validation failed, please see details");
        }

        [Fact(DisplayName = "Should change user status")]
        [Trait("Category", "Users Controller Tests")]
        public void UsersController_ChangeStatus_ShouldUpdateUser()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetUsersController();

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidUserToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.ChangeStatus(1) as NoContentResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(204);
        }

        [Fact(DisplayName = "Should not change status when user not found")]
        [Trait("Category", "Users Controller Tests")]
        public void UsersController_ChangeStatus_ShouldReturnNotFound()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetUsersController();

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidUserToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.ChangeStatus(2) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(404);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("User Not Found");
        }
    }
}
