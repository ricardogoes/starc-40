using AutoMoq;
using Bogus;
using MediatR;
using Moq;
using STARC.Api.Controllers;
using STARC.Application;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace STARC.Tests.UserProfiles
{
    [CollectionDefinition(nameof(UserProfilesCollection))]
    public class UserProfilesCollection
        : ICollectionFixture<UserProfilesTestsFixture>
    {
    }

    public class UserProfilesTestsFixture
    {
        public Mock<IUserProfileAppService> UserProfileAppServiceMock { get; set; }
        public Mock<IUserProfileRepository> UserProfileRepositoryMock { get; set; }
        
        public UserProfileAppService GetUserProfileAppService()
        {
            var mocker = new AutoMoqer();
            mocker.Create<UserProfileAppService>();

            var userAppService = mocker.Resolve<UserProfileAppService>();

            UserProfileRepositoryMock = mocker.GetMock<IUserProfileRepository>();

            return userAppService;
        }

        public UserProfilesController GetUserProfilesController()
        {
            var mocker = new AutoMoqer();
            mocker.Create<UserProfilesController>();

            var userAppService = mocker.Resolve<UserProfilesController>();

            UserProfileAppServiceMock = mocker.GetMock<IUserProfileAppService>();

            return userAppService;
        }

        public UserProfile GenerateValidUserProfile()
        {
            var userProfileTests = new Faker<UserProfile>("pt-BR")
              .CustomInstantiator(f => new UserProfile
               {
                   UserProfileId = 3,
                   ProfileName = "Usuário Comum"
               });

            return userProfileTests.Generate(1).FirstOrDefault();
        }

        public IEnumerable<UserProfile> GenerateProfiles()
        {
            Random random = new Random();

            var userProfileId = random.Next(1, 3);
            string profileName = string.Empty;
            switch(userProfileId)
            {
                case 1:
                    profileName = "Administrador Sistema";
                    break;
                case 2:
                    profileName = "Administrador";
                    break;
                case 3:
                    profileName = "Usuário Comum";
                    break;
            }

            var userProfileTests = new Faker<UserProfile>("pt-BR")
              .CustomInstantiator(f => new UserProfile
              {
                  UserProfileId = userProfileId,
                  ProfileName = profileName
              });

            return userProfileTests.Generate(3);
        }
    }
}
