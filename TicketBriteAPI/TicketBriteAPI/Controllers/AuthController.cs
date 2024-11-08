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
        private readonly AuthService _authService;

        public AuthController(IConfiguration iConfig, ApplicationDbContext context)
        {
            _userService = new UserService(new UserRepository(context));
            _jwtTokenService = new JwtTokenService(iConfig, _userService);
            _applicationDbContext = context;
            _authService = new AuthService(new AuthRepository(context));
        }


        [HttpPost("login")]
        public JsonResult Login(LoginViewModel model)
        {
            try
            {
                User user = _authService.VerifyUser(model.UserEmail, model.Password);

                var token = _jwtTokenService.GenerateJwtToken(user); // Token genereren
                return new JsonResult(Ok(new { Token = token }));
            }
            catch (Exception ex)
            {
                return new JsonResult(NotFound(ex.Message));    
            }
        }

        [HttpPost("/auth/guest/{guestID}/{verificationID}")]
        public JsonResult GuestLogin(Guid guestID, Guid verificationID)
        {
            try
            {
                Guest guest = _authService.VerifyGuest(guestID, verificationID);

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
                if (!ModelState.IsValid)
                {
                    throw new Exception("Een of meerdere velden zijn leeg!");
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
                return new JsonResult(Ok(new { Token = token }));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
          
        }
    }
}
