using AutoMoq;
using Bogus;
using Moq;
using STARC.Api.Controllers;
using STARC.Application;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.Interfaces.Repositories;
using STARC.Domain.ViewModels.UsersInProjects;
using STARC.Infra.CrossCutting.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace STARC.Tests.UserInProjects
{
    [CollectionDefinition(nameof(UsersInProjectsCollection))]
    public class UsersInProjectsCollection : ICollectionFixture<UsersInProjectsTestsFixture>
    {
    }

    public class UsersInProjectsTestsFixture
    {
        public Mock<IUsersInProjectsAppService> UsersInProjectsAppServiceMock { get; set; }
        public Mock<IUsersInProjectsRepository> UsersInProjectsRepositoryMock { get; set; }
        public Mock<IUserAppService> UserAppServiceMock { get; set; }
        public Mock<IProjectAppService> ProjectAppServiceMock { get; set; }

        public UsersInProjectsAppService GetUsersInProjectsAppService()
        {
            var mocker = new AutoMoqer();
            mocker.Create<UsersInProjectsAppService>();

            var userAppService = mocker.Resolve<UsersInProjectsAppService>();

            UsersInProjectsRepositoryMock = mocker.GetMock<IUsersInProjectsRepository>();
            UserAppServiceMock = mocker.GetMock<IUserAppService>();
            ProjectAppServiceMock = mocker.GetMock<IProjectAppService>();
            
            return userAppService;
        }

        public UsersInProjectsController GetUsersInProjectsController()
        {
            var mocker = new AutoMoqer();
            mocker.Create<UsersInProjectsController>();

            MappingConfiguration.Configure();
            var userController = mocker.Resolve<UsersInProjectsController>();

            UsersInProjectsAppServiceMock = mocker.GetMock<IUsersInProjectsAppService>();
            UserAppServiceMock = mocker.GetMock<IUserAppService>();
            ProjectAppServiceMock = mocker.GetMock<IProjectAppService>();
            
            return userController;
        }

        public IEnumerable<UsersInProjectsToQueryViewModel> GenerateUsersInProjects(int number)
        {
            Random random = new Random();

            var userTests = new Faker<UsersInProjectsToQueryViewModel>("pt-BR")
               .CustomInstantiator(f => new UsersInProjectsToQueryViewModel
               {
                   UserInProjectId = random.Next(1, 1000),
                   UserId = random.Next(1,100),
                   User = f.Person.UserName,
                   ProjectId = random.Next(1,100),
                   Project = f.Person.Website,
                   CreatedName = "Administrador Sistema",
                   CreatedDate = DateTime.Now.AddDays(-1)
               });

            return userTests.Generate(number);
        }

        public UsersInProjects GenerateValidUsersInProjects()
        {
            Random random = new Random();

            var userTests = new Faker<UsersInProjects>("pt-BR")
               .CustomInstantiator(f => new UsersInProjects
               {
                   UserInProjectId = 1,
                   UserId = 2,
                   ProjectId = 1,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7)
               });

            return userTests.Generate(1).FirstOrDefault();
        }


        public UsersInProjectsToQueryViewModel GenerateValidUsersInProjectsToQuery()
        {
            Random random = new Random();

            var userTests = new Faker<UsersInProjectsToQueryViewModel>("pt-BR")
               .CustomInstantiator(f => new UsersInProjectsToQueryViewModel
               {
                   UserInProjectId = 1,
                   UserId = 2,
                   ProjectId = 1,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7)
               });

            return userTests.Generate(1).FirstOrDefault();
        }

        public UsersInProjectsToInsertViewModel GenerateValidUsersInProjectsToInsert()
        {
            Random random = new Random();

            var userTests = new Faker<UsersInProjectsToInsertViewModel>("pt-BR")
               .CustomInstantiator(f => new UsersInProjectsToInsertViewModel
               {
                   UserId = 2,
                   ProjectId = 1                   
               });

            return userTests.Generate(1).FirstOrDefault();

        }
        public UsersInProjects GenerateUsersInProjectsWithInvalidProject()
        {
            Random random = new Random();

            var userTests = new Faker<UsersInProjects>("pt-BR")
               .CustomInstantiator(f => new UsersInProjects
               {
                   UserInProjectId = 1,
                   UserId = 2,
                   ProjectId = 0,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7)
               });

            return userTests.Generate(1).FirstOrDefault();
        }

        public UsersInProjects GenerateUsersInProjectsWithInvalidUser()
        {
            Random random = new Random();

            var userTests = new Faker<UsersInProjects>("pt-BR")
               .CustomInstantiator(f => new UsersInProjects
               {
                   UserInProjectId = 1,
                   UserId = 0,
                   ProjectId = 1,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7)
               });

            return userTests.Generate(1).FirstOrDefault();
        }
    }

}
