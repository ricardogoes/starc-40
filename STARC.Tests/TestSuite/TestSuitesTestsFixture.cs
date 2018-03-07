using AutoMoq;
using Bogus;
using Moq;
using STARC.Api.Controllers;
using STARC.Application;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.Interfaces.Repositories;
using STARC.Domain.ViewModels.TestSuite;
using STARC.Infra.CrossCutting.AutoMapper;
using System;
using System.Linq;
using Xunit;

namespace STARC.Tests.TestSuite
{
    [CollectionDefinition(nameof(TestSuitesCollection))]
    public class TestSuitesCollection : ICollectionFixture<TestSuitesTestsFixture>
    {
    }

    public class TestSuitesTestsFixture
    {
        public Mock<ITestSuiteAppService> TestSuiteAppServiceMock { get; set; }
        public Mock<ITestSuiteRepository> TestSuiteRepositoryMock { get; set; }
        public Mock<ITestPlanAppService> TestPlanAppServiceMock { get; set; }
        public Mock<IUserAppService> UserAppServiceMock { get; set; }

        public TestSuiteAppService GetTestSuiteAppService()
        {
            var mocker = new AutoMoqer();
            mocker.Create<TestSuiteAppService>();

            var testSuiteAppService = mocker.Resolve<TestSuiteAppService>();

            TestSuiteRepositoryMock = mocker.GetMock<ITestSuiteRepository>();
            TestPlanAppServiceMock = mocker.GetMock<ITestPlanAppService>();
            UserAppServiceMock = mocker.GetMock<IUserAppService>();

            return testSuiteAppService;
        }

        public TestSuitesController GetTestSuitesController()
        {
            var mocker = new AutoMoqer();
            mocker.Create<TestSuitesController>();

            MappingConfiguration.Configure();

            var testSuitesController = mocker.Resolve<TestSuitesController>();

            TestSuiteAppServiceMock = mocker.GetMock<ITestSuiteAppService>();
            UserAppServiceMock = mocker.GetMock<IUserAppService>();

            return testSuitesController;
        }

        public STARC.Domain.Entities.TestSuite GenerateValidTestSuite()
        {
            var testSuite = new Faker<STARC.Domain.Entities.TestSuite>("pt-BR")
               .CustomInstantiator(f => new STARC.Domain.Entities.TestSuite
               {
                   TestSuiteId = 1,
                   TestPlanId = 2,
                   ParentTestSuiteId = 1,
                   Name = "Test Suite 2",
                   Description = "Test Suite 2",
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-15)
               });

            return testSuite.Generate(1).FirstOrDefault();
        }

        public STARC.Domain.Entities.TestSuite GenerateTestSuiteWithInvalidTestPlan()
        {
            var testSuite = new Faker<STARC.Domain.Entities.TestSuite>("pt-BR")
               .CustomInstantiator(f => new STARC.Domain.Entities.TestSuite
               {
                   TestSuiteId = 1,
                   TestPlanId = 99,
                   ParentTestSuiteId = 1,
                   Name = "Test Suite 2",
                   Description = "Test Suite 2",
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-15)
               });

            return testSuite.Generate(1).FirstOrDefault();
        }

        public TestSuiteToQueryViewModel GenerateValidTestSuiteToQuery()
        {
            var testSuite = new Faker<TestSuiteToQueryViewModel>("pt-BR")
               .CustomInstantiator(f => new TestSuiteToQueryViewModel
               {
                   TestSuiteId = 1,
                   TestPlanId = 2,
                   TestPlan = "Test Plan 1",
                   ParentTestSuiteId = 1,
                   ParentTestSuite = "Test Suite 1",
                   Name = "Test Suite 2",
                   Description = "Test Suite 2",
                   CreatedBy = 1,
                   CreatedName = "Administrador Sistema",
                   CreatedDate = DateTime.Now.AddDays(-15)
               });

            return testSuite.Generate(1).FirstOrDefault();
        }

        public TestSuiteToInsertViewModel GenerateValidTestSuiteToInsert()
        {
            var testSuite = new Faker<TestSuiteToInsertViewModel>("pt-BR")
               .CustomInstantiator(f => new TestSuiteToInsertViewModel
               {
                   TestPlanId = 2,
                   ParentTestSuiteId = 1,
                   Name = "Test Suite 2",
                   Description = "Test Suite 2"                   
               });

            return testSuite.Generate(1).FirstOrDefault();
        }

        public TestSuiteToUpdateViewModel GenerateValidTestSuiteToUpdate()
        {
            var testSuite = new Faker<TestSuiteToUpdateViewModel>("pt-BR")
               .CustomInstantiator(f => new TestSuiteToUpdateViewModel
               {
                   TestSuiteId = 2,
                   TestPlanId = 2,
                   ParentTestSuiteId = 1,
                   Name = "Test Suite 2",
                   Description = "Test Suite 2"
               });

            return testSuite.Generate(1).FirstOrDefault();
        }
    }
}
