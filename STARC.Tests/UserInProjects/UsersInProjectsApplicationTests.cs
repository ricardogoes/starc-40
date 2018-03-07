using FluentAssertions;
using STARC.Domain.Models;
using STARC.Tests.Projects;
using STARC.Tests.Users;
using System;
using System.Linq;
using Xunit;

namespace STARC.Tests.UserInProjects
{
    [Collection(nameof(UsersInProjectsCollection))]
    public class UsersInProjectsAppTests
    {
        public UsersInProjectsTestsFixture Fixture { get; set; }

        public UsersInProjectsAppTests(UsersInProjectsTestsFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact(DisplayName = "Should add new user in project")]
        [Trait("Category", "Users In Projects Application Tests")]
        public void UsersInProjectsApplication_Add_ShouldAddUser()
        {
            // Arrange
            var userApp = Fixture.GetUsersInProjectsAppService();
            var user = Fixture.GenerateValidUsersInProjects();

            // Act
            userApp.Add(user);

            // Assert Fluent Assertions
            Fixture.UsersInProjectsRepositoryMock.Verify(repo => repo.Add(user));
        }

        [Fact(DisplayName = "Should delete user in project")]
        [Trait("Category", "Users In Projects Application Tests")]
        public void UsersInProjectsApplication_Delete_ShouldDeleteUser()
        {
            // Arrange
            var userApp = Fixture.GetUsersInProjectsAppService();
            var user = Fixture.GenerateValidUsersInProjects();

            // Act
            userApp.Delete(user.UserInProjectId);

            // Assert Fluent Assertions
            Fixture.UsersInProjectsRepositoryMock.Verify(repo => repo.Delete(user.UserInProjectId));
        }

        [Fact(DisplayName = "Should get users in projects by customer")]
        [Trait("Category", "Users In Projects Application Tests")]
        public void UsersInProjectsApplication_GetByCustomer_ShouldReturnsOnlyUsersFromCustomer1()
        {
            // Arrange
            var userApp = Fixture.GetUsersInProjectsAppService();
            Fixture.UsersInProjectsRepositoryMock.Setup(c => c.GetByCustomer(1)).Returns(Fixture.GenerateUsersInProjects(50));

            // Act
            var users = userApp.GetByCustomer(1).ToList();

            // Assert Fluent Assertions
            users.Should().HaveCount(c => c == 50).And.OnlyHaveUniqueItems();
        }

        [Fact(DisplayName = "Should not get users in projects by invalid customer")]
        [Trait("Category", "Users In Projects Application Tests")]
        public void UsersInProjectsApplication_GetByCustomer_ShouldNotReturnUser()
        {
            // Arrange
            var userApp = Fixture.GetUsersInProjectsAppService();
            Fixture.UsersInProjectsRepositoryMock.Setup(c => c.GetByCustomer(1)).Returns(Fixture.GenerateUsersInProjects(50));

            // Act
            var users = userApp.GetByCustomer(2).ToList();

            // Assert Fluent Assertions
            users.Should().HaveCount(0);
        }

        [Fact(DisplayName = "Should not get users in projects by customer when customerId = 0")]
        [Trait("Category", "Users In Projects Application Tests")]
        public void UsersInProjectsApplication_GetByCustomer_ShouldThrowsArgumentException()
        {
            // Arrange
            var userApp = Fixture.GetUsersInProjectsAppService();

            // Act
            var ex = Assert.Throws<ArgumentException>(() => userApp.GetByCustomer(0).ToList());

            // Assert Fluent Assertions
            ex.Message.Should().Be("CustomerId invalid");
        }

        [Fact(DisplayName = "Should get users in projects by id")]
        [Trait("Category", "Users In Projects Application Tests")]
        public void UsersInProjectsApplication_GetById_ShouldReturnUser()
        {
            // Arrange
            var userApp = Fixture.GetUsersInProjectsAppService();
            Fixture.UsersInProjectsRepositoryMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidUsersInProjectsToQuery());

            // Act
            var user = userApp.GetById(1);

            // Assert Fluent Assertions
            user.UserInProjectId.Should().Be(1);
        }

        [Fact(DisplayName = "Should not get users in projects by invalid id")]
        [Trait("Category", "Users In Projects Application Tests")]
        public void UsersInProjectsApplication_GetByIdInvalid_ShouldNotReturnUser()
        {
            // Arrange
            var userApp = Fixture.GetUsersInProjectsAppService();
            Fixture.UsersInProjectsRepositoryMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidUsersInProjectsToQuery());

            // Act
            var user = userApp.GetById(2);

            // Assert Fluent Assertions
            user.Should().BeNull();
        }

        [Fact(DisplayName = "Should get users in projects by project")]
        [Trait("Category", "Users In Projects Application Tests")]
        public void UsersInProjectsApplication_GetByProject_ShouldReturnsOnlyUsersFromProject1()
        {
            // Arrange
            var userApp = Fixture.GetUsersInProjectsAppService();
            Fixture.UsersInProjectsRepositoryMock.Setup(c => c.GetByProject(1)).Returns(Fixture.GenerateUsersInProjects(50));

            // Act
            var users = userApp.GetByProject(1).ToList();

            // Assert Fluent Assertions
            users.Should().HaveCount(c => c == 50).And.OnlyHaveUniqueItems();
        }

        [Fact(DisplayName = "Should not get users in projects by invalid project")]
        [Trait("Category", "Users In Projects Application Tests")]
        public void UsersInProjectsApplication_GetByProject_ShouldNotReturnUser()
        {
            // Arrange
            var userApp = Fixture.GetUsersInProjectsAppService();
            Fixture.UsersInProjectsRepositoryMock.Setup(c => c.GetByProject(1)).Returns(Fixture.GenerateUsersInProjects(50));

            // Act
            var users = userApp.GetByProject(2).ToList();

            // Assert Fluent Assertions
            users.Should().HaveCount(0);
        }

        [Fact(DisplayName = "Should not get users in projects when projectId = 0")]
        [Trait("Category", "Users In Projects Application Tests")]
        public void UsersInProjectsApplication_GetByProject_ShouldThrowsArgumentException()
        {
            // Arrange
            var userApp = Fixture.GetUsersInProjectsAppService();

            // Act
            var ex = Assert.Throws<ArgumentException>(() => userApp.GetByProject(0).ToList());

            // Assert Fluent Assertions
            ex.Message.Should().Be("ProjectId invalid");
        }

        [Fact(DisplayName = "Should get users in projects by user")]
        [Trait("Category", "Users In Projects Application Tests")]
        public void UsersInProjectsApplication_GetByUser_ShouldReturnsOnlyUsersFromUser1()
        {
            // Arrange
            var userApp = Fixture.GetUsersInProjectsAppService();
            Fixture.UsersInProjectsRepositoryMock.Setup(c => c.GetByUser(1)).Returns(Fixture.GenerateUsersInProjects(50));

            // Act
            var users = userApp.GetByUser(1).ToList();

            // Assert Fluent Assertions
            users.Should().HaveCount(c => c == 50).And.OnlyHaveUniqueItems();
        }

        [Fact(DisplayName = "Should not get users in projects by invalid user")]
        [Trait("Category", "Users In Projects Application Tests")]
        public void UsersInProjectsApplication_GetByUser_ShouldNotReturnUser()
        {
            // Arrange
            var userApp = Fixture.GetUsersInProjectsAppService();
            Fixture.UsersInProjectsRepositoryMock.Setup(c => c.GetByUser(1)).Returns(Fixture.GenerateUsersInProjects(50));

            // Act
            var users = userApp.GetByUser(2).ToList();

            // Assert Fluent Assertions
            users.Should().HaveCount(0);
        }

        [Fact(DisplayName = "Should not get users in projects when userId = 0")]
        [Trait("Category", "Users In Projects Application Tests")]
        public void UsersInProjectsApplication_GetByUser_ShouldThrowsArgumentException()
        {
            // Arrange
            var userApp = Fixture.GetUsersInProjectsAppService();

            // Act
            var ex = Assert.Throws<ArgumentException>(() => userApp.GetByUser(0).ToList());

            // Assert Fluent Assertions
            ex.Message.Should().Be("UserId invalid");
        }

        [Fact(DisplayName = "Should userId and projectId is unique false with duplicated date")]
        [Trait("Category", "Users In Projects Application Tests")]
        public void UserInProjectsApplication_IsUserIdAndProjectIdUnique_ShouldReturnFalse()
        {
            // Arrange
            var userApp = Fixture.GetUsersInProjectsAppService();
            Fixture.UsersInProjectsRepositoryMock.Setup(c => c.GetByUserAndProject(1, 1)).Returns(Fixture.GenerateValidUsersInProjectsToQuery());

            // Act
            var isUserIdAndPasswordUnique = userApp.IsUserAndProjectUnique(1, 1);

            // Assert Fluent Assertions
            isUserIdAndPasswordUnique.Should().BeFalse();
        }

        [Fact(DisplayName = "Should userId and projectId be unique")]
        [Trait("Category", "Users In Projects Application Tests")]
        public void UserInProjectsApplication_IsUserIdAndProjectIdUnique_ShouldReturnTrue()
        {
            // Arrange
            var userApp = Fixture.GetUsersInProjectsAppService();
            Fixture.UsersInProjectsRepositoryMock.Setup(c => c.GetByUserAndProject(1,1)).Returns(Fixture.GenerateValidUsersInProjectsToQuery());

            // Act
            var isUserIdAndPasswordUnique = userApp.IsUserAndProjectUnique(1, 2);

            // Assert Fluent Assertions
            isUserIdAndPasswordUnique.Should().BeTrue();
        }        

        [Fact(DisplayName = "Should user in project be valid")]
        [Trait("Category", "Users In Projects Application Tests")]
        public void UsersInProjectsApplication_IsValid_ShouldReturnTrue()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var ProjectFixture = new ProjectsTestsFixture();

            var userApp = Fixture.GetUsersInProjectsAppService();
            var user = Fixture.GenerateValidUsersInProjects();

            Fixture.UserAppServiceMock.Setup(c => c.GetById(2)).Returns(UserFixture.GenerateValidUserToQuery());
            Fixture.ProjectAppServiceMock.Setup(c => c.GetById(1)).Returns(ProjectFixture.GenerateValidProjectToQuery());

            // Act
            EntityValidationResult validation = userApp.IsValid(user);

            // Assert Fluent Assertions
            validation.Status.Should().BeTrue();
            validation.ValidationMessages.Should().HaveCount(c => c == 0);
        }

        [Fact(DisplayName = "Should user in project not be valid with duplicated data")]
        [Trait("Category", "Users In Projects Application Tests")]
        public void UsersInProjectsApplication_IsValid_Duplicated_ShouldReturnFalse()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var ProjectFixture = new ProjectsTestsFixture();

            var userApp = Fixture.GetUsersInProjectsAppService();
            var user = Fixture.GenerateValidUsersInProjects();

            Fixture.UsersInProjectsRepositoryMock.Setup(c => c.GetByUserAndProject(2, 1)).Returns(Fixture.GenerateValidUsersInProjectsToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(2)).Returns(UserFixture.GenerateValidUserToQuery());
            Fixture.ProjectAppServiceMock.Setup(c => c.GetById(1)).Returns(ProjectFixture.GenerateValidProjectToQuery());

            // Act
            EntityValidationResult validation = userApp.IsValid(user);

            // Assert Fluent Assertions
            validation.Status.Should().BeFalse();
            validation.ValidationMessages.Should().HaveCount(c => c == 1);
            validation.ValidationMessages[0].Should().Be("User already associate to project");
        }

        [Fact(DisplayName = "Should user in project not be valid with invalid user")]
        [Trait("Category", "Users In Projects Application Tests")]
        public void UsersInProjectsApplication_IsValid_InvalidUserId_ShouldReturnFalse()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var ProjectFixture = new ProjectsTestsFixture();

            var userApp = Fixture.GetUsersInProjectsAppService();
            var user = Fixture.GenerateUsersInProjectsWithInvalidUser();

            Fixture.UserAppServiceMock.Setup(c => c.GetById(2)).Returns(UserFixture.GenerateValidUserToQuery());
            Fixture.ProjectAppServiceMock.Setup(c => c.GetById(1)).Returns(ProjectFixture.GenerateValidProjectToQuery());

            // Act
            EntityValidationResult validation = userApp.IsValid(user);

            // Assert Fluent Assertions
            validation.Status.Should().BeFalse();
            validation.ValidationMessages.Should().HaveCount(c => c == 1);
            validation.ValidationMessages[0].Should().Be("User Not Found");
        }

        [Fact(DisplayName = "Should user in project not be valid with invalid project")]
        [Trait("Category", "Users In Projects Application Tests")]
        public void UsersInProjectsApplication_IsValid_InvalidProjectId_ShouldReturnFalse()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var ProjectFixture = new ProjectsTestsFixture();

            var userApp = Fixture.GetUsersInProjectsAppService();
            var user = Fixture.GenerateUsersInProjectsWithInvalidProject();

            Fixture.UserAppServiceMock.Setup(c => c.GetById(2)).Returns(UserFixture.GenerateValidUserToQuery());
            Fixture.ProjectAppServiceMock.Setup(c => c.GetById(1)).Returns(ProjectFixture.GenerateValidProjectToQuery());

            // Act
            EntityValidationResult validation = userApp.IsValid(user);

            // Assert Fluent Assertions
            validation.Status.Should().BeFalse();
            validation.ValidationMessages.Should().HaveCount(c => c == 1);
            validation.ValidationMessages[0].Should().Be("Project Not Found");
        }
    }
}
