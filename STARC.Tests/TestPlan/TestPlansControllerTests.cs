using FluentAssertions;
using Moq;
using Microsoft.AspNetCore.Mvc;
using STARC.Api.Models;
using STARC.Domain.Entities;
using STARC.Domain.ViewModels.TestPlan;
using System.Collections.Generic;
using Xunit;
using STARC.Domain.Models;
using STARC.Tests.Users;

namespace STARC.Tests.TestPlans
{
    [Collection(nameof(TestPlansCollection))]
    public class TestPlansControllerTests
    {
        public TestPlansTestsFixture Fixture { get; set; }

        public TestPlansControllerTests(TestPlansTestsFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact(DisplayName = "Should get test plans by project")]
        [Trait("Category", "Test Plans Controller Tests")]
        public void TestPlansController_GetByProject_ShouldReturnsAllTestPlans()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestPlansController();

            Fixture.TestPlanAppServiceMock.Setup(c => c.GetByProject(1)).Returns(Fixture.GetMixedTestPlans());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.GetByProject(1) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var testPlans = data.Data as IEnumerable<TestPlanToQueryViewModel>;
            testPlans.Should().HaveCount(c => c == 100);
        }

        [Fact(DisplayName = "Should get active test plans by project")]
        [Trait("Category", "Test Plans Controller Tests")]
        public void TestPlansController_GetActive_ShouldReturnsActiveTestPlans()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestPlansController();

            Fixture.TestPlanAppServiceMock.Setup(c => c.GetActiveByProject(1)).Returns(Fixture.GetActiveTestPlans());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.GetActiveByProject(1) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var testPlans = data.Data as IEnumerable<TestPlanToQueryViewModel>;
            testPlans.Should().HaveCount(c => c == 50);
            testPlans.Should().NotContain(c => !c.Status);
        }

        [Fact(DisplayName = "Should get test plan by testPlanId")]
        [Trait("Category", "Test Plans Controller Tests")]
        public void TestPlansController_GetById_ShouldReturnTestPlan()
        {
            // Arrange
            var controller = Fixture.GetTestPlansController();
            Fixture.TestPlanAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidTestPlanToQuery());

            // Act
            var response = controller.Get(1) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var testPlan = data.Data as TestPlanToQueryViewModel;
            testPlan.Should().NotBeNull();
            testPlan.TestPlanId.Should().Be(1);
        }

        [Fact(DisplayName = "Should not get test plan by invalid testPlanId")]
        [Trait("Category", "Test Plans Controller Tests")]
        public void TestPlansController_GetById_ShouldNotReturnTestPlan()
        {
            // Arrange
            var controller = Fixture.GetTestPlansController();
            Fixture.TestPlanAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidTestPlanToQuery());

            // Act
            var response = controller.Get(2) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Failed);
            data.Message.Should().Be("TestPlan not Found");
        }

        [Fact(DisplayName = "Should insert new test plan")]
        [Trait("Category", "Test Plans Controller Tests")]
        public void TestPlansController_Insert_ShouldAddTestPlan()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestPlansController();
            var testPlanToInsert = Fixture.GenerateValidTestPlanToInsert();

            Fixture.TestPlanAppServiceMock.Setup(c => c.Add(It.IsAny<TestPlan>())).Returns(2);
            Fixture.TestPlanAppServiceMock.Setup(c => c.IsValid(It.IsAny<TestPlan>()))
                .Returns(new EntityValidationResult
                {
                    Status = true
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Insert(testPlanToInsert) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(201);
        }

        [Fact(DisplayName = "Should not insert when test plan is null")]
        [Trait("Category", "Test Plans Controller Tests")]
        public void TestPlansController_Insert_ShouldReturnBadRequestTestPlanNull()
        {
            // Arrange
            var controller = Fixture.GetTestPlansController();

            // Act
            var response = controller.Insert(null) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("TestPlan is null");
        }

        [Fact(DisplayName = "Should not insert when test plan is not valid")]
        [Trait("Category", "Test Plans Controller Tests")]
        public void TestPlansController_Insert_ShouldReturnBadRequestNotValid()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestPlansController();
            var testPlanToInsert = Fixture.GenerateValidTestPlanToInsert();

            Fixture.TestPlanAppServiceMock.Setup(c => c.Add(It.IsAny<TestPlan>())).Returns(2);
            Fixture.TestPlanAppServiceMock.Setup(c => c.IsValid(It.IsAny<TestPlan>()))
                .Returns(new EntityValidationResult
                {
                    Status = false
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Insert(testPlanToInsert) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Validation failed, please see details");
        }

        [Fact(DisplayName = "Should update test plan")]
        [Trait("Category", "Test Plans Controller Tests")]
        public void TestPlansController_Update_ShouldUpdateTestPlan()
        {
            var UserFixture = new UsersTestsFixture();

            // Arrange
            var controller = Fixture.GetTestPlansController();
            var testPlanToUpdate = Fixture.GenerateValidTestPlanToUpdate();

            Fixture.TestPlanAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidTestPlanToQuery());
            Fixture.TestPlanAppServiceMock.Setup(c => c.IsValid(It.IsAny<TestPlan>()))
                .Returns(new EntityValidationResult
                {
                    Status = true
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Update(1, testPlanToUpdate) as NoContentResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(204);
        }

        [Fact(DisplayName = "Should not update when Id <> TestPlanId")]
        [Trait("Category", "Test Plans Controller Tests")]
        public void TestPlansController_Update_ShouldReturnBadRequestInvalidRequest()
        {
            // Arrange
            var controller = Fixture.GetTestPlansController();
            var testPlanToUpdate = Fixture.GenerateValidTestPlanToUpdate();

            // Act
            var response = controller.Update(2, testPlanToUpdate) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Invalid request");
        }

        [Fact(DisplayName = "Should not update when test plan is null")]
        [Trait("Category", "Test Plans Controller Tests")]
        public void TestPlansController_Update_ShouldReturnBadRequestInvalidRequestTestPlanNull()
        {
            // Arrange
            var controller = Fixture.GetTestPlansController();

            // Act
            var response = controller.Update(2, null) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Invalid request");
        }
        
        [Fact(DisplayName = "Should not update when test plan not found")]
        [Trait("Category", "Test Plans Controller Tests")]
        public void TestPlansController_Update_ShouldReturnTestPlanNotFound()
        {

            // Arrange
            var controller = Fixture.GetTestPlansController();
            var testPlanToUpdate = Fixture.GenerateValidTestPlanToUpdate();
            Fixture.TestPlanAppServiceMock.Setup(c => c.GetById(2)).Returns(Fixture.GenerateValidTestPlanToQuery());

            // Act
            var response = controller.Update(1, testPlanToUpdate) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(404);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("TestPlan Not Found");
        }

        [Fact(DisplayName = "Should not update when test plan is not valid")]
        [Trait("Category", "Test Plans Controller Tests")]
        public void TestPlansController_Update_ShouldReturnBadRequestNotValid()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestPlansController();
            var testPlanToUpdate = Fixture.GenerateValidTestPlanToUpdate();

            Fixture.TestPlanAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidTestPlanToQuery());
            Fixture.TestPlanAppServiceMock.Setup(c => c.IsValid(It.IsAny<TestPlan>()))
                .Returns(new EntityValidationResult
                {
                    Status = false
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Update(1, testPlanToUpdate) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Validation failed, please see details");
        }

        [Fact(DisplayName = "Should change test plan status")]
        [Trait("Category", "Test Plans Controller Tests")]
        public void TestPlansController_ChangeStatus_ShouldUpdateTestPlan()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestPlansController();

            Fixture.TestPlanAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidTestPlanToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.ChangeStatus(1) as NoContentResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(204);
        }

        [Fact(DisplayName = "Should not change status when test plan not found")]
        [Trait("Category", "Test Plans Controller Tests")]
        public void TestPlansController_ChangeStatus_ShouldReturnNotFound()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestPlansController();

            Fixture.TestPlanAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidTestPlanToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.ChangeStatus(2) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(404);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Test Plan Not Found");
        }
    }
}
