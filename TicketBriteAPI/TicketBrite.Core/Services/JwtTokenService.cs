using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;

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

        public string GenerateJwtToken(User user)
        {
            Role role = userService.GetUserRole(user.userID);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.userID.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.userName),
                new Claim(ClaimTypes.Role, role.roleName),
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
    }
}
