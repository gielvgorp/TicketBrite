using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Services;
using TicketBrite.Data.ApplicationDbContext;
using TicketBrite.Data.Repositories;
using TicketBrite.DTO;
using TicketBriteAPI.Models;

namespace TicketBriteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly UserService _userService;
        private readonly AuthService _authService;

        public AuthController(IConfiguration iConfig, ApplicationDbContext context)
        {
            _userService = new UserService(new UserRepository(context));
            _jwtTokenService = new JwtTokenService(iConfig, _userService);
            _authService = new AuthService(new AuthRepository(context));
        }


        [HttpPost("login")]
        public JsonResult Login(LoginViewModel model)
        {
            bool verified = _authService.VerifyUser(model.UserEmail, model.Password);

            if (!verified) 
                return new JsonResult(NotFound("Gebruiker niet gevonden"));

            var token = _jwtTokenService.GenerateJwtToken(_userService.GetUserByEmail(model.UserEmail));

            return new JsonResult(Ok(new { Token = token }));
        }

        [HttpPost("/auth/guest/{guestID}/{verificationID}")]
        public JsonResult GuestLogin(Guid guestID, Guid verificationID)
        {
            try
            {
                GuestDTO guest = _authService.VerifyGuest(guestID, verificationID);

                var token = _jwtTokenService.GenerateJwtToken(guest); // Token genereren
                return new JsonResult(Ok(new { Token = token }));
            }
            catch (Exception ex)
            {
                return new JsonResult(NotFound(ex.Message));
            }
        }

        [HttpPost("Register")]
        public JsonResult RegisterUser(RegisterViewModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.FullName) || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                {
                    throw new Exception("Een of meerdere velden zijn leeg!");
                }

                CreateUserDTO user = new CreateUserDTO
                {
                    UserName = model.FullName,
                    UserEmail = model.Email,
                    Password = model.Password
                };

                _userService.AddUser(user);

                UserDTO result = _userService.GetUserByEmail(model.Email);

                if(result == null)
                    throw new ArgumentNullException("Gebruiker is niet gevonden!");

                var token = _jwtTokenService.GenerateJwtToken(result);
                return new JsonResult(Ok(new { Token = token }));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
          
        }
    }
}
