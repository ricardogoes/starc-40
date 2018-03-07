using FluentAssertions;
using Moq;
using Microsoft.AspNetCore.Mvc;
using STARC.Api.Models;
using STARC.Domain.Entities;
using STARC.Domain.ViewModels.Projects;
using System.Collections.Generic;
using Xunit;
using STARC.Domain.Models;
using STARC.Tests.Users;

namespace STARC.Tests.Projects
{
    [Collection(nameof(ProjectsCollection))]
    public class ProjectsControllerTests
    {
        public ProjectsTestsFixture Fixture { get; set; }

        public ProjectsControllerTests(ProjectsTestsFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact(DisplayName = "Should get active projects by customer")]
        [Trait("Category", "Projects Controller Tests")]
        public void ProjectsController_GetActive_ShouldReturnsActiveProjects()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetProjectsController();

            Fixture.ProjectAppServiceMock.Setup(c => c.GetActiveByCustomer(1)).Returns(Fixture.GetActiveProjects());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.GetActiveByCustomer(1) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var projects = data.Data as IEnumerable<ProjectToQueryViewModel>;
            projects.Should().HaveCount(c => c == 50);
            projects.Should().NotContain(c => !c.Status);
        }

        [Fact(DisplayName = "Should get project by projectId")]
        [Trait("Category", "Projects Controller Tests")]
        public void ProjectsController_GetById_ShouldReturnProject()
        {
            // Arrange
            var controller = Fixture.GetProjectsController();
            Fixture.ProjectAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidProjectToQuery());

            // Act
            var response = controller.Get(1) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var project = data.Data as ProjectToQueryViewModel;
            project.Should().NotBeNull();
            project.ProjectId.Should().Be(1);
        }

        [Fact(DisplayName = "Should not get project by invalid projectId")]
        [Trait("Category", "Projects Controller Tests")]
        public void ProjectsController_GetById_ShouldNotReturnProject()
        {
            // Arrange
            var controller = Fixture.GetProjectsController();
            Fixture.ProjectAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidProjectToQuery());

            // Act
            var response = controller.Get(2) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Failed);
            data.Message.Should().Be("Project not Found");
        }

        [Fact(DisplayName = "Should get project by customer")]
        [Trait("Category", "Projects Controller Tests")]
        public void ProjectsController_GetByCustomerId_ShouldReturnProject()
        {
            // Arrange
            var controller = Fixture.GetProjectsController();
            Fixture.ProjectAppServiceMock.Setup(c => c.GetByCustomer(1)).Returns(Fixture.GetMixedProjectsByCustomer());

            // Act
            var response = controller.GetByCustomer(1) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var projects = data.Data as IEnumerable<ProjectToQueryViewModel>;
            projects.Should().HaveCount(c => c == 50);
        }

        [Fact(DisplayName = "Should not get project by invalid customer")]
        [Trait("Category", "Projects Controller Tests")]
        public void ProjectsController_GetByCustomerId_ShouldNotReturnProject()
        {
            // Arrange
            var controller = Fixture.GetProjectsController();
            Fixture.ProjectAppServiceMock.Setup(c => c.GetByCustomer(1)).Returns(Fixture.GetMixedProjectsByCustomer());

            // Act
            var response = controller.GetByCustomer(2) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var projects = data.Data as IEnumerable<ProjectToQueryViewModel>;
            projects.Should().HaveCount(c => c == 0);
        }

        [Fact(DisplayName = "Should insert new project")]
        [Trait("Category", "Projects Controller Tests")]
        public void ProjectsController_Insert_ShouldAddProject()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetProjectsController();
            var projectToInsert = Fixture.GenerateValidProjectToInsert();

            Fixture.ProjectAppServiceMock.Setup(c => c.Add(It.IsAny<Project>())).Returns(2);
            Fixture.ProjectAppServiceMock.Setup(c => c.IsValid(It.IsAny<Project>()))
                .Returns(new EntityValidationResult
                {
                    Status = true
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Insert(projectToInsert) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(201);
        }

        [Fact(DisplayName = "Should not insert when project is null")]
        [Trait("Category", "Projects Controller Tests")]
        public void ProjectsController_Insert_ShouldReturnBadRequestProjectNull()
        {
            // Arrange
            var controller = Fixture.GetProjectsController();

            // Act
            var response = controller.Insert(null) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Project is null");
        }

        [Fact(DisplayName = "Should not insert when project is not valid")]
        [Trait("Category", "Projects Controller Tests")]
        public void ProjectsController_Insert_ShouldReturnBadRequestNotValid()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetProjectsController();
            var projectToInsert = Fixture.GenerateValidProjectToInsert();

            Fixture.ProjectAppServiceMock.Setup(c => c.Add(It.IsAny<Project>())).Returns(2);
            Fixture.ProjectAppServiceMock.Setup(c => c.IsValid(It.IsAny<Project>()))
                .Returns(new EntityValidationResult
                {
                    Status = false
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Insert(projectToInsert) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Validation failed, please see details");
        }

        [Fact(DisplayName = "Should update project")]
        [Trait("Category", "Projects Controller Tests")]
        public void ProjectsController_Update_ShouldUpdateProject()
        {
            var UserFixture = new UsersTestsFixture();

            // Arrange
            var controller = Fixture.GetProjectsController();
            var projectToUpdate = Fixture.GenerateValidProjectToUpdate();

            Fixture.ProjectAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidProjectToQuery());
            Fixture.ProjectAppServiceMock.Setup(c => c.IsValid(It.IsAny<Project>()))
                .Returns(new EntityValidationResult
                {
                    Status = true
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Update(1, projectToUpdate) as NoContentResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(204);
        }

        [Fact(DisplayName = "Should not update when projectId <> id")]
        [Trait("Category", "Projects Controller Tests")]
        public void ProjectsController_Update_ShouldReturnBadRequestInvalidRequest()
        {
            // Arrange
            var controller = Fixture.GetProjectsController();
            var projectToUpdate = Fixture.GenerateValidProjectToUpdate();

            // Act
            var response = controller.Update(2, projectToUpdate) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Invalid request");
        }

        [Fact(DisplayName = "Should not update when project is null")]
        [Trait("Category", "Projects Controller Tests")]
        public void ProjectsController_Update_ShouldReturnBadRequestInvalidRequestProjectNull()
        {
            // Arrange
            var controller = Fixture.GetProjectsController();

            // Act
            var response = controller.Update(2, null) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Invalid request");
        }

        [Fact(DisplayName = "Should not update when project is not found")]
        [Trait("Category", "Projects Controller Tests")]
        public void ProjectsController_Update_ShouldReturnProjectNotFound()
        {

            // Arrange
            var controller = Fixture.GetProjectsController();
            var projectToUpdate = Fixture.GenerateValidProjectToUpdate();
            Fixture.ProjectAppServiceMock.Setup(c => c.GetById(2)).Returns(Fixture.GenerateValidProjectToQuery());

            // Act
            var response = controller.Update(1, projectToUpdate) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(404);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Project Not Found");
        }

        [Fact(DisplayName = "Should not update when project is not valid")]
        [Trait("Category", "Projects Controller Tests")]
        public void ProjectsController_Update_ShouldReturnBadRequestNotValid()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetProjectsController();
            var projectToUpdate = Fixture.GenerateValidProjectToUpdate();

            Fixture.ProjectAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidProjectToQuery());
            Fixture.ProjectAppServiceMock.Setup(c => c.IsValid(It.IsAny<Project>()))
                .Returns(new EntityValidationResult
                {
                    Status = false
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Update(1, projectToUpdate) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Validation failed, please see details");
        }

        [Fact(DisplayName = "Should change project status")]
        [Trait("Category", "Projects Controller Tests")]
        public void ProjectsController_ChangeStatus_ShouldUpdateProject()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetProjectsController();

            Fixture.ProjectAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidProjectToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.ChangeStatus(1) as NoContentResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(204);
        }

        [Fact(DisplayName = "Should not change status when project not found")]
        [Trait("Category", "Projects Controller Tests")]
        public void ProjectsController_ChangeStatus_ShouldReturnNotFound()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetProjectsController();

            Fixture.ProjectAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidProjectToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.ChangeStatus(2) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(404);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Project Not Found");
        }
    }
}
