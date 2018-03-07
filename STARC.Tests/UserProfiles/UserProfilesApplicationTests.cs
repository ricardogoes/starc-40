using FluentAssertions;
using System.Linq;
using Xunit;

namespace STARC.Tests.UserProfiles
{
    [Collection(nameof(UserProfilesCollection))]
    public class UserProfileApplicationTests
    {
        public UserProfilesTestsFixture Fixture { get; set; }

        public UserProfileApplicationTests(UserProfilesTestsFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact(DisplayName = "Should get all user profiles")]
        [Trait("Category", "User Profiles Application Tests")]
        public void UserProfileApplication_GetAllToGrid_ShouldReturnsAllUserProfile()
        {
            // Arrange
            var userProfileApp = Fixture.GetUserProfileAppService();
            Fixture.UserProfileRepositoryMock.Setup(c => c.GetAll()).Returns(Fixture.GenerateProfiles());

            // Act
            var userProfiles = userProfileApp.GetAll().ToList();

            // Assert Fluent Assertions
            userProfiles.Should().HaveCount(c => c > 1).And.OnlyHaveUniqueItems();
        }

        [Fact(DisplayName = "Should get user profile by userProfileId")]
        [Trait("Category", "User Profiles Application Tests")]
        public void UserProfileApplication_GetById_ShouldReturnUserProfile()
        {
            // Arrange
            var userProfileApp = Fixture.GetUserProfileAppService();
            Fixture.UserProfileRepositoryMock.Setup(c => c.GetById(3)).Returns(Fixture.GenerateValidUserProfile());

            // Act
            var userProfile = userProfileApp.GetById(3);

            // Assert Fluent Assertions
            userProfile.UserProfileId.Should().Be(3);
            userProfile.ProfileName.Should().Be("Usuário Comum");
        }

        [Fact(DisplayName = "Should not get user profile by invalid userProfileId")]
        [Trait("Category", "User Profiles Application Tests")]
        public void UserProfileApplication_GetByIdInvalid_ShouldNotReturnUserProfile()
        {
            // Arrange
            var userProfileApp = Fixture.GetUserProfileAppService();
            Fixture.UserProfileRepositoryMock.Setup(c => c.GetById(3)).Returns(Fixture.GenerateValidUserProfile());

            // Act
            var userProfile = userProfileApp.GetById(2);

            // Assert Fluent Assertions
            userProfile.Should().BeNull();
        }
    }
}
