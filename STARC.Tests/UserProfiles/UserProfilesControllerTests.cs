using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using STARC.Api.Models;
using STARC.Domain.Entities;
using System.Collections.Generic;
using Xunit;

namespace STARC.Tests.UserProfiles
{
    [Collection(nameof(UserProfilesCollection))]
    public class UserProfilesControllerTests
    {
        public UserProfilesTestsFixture Fixture { get; set; }

        public UserProfilesControllerTests(UserProfilesTestsFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact(DisplayName = "Should get all user profiles")]
        [Trait("Category", "User Profiles Controller Tests")]
        public void UserProfilesController_GetAllToGrid_ShouldReturnsAllUserProfiles()
        {
            // Arrange
            var controller = Fixture.GetUserProfilesController();
            Fixture.UserProfileAppServiceMock.Setup(c => c.GetAll()).Returns(Fixture.GenerateProfiles());

            // Act
            var response = controller.Get() as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Success);
            var userProfiles = data.Data as IEnumerable<UserProfile>;
            userProfiles.Should().HaveCount(c => c == 3);
        }

        [Fact(DisplayName = "Should get user profile by id")]
        [Trait("Category", "User Profiles Controller Tests")]
        public void UserProfilesController_GetById_ShouldReturnUserProfile()
        {
            // Arrange
            var controller = Fixture.GetUserProfilesController();
            Fixture.UserProfileAppServiceMock.Setup(c => c.GetById(3)).Returns(Fixture.GenerateValidUserProfile());

            // Act
            var response = controller.Get(3) as ObjectResult;
            var data = response.Value as UserProfile;

            // Assert Fluent Assertions
            data.Should().NotBeNull();
            data.UserProfileId.Should().Be(3);
        }

        [Fact(DisplayName = "Should not get user profile when userProfileId = 1")]
        [Trait("Category", "User Profiles Controller Tests")]
        public void UserProfilesController_GetById_ShouldNotReturnUserProfileBadRequest()
        {
            // Arrange
            var controller = Fixture.GetUserProfilesController();
            Fixture.UserProfileAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidUserProfile());

            // Act
            var response = controller.Get(1) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Failed);
            data.Message.Should().Be("Profile not Found");
        }

        [Fact(DisplayName = "Should not get user profile with invalid userProfileId")]
        [Trait("Category", "User Profiles Controller Tests")]
        public void UserProfilesController_GetById_ShouldNotReturnUserProfile()
        {
            // Arrange
            var controller = Fixture.GetUserProfilesController();
            Fixture.UserProfileAppServiceMock.Setup(c => c.GetById(1)).Returns(Fixture.GenerateValidUserProfile());

            // Act
            var response = controller.Get(2) as ObjectResult;
            var data = response.Value as ApiResponse;

            // Assert Fluent Assertions
            data.State.Should().Be(ApiResponseState.Failed);
            data.Message.Should().Be("Profile not Found");
        }
    }
}
