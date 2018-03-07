using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using STARC.Api.Models;
using STARC.Domain.Entities;
using STARC.Domain.Models;
using STARC.Domain.ViewModels.UsersInProjects;
using STARC.Tests.Projects;
using STARC.Tests.Users;
using System.Collections.Generic;
using Xunit;

namespace STARC.Tests.UserInProjects
{
    [Collection(nameof(UsersInProjectsCollection))]
    public class UsersInProjectsControllerTests
    {
        public UsersInProjectsTestsFixture Fixture { get; set; }

        public UsersInProjectsControllerTests(UsersInProjectsTestsFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact(DisplayName = "Should get user in project by id")]
        [Trait("Category", "Users In Projects Controller Tests")]
        public void ProjectsController_GetById_ShouldReturnUserInProject()
        {
            // Arrange
            var controller = Fixture.GetUsersInProjectsController();
            Fixture.UsersInProjectsAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidUsersInProjectsToQuery());

            // Act
            var response = controller.Get(1) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var project = data.Data as UsersInProjectsToQueryViewModel;
            project.Should().NotBeNull();
            project.ProjectId.Should().Be(1);
        }

        [Fact(DisplayName = "Should not get user in project by invalid id")]
        [Trait("Category", "Users In Projects Controller Tests")]
        public void ProjectsController_GetById_ShouldNotReturnUserInProject()
        {
            // Arrange
            var controller = Fixture.GetUsersInProjectsController();
            Fixture.UsersInProjectsAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidUsersInProjectsToQuery());

            // Act
            var response = controller.Get(2) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Failed);
            data.Message.Should().Be("User in project not found");
        }

        [Fact(DisplayName = "Should get user in project by user")]
        [Trait("Category", "Users In Projects Controller Tests")]
        public void ProjectsController_GetByUserId_ShouldReturnUserInProject()
        {
            // Arrange
            var userFixture = new UsersTestsFixture();
            var controller = Fixture.GetUsersInProjectsController();

            Fixture.UsersInProjectsAppServiceMock.Setup(c => c.GetByUser(1)).Returns(Fixture.GenerateUsersInProjects(50));
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(userFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.GetByUser(1) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var projects = data.Data as IEnumerable<UsersInProjectsToQueryViewModel>;
            projects.Should().HaveCount(c => c == 50);
        }

        [Fact(DisplayName = "Should not get user in project by invalid user")]
        [Trait("Category", "Users In Projects Controller Tests")]
        public void ProjectsController_GetByUserId_ShouldNotReturnUserInProject()
        {
            // Arrange
            var userFixture = new UsersTestsFixture();
            var controller = Fixture.GetUsersInProjectsController();

            Fixture.UsersInProjectsAppServiceMock.Setup(c => c.GetByUser(1)).Returns(Fixture.GenerateUsersInProjects(50));
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(userFixture.GenerateValidUserToQuery());
            
            // Act
            var response = controller.GetByUser(2) as ObjectResult;
            
            // Assert Fluent Assertions
            response.StatusCode.Should().Be(404);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("User not found");
        }

        [Fact(DisplayName = "Should not get user in project with by user when customer is invalid")]
        [Trait("Category", "Users In Projects Controller Tests")]
        public void ProjectsController_GetByUserId_ShouldNotReturnUserInProjectInvalidCustomer()
        {
            // Arrange
            var userFixture = new UsersTestsFixture();
            var controller = Fixture.GetUsersInProjectsController();

            Fixture.UsersInProjectsAppServiceMock.Setup(c => c.GetByUser(1)).Returns(Fixture.GenerateUsersInProjects(50));
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(userFixture.GenerateValidUserToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(2)).Returns(userFixture.GenerateValidUserToQueryWithAnotherCustomer());

            // Act
            var response = controller.GetByCustomer(2) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(404);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Customer not found");
        }

        [Fact(DisplayName = "Should get user in project by project")]
        [Trait("Category", "Users In Projects Controller Tests")]
        public void ProjectsController_GetByProjectId_ShouldReturnUserInProject()
        {
            // Arrange
            var userFixture = new UsersTestsFixture();
            var projectFixture = new ProjectsTestsFixture();
            var controller = Fixture.GetUsersInProjectsController();

            Fixture.UsersInProjectsAppServiceMock.Setup(c => c.GetByProject(1)).Returns(Fixture.GenerateUsersInProjects(50));
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(userFixture.GenerateValidUserToQuery());
            Fixture.ProjectAppServiceMock.Setup(c => c.GetById(1)).Returns(projectFixture.GenerateValidProjectToQuery());

            // Act
            var response = controller.GetByProject(1) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var projects = data.Data as IEnumerable<UsersInProjectsToQueryViewModel>;
            projects.Should().HaveCount(c => c == 50);
        }

        [Fact(DisplayName = "Should not get user in project with invalid project")]
        [Trait("Category", "Users In Projects Controller Tests")]
        public void ProjectsController_GetByProjectId_ShouldNotReturnUserInProject()
        {
            // Arrange
            var userFixture = new UsersTestsFixture();
            var projectFixture = new ProjectsTestsFixture();
            var controller = Fixture.GetUsersInProjectsController();

            Fixture.UsersInProjectsAppServiceMock.Setup(c => c.GetByProject(1)).Returns(Fixture.GenerateUsersInProjects(50));
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(userFixture.GenerateValidUserToQuery());
            
            // Act
            var response = controller.GetByProject(2) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(404);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Project not found");
        }

        [Fact(DisplayName = "Should not get user in project with by project when customer is invalid")]
        [Trait("Category", "Users In Projects Controller Tests")]
        public void ProjectsController_GetByProjectId_ShouldNotReturnUserInProjectInvalidCustomer()
        {
            // Arrange
            var userFixture = new UsersTestsFixture();
            var projectFixture = new ProjectsTestsFixture();
            var controller = Fixture.GetUsersInProjectsController();

            Fixture.UsersInProjectsAppServiceMock.Setup(c => c.GetByUser(1)).Returns(Fixture.GenerateUsersInProjects(50));
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(userFixture.GenerateValidUserToQuery());
            Fixture.ProjectAppServiceMock.Setup(c => c.GetById(2)).Returns(projectFixture.GenerateValidProjectWithAnotherCustomer());

            // Act
            var response = controller.GetByProject(2) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(404);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Project not found");
        }

        [Fact(DisplayName = "Should get user in project by customer")]
        [Trait("Category", "Users In Projects Controller Tests")]
        public void ProjectsController_GetByCustomerId_ShouldReturnUserInProject()
        {
            // Arrange
            var userFixture = new UsersTestsFixture();
            var controller = Fixture.GetUsersInProjectsController();

            Fixture.UsersInProjectsAppServiceMock.Setup(c => c.GetByCustomer(1)).Returns(Fixture.GenerateUsersInProjects(50));
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(userFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.GetByCustomer(1) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var projects = data.Data as IEnumerable<UsersInProjectsToQueryViewModel>;
            projects.Should().HaveCount(c => c == 50);
        }

        [Fact(DisplayName = "Should not get user in project by invalid customer")]
        [Trait("Category", "Users In Projects Controller Tests")]
        public void ProjectsController_GetByCustomerId_ShouldNotReturnUserInProject()
        {
            // Arrange
            var userFixture = new UsersTestsFixture();
            var controller = Fixture.GetUsersInProjectsController();

            Fixture.UsersInProjectsAppServiceMock.Setup(c => c.GetByCustomer(1)).Returns(Fixture.GenerateUsersInProjects(50));
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(userFixture.GenerateValidUserToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(2)).Returns(userFixture.GenerateValidUserToQueryWithAnotherCustomer());

            // Act
            var response = controller.GetByCustomer(2) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(404);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Customer not found");
        }

        [Fact(DisplayName = "Should insert new user in project")]
        [Trait("Category", "Users In Projects Controller Tests")]
        public void ProjectsController_Insert_ShouldAddUserInProject()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetUsersInProjectsController();
            var userInProjectToInsert = Fixture.GenerateValidUsersInProjectsToInsert();

            Fixture.UsersInProjectsAppServiceMock.Setup(c => c.Add(It.IsAny<UsersInProjects>())).Returns(2);
            Fixture.UsersInProjectsAppServiceMock.Setup(c => c.IsValid(It.IsAny<UsersInProjects>()))
                .Returns(new EntityValidationResult
                {
                    Status = true
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Insert(userInProjectToInsert) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(201);
        }

        [Fact(DisplayName = "Should not insert when user in project is null")]
        [Trait("Category", "Users In Projects Controller Tests")]
        public void ProjectsController_Insert_ShouldReturnBadRequestUserIiProjectNull()
        {
            // Arrange
            var controller = Fixture.GetUsersInProjectsController();

            // Act
            var response = controller.Insert(null) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("User in project is null");
        }

        [Fact(DisplayName = "Should not insert when user in project is not valid")]
        [Trait("Category", "Users In Projects Controller Tests")]
        public void ProjectsController_Insert_ShouldReturnBadRequestNotValid()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetUsersInProjectsController();
            var userInProjectToInsert = Fixture.GenerateValidUsersInProjectsToInsert();

            Fixture.UsersInProjectsAppServiceMock.Setup(c => c.Add(It.IsAny<UsersInProjects>())).Returns(2);
            Fixture.UsersInProjectsAppServiceMock.Setup(c => c.IsValid(It.IsAny<UsersInProjects>()))
                .Returns(new EntityValidationResult
                {
                    Status = false
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Insert(userInProjectToInsert) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Validation failed, please see details");
        }

        [Fact(DisplayName = "Should delete user in project")]
        [Trait("Category", "Users In Projects Controller Tests")]
        public void ProjectsController_Delete_ShouldDeleteUserInProject()
        {
            // Arrange
            var controller = Fixture.GetUsersInProjectsController();

            Fixture.UsersInProjectsAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidUsersInProjectsToQuery());
            
            // Act
            var response = controller.Delete(1) as NoContentResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(204);
        }

        [Fact(DisplayName = "Should not delete when user in project not found")]
        [Trait("Category", "Users In Projects Controller Tests")]
        public void ProjectsController_Delete_ShouldNotDeleteUserInProject()
        {
            // Arrange
            var controller = Fixture.GetUsersInProjectsController();

            Fixture.UsersInProjectsAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidUsersInProjectsToQuery());

            // Act
            var response = controller.Delete(2) as ObjectResult;

            response.StatusCode.Should().Be(404);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("User in Project Not Found");
        }
    }
}
