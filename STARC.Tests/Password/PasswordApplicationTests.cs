using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace STARC.Tests.Password
{
    [Collection(nameof(PasswordCollection))]
    public class PasswordApplicationTests
    {
        public PasswordTestsFixture Fixture { get; set; }

        public PasswordApplicationTests(PasswordTestsFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact(DisplayName = "Should set cripto password successfully")]
        [Trait("Category", "Password Application Tests")]
        public void PasswordManager_SetCriptoPassword_ShouldReturnsHashPassword()
        {
            // Arrange
            var app = Fixture.GetPasswordAppService();

            // Act
            var hashPassword = app.SetCriptoPassword("admin");

            // Assert Fluent Assertions
            hashPassword.Password.Should().NotBeNullOrEmpty();
            hashPassword.Password.Should().NotBe("admin");
            hashPassword.Salt.Should().BeOfType<byte[]>();
        }

        [Fact(DisplayName = "Should get cripto password successfully")]
        [Trait("Category", "Password Application Tests")]
        public void PasswordManager_GetCriptoPassword_ShouldReturnsPassword()
        {
            // Arrange
            var app = Fixture.GetPasswordAppService();
            var salt = Fixture.GenerateSalt("0xF2AAE5AB737144A57EC78745C82F7433");

            // Act
            var password = app.GetCriptoPassword("admin", salt);

            // Assert Fluent Assertions
            password.Should().Be("/hR/l/WEyLUdT3dfp46+tI8mNfpwoAwtPrEyTZ2fz/I=");
        }

        [Fact(DisplayName = "Should not get cripto password when password is blank")]
        [Trait("Category", "Password Application Tests")]
        public void PasswordManager_GetCriptoPassword_BlankPassword_ShouldThrowsArgumentException()
        {
            // Arrange
            var app = Fixture.GetPasswordAppService();
            var salt = Fixture.GenerateSalt("0xF2AAE5AB737144A57EC78745C82F7433");            

            // Act
            Exception ex = Assert.Throws<ArgumentException>(() => app.GetCriptoPassword(string.Empty, salt));

            // Assert Fluent Assertions
            ex.Message.Should().Be("Invalid parameters");
        }

        [Fact(DisplayName = "Should not get cripto password when salt is null")]
        [Trait("Category", "Password Application Tests")]
        public void PasswordManager_GetCriptoPassword_NullSalt_ShouldThrowsArgumentException()
        {
            // Arrange
            var app = Fixture.GetPasswordAppService();

            // Act
            Exception ex = Assert.Throws<ArgumentException>(() => app.GetCriptoPassword("admin", null));

            // Assert Fluent Assertions
            ex.Message.Should().Be("Invalid parameters");
        }

        [Fact(DisplayName = "Should get hash password")]
        [Trait("Category", "Password Application Tests")]
        public void PasswordManager_GetHashPassword_ShouldReturn()
        {
            // Arrange
            var app = Fixture.GetPasswordAppService();
            var salt = Fixture.GenerateSalt("0xF2AAE5AB737144A57EC78745C82F7433");

            Fixture.PasswordRepositoryMock.Setup(c => c.GetHashPassword(1)).Returns(salt);

            // Act
            var passwordHash = app.GetHashPassword(1);

            // Assert Fluent Assertions
            passwordHash.Should().BeOfType<byte[]>();
        }
    }
}
