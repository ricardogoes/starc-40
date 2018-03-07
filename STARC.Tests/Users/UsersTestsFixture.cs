using AutoMoq;
using Bogus;
using Bogus.DataSets;
using Moq;
using STARC.Api.Controllers;
using STARC.Application;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.Interfaces.Repositories;
using STARC.Domain.ViewModels.Users;
using STARC.Infra.CrossCutting.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace STARC.Tests.Users
{
    [CollectionDefinition(nameof(UsersCollection))]
    public class UsersCollection : ICollectionFixture<UsersTestsFixture>
    {
    }

    public class UsersTestsFixture
    {
        public Mock<IUserAppService> UserAppServiceMock { get; set; }
        public Mock<IUserRepository> UserRepositoryMock { get; set; }
        public Mock<ICustomerAppService> CustomerAppServiceMock { get; set; }
        public Mock<IUserProfileAppService> UserProfileAppServiceMock { get; set; }
        public Mock<IPasswordAppService> PasswordAppServiceMock { get; set; }

        public UserAppService GetUserAppService()
        {
            var mocker = new AutoMoqer();
            mocker.Create<UserAppService>();

            var userAppService = mocker.Resolve<UserAppService>();

            UserRepositoryMock = mocker.GetMock<IUserRepository>();
            CustomerAppServiceMock = mocker.GetMock<ICustomerAppService>();
            UserProfileAppServiceMock = mocker.GetMock<IUserProfileAppService>();           

            return userAppService;
        }

        public UsersController GetUsersController()
        {
            var mocker = new AutoMoqer();
            mocker.Create<UsersController>();

            MappingConfiguration.Configure();
            var usersController = mocker.Resolve<UsersController>();

            UserAppServiceMock = mocker.GetMock<IUserAppService>();
            PasswordAppServiceMock = mocker.GetMock<IPasswordAppService>();

            return usersController;
        }

        private static IEnumerable<UserToQueryViewModel> GenerateUsers(int number, bool isActive)
        {
            Random random = new Random();

            var userTests = new Faker<UserToQueryViewModel>("pt-BR")
               .CustomInstantiator(f => new UserToQueryViewModel
               {
                   UserId = random.Next(1,1000),
                   FirstName = f.Name.FirstName(Name.Gender.Male),
                   LastName = f.Name.LastName(Name.Gender.Male),
                   Username = f.Person.UserName,
                   Email = f.Person.Email,
                   Status = true,
                   UserProfileId = 3,
                   CustomerId = 1,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7),
                   LastUpdatedBy = 1,
                   LastUpdatedDate = DateTime.Now.AddDays(-3)
               });

            return userTests.Generate(number);
        }

        public IEnumerable<UserToQueryViewModel> GetUsers()
        {
            return GenerateUsers(50, true);
        }

        public IEnumerable<UserToQueryViewModel> GetMixedUsers()
        {
            var users = new List<UserToQueryViewModel>();

            users.AddRange(GenerateUsers(50, true).ToList());
            users.AddRange(GenerateUsers(50, false).ToList());

            return users;
        }

        public UserToQueryViewModel GenerateValidUserToQuery()
        {
            var userTests = new Faker<UserToQueryViewModel>("pt-BR")
               .CustomInstantiator(f => new UserToQueryViewModel
               {
                   UserId = 1,
                   FirstName = f.Name.FirstName(Name.Gender.Male),
                   LastName = f.Name.LastName(Name.Gender.Male),
                   Username = "ricardo.goes",
                   Email = "ricardo.goes@gmail.com",
                   Status = true,
                   UserProfileId = 3,
                   CustomerId = 1,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7),
                   LastUpdatedBy = 1,
                   LastUpdatedDate = DateTime.Now.AddDays(-3)
               });

            return userTests.Generate(1).FirstOrDefault();
        }

        public UserToQueryViewModel GenerateValidUserToQueryWithAnotherCustomer()
        {
            var userTests = new Faker<UserToQueryViewModel>("pt-BR")
               .CustomInstantiator(f => new UserToQueryViewModel
               {
                   UserId = 1,
                   FirstName = f.Name.FirstName(Name.Gender.Male),
                   LastName = f.Name.LastName(Name.Gender.Male),
                   Username = "ricardo.goes",
                   Email = "ricardo.goes@gmail.com",
                   Status = true,
                   UserProfileId = 3,
                   CustomerId = 2,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7),
                   LastUpdatedBy = 1,
                   LastUpdatedDate = DateTime.Now.AddDays(-3)
               });

            return userTests.Generate(1).FirstOrDefault();
        }

        public User GenerateValidUserToAdd()
        {
            var userTests = new Faker<User>("pt-BR")
               .CustomInstantiator(f => new User
               {
                   UserId = 1,
                   FirstName = f.Name.FirstName(Name.Gender.Male),
                   LastName = f.Name.LastName(Name.Gender.Male),
                   Username = "ricardo.goes",
                   Email = "ricardo.goes@gmail.com",
                   Status = true,
                   UserProfileId = 3,
                   CustomerId = 1,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7),
                   LastUpdatedBy = 1,
                   LastUpdatedDate = DateTime.Now.AddDays(-3)
               });

            return userTests.Generate(1).FirstOrDefault();
        }

        public UserToInsertViewModel GenerateValidUserToInsert()
        {
            var userTests = new Faker<UserToInsertViewModel>("pt-BR")
               .CustomInstantiator(f => new UserToInsertViewModel
               {
                   FirstName = f.Name.FirstName(Name.Gender.Male),
                   LastName = f.Name.LastName(Name.Gender.Male),
                   Username = "ricardo.goes",
                   Password = "admin",
                   Email = "ricardo.goes@gmail.com",
                   UserProfileId = 3,
                   CustomerId = 1                   
               });

            return userTests.Generate(1).FirstOrDefault();
        }

        public UserToInsertViewModel GenerateValidUserToInsertWithUserProfileSystemAdmin()
        {
            var userTests = new Faker<UserToInsertViewModel>("pt-BR")
               .CustomInstantiator(f => new UserToInsertViewModel
               {
                   FirstName = f.Name.FirstName(Name.Gender.Male),
                   LastName = f.Name.LastName(Name.Gender.Male),
                   Username = "ricardo.goes",
                   Password = "admin",
                   Email = "ricardo.goes@gmail.com",
                   UserProfileId = 1,
                   CustomerId = 1
               });

            return userTests.Generate(1).FirstOrDefault();
        }

        public UserToUpdateViewModel GenerateValidUserToUpdate()
        {
            var userTests = new Faker<UserToUpdateViewModel>("pt-BR")
               .CustomInstantiator(f => new UserToUpdateViewModel
               {
                   UserId = 1,
                   FirstName = f.Name.FirstName(Name.Gender.Male),
                   LastName = f.Name.LastName(Name.Gender.Male),
                   Email = "ricardo.goes@gmail.com",
                   UserProfileId = 3,
                   CustomerId = 1
               });

            return userTests.Generate(1).FirstOrDefault();
        }

        public UserToUpdateViewModel GenerateValidUserToUpdateWithUserProfileSystemAdmin()
        {
            var userTests = new Faker<UserToUpdateViewModel>("pt-BR")
               .CustomInstantiator(f => new UserToUpdateViewModel
               {
                   UserId = 1,
                   FirstName = f.Name.FirstName(Name.Gender.Male),
                   LastName = f.Name.LastName(Name.Gender.Male),
                   Email = "ricardo.goes@gmail.com",
                   UserProfileId = 1,
                   CustomerId = 1
               });

            return userTests.Generate(1).FirstOrDefault();
        }

        public User GenerateUserWithInvalidCustomer()
        {
            var userTests = new Faker<User>("pt-BR")
               .CustomInstantiator(f => new User
               {
                   UserId = 1,
                   FirstName = f.Name.FirstName(Name.Gender.Male),
                   LastName = f.Name.LastName(Name.Gender.Male),
                   Username = "ricardo.goes",
                   Password = "admin",
                   PasswordHash = null,
                   Email = "ricardo.goes@gmail.com",
                   Status = true,
                   UserProfileId = 3,
                   CustomerId = 0,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7),
                   LastUpdatedBy = 1,
                   LastUpdatedDate = DateTime.Now.AddDays(-3)
               });

            return userTests.Generate(1).FirstOrDefault();
        }

        public User GenerateUserWithInvalidProfile()
        {
            var userTests = new Faker<User>("pt-BR")
               .CustomInstantiator(f => new User
               {
                   UserId = 1,
                   FirstName = f.Name.FirstName(Name.Gender.Male),
                   LastName = f.Name.LastName(Name.Gender.Male),
                   Username = "ricardo.goes",
                   Password = "admin",
                   PasswordHash = null,
                   Email = "ricardo.goes@gmail.com",
                   Status = true,
                   UserProfileId = 0,
                   CustomerId = 1,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7),
                   LastUpdatedBy = 1,
                   LastUpdatedDate = DateTime.Now.AddDays(-3)
               });

            return userTests.Generate(1).FirstOrDefault();
        }

        public User GenerateValidUserWithUserId0()
        {
            var userTests = new Faker<User>("pt-BR")
               .CustomInstantiator(f => new User
               {
                   UserId = 0,
                   FirstName = f.Name.FirstName(Name.Gender.Male),
                   LastName = f.Name.LastName(Name.Gender.Male),
                   Username = "ricardo.goes",
                   Password = "admin",
                   PasswordHash = null,
                   Email = "ricardo.goes@gmail.com",
                   Status = true,
                   UserProfileId = 3,
                   CustomerId = 1,
                   CreatedBy = 1,
                   CreatedDate = DateTime.Now.AddDays(-7),
                   LastUpdatedBy = 1,
                   LastUpdatedDate = DateTime.Now.AddDays(-3)
               });

            return userTests.Generate(1).FirstOrDefault();
        }

        private static IEnumerable<UserToQueryViewModel> GenerateUsersToQuery(int number, bool isActive, long customerId)
        {
            Random random = new Random();

            var userProjects = new Faker<UserToQueryViewModel>("pt-BR")
                .CustomInstantiator(f => new UserToQueryViewModel
                {
                    UserId = 1,
                    FirstName = f.Name.FirstName(Name.Gender.Male),
                    LastName = f.Name.LastName(Name.Gender.Male),
                    Username = "ricardo.goes",
                    Email = "ricardo.goes@gmail.com",
                    Status = true,
                    UserProfileId = 3,
                    CustomerId = 1,
                    ProfileName = "Usuário Comum",
                    LastUpdatedName = "Administrador Sistema",
                    LastUpdatedDate = DateTime.Now.AddDays(-3)
                });

            return userProjects.Generate(number);
        }

        public IEnumerable<UserToQueryViewModel> GetMixedUsersByCustomer()
        {
            var users = new List<UserToQueryViewModel>();

            users.AddRange(GenerateUsersToQuery(50, true, 1).ToList());

            return users;

        }
    }
}
