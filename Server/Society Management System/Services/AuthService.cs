using Microsoft.AspNetCore.Identity;
using Society_Management_System.DTO;
using Society_Management_System.Models;

namespace Society_Management_System.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterUser(RegisterDto user);
        Task<string> Login(Login login);
    }

    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<bool> RegisterUser(RegisterDto User)
        {
            var user = new User
            {
                FullName = User.FullName,
                UserName = User.Email,
                Email = User.Email,
            };
            
            var result = await _userManager.CreateAsync(user,User.Password);
            if (result.Succeeded)
            {
               
                return true;
            }
            else {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error: {error.Code} - {error.Description}");
                }

                return false;
            }
        }

        public async Task<string> Login(Login login)
        {
            var user = await _userManager.FindByNameAsync(login.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                return  _tokenService.CreateToken(user); 
            }

            return string.Empty;
        }
    }

}
