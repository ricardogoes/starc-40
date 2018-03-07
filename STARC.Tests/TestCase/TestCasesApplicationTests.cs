using FluentAssertions;
using STARC.Domain.Models;
using STARC.Tests.TestPlans;
using STARC.Tests.TestSuite;
using STARC.Tests.Users;
using System.Linq;
using Xunit;

namespace STARC.Tests.TestCase
{
    [Collection(nameof(TestCasesCollection))]
    public class TestCasesApplicationTests
    {
        public TestCasesTestsFixture Fixture { get; set; }

        public TestCasesApplicationTests(TestCasesTestsFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact(DisplayName = "Should get test cases by test plan")]
        [Trait("Category", "Test Cases Application Tests")]
        public void TestCaseApplication_GetByTestPlan_ShouldReturnsTestCases()
        {
            // Arrange
            var app = Fixture.GetTestCaseAppService();
            Fixture.TestCaseRepositoryMock.Setup(c => c.GetByTestPlan(1)).Returns(Fixture.GetMixedTestCasesByTestPlan());

            // Act
            var testPlans = app.GetByTestPlan(1).ToList();

            // Assert Fluent Assertions
            testPlans.Should().HaveCount(c => c > 1).And.OnlyHaveUniqueItems();
        }

        [Fact(DisplayName = "Should get test case by testCaseId")]
        [Trait("Category", "Test Cases Application Tests")]
        public void TestCaseApplication_GetById_ShouldReturnTestCase()
        {
            // Arrange
            var app = Fixture.GetTestCaseAppService();
            Fixture.TestCaseRepositoryMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidTestCaseToQuery());

            // Act
            var testPlan = app.GetById(1);

            // Assert Fluent Assertions
            testPlan.TestCaseId.Should().Be(1);
        }

        [Fact(DisplayName = "Should not get test case by invalid testCaseId")]
        [Trait("Category", "Test Cases Application Tests")]
        public void TestCaseApplication_GetById_ShouldNotReturnTestCase()
        {
            // Arrange
            var app = Fixture.GetTestCaseAppService();
            Fixture.TestCaseRepositoryMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidTestCaseToQuery());

            // Act
            var testPlan = app.GetById(2);

            // Assert Fluent Assertions
            testPlan.Should().BeNull();
        }

        [Fact(DisplayName = "Should test case be valid")]
        [Trait("Category", "Test Cases Application Tests")]
        public void TestCaseApplication_IsValid_ShouldReturnTrue()
        {
            // Arrange
            var TestSuiteFixture = new TestSuitesTestsFixture();
            var UserFixture = new UsersTestsFixture();

            var app = Fixture.GetTestCaseAppService();
            var testCase = Fixture.GenerateValidTestCase();

            Fixture.TestSuiteAppServiceMock.Setup(c => c.GetById(1)).Returns(TestSuiteFixture.GenerateValidTestSuiteToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            EntityValidationResult validation = app.IsValid(testCase);

            // Assert Fluent Assertions
            validation.Status.Should().BeTrue();
            validation.ValidationMessages.Should().HaveCount(c => c == 0);
        }

        [Fact(DisplayName = "Should test case be invalid with invalid test Suite")]
        [Trait("Category", "Test Cases Application Tests")]
        public void TestCaseApplication_IsValid_ShouldReturnFalseAndMessageInvalidCustomer()
        {
            var TestSuiteFixture = new TestSuitesTestsFixture();
            var UserFixture = new UsersTestsFixture();

            // Arrange
            var app = Fixture.GetTestCaseAppService();
            var testCase = Fixture.GenerateTestCaseWithInvalidTestSuite();

            Fixture.TestSuiteAppServiceMock.Setup(c => c.GetById(1)).Returns(TestSuiteFixture.GenerateValidTestSuiteToQuery());
            Fixture.UserAppServiceMock.Setup(c => c.GetById(1)).Returns(UserFixture.GenerateValidUserToQuery());

            // Act
            EntityValidationResult validation = app.IsValid(testCase);

            // Assert Fluent Assertions
            validation.Status.Should().BeFalse();
            validation.ValidationMessages.Should().HaveCount(c => c == 1);
            validation.ValidationMessages[0].Should().Be("Test Suite Not Found");
        }

        [Fact(DisplayName = "Should add new test case")]
        [Trait("Category", "Test Cases Application Tests")]
        public void TestCaseApplication_Add_ShouldAddTestCase()
        {
            // Arrange
            var app = Fixture.GetTestCaseAppService();
            var testCase = Fixture.GenerateValidTestCase();

            // Act
            app.Add(testCase);

            // Assert Fluent Assertions
            Fixture.TestCaseRepositoryMock.Verify(repo => repo.Add(testCase));
        }

        [Fact(DisplayName = "Should update test case")]
        [Trait("Category", "Test Cases Application Tests")]
        public void TestCaseApplication_Update_ShouldUpdateTestCase()
        {
            // Arrange
            var app = Fixture.GetTestCaseAppService();
            var testCase = Fixture.GenerateValidTestCase();

            // Act
            testCase.Name = "aaaaa";
            app.Update(testCase);

            // Assert Fluent Assertions
            Fixture.TestCaseRepositoryMock.Verify(repo => repo.Update(testCase));
        }

        [Fact(DisplayName = "Should change test case status")]
        [Trait("Category", "Test Cases Application Tests")]
        public void TestCaseApplication_ChangeStatus_ShouldUpdateStatus()
        {
            // Arrange
            var app = Fixture.GetTestCaseAppService();
            var testCase = Fixture.GenerateValidTestCase();

            // Act
            testCase.ChangeStatus();
            app.ChangeStatus(testCase);

            // Assert Fluent Assertions
            Fixture.TestCaseRepositoryMock.Verify(repo => repo.ChangeStatus(testCase));
        }
    }
}
