using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Services;
using TicketBrite.Data.ApplicationDbContext;
using TicketBrite.Data.Repositories;
using TicketBriteAPI.Models;

namespace TicketBriteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly UserService _userService;

        public AuthController(IConfiguration iConfig, ApplicationDbContext context)
        {
            _userService = new UserService(new UserRepository(context));
            _jwtTokenService = new JwtTokenService(iConfig, _userService);
        }


        [HttpPost("login")]
        public IActionResult Login(LoginViewModel model)
        {
            bool verified = _userService.VerifyUser(model.UserEmail, model.Password);

            if (!verified) return NotFound("Gebruiker niet gevonden");

            var token = _jwtTokenService.GenerateJwtToken(_userService.GetUserByEmail(model.UserEmail)); // Token genereren
            return Ok(new { Token = token });
        }

        [HttpPost("Register")]
        public IActionResult RegisterUser(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Een of meerdere velden zijn leeg!");
            }


            User user = new User
            {
                userName = model.FullName,
                userEmail = model.Email,
                userPasswordHash = _userService.HashPassword(model.Password),
                roleID = Guid.Parse("43A72AC5-91BA-402D-83F5-20F23B637A92"),
                organizationID = Guid.Empty
            };

            _userService.AddUser(user);

            var token = _jwtTokenService.GenerateJwtToken(user); // Token genereren
            return Ok(new { Token = token });
        }
    }
}
