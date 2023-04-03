using Microsoft.AspNetCore.Mvc;
using MyMoneyAI.Application.DTOs;
using MyMoneyAI.Application.Interfaces;
using MyMoneyAI.Domain.Entities;
using MyMoneyAI.Domain.Interfaces;

namespace MyMoneyAI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AccountController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
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

            var token = _tokenService.GenerateToken(user);
            return Ok(new LoginResponseDto { UserId = user.Id, UserName = user.UserName, Token = token });
        }
    }
}
