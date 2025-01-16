using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TicketBriteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrototypeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PrototypeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string GenerateJwtToken(string roleID)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, "test"),
                new Claim("role", roleID)
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

        [HttpPost("auth")]
        public IActionResult Auth([FromBody] string selectedValue)
        {
            string token = GenerateJwtToken(selectedValue);

            return Ok(token);
        }

        [HttpGet("auth-check")]
        [Authorize]
        public IActionResult AuthCheck()
        {
            var username = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            return Ok(username);
        }

        [HttpGet("authorize-check/{requestRole}")]
        [Authorize]
        public IActionResult AuthCheck(string requestRole)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

            if(requestRole == role)
            {
                return Ok("Je hebt rechten!");
            }

            return StatusCode(403, new { message = "Geen toegang!" });
        }
    }
}
