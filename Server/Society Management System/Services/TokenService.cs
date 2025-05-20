using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Society_Management_System.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Society_Management_System.Services
{
    public interface ITokenService
    {
        string CreateToken(IdentityUser user);
    }
public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string CreateToken(IdentityUser user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.UserName )
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);            
            var expirationTime = DateTime.UtcNow.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: expirationTime,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}
