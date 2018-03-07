using AutoMoq;
using Bogus;
using Bogus.DataSets;
using Moq;
using STARC.Api.Controllers;
using STARC.Application;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.Interfaces.Repositories;
using STARC.Domain.ViewModels.TestPlan;
using STARC.Infra.CrossCutting.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace STARC.Tests.TestPlans
{
    [CollectionDefinition(nameof(TestPlansCollection))]
    public class TestPlansCollection : ICollectionFixture<TestPlansTestsFixture>
    {
    }

    public class TestPlansTestsFixture
    {
        public Mock<ITestPlanAppService> TestPlanAppServiceMock { get; set; }
        public Mock<ITestPlanRepository> TestPlanRepositoryMock { get; set; }
        public Mock<IProjectAppService> ProjectAppServiceMock { get; set; }
        public Mock<IUserAppService> UserAppServiceMock { get; set; }

        public TestPlanAppService GetTestPlanAppService()
        {
            var mocker = new AutoMoqer();
            mocker.Create<TestPlanAppService>();

            var testPlanAppService = mocker.Resolve<TestPlanAppService>();

            TestPlanRepositoryMock = mocker.GetMock<ITestPlanRepository>();
            ProjectAppServiceMock = mocker.GetMock<IProjectAppService>();
            UserAppServiceMock = mocker.GetMock<IUserAppService>();

            return testPlanAppService;
        }

        public TestPlansController GetTestPlansController()
        {
            var mocker = new AutoMoqer();
            mocker.Create<TestPlansController>();

            MappingConfiguration.Configure();
            var testPlansController = mocker.Resolve<TestPlansController>();

            TestPlanAppServiceMock = mocker.GetMock<ITestPlanAppService>();
            UserAppServiceMock = mocker.GetMock<IUserAppService>();

            return testPlansController;
        }

        private static IEnumerable<TestPlanToQueryViewModel> GenerateTestPlanToQuery(int number, bool isActive, long projectId)
        {
            Random random = new Random();

            var testPlanTests = new Faker<TestPlanToQueryViewModel>("pt-BR")
                .CustomInstantiator(f => new TestPlanToQueryViewModel
                {
                    TestPlanId= random.Next(1,1000),
                    Name= f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                    Description= f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                    StartDate= f.Date.Between(DateTime.Now.AddMonths(-6),DateTime.Now.AddMonths(-3)),
                    FinishDate = DateTime.Now,
                    Status = isActive,
                    OwnerId= random.Next(1, 1000),
                    Owner = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                    ProjectId = projectId,
                    Project = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                    LastUpdatedName = "Administrador Sistema",
                    LastUpdatedDate= DateTime.Now.AddDays(-3)
                });

            return testPlanTests.Generate(number);
        }

        public IEnumerable<TestPlanToQueryViewModel> GetMixedTestPlansByProject()
        {
            var testPlans = new List<TestPlanToQueryViewModel>();

            testPlans.AddRange(GenerateTestPlanToQuery(50, true, 1).ToList());

            return testPlans;

        }

        private static IEnumerable<TestPlanToQueryViewModel> GenerateTestPlan(int number, bool isActive, long projectId)
        {
            Random random = new Random();

            var testPlanTests = new Faker<TestPlanToQueryViewModel>("pt-BR")
                .CustomInstantiator(f => new TestPlanToQueryViewModel
                {
                    TestPlanId = random.Next(1, 1000),
                    Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                    Description = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                    StartDate = f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-3)),
                    FinishDate = DateTime.Now,
                    Status = isActive,
                    OwnerId = 1,
                    Owner = "Administrador Sistema",
                    ProjectId = projectId,
                    Project = "Projeto Unit Test",
                    LastUpdatedName = "Administrador Sistema",
                    LastUpdatedDate = DateTime.Now.AddDays(-3)
                });

            return testPlanTests.Generate(number);
        }

        public IEnumerable<TestPlanToQueryViewModel> GetActiveTestPlans()
        {
            var testPlans = new List<TestPlanToQueryViewModel>();

            testPlans.AddRange(GenerateTestPlan(50, true, 1).ToList());

            return testPlans;
        }

        public IEnumerable<TestPlanToQueryViewModel> GetMixedTestPlans()
        {
            var testPlans = new List<TestPlanToQueryViewModel>();

            testPlans.AddRange(GenerateTestPlan(50, true, 1).ToList());
            testPlans.AddRange(GenerateTestPlan(50, false, 1).ToList());

            return testPlans;
        }

        public TestPlan GenerateValidTestPlan()
        {
            var testPlanTests = new Faker<TestPlan>("pt-BR")
               .CustomInstantiator(f => new TestPlan
               {
                   TestPlanId = 1,
                   Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   Description = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   StartDate = f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-3)),
                   FinishDate = DateTime.Now,
                   Status = true,
                   OwnerId = 1,
                   ProjectId = 1,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7),
                   LastUpdatedBy = 1,
                   LastUpdatedDate = DateTime.Now.AddDays(-3)
               });

            return testPlanTests.Generate(1).FirstOrDefault();
        }

        public TestPlanToQueryViewModel GenerateValidTestPlanToQuery()
        {
            var testPlanTests = new Faker<TestPlanToQueryViewModel>("pt-BR")
               .CustomInstantiator(f => new TestPlanToQueryViewModel
               {
                   TestPlanId = 1,
                   Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   Description = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   StartDate = f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-3)),
                   FinishDate = DateTime.Now,
                   Status = true,
                   OwnerId = 1,
                   ProjectId = 1,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7),
                   LastUpdatedBy = 1,
                   LastUpdatedDate = DateTime.Now.AddDays(-3)
               });

            return testPlanTests.Generate(1).FirstOrDefault();
        }

        public TestPlan GenerateValidTestPlanWithAnotherProject()
        {
            var testPlanTests = new Faker<TestPlan>("pt-BR")
               .CustomInstantiator(f => new TestPlan
               {
                   TestPlanId = 1,
                   Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   Description = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   StartDate = f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-3)),
                   FinishDate = DateTime.Now,
                   Status = true,
                   OwnerId = 1,
                   ProjectId = 2,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7),
                   LastUpdatedBy = 1,
                   LastUpdatedDate = DateTime.Now.AddDays(-3)
               });

            return testPlanTests.Generate(1).FirstOrDefault();
        }

        public TestPlanToInsertViewModel GenerateValidTestPlanToInsert()
        {
            var testPlanTests = new Faker<TestPlanToInsertViewModel>("pt-BR")
               .CustomInstantiator(f => new TestPlanToInsertViewModel
               {
                   Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   Description = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   StartDate = f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-3)),
                   FinishDate = DateTime.Now,
                   OwnerId = 1,
                   ProjectId = 1                   
               });

            return testPlanTests.Generate(1).FirstOrDefault();
        }

        public TestPlanToUpdateViewModel GenerateValidTestPlanToUpdate()
        {
            var testPlanTests = new Faker<TestPlanToUpdateViewModel>("pt-BR")
               .CustomInstantiator(f => new TestPlanToUpdateViewModel
               {
                   TestPlanId = 1,
                   Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   Description = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   StartDate = f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-3)),
                   FinishDate = DateTime.Now,
                   OwnerId = 1,
                   ProjectId = 1
               });

            return testPlanTests.Generate(1).FirstOrDefault();
        }

        public TestPlan GenerateTestPlanWithInvalidDates()
        {
            var testPlanTests = new Faker<TestPlan>("pt-BR")
               .CustomInstantiator(f => new TestPlan
               {
                   TestPlanId = 1,
                   Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   Description = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   StartDate = DateTime.Now,
                   FinishDate = f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-3)),
                   Status = true,
                   OwnerId = 1,
                   ProjectId = 1,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7),
                   LastUpdatedBy = 1,
                   LastUpdatedDate = DateTime.Now.AddDays(-3)
               });

            return testPlanTests.Generate(1).FirstOrDefault();
        }

        public TestPlan GenerateTestPlanWithInvalidProject()
        {
            var testPlanTests = new Faker<TestPlan>("pt-BR")
               .CustomInstantiator(f => new TestPlan
               {
                   TestPlanId = 1,
                   Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   Description = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   StartDate = f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-3)),
                   FinishDate = DateTime.Now,
                   Status = true,
                   OwnerId = 1,
                   ProjectId = 0,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7),
                   LastUpdatedBy = 1,
                   LastUpdatedDate = DateTime.Now.AddDays(-3)
               });

            return testPlanTests.Generate(1).FirstOrDefault();
        }

        public TestPlan GenerateTestPlanWithInvalidOwner()
        {
            var testPlanTests = new Faker<TestPlan>("pt-BR")
               .CustomInstantiator(f => new TestPlan
               {
                   TestPlanId = 1,
                   Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   Description = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   StartDate = f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-3)),
                   FinishDate = DateTime.Now,
                   Status = true,
                   OwnerId = 0,
                   ProjectId = 1,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7),
                   LastUpdatedBy = 1,
                   LastUpdatedDate = DateTime.Now.AddDays(-3)
               });

            return testPlanTests.Generate(1).FirstOrDefault();
        }
    }
}
