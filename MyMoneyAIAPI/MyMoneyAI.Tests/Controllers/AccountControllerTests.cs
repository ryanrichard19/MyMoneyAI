using Microsoft.AspNetCore.Mvc;
using Moq;
using MyMoneyAI.API.Controllers;
using MyMoneyAI.Application.DTOs;
using MyMoneyAI.Application.Interfaces;
using MyMoneyAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Tests.Controllers
{
    public class AccountControllerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _tokenServiceMock = new Mock<ITokenService>();
            _controller = new AccountController(_userRepositoryMock.Object, _tokenServiceMock.Object);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnOkObjectResult_WhenUserIsRegisteredSuccessfully()
        {
            // Arrange
            var registerUserDto = new RegisterUserDto { Username = "testuser", Password = "Test@123" };
            var user = new User { Id = "1", UserName = registerUserDto.Username };

            _userRepositoryMock.Setup(x => x.RegisterUserAsync(It.IsAny<User>(), registerUserDto.Password))
                              .ReturnsAsync(user);

            // Act
            var result = await _controller.Register(registerUserDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnBadRequestObjectResult_WhenUserRegistrationFails()
        {
            // Arrange
            var registerUserDto = new RegisterUserDto { Username = "testuser", Password = "Test@123" };

            _userRepositoryMock.Setup(x => x.RegisterUserAsync(It.IsAny<User>(), registerUserDto.Password))
                              .ReturnsAsync((User)null);

            // Act
            var result = await _controller.Register(registerUserDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task LoginUserAsync_ShouldReturnOkObjectResult_WhenCredentialsAreValid()
        {
            // Arrange
            var loginUserDto = new LoginUserDto { Username = "testuser", Password = "Test@123" };
            var user = new User { Id = "1", UserName = loginUserDto.Username };
            var token = "sample.token.string";

            _userRepositoryMock.Setup(x => x.GetUserByUsernameAsync(loginUserDto.Username))
                              .ReturnsAsync(user);
            _userRepositoryMock.Setup(x => x.CheckPasswordAsync(user, loginUserDto.Password))
                              .ReturnsAsync(true);
            _tokenServiceMock.Setup(x => x.GenerateToken(user)).Returns(token);

            // Act
            var result = await _controller.Login(loginUserDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<LoginResponseDto>(okResult.Value);
            Assert.Equal(user.Id, response.UserId);
            Assert.Equal(user.UserName, response.UserName);
            Assert.Equal(token, response.Token);
        }

        [Fact]
        public async Task LoginUserAsync_ShouldReturnUnauthorizedObjectResult_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginUserDto = new LoginUserDto { Username = "testuser", Password = "WrongPassword" };
            var user = new User { Id = "1", UserName = loginUserDto.Username };

            _userRepositoryMock.Setup(x => x.GetUserByUsernameAsync(loginUserDto.Username))
                              .ReturnsAsync(user);
            _userRepositoryMock.Setup(x => x.CheckPasswordAsync(user, loginUserDto.Password))
                              .ReturnsAsync(false);

            // Act
            var result = await _controller.Login(loginUserDto);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }
    }
}
