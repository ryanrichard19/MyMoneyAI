using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMoneyAI.Application.Services;
using MyMoneyAI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using MyMoneyAI.API.Models;
using MyMoneyAI.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MyMoneyAI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ITokenService _tokenService;

        public UserController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Authentication works!");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // This is just a temporary user for testing purposes
            var user = new User { UserName = "TestUser", Email = "test@example.com" };
            var password = "Test123!";

            // Check the user's credentials (replace this with proper authentication later)
            if (request.Username == user.UserName && request.Password == password)
            {
                var token = _tokenService.GenerateToken(user);
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }
}
