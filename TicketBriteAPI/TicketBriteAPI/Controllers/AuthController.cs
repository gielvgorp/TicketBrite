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
        private readonly ApplicationDbContext _applicationDbContext;

        public AuthController(IConfiguration iConfig, ApplicationDbContext context)
        {
            _userService = new UserService(new UserRepository(context));
            _jwtTokenService = new JwtTokenService(iConfig, _userService);
            _applicationDbContext = context;
        }


        [HttpPost("login")]
        public IActionResult Login(LoginViewModel model)
        {
            User user = _applicationDbContext.Users.FirstOrDefault(u => u.userEmail == model.UserEmail && u.userPasswordHash == model.Password);

            if (user == null) return NotFound("Gebruiker niet gevonden");

            var token = _jwtTokenService.GenerateJwtToken(user); // Token genereren
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
                userPasswordHash = model.Password,
                roleID = Guid.Empty,
                organizationID = Guid.Empty
            };

            _userService.AddUser(user);

            var token = _jwtTokenService.GenerateJwtToken(user); // Token genereren
            return Ok(new { Token = token });
        }
    }
}
