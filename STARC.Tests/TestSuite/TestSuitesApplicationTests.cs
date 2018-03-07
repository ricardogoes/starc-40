using FluentAssertions;
using STARC.Domain.Models;
using STARC.Tests.TestPlans;
using Xunit;

namespace STARC.Tests.TestSuite
{
    [Collection(nameof(TestSuitesCollection))]
    public class TestSuitesApplicationTests
    {
        public TestSuitesTestsFixture Fixture { get; set; }

        public TestSuitesApplicationTests(TestSuitesTestsFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact(DisplayName = "Should add new test suite")]
        [Trait("Category", "Test Suites Application Tests")]
        public void TestSuiteApplication_Add_ShouldAddTestPlan()
        {
            // Arrange
            var app = Fixture.GetTestSuiteAppService();
            var testSuite = Fixture.GenerateValidTestSuite();

            // Act
            app.Add(testSuite);

            // Assert Fluent Assertions
            Fixture.TestSuiteRepositoryMock.Verify(repo => repo.Add(testSuite));
        }

        [Fact(DisplayName = "Should update test suite")]
        [Trait("Category", "Test Suites Application Tests")]
        public void TestSuiteApplication_Update_ShouldUpdateTestPlan()
        {
            // Arrange
            var app = Fixture.GetTestSuiteAppService();
            var testSuite = Fixture.GenerateValidTestSuite();

            // Act
            testSuite.Name = "aaaaa";
            app.Update(testSuite);

            // Assert Fluent Assertions
            Fixture.TestSuiteRepositoryMock.Verify(repo => repo.Update(testSuite));
        }

        [Fact(DisplayName = "Should delete test suite")]
        [Trait("Category", "Test Suites Application Tests")]
        public void TestSuiteApplication_ChangeStatus_ShouldUpdateStatus()
        {
            // Arrange
            var app = Fixture.GetTestSuiteAppService();

            // Act
            app.Delete(1);

            // Assert Fluent Assertions
            Fixture.TestSuiteRepositoryMock.Verify(repo => repo.Delete(1));
        }

        [Fact(DisplayName = "Should get test suite by testSuiteId")]
        [Trait("Category", "Test Plans Application Tests")]
        public void TestSuiteApplication_GetById_ShouldReturnTestSuite()
        {
            // Arrange
            var app = Fixture.GetTestSuiteAppService();
            Fixture.TestSuiteRepositoryMock.Setup(c => c.GetById(2)).Returns(Fixture.GenerateValidTestSuiteToQuery());

            // Act
            var testPlan = app.GetById(2);

            // Assert Fluent Assertions
            testPlan.TestPlanId.Should().Be(2);
        }

        [Fact(DisplayName = "Should not get test suite by invalid testSuiteId")]
        [Trait("Category", "Test Suites Application Tests")]
        public void TestSuiteApplication_GetById_ShouldNotReturnTestSuite()
        {
            // Arrange
            var testPlanApp = Fixture.GetTestSuiteAppService();
            Fixture.TestSuiteRepositoryMock.Setup(c => c.GetById(2)).Returns(Fixture.GenerateValidTestSuiteToQuery());

            // Act
            var testPlan = testPlanApp.GetById(3);

            // Assert Fluent Assertions
            testPlan.Should().BeNull();
        }

        [Fact(DisplayName = "Should test suite be valid")]
        [Trait("Category", "Test Suites Application Tests")]
        public void TestSuiteApplication_IsValid_ShouldReturnTrue()
        {
            // Arrange
            var TestPlanFixture = new TestPlansTestsFixture();

            var app = Fixture.GetTestSuiteAppService();
            var testSuite = Fixture.GenerateValidTestSuite();

            Fixture.TestPlanAppServiceMock.Setup(c => c.GetById(2)).Returns(TestPlanFixture.GenerateValidTestPlanToQuery());
            
            // Act
            EntityValidationResult validation = app.IsValid(testSuite);

            // Assert Fluent Assertions
            validation.Status.Should().BeTrue();
            validation.ValidationMessages.Should().HaveCount(c => c == 0);
        }

        [Fact(DisplayName = "Should test suite not be valid with invalid test plan")]
        [Trait("Category", "Test Suites Application Tests")]
        public void TestSuiteApplication_IsValid_ShouldReturnFalseAndMessageInvalidCustomer()
        {
            var TestPlanFixture = new TestPlansTestsFixture();

            // Arrange
            var app = Fixture.GetTestSuiteAppService();
            var testSuite = Fixture.GenerateTestSuiteWithInvalidTestPlan();

            Fixture.TestPlanAppServiceMock.Setup(c => c.GetById(1)).Returns(TestPlanFixture.GenerateValidTestPlanToQuery());

            // Act
            EntityValidationResult validation = app.IsValid(testSuite);

            // Assert Fluent Assertions
            validation.Status.Should().BeFalse();
            validation.ValidationMessages.Should().HaveCount(c => c == 1);
            validation.ValidationMessages[0].Should().Be("Test Plan Not Found");
        }
    }
}
