using FluentAssertions;
using STARC.Domain.Models;
using STARC.Tests.Projects;
using STARC.Tests.Users;
using System;
using System.Linq;
using Xunit;

namespace STARC.Tests.TestPlans
{
    [Collection(nameof(TestPlansCollection))]
    public class TestPlanApplicationTests
    {
        public TestPlansTestsFixture Fixture { get; set; }
        
        public TestPlanApplicationTests(TestPlansTestsFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact(DisplayName = "Should get test plans by project")]
        [Trait("Category", "Test Plans Application Tests")]
        public void TestPlanApplication_GetByProject_ShouldReturnsAllTestPlan()
        {
            // Arrange
            var testPlanApp = Fixture.GetTestPlanAppService();
            Fixture.TestPlanRepositoryMock.Setup(c => c.GetByProject(1)).Returns(Fixture.GetMixedTestPlans());

            // Act
            var testPlans = testPlanApp.GetByProject(1).ToList();

            // Assert Fluent Assertions
            testPlans.Should().HaveCount(c => c > 1).And.OnlyHaveUniqueItems();
        }

        [Fact(DisplayName = "Should get active test plans by project")]
        [Trait("Category", "Test Plans Application Tests")]
        public void TestPlanApplication_GetActiveByProject_ShouldReturnsOnlyActiveTestPlan()
        {
            // Arrange
            var testPlanApp = Fixture.GetTestPlanAppService();
            Fixture.TestPlanRepositoryMock.Setup(c => c.GetActiveByProject(1)).Returns(Fixture.GetActiveTestPlans());

            // Act
            var testPlans = testPlanApp.GetActiveByProject(1).ToList();

            // Assert Fluent Assertions
            testPlans.Should().HaveCount(c => c > 1).And.OnlyHaveUniqueItems();
            testPlans.Should().NotContain(c => !c.Status);
        }

        [Fact(DisplayName = "Should get test plan by testPlanId")]
        [Trait("Category", "Test Plans Application Tests")]
        public void TestPlanApplication_GetById_ShouldReturnTestPlan()
        {
            // Arrange
            var testPlanApp = Fixture.GetTestPlanAppService();
            Fixture.TestPlanRepositoryMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidTestPlanToQuery());

            // Act
            var testPlan = testPlanApp.GetById(1);

            // Assert Fluent Assertions
            testPlan.TestPlanId.Should().Be(1);
        }

        [Fact(DisplayName = "Should not get test plan by invalid testPlanId")]
        [Trait("Category", "Test Plans Application Tests")]
        public void TestPlanApplication_GetByIdInvalid_ShouldNotReturnTestPlan()
        {
            // Arrange
            var testPlanApp = Fixture.GetTestPlanAppService();
            Fixture.TestPlanRepositoryMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidTestPlanToQuery());

            // Act
            var testPlan = testPlanApp.GetById(2);

            // Assert Fluent Assertions
            testPlan.Should().BeNull();
        }

        [Fact(DisplayName = "Should test plan be valid")]
        [Trait("Category", "Test Plans Application Tests")]
        public void TestPlanApplication_IsValid_ShouldReturnTrue()
        {
            // Arrange
            var ProjectFixture = new ProjectsTestsFixture();
            var UserFixture = new UsersTestsFixture();

            var testPlanApp = Fixture.GetTestPlanAppService();
            var testPlan = Fixture.GenerateValidTestPlan();

            Fixture.ProjectAppServiceMock.Setup(c => c.GetById(1)).Returns(ProjectFixture.GenerateValidProjectToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            EntityValidationResult validation = testPlanApp.IsValid(testPlan);

            // Assert Fluent Assertions
            validation.Status.Should().BeTrue();
            validation.ValidationMessages.Should().HaveCount(c => c == 0);
        }

        [Fact(DisplayName = "Should test plan not be valid with invalid dates")]
        [Trait("Category", "Test Plans Application Tests")]
        public void TestPlanApplication_IsValid_ShouldReturnFalseAndMessageInvalidDates()
        {
            var ProjectFixture = new ProjectsTestsFixture();
            var UserFixture = new UsersTestsFixture();

            // Arrange
            var testPlanApp = Fixture.GetTestPlanAppService();
            var testPlan = Fixture.GenerateTestPlanWithInvalidDates();

            Fixture.ProjectAppServiceMock.Setup(c => c.GetById(1)).Returns(ProjectFixture.GenerateValidProjectToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            EntityValidationResult validation = testPlanApp.IsValid(testPlan);

            // Assert Fluent Assertions
            validation.Status.Should().BeFalse();
            validation.ValidationMessages.Should().HaveCount(c => c == 1);
            validation.ValidationMessages[0].Should().Be("Finish Date must be greater than Start Date");
        }

        [Fact(DisplayName = "Should test plan not be valid with invalid project")]
        [Trait("Category", "Test Plans Application Tests")]
        public void TestPlanApplication_IsValid_ShouldReturnFalseAndMessageInvalidCustomer()
        {
            var ProjectFixture = new ProjectsTestsFixture();
            var UserFixture = new UsersTestsFixture();

            // Arrange
            var testPlanApp = Fixture.GetTestPlanAppService();
            var testPlan = Fixture.GenerateTestPlanWithInvalidProject();

            Fixture.ProjectAppServiceMock.Setup(c => c.GetById(1)).Returns(ProjectFixture.GenerateValidProjectToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            EntityValidationResult validation = testPlanApp.IsValid(testPlan);

            // Assert Fluent Assertions
            validation.Status.Should().BeFalse();
            validation.ValidationMessages.Should().HaveCount(c => c == 1);
            validation.ValidationMessages[0].Should().Be("Project Not Found");
        }

        [Fact(DisplayName = "Should test plan not be valid with invalid owner")]
        [Trait("Category", "Test Plans Application Tests")]
        public void TestPlanApplication_IsValid_ShouldReturnFalseAndMessageInvalidOwner()
        {
            var ProjectFixture = new ProjectsTestsFixture();
            var UserFixture = new UsersTestsFixture();

            // Arrange
            var testPlanApp = Fixture.GetTestPlanAppService();
            var testPlan = Fixture.GenerateTestPlanWithInvalidOwner();

            Fixture.ProjectAppServiceMock.Setup(c => c.GetById(1)).Returns(ProjectFixture.GenerateValidProjectToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            EntityValidationResult validation = testPlanApp.IsValid(testPlan);

            // Assert Fluent Assertions
            validation.Status.Should().BeFalse();
            validation.ValidationMessages.Should().HaveCount(c => c == 1);
            validation.ValidationMessages[0].Should().Be("Owner Not Found");
        }

        [Fact(DisplayName = "Should test plan not be valid when test plan is null")]
        [Trait("Category", "Test Plans Application Tests")]
        public void TestPlanApplication_IsValid_ShouldThrowsArgumentException()
        {
            // Arrange
            var testPlanApp = Fixture.GetTestPlanAppService();

            // Act
            var ex = Assert.Throws<ArgumentException>(() => testPlanApp.IsValid(null));

            // Assert Fluent Assertions
            ex.Message.Should().Be("Test Plan invalid");
        }

        [Fact(DisplayName = "Should add new test plan")]
        [Trait("Category", "Test Plans Application Tests")]
        public void TestPlanApplication_Add_ShouldAddTestPlan()
        {
            // Arrange
            var testPlanApp = Fixture.GetTestPlanAppService();
            var testPlan = Fixture.GenerateValidTestPlan();

            // Act
            testPlanApp.Add(testPlan);

            // Assert Fluent Assertions
            Fixture.TestPlanRepositoryMock.Verify(repo => repo.Add(testPlan));
        }

        [Fact(DisplayName = "Should update test plan")]
        [Trait("Category", "Test Plans Application Tests")]
        public void TestPlanApplication_Update_ShouldUpdateTestPlan()
        {
            // Arrange
            var testPlanApp = Fixture.GetTestPlanAppService();
            var testPlan = Fixture.GenerateValidTestPlan();

            // Act
            testPlan.Name = "aaaaa";
            testPlanApp.Update(testPlan);

            // Assert Fluent Assertions
            Fixture.TestPlanRepositoryMock.Verify(repo => repo.Update(testPlan));
        }

        [Fact(DisplayName = "Should change test plan status")]
        [Trait("Category", "Test Plans Application Tests")]
        public void TestPlanApplication_ChangeStatus_ShouldUpdateStatus()
        {
            // Arrange
            var testPlanApp = Fixture.GetTestPlanAppService();
            var testPlan = Fixture.GenerateValidTestPlan();

            // Act
            testPlan.ChangeStatus();
            testPlanApp.ChangeStatus(testPlan);

            // Assert Fluent Assertions
            Fixture.TestPlanRepositoryMock.Verify(repo => repo.ChangeStatus(testPlan));
        }
    }
}
