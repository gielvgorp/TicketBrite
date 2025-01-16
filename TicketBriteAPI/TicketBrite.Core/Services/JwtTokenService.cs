using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TicketBrite.DTO;

namespace TicketBrite.Core.Services
{
    public class JwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserService userService;

        public JwtTokenService(IConfiguration configuration, UserService _userService)
        {
            _configuration = configuration;
            userService = _userService;
        }

        public string GenerateJwtToken(UserDTO user)
        {
            RoleDTO role = userService.GetUserRole(user.userID);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.userID.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.userName),
                new Claim("role", role.roleName),
                new Claim(JwtRegisteredClaimNames.Email, user.userEmail)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["JwtSettings:ExpiryInMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateJwtToken(GuestDTO guest)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, guest.guestID.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, guest.guestName),
                new Claim("role", "Guest"),
                new Claim(JwtRegisteredClaimNames.Email, guest.guestEmail)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["JwtSettings:ExpiryInMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
