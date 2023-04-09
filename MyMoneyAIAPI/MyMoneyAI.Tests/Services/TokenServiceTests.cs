using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using MyMoneyAI.Application.Services;
using MyMoneyAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Tests.Services
{
    public class TokenServiceTests
    {
        [Fact]
        public async Task GenerateToken_ShouldReturnValidToken()
        {
            // Arrange
            var jwtSettings = new JwtSettings { Secret = "aVeryLongSecretKeyForTesting", ExpirationInMinutes = 60 };

            var userStoreMock = new Mock<IUserStore<User>>();
            var userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            var tokenService = new TokenService(Options.Create(jwtSettings), userManagerMock.Object);

            var user = new User { Id = "1", UserName = "testuser" };

            // Act
            var token = await tokenService.GenerateToken(user);

            // Assert
            Assert.NotNull(token);

            var jwtToken = tokenService.ValidateToken(token);
            Assert.NotNull(jwtToken);

            var idClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid");
            Assert.NotNull(idClaim);
            Assert.Equal(user.Id, idClaim.Value);

            var userNameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "unique_name");
            Assert.NotNull(userNameClaim);
            Assert.Equal(user.UserName, userNameClaim.Value);
        }

        [Fact]
        public void ValidateToken_ShouldReturnNullForInvalidToken()
        {
            // Arrange
            var jwtSettings = new JwtSettings { Secret = "aVeryLongSecretKeyForTesting", ExpirationInMinutes = 60 };

            var userStoreMock = new Mock<IUserStore<User>>();
            var userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            var tokenService = new TokenService(Options.Create(jwtSettings), userManagerMock.Object);
            var invalidToken = "invalid.token.string";

            // Act
            var jwtToken = tokenService.ValidateToken(invalidToken);

            // Assert
            Assert.Null(jwtToken);
        }
    }
}
