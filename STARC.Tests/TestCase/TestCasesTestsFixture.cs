using AutoMoq;
using Bogus;
using Moq;
using STARC.Api.Controllers;
using STARC.Application;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.Interfaces.Repositories;
using STARC.Domain.ViewModels.TestCase;
using STARC.Infra.CrossCutting.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace STARC.Tests.TestCase
{
    [CollectionDefinition(nameof(TestCasesCollection))]
    public class TestCasesCollection : ICollectionFixture<TestCasesTestsFixture>
    {
    }

    public class TestCasesTestsFixture
    {
        public Mock<ITestCaseAppService> TestCaseAppServiceMock { get; set; }
        public Mock<ITestCaseRepository> TestCaseRepositoryMock { get; set; }
        public Mock<ITestSuiteAppService> TestSuiteAppServiceMock { get; set; }
        public Mock<IUserAppService> UserAppServiceMock { get; set; }

        public TestCaseAppService GetTestCaseAppService()
        {
            var mocker = new AutoMoqer();
            mocker.Create<TestCaseAppService>();

            var testCaseAppService = mocker.Resolve<TestCaseAppService>();

            TestCaseRepositoryMock = mocker.GetMock<ITestCaseRepository>();
            TestSuiteAppServiceMock = mocker.GetMock<ITestSuiteAppService>();
            UserAppServiceMock = mocker.GetMock<IUserAppService>();

            return testCaseAppService;
        }

        public TestCasesController GetTestCasesController()
        {
            var mocker = new AutoMoqer();
            mocker.Create<TestCasesController>();

            MappingConfiguration.Configure();

            var testCasesController = mocker.Resolve<TestCasesController>();

            TestCaseAppServiceMock = mocker.GetMock<ITestCaseAppService>();
            UserAppServiceMock = mocker.GetMock<IUserAppService>();

            return testCasesController;
        }

        private static IEnumerable<TestCaseToQueryViewModel> GenerateTestCaseToQuery(int number, bool isActive)
        {
            Random random = new Random();

            var testCases = new Faker<TestCaseToQueryViewModel>("pt-BR")
                .CustomInstantiator(f => new TestCaseToQueryViewModel
                {
                    TestCaseId = 1,
                    TestSuiteId = 1,
                    TestSuite = "Test Suite 1",
                    Name = "Test Case 1",
                    Description = "Test Case 1",
                    PreConditions = "PreConditions - Test Case 1",
                    PosConditions = "PosConditions - Test Case 1",
                    ExpectedResult = "ExpectedResult - Test Case 1",
                    Status = true,
                    Type = "Principal",
                    CreatedBy = 1,
                    CreatedName = "Administrador Sistema",
                    CreatedDate = DateTime.Now.AddDays(-15),
                    LastUpdatedBy = 1,
                    LastUpdatedName = "Administrador Sistema",
                    LastUpdatedDate = DateTime.Now.AddDays(-15)
                });

            return testCases.Generate(number);
        }

        public IEnumerable<TestCaseToQueryViewModel> GetMixedTestCasesByTestPlan()
        {
            var testCases = new List<TestCaseToQueryViewModel>();

            testCases.AddRange(GenerateTestCaseToQuery(50, true).ToList());

            return testCases;

        }

        public STARC.Domain.Entities.TestCase GenerateValidTestCase()
        {
            Random random = new Random();

            var testCases = new Faker<STARC.Domain.Entities.TestCase>("pt-BR")
                .CustomInstantiator(f => new STARC.Domain.Entities.TestCase
                {
                    TestCaseId = 1,
                    TestSuiteId = 1,
                    Name = "Test Case 1",
                    Description = "Test Case 1",
                    PreConditions = "PreConditions - Test Case 1",
                    PosConditions = "PosConditions - Test Case 1",
                    ExpectedResult = "ExpectedResult - Test Case 1",
                    Status = true,
                    Type = "Principal",
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now.AddDays(-15),
                    LastUpdatedBy = 1,
                    LastUpdatedDate = DateTime.Now.AddDays(-15)
                });

            return testCases.Generate(1).FirstOrDefault();
        }

        public STARC.Domain.Entities.TestCase GenerateTestCaseWithInvalidTestSuite()
        {
            Random random = new Random();

            var testCases = new Faker<STARC.Domain.Entities.TestCase>("pt-BR")
                .CustomInstantiator(f => new STARC.Domain.Entities.TestCase
                {
                    TestCaseId = 1,
                    TestSuiteId = 99,
                    Name = "Test Case 99",
                    Description = "Test Case 1",
                    PreConditions = "PreConditions - Test Case 1",
                    PosConditions = "PosConditions - Test Case 1",
                    ExpectedResult = "ExpectedResult - Test Case 1",
                    Status = true,
                    Type = "Principal",
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now.AddDays(-15),
                    LastUpdatedBy = 1,
                    LastUpdatedDate = DateTime.Now.AddDays(-15)
                });

            return testCases.Generate(1).FirstOrDefault();
        }

        public TestCaseToQueryViewModel GenerateValidTestCaseToQuery()
        {
            Random random = new Random();

            var testCases = new Faker<TestCaseToQueryViewModel>("pt-BR")
                .CustomInstantiator(f => new TestCaseToQueryViewModel
                {
                    TestCaseId = 1,
                    TestSuiteId = 1,
                    TestSuite = "Test Suite 1",
                    Name = "Test Case 1",
                    Description = "Test Case 1",
                    PreConditions = "PreConditions - Test Case 1",
                    PosConditions = "PosConditions - Test Case 1",
                    ExpectedResult = "ExpectedResult - Test Case 1",
                    Status = true,
                    Type = "Principal",
                    CreatedBy = 1,
                    CreatedName = "Administrador Sistema",
                    CreatedDate = DateTime.Now.AddDays(-15),
                    LastUpdatedBy = 1,
                    LastUpdatedName = "Administrador Sistema",
                    LastUpdatedDate = DateTime.Now.AddDays(-15)
                });

            return testCases.Generate(1).FirstOrDefault();
        }

        public TestCaseToInsertViewModel GenerateValidTestCaseToInsert()
        {
            Random random = new Random();

            var testCases = new Faker<TestCaseToInsertViewModel>("pt-BR")
                .CustomInstantiator(f => new TestCaseToInsertViewModel
                {
                    TestSuiteId = 1,
                    Name = "Test Case 1",
                    Description = "Test Case 1",
                    PreConditions = "PreConditions - Test Case 1",
                    PosConditions = "PosConditions - Test Case 1",
                    ExpectedResult = "ExpectedResult - Test Case 1",
                    Type = "Principal"                    
                });

            return testCases.Generate(1).FirstOrDefault();
        }

        public TestCaseToUpdateViewModel GenerateValidTestCaseToUpdate()
        {
            Random random = new Random();

            var testCases = new Faker<TestCaseToUpdateViewModel>("pt-BR")
                .CustomInstantiator(f => new TestCaseToUpdateViewModel
                {
                    TestCaseId = 1,
                    TestSuiteId = 1,
                    Name = "Test Case 1",
                    Description = "Test Case 1",
                    PreConditions = "PreConditions - Test Case 1",
                    PosConditions = "PosConditions - Test Case 1",
                    ExpectedResult = "ExpectedResult - Test Case 1",
                    Type = "Principal"
                });

            return testCases.Generate(1).FirstOrDefault();
        }
    }
}
