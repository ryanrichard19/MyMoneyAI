using Microsoft.AspNetCore.Identity;
using Moq;
using MyMoneyAI.Domain.Entities;
using MyMoneyAI.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Tests
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task RegisterUserAsync_ShouldCreateUser()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var userRepository = new UserRepository(userManagerMock.Object);
            var newUser = new User { UserName = "testuser" };
            var password = "TestPassword123!";

            // Act
            var createdUser = await userRepository.RegisterUserAsync(newUser, password);

            // Assert
            Assert.NotNull(createdUser);
            Assert.Equal(newUser.UserName, createdUser.UserName);
            userManagerMock.Verify(um => um.CreateAsync(newUser, password), Times.Once);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            var testUser = new User { Id = "1", UserName = "testuser" };
            userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(testUser);

            var userRepository = new UserRepository(userManagerMock.Object);

            // Act
            var user = await userRepository.GetUserByIdAsync(testUser.Id);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(testUser.Id, user.Id);
            Assert.Equal(testUser.UserName, user.UserName);
            userManagerMock.Verify(um => um.FindByIdAsync(testUser.Id), Times.Once);
        }
   
    }
}
