using Microsoft.AspNetCore.Identity;
using Moq;
using MyMoneyAI.Application.Interfaces;
using MyMoneyAI.Domain.Entities;
using MyMoneyAI.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Tests.Repositories
{
    public class UserRepositoryTests
    {

        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly IUserRepository _userRepository;

        public UserRepositoryTests()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            _userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            _userRepository = new UserRepository(_userManagerMock.Object);
        }


        [Fact]
        public async Task GetUserByUsernameAsync_ShouldReturnUser()
        {
            // Arrange
            var username = "testuser";
            var user = new User { Id = "1", UserName = username };

            _userManagerMock.Setup(x => x.FindByNameAsync(username)).ReturnsAsync(user);

            // Act
            var result = await _userRepository.GetUserByUsernameAsync(username);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.UserName, result.UserName);
            _userManagerMock.Verify(x => x.FindByNameAsync(username), Times.Once);
        }

        [Fact]
        public async Task CheckPasswordAsync_ShouldReturnTrueWhenPasswordIsValid()
        {
            // Arrange
            var user = new User { Id = "1", UserName = "testuser" };
            var password = "ValidPassword";

            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, password)).ReturnsAsync(true);

            // Act
            var result = await _userRepository.CheckPasswordAsync(user, password);

            // Assert
            Assert.True(result);
            _userManagerMock.Verify(x => x.CheckPasswordAsync(user, password), Times.Once);
        }

        [Fact]
        public async Task CheckPasswordAsync_ShouldReturnFalseWhenPasswordIsInvalid()
        {
            // Arrange
            var user = new User { Id = "1", UserName = "testuser" };
            var password = "InvalidPassword";

            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, password)).ReturnsAsync(false);

            // Act
            var result = await _userRepository.CheckPasswordAsync(user, password);

            // Assert
            Assert.False(result);
            _userManagerMock.Verify(x => x.CheckPasswordAsync(user, password), Times.Once);
        }


        [Fact]
        public async Task RegisterUserAsync_ShouldCreateUser()
        {
            // Arrange
            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var userRepository = new UserRepository(_userManagerMock.Object);
            var newUser = new User { UserName = "testuser" };
            var password = "TestPassword123!";

            // Act
            var createdUser = await userRepository.RegisterUserAsync(newUser, password);

            // Assert
            Assert.NotNull(createdUser);
            Assert.Equal(newUser.UserName, createdUser.UserName);
            _userManagerMock.Verify(um => um.CreateAsync(newUser, password), Times.Once);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser()
        {
            // Arrange
            var testUser = new User { Id = "1", UserName = "testuser" };
            _userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(testUser);

            var userRepository = new UserRepository(_userManagerMock.Object);

            // Act
            var user = await userRepository.GetUserByIdAsync(testUser.Id);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(testUser.Id, user.Id);
            Assert.Equal(testUser.UserName, user.UserName);
            _userManagerMock.Verify(um => um.FindByIdAsync(testUser.Id), Times.Once);
        }

    }
}
