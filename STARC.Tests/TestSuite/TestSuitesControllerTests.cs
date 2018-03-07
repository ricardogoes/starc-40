using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using STARC.Api.Models;
using STARC.Domain.Entities;
using STARC.Domain.Models;
using STARC.Domain.ViewModels.TestSuite;
using STARC.Tests.Users;
using Xunit;

namespace STARC.Tests.TestSuite
{
    [Collection(nameof(TestSuitesCollection))]
    public class TestSuitesControllerTests
    {
        public TestSuitesTestsFixture Fixture { get; set; }

        public TestSuitesControllerTests(TestSuitesTestsFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact(DisplayName = "Should get test suite by testSuiteId")]
        [Trait("Category", "Test Suites Controller Tests")]
        public void TestSuite_GetById_ShouldReturnTestSuite()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestSuitesController();

            Fixture.TestSuiteAppServiceMock.Setup(c => c.GetById(2)).Returns(Fixture.GenerateValidTestSuiteToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Get(2) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert
            response.StatusCode.Should().Be(200);
            var testSuite = data.Data as TestSuiteToQueryViewModel;
            testSuite.Should().NotBeNull();
            testSuite.TestSuiteId.Should().Be(1);
        }

        [Fact(DisplayName = "Should not get test suite by invalid testSuiteId")]
        [Trait("Category", "Test Suites Controller Tests")]
        public void TestSuite_GetById_ShouldReturnNotFound()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestSuitesController();

            Fixture.TestSuiteAppServiceMock.Setup(c => c.GetById(2)).Returns(Fixture.GenerateValidTestSuiteToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Get(3) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert
            response.StatusCode.Should().Be(404);
            data.Message.Should().Be("Test Suite not Found");
        }

        [Fact(DisplayName = "Should insert new test suite")]
        [Trait("Category", "Test Suites Controller Tests")]
        public void TestSuite_Insert_ShouldInsertNewTestSuite()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestSuitesController();
            var testSuiteToInsert = Fixture.GenerateValidTestSuiteToInsert();

            Fixture.TestSuiteAppServiceMock.Setup(c => c.Add(It.IsAny<STARC.Domain.Entities.TestSuite>())).Returns(2);
            Fixture.TestSuiteAppServiceMock.Setup(c => c.IsValid(It.IsAny<STARC.Domain.Entities.TestSuite>()))
                .Returns(new EntityValidationResult
                {
                    Status = true
                });
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Insert(testSuiteToInsert) as ObjectResult;

            // Assert
            response.StatusCode.Should().Be(201);
        }

        [Fact(DisplayName = "Should not insert when test suite is null")]
        [Trait("Category", "Test Suites Controller Tests")]
        public void TestSuite_Insert_ShouldReturnBadRequest_TestSuiteNull()
        {
            // Arrange
            var controller = Fixture.GetTestSuitesController();
            
            // Act
            var response = controller.Insert(null) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Test Suite is null");
        }

        [Fact(DisplayName = "Should not insert with invalid test plan")]
        [Trait("Category", "Test Suites Controller Tests")]
        public void TestSuite_Insert_ShouldReturnBadRequest_InvalidTestPlan()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestSuitesController();
            var testSuiteToInsert = Fixture.GenerateValidTestSuiteToInsert();

            Fixture.TestSuiteAppServiceMock.Setup(c => c.Add(It.IsAny<STARC.Domain.Entities.TestSuite>())).Returns(2);
            Fixture.TestSuiteAppServiceMock.Setup(c => c.IsValid(It.IsAny<STARC.Domain.Entities.TestSuite>()))
                .Returns(new EntityValidationResult
                {
                    Status = false
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Insert(testSuiteToInsert) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Validation failed, please see details");
        }

        [Fact(DisplayName = "Should update test suite")]
        [Trait("Category", "Test Suites Controller Tests")]
        public void TestSuite_Update_ShouldUpdateTestSuite()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestSuitesController();
            var testSuiteToUpdate = Fixture.GenerateValidTestSuiteToUpdate();

            Fixture.TestSuiteAppServiceMock.Setup(c => c.GetById(2)).Returns(Fixture.GenerateValidTestSuiteToQuery());
            Fixture.TestSuiteAppServiceMock.Setup(c => c.IsValid(It.IsAny<STARC.Domain.Entities.TestSuite>()))
                .Returns(new EntityValidationResult
                {
                    Status = true
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Update(2, testSuiteToUpdate) as NoContentResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(204);
        }

        [Fact(DisplayName = "Should not update when test suite is null")]
        [Trait("Category", "Test Suites Controller Tests")]
        public void TestSuite_Update_ShouldReturnBadRequest_TestSuiteNull()
        {
            // Arrange
            var controller = Fixture.GetTestSuitesController();

            // Act
            var response = controller.Update(2, null) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Invalid request");
        }

        [Fact(DisplayName = "Should not update when Id <> TestSuiteId")]
        [Trait("Category", "Test Suites Controller Tests")]
        public void TestSuite_Update_ShouldReturnBadRequest_Id()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestSuitesController();
            var testSuiteToUpdate = Fixture.GenerateValidTestSuiteToUpdate();

            Fixture.TestSuiteAppServiceMock.Setup(c => c.GetById(2)).Returns(Fixture.GenerateValidTestSuiteToQuery());
            Fixture.TestSuiteAppServiceMock.Setup(c => c.IsValid(It.IsAny<STARC.Domain.Entities.TestSuite>()))
                .Returns(new EntityValidationResult
                {
                    Status = true
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Update(1, testSuiteToUpdate) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Invalid request");
        }

        [Fact(DisplayName = "Should not update when test suite not found")]
        [Trait("Category", "Test Suites Controller Tests")]
        public void TestSuite_Update_ShouldReturnNotFound()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestSuitesController();
            var testSuiteToUpdate = Fixture.GenerateValidTestSuiteToUpdate();

            Fixture.TestSuiteAppServiceMock.Setup(c => c.GetById(3)).Returns(Fixture.GenerateValidTestSuiteToQuery());
            Fixture.TestSuiteAppServiceMock.Setup(c => c.IsValid(It.IsAny<STARC.Domain.Entities.TestSuite>()))
                .Returns(new EntityValidationResult
                {
                    Status = true
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Update(2, testSuiteToUpdate) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(404);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Test Suite Not Found");
        }

        [Fact(DisplayName = "Should not update when test suite is not valid")]
        [Trait("Category", "Test Suites Controller Tests")]
        public void TestSuite_Update_ShouldReturnBadRequest_NotValid()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestSuitesController();
            var testSuiteToUpdate = Fixture.GenerateValidTestSuiteToUpdate();

            Fixture.TestSuiteAppServiceMock.Setup(c => c.GetById(2)).Returns(Fixture.GenerateValidTestSuiteToQuery());
            Fixture.TestSuiteAppServiceMock.Setup(c => c.IsValid(It.IsAny<STARC.Domain.Entities.TestSuite>()))
                .Returns(new EntityValidationResult
                {
                    Status = false
                });

            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Update(2, testSuiteToUpdate) as ObjectResult;

            // Assert Fluent Assertions
            response.StatusCode.Should().Be(400);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Validation failed, please see details");
        }

        [Fact(DisplayName = "Should delete test suite")]
        [Trait("Category", "Test Suites Controller Tests")]
        public void TestSuite_Delete_ShouldDeleteTestSuite()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestSuitesController();

            Fixture.TestSuiteAppServiceMock.Setup(c => c.GetById(2)).Returns(Fixture.GenerateValidTestSuiteToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Delete(2) as NoContentResult;

            // Assert
            response.StatusCode.Should().Be(204);
        }

        [Fact(DisplayName = "Should not delete when test suite not found")]
        [Trait("Category", "Test Suites Controller Tests")]
        public void TestSuite_Delete_ShouldReturnNotFound()
        {
            // Arrange
            var UserFixture = new UsersTestsFixture();
            var controller = Fixture.GetTestSuitesController();

            Fixture.TestSuiteAppServiceMock.Setup(c => c.GetById(2)).Returns(Fixture.GenerateValidTestSuiteToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            var response = controller.Delete(3) as ObjectResult;            

            // Assert
            response.StatusCode.Should().Be(404);
            var apiResponse = response.Value as ApiResponse;
            apiResponse.Message.Should().Be("Test Suite Not Found");
        }
    }
}
