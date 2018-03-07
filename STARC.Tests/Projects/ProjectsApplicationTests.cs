using FluentAssertions;
using STARC.Domain.Models;
using STARC.Tests.Customers;
using STARC.Tests.Users;
using System;
using System.Linq;
using Xunit;

namespace STARC.Tests.Projects
{
    [Collection(nameof(ProjectsCollection))]
    public class ProjectApplicationTests
    {
        public ProjectsTestsFixture Fixture { get; set; }
        
        public ProjectApplicationTests(ProjectsTestsFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact(DisplayName = "Should get active projects by customer")]
        [Trait("Category", "Projects Application Tests")]
        public void ProjectApplication_GetActive_ShouldReturnsOnlyActiveProject()
        {
            // Arrange
            var projectApp = Fixture.GetProjectAppService();
            Fixture.ProjectRepositoryMock.Setup(c => c.GetActiveByCustomer(1)).Returns(Fixture.GetActiveProjects());

            // Act
            var projects = projectApp.GetActiveByCustomer(1).ToList();

            // Assert Fluent Assertions
            projects.Should().HaveCount(c => c > 1).And.OnlyHaveUniqueItems();
            projects.Should().NotContain(c => !c.Status);
        }

        [Fact(DisplayName = "Should get projects by customer")]
        [Trait("Category", "Projects Application Tests")]
        public void ProjectApplication_GetByCustomer_ShouldReturnsOnlyProjectsFromCustomer1()
        {
            // Arrange
            var projectApp = Fixture.GetProjectAppService();
            Fixture.ProjectRepositoryMock.Setup(c => c.GetByCustomer(1)).Returns(Fixture.GetMixedProjectsByCustomer());

            // Act
            var projects = projectApp.GetByCustomer(1).ToList();

            // Assert Fluent Assertions
            projects.Should().HaveCount(c => c == 50).And.OnlyHaveUniqueItems();
        }

        [Fact(DisplayName = "Should not get projects by invalid customer")]
        [Trait("Category", "Projects Application Tests")]
        public void ProjectApplication_GetByCustomer_ShouldNotReturnProject()
        {
            // Arrange
            var projectApp = Fixture.GetProjectAppService();
            Fixture.ProjectRepositoryMock.Setup(c => c.GetByCustomer(1)).Returns(Fixture.GetMixedProjectsByCustomer());

            // Act
            var projects = projectApp.GetByCustomer(2).ToList();

            // Assert Fluent Assertions
            projects.Should().HaveCount(0);
        }

        [Fact(DisplayName = "Should not get projects by customer with customerId = 0")]
        [Trait("Category", "Projects Application Tests")]
        public void ProjectApplication_GetByCustomer_CustomerIdInvalid_ShouldNotReturnProject()
        {
            // Arrange
            var projectApp = Fixture.GetProjectAppService();
            
            // Act
            var ex = Assert.Throws<ArgumentException>(() => projectApp.GetByCustomer(0).ToList());

            // Assert Fluent Assertions
            ex.Message.Should().Be("CustomerId invalid");
        }

        [Fact(DisplayName = "Should get project by projectId")]
        [Trait("Category", "Projects Application Tests")]
        public void ProjectApplication_GetById_ShouldReturnProject()
        {
            // Arrange
            var projectApp = Fixture.GetProjectAppService();
            Fixture.ProjectRepositoryMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidProjectToQuery());

            // Act
            var project = projectApp.GetById(1);

            // Assert Fluent Assertions
            project.ProjectId.Should().Be(1);
        }

        [Fact(DisplayName = "Should not get project by invalid projectId")]
        [Trait("Category", "Projects Application Tests")]
        public void ProjectApplication_GetByIdInvalid_ShouldNotReturnProject()
        {
            // Arrange
            var projectApp = Fixture.GetProjectAppService();
            Fixture.ProjectRepositoryMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidProjectToQuery());

            // Act
            var project = projectApp.GetById(2);

            // Assert Fluent Assertions
            project.Should().BeNull();
        }

        [Fact(DisplayName = "Should project be valid")]
        [Trait("Category", "Projects Application Tests")]
        public void ProjectApplication_IsValid_ShouldReturnTrue()
        {
            // Arrange
            var CustomerFixture = new CustomersTestsFixture();
            var UserFixture = new UsersTestsFixture();

            var projectApp = Fixture.GetProjectAppService();
            var project = Fixture.GenerateValidProject();

            Fixture.CustomerAppServiceMock.Setup(c => c.GetById(1)).Returns(CustomerFixture.GenerateValidCustomerToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            EntityValidationResult validation = projectApp.IsValid(project);

            // Assert Fluent Assertions
            validation.Status.Should().BeTrue();
            validation.ValidationMessages.Should().HaveCount(c => c == 0);
        }

        [Fact(DisplayName = "Should project not be valid with invalid dates")]
        [Trait("Category", "Projects Application Tests")]
        public void ProjectApplication_IsValid_ShouldReturnFalseAndMessageInvalidDates()
        {
            var CustomerFixture = new CustomersTestsFixture();
            var UserFixture = new UsersTestsFixture();

            // Arrange
            var projectApp = Fixture.GetProjectAppService();
            var project = Fixture.GenerateProjectWithInvalidDates();

            Fixture.CustomerAppServiceMock.Setup(c => c.GetById(1)).Returns(CustomerFixture.GenerateValidCustomerToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            EntityValidationResult validation = projectApp.IsValid(project);

            // Assert Fluent Assertions
            validation.Status.Should().BeFalse();
            validation.ValidationMessages.Should().HaveCount(c => c == 1);
            validation.ValidationMessages[0].Should().Be("Finish Date must be greater than Start Date");
        }

        [Fact(DisplayName = "Should project not be valid with invalid customer")]
        [Trait("Category", "Projects Application Tests")]
        public void ProjectApplication_IsValid_ShouldReturnFalseAndMessageInvalidCustomer()
        {
            var CustomerFixture = new CustomersTestsFixture();
            var UserFixture = new UsersTestsFixture();

            // Arrange
            var projectApp = Fixture.GetProjectAppService();
            var project = Fixture.GenerateProjectWithInvalidCustomer();

            Fixture.CustomerAppServiceMock.Setup(c => c.GetById(1)).Returns(CustomerFixture.GenerateValidCustomerToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            EntityValidationResult validation = projectApp.IsValid(project);

            // Assert Fluent Assertions
            validation.Status.Should().BeFalse();
            validation.ValidationMessages.Should().HaveCount(c => c == 1);
            validation.ValidationMessages[0].Should().Be("Customer Not Found");
        }

        [Fact(DisplayName = "Should project not be valid with invalid owner")]
        [Trait("Category", "Projects Application Tests")]
        public void ProjectApplication_IsValid_ShouldReturnFalseAndMessageInvalidOwner()
        {
            var CustomerFixture = new CustomersTestsFixture();
            var UserFixture = new UsersTestsFixture();

            // Arrange
            var projectApp = Fixture.GetProjectAppService();
            var project = Fixture.GenerateProjectWithInvalidOwner();

            Fixture.CustomerAppServiceMock.Setup(c => c.GetById(1)).Returns(CustomerFixture.GenerateValidCustomerToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            EntityValidationResult validation = projectApp.IsValid(project);

            // Assert Fluent Assertions
            validation.Status.Should().BeFalse();
            validation.ValidationMessages.Should().HaveCount(c => c == 1);
            validation.ValidationMessages[0].Should().Be("Owner Not Found");
        }

        [Fact(DisplayName = "Should project not be valid when project is null")]
        [Trait("Category", "Projects Application Tests")]
        public void ProjectApplication_IsValid_ShouldThrowsArgumentException()
        {
            // Arrange
            var projectApp = Fixture.GetProjectAppService();

            // Act
            var ex = Assert.Throws<ArgumentException>(() => projectApp.IsValid(null));

            // Assert Fluent Assertions
            ex.Message.Should().Be("Project invalid");
        }

        [Fact(DisplayName = "Should insert new project")]
        [Trait("Category", "Projects Application Tests")]
        public void ProjectApplication_Add_ShouldAddProject()
        {
            // Arrange
            var projectApp = Fixture.GetProjectAppService();
            var project = Fixture.GenerateValidProject();

            // Act
            projectApp.Add(project);

            // Assert Fluent Assertions
            Fixture.ProjectRepositoryMock.Verify(repo => repo.Add(project));
        }

        [Fact(DisplayName = "Should update project")]
        [Trait("Category", "Projects Application Tests")]
        public void ProjectApplication_Update_ShouldUpdateProject()
        {
            // Arrange
            var projectApp = Fixture.GetProjectAppService();
            var project = Fixture.GenerateValidProject();

            // Act
            project.Name = "aaaaa";
            projectApp.Update(project);

            // Assert Fluent Assertions
            Fixture.ProjectRepositoryMock.Verify(repo => repo.Update(project));
        }

        [Fact(DisplayName = "Should change project status")]
        [Trait("Category", "Projects Application Tests")]
        public void ProjectApplication_ChangeStatus_ShouldUpdateStatus()
        {
            // Arrange
            var projectApp = Fixture.GetProjectAppService();
            var project = Fixture.GenerateValidProject();

            // Act
            project.ChangeStatus();
            projectApp.ChangeStatus(project);

            // Assert Fluent Assertions
            Fixture.ProjectRepositoryMock.Verify(repo => repo.ChangeStatus(project));
        }
    }
}
