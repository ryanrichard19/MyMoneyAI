using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMoneyAI.Application.Services;
using MyMoneyAI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using MyMoneyAI.API.Models;
using MyMoneyAI.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MyMoneyAI.Domain.Interfaces;
using MyMoneyAI.Application.DTOs;

namespace MyMoneyAI.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, ITokenService tokenService, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _logger = logger;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("test")]
        public IActionResult Test()
        {
            _logger.LogInformation("Get method called");
            return Ok("Authentication works!");
        }
       

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            var newUser = new User { UserName = registerUserDto.Username, Email = registerUserDto.Email };
            var user = await _userRepository.RegisterUserAsync(newUser, registerUserDto.Password);

            if (user == null)
            {
                return BadRequest("Registration failed.");
            }

            return Ok(new RegisterResponse { UserId = user.Id, UserName = user.UserName });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginUserDto.Username);

            if (user == null || !await _userRepository.CheckPasswordAsync(user, loginUserDto.Password))
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = await _tokenService.GenerateToken(user);
            return Ok(new LoginResponseDto { UserId = user.Id, UserName = user.UserName, Token = token });
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok("Authentication GetAllUsers with Adminworks!");
        }


    }
}
