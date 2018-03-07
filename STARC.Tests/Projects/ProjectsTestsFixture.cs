using AutoMoq;
using Bogus;
using Bogus.DataSets;
using Moq;
using STARC.Api.Controllers;
using STARC.Application;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.Interfaces.Repositories;
using STARC.Domain.ViewModels.Projects;
using STARC.Infra.CrossCutting.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace STARC.Tests.Projects
{
    [CollectionDefinition(nameof(ProjectsCollection))]
    public class ProjectsCollection : ICollectionFixture<ProjectsTestsFixture>
    {
    }

    public class ProjectsTestsFixture
    {
        public Mock<IProjectAppService> ProjectAppServiceMock { get; set; }
        public Mock<IProjectRepository> ProjectRepositoryMock { get; set; }
        public Mock<ICustomerAppService> CustomerAppServiceMock { get; set; }
        public Mock<IUserAppService> UserAppServiceMock { get; set; }

        public ProjectAppService GetProjectAppService()
        {
            var mocker = new AutoMoqer();
            mocker.Create<ProjectAppService>();

            var projectAppService = mocker.Resolve<ProjectAppService>();

            ProjectRepositoryMock = mocker.GetMock<IProjectRepository>();
            CustomerAppServiceMock = mocker.GetMock<ICustomerAppService>();
            UserAppServiceMock = mocker.GetMock<IUserAppService>();

            return projectAppService;
        }

        public ProjectsController GetProjectsController()
        {
            var mocker = new AutoMoqer();
            mocker.Create<ProjectsController>();

            MappingConfiguration.Configure();
            var projectsController = mocker.Resolve<ProjectsController>();

            ProjectAppServiceMock = mocker.GetMock<IProjectAppService>();
            UserAppServiceMock = mocker.GetMock<IUserAppService>();

            return projectsController;
        }

        private static IEnumerable<ProjectToQueryViewModel> GenerateProjectToQuery(int number, bool isActive, long customerId)
        {
            Random random = new Random();

            var projectTests = new Faker<ProjectToQueryViewModel>("pt-BR")
                .CustomInstantiator(f => new ProjectToQueryViewModel
                {
                    ProjectId= random.Next(1,1000),
                    Name= f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                    Description= f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                    StartDate= f.Date.Between(DateTime.Now.AddMonths(-6),DateTime.Now.AddMonths(-3)),
                    FinishDate = DateTime.Now,
                    Status = isActive,
                    OwnerId= random.Next(1, 1000),
                    Owner = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                    CustomerId = customerId,
                    Customer = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                    LastUpdatedName = "Administrador Sistema",
                    LastUpdatedDate= DateTime.Now.AddDays(-3)
                });

            return projectTests.Generate(number);
        }

        public IEnumerable<ProjectToQueryViewModel> GetMixedProjectsByCustomer()
        {
            var projects = new List<ProjectToQueryViewModel>();

            projects.AddRange(GenerateProjectToQuery(50, true, 1).ToList());

            return projects;

        }

        private static IEnumerable<ProjectToQueryViewModel> GenerateProject(int number, bool isActive, long customerId)
        {
            Random random = new Random();

            var projectTests = new Faker<ProjectToQueryViewModel>("pt-BR")
                .CustomInstantiator(f => new ProjectToQueryViewModel
                {
                    ProjectId = random.Next(1, 1000),
                    Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                    Description = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                    StartDate = f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-3)),
                    FinishDate = DateTime.Now,
                    Status = isActive,
                    OwnerId = 1,
                    CustomerId = customerId,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now.AddDays(-7),
                    LastUpdatedBy = 1,
                    LastUpdatedDate = DateTime.Now.AddDays(-3)
                });

            return projectTests.Generate(number);
        }

        public IEnumerable<ProjectToQueryViewModel> GetActiveProjects()
        {
            var projects = new List<ProjectToQueryViewModel>();

            projects.AddRange(GenerateProject(50, true, 1).ToList());

            return projects;
        }

        public IEnumerable<ProjectToQueryViewModel> GetMixedProjects()
        {
            var projects = new List<ProjectToQueryViewModel>();

            projects.AddRange(GenerateProject(50, true, 1).ToList());
            projects.AddRange(GenerateProject(50, false, 1).ToList());

            return projects;
        }

        public Project GenerateValidProject()
        {
            var projectTests = new Faker<Project>("pt-BR")
               .CustomInstantiator(f => new Project
               {
                   ProjectId = 1,
                   Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   Description = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   StartDate = f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-3)),
                   FinishDate = DateTime.Now,
                   Status = true,
                   OwnerId = 1,
                   CustomerId = 1,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7),
                   LastUpdatedBy = 1,
                   LastUpdatedDate = DateTime.Now.AddDays(-3)
               });

            return projectTests.Generate(1).FirstOrDefault();
        }

        public ProjectToQueryViewModel GenerateValidProjectToQuery()
        {
            var projectTests = new Faker<ProjectToQueryViewModel>("pt-BR")
               .CustomInstantiator(f => new ProjectToQueryViewModel
               {
                   ProjectId = 1,
                   Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   Description = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   StartDate = f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-3)),
                   FinishDate = DateTime.Now,
                   Status = true,
                   OwnerId = 1,
                   CustomerId = 1,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7),
                   LastUpdatedBy = 1,
                   LastUpdatedDate = DateTime.Now.AddDays(-3)
               });

            return projectTests.Generate(1).FirstOrDefault();
        }

        public ProjectToQueryViewModel GenerateValidProjectWithAnotherCustomer()
        {
            var projectTests = new Faker<ProjectToQueryViewModel>("pt-BR")
               .CustomInstantiator(f => new ProjectToQueryViewModel
               {
                   ProjectId = 1,
                   Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   Description = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   StartDate = f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-3)),
                   FinishDate = DateTime.Now,
                   Status = true,
                   OwnerId = 1,
                   CustomerId = 2,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7),
                   LastUpdatedBy = 1,
                   LastUpdatedDate = DateTime.Now.AddDays(-3)
               });

            return projectTests.Generate(1).FirstOrDefault();
        }

        public ProjectToInsertViewModel GenerateValidProjectToInsert()
        {
            var projectTests = new Faker<ProjectToInsertViewModel>("pt-BR")
               .CustomInstantiator(f => new ProjectToInsertViewModel
               {
                   Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   Description = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   StartDate = f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-3)),
                   FinishDate = DateTime.Now,
                   OwnerId = 1,
                   CustomerId = 1                   
               });

            return projectTests.Generate(1).FirstOrDefault();
        }

        public ProjectToUpdateViewModel GenerateValidProjectToUpdate()
        {
            var projectTests = new Faker<ProjectToUpdateViewModel>("pt-BR")
               .CustomInstantiator(f => new ProjectToUpdateViewModel
               {
                   ProjectId = 1,
                   Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   Description = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   StartDate = f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-3)),
                   FinishDate = DateTime.Now,
                   OwnerId = 1,
                   CustomerId = 1
               });

            return projectTests.Generate(1).FirstOrDefault();
        }

        public Project GenerateProjectWithInvalidDates()
        {
            var projectTests = new Faker<Project>("pt-BR")
               .CustomInstantiator(f => new Project
               {
                   ProjectId = 1,
                   Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   Description = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   StartDate = DateTime.Now,
                   FinishDate = f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-3)),
                   Status = true,
                   OwnerId = 1,
                   CustomerId = 1,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7),
                   LastUpdatedBy = 1,
                   LastUpdatedDate = DateTime.Now.AddDays(-3)
               });

            return projectTests.Generate(1).FirstOrDefault();
        }

        public Project GenerateProjectWithInvalidCustomer()
        {
            var projectTests = new Faker<Project>("pt-BR")
               .CustomInstantiator(f => new Project
               {
                   ProjectId = 1,
                   Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   Description = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   StartDate = f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-3)),
                   FinishDate = DateTime.Now,
                   Status = true,
                   OwnerId = 1,
                   CustomerId = 0,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7),
                   LastUpdatedBy = 1,
                   LastUpdatedDate = DateTime.Now.AddDays(-3)
               });

            return projectTests.Generate(1).FirstOrDefault();
        }

        public Project GenerateProjectWithInvalidOwner()
        {
            var projectTests = new Faker<Project>("pt-BR")
               .CustomInstantiator(f => new Project
               {
                   ProjectId = 1,
                   Name = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   Description = f.Name.FirstName(Name.Gender.Male) + ' ' + f.Name.LastName(Name.Gender.Male),
                   StartDate = f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-3)),
                   FinishDate = DateTime.Now,
                   Status = true,
                   OwnerId = 0,
                   CustomerId = 1,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7),
                   LastUpdatedBy = 1,
                   LastUpdatedDate = DateTime.Now.AddDays(-3)
               });

            return projectTests.Generate(1).FirstOrDefault();
        }
    }
}
