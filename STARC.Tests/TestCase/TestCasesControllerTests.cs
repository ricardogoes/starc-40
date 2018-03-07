using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using STARC.Api.Models;
using STARC.Domain.Models;
using STARC.Domain.ViewModels.TestCase;
using STARC.Tests.Users;
using System.Collections.Generic;
using Xunit;

namespace STARC.Tests.TestCase
{
    [Collection(nameof(TestCasesCollection))]
    public class TestCasesControllerTests
    {
        public TestCasesTestsFixture Fixture { get; set; }

        public TestCasesControllerTests(TestCasesTestsFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact(DisplayName = "Should get test cases by test plan")]
        [Trait("Category", "Test Cases Controller Tests")]
        public void TestCasesController_GetByTestPlan_ShouldReturnTestCases()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestCasesController();

            Fixture.TestCaseAppServiceMock.Setup(c => c.GetByTestPlan(1)).Returns(Fixture.GetMixedTestCasesByTestPlan());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.GetByTestPlan(1) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var testCases = data.Data as IEnumerable<TestCaseToQueryViewModel>;
            testCases.Should().HaveCount(c => c == 50);
        }

        [Fact(DisplayName = "Should get test case by testCaseId")]
        [Trait("Category", "Test Cases Controller Tests")]
        public void TestCasesController_GetById_ShouldReturnTestCase()
        {
            // Arrange
            var controller = Fixture.GetTestCasesController();
            Fixture.TestCaseAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidTestCaseToQuery());

            // Act
            var response = controller.Get(1) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var testPlan = data.Data as TestCaseToQueryViewModel;
            testPlan.Should().NotBeNull();
            testPlan.TestCaseId.Should().Be(1);
        }

        [Fact(DisplayName = "Should not get test case by invalid testCaseId")]
        [Trait("Category", "Test Cases Controller Tests")]
        public void TestCasesController_GetById_ShouldReturnNotFound()
        {
            // Arrange
            var controller = Fixture.GetTestCasesController();
            Fixture.TestCaseAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidTestCaseToQuery());
            
            // Act
            var response = controller.Get(2) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Failed);
            data.Message.Should().Be("Test Case not Found");
        }

        [Fact(DisplayName = "Should insert new test case")]
        [Trait("Category", "Test Cases Controller Tests")]
        public void TestCasesController_Insert_ShouldInsertNewTestCase()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestCasesController();
            var testCaseToInsert = Fixture.GenerateValidTestCaseToInsert();

            Fixture.TestCaseAppServiceMock.Setup(c => c.Add(It.IsAny<STARC.Domain.Entities.TestCase>())).Returns(2);
            Fixture.TestCaseAppServiceMock.Setup(c => c.IsValid(It.IsAny<STARC.Domain.Entities.TestCase>()))
                .Returns(new EntityValidationResult
                {
                    Status = true
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Insert(testCaseToInsert) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(201);
        }

        [Fact(DisplayName = "Should not insert when test case is null")]
        [Trait("Category", "Test Cases Controller Tests")]
        public void TestCasesController_Insert_ShouldReturnBadRequest_Null()
        {
            // Arrange
            var controller = Fixture.GetTestCasesController();

            // Act
            var response = controller.Insert(null) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Test Case is null");
        }

        [Fact(DisplayName = "Should not insert when test case is not valid")]
        [Trait("Category", "Test Cases Controller Tests")]
        public void TestCasesController_Insert_ShouldReturnBadRequest_NotValid()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestCasesController();
            var testCaseToInsert = Fixture.GenerateValidTestCaseToInsert();

            Fixture.TestCaseAppServiceMock.Setup(c => c.Add(It.IsAny<STARC.Domain.Entities.TestCase>())).Returns(2);
            Fixture.TestCaseAppServiceMock.Setup(c => c.IsValid(It.IsAny<STARC.Domain.Entities.TestCase>()))
                .Returns(new EntityValidationResult
                {
                    Status = false
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Insert(testCaseToInsert) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Validation failed, please see details");
        }

        [Fact(DisplayName = "Should update test case")]
        [Trait("Category", "Test Cases Controller Tests")]
        public void TestCasesController_Update_ShouldUpdateTestCase()
        {
            var UserFixture = new UsersTestsFixture();

            // Arrange
            var controller = Fixture.GetTestCasesController();
            var testCaseToUpdate = Fixture.GenerateValidTestCaseToUpdate();

            Fixture.TestCaseAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidTestCaseToQuery());
            Fixture.TestCaseAppServiceMock.Setup(c => c.IsValid(It.IsAny<STARC.Domain.Entities.TestCase>()))
                .Returns(new EntityValidationResult
                {
                    Status = true
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Update(1, testCaseToUpdate) as NoContentResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(204);
        }

        [Fact(DisplayName = "Should not update when test case is null")]
        [Trait("Category", "Test Cases Controller Tests")]
        public void TestCasesController_Update_ShouldReturnBadRequest_Null()
        {
            // Arrange
            var controller = Fixture.GetTestCasesController();

            // Act
            var response = controller.Update(1, null) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Invalid request");
        }

        [Fact(DisplayName = "Should not update when testCaseId <> id")]
        [Trait("Category", "Test Cases Controller Tests")]
        public void TestCasesController_Update_ShouldReturnBadRequest_InvalidId()
        {
            // Arrange
            var controller = Fixture.GetTestCasesController();
            var testCaseToUpdate = Fixture.GenerateValidTestCaseToUpdate();

            // Act
            var response = controller.Update(2, testCaseToUpdate) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Invalid request");
        }

        [Fact(DisplayName = "Should not update when test case not found")]
        [Trait("Category", "Test Cases Controller Tests")]
        public void TestCasesController_Update_ShouldReturnNotFound()
        {
            // Arrange
            var controller = Fixture.GetTestCasesController();
            var testCaseToUpdate = Fixture.GenerateValidTestCaseToUpdate();

            Fixture.TestCaseAppServiceMock.Setup(c => c.GetById(3)).Returns(Fixture.GenerateValidTestCaseToQuery());

            // Act
            var response = controller.Update(1, testCaseToUpdate) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(404);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Test Case Not Found");
        }

        [Fact(DisplayName = "Should not update when test case is not valid")]
        [Trait("Category", "Test Cases Controller Tests")]
        public void TestCasesController_Update_ShouldReturnBadRequestNotValid()
        {
            var UserFixture = new UsersTestsFixture();

            // Arrange
            var controller = Fixture.GetTestCasesController();
            var testCaseToUpdate = Fixture.GenerateValidTestCaseToUpdate();

            Fixture.TestCaseAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidTestCaseToQuery());
            Fixture.TestCaseAppServiceMock.Setup(c => c.IsValid(It.IsAny<STARC.Domain.Entities.TestCase>()))
                .Returns(new EntityValidationResult
                {
                    Status = false
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Update(1, testCaseToUpdate) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Validation failed, please see details");
        }

        [Fact(DisplayName = "Should change test case status")]
        [Trait("Category", "Test Cases Controller Tests")]
        public void TestCasesController_ChangeStatus_ShouldChangeStatus()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestCasesController();

            Fixture.TestCaseAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidTestCaseToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.ChangeStatus(1) as NoContentResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(204);
        }

        [Fact(DisplayName = "Should not change status when test case not found")]
        [Trait("Category", "Test Cases Controller Tests")]
        public void TestCasesController_ChangeStatus_ShouldReturnNotFound()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestCasesController();

            Fixture.TestCaseAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidTestCaseToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.ChangeStatus(2) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(404);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Test Case Not Found");
        }
    }
}
