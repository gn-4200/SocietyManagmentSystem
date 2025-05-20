using Microsoft.AspNetCore.Mvc;
using Society_Management_System.DTO;
using Society_Management_System.Models;
using Society_Management_System.Services;

namespace Society_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto user)
        {
            var result = await _authService.RegisterUser(user);

            if (!result)
                return BadRequest(result);

            
            var loginDto = new Login
            {
                Email = user.Email,
                Password = user.Password
            };

            var token = await _authService.Login(loginDto);

            if (string.IsNullOrEmpty(token))
                return Unauthorized("User created but failed to generate token");

            return Ok(new { Token = token });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(Login login)
        {
            var token = await _authService.Login(login);
            return string.IsNullOrEmpty(token) ? Unauthorized("Invalid credentials") : Ok(new { Token = token });
        }
    }

}
