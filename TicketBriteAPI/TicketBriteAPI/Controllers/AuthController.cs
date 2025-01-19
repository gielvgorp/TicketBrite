using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
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

        public AuthController(IConfiguration iConfig, UserService userService, AuthService authService)
        {
            _userService = userService;
            _jwtTokenService = new JwtTokenService(iConfig, userService);
            _authService = authService;
        }


        [HttpPost("login")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public JsonResult Login(LoginViewModel model)
        {
            try
            {
                if(string.IsNullOrEmpty(model.UserEmail)) 
                {
                    throw new ValidationException(string.Format(ExceptionMessages.FieldRequired, "Email adres"));
                }

                if (string.IsNullOrEmpty(model.Password))
                {
                    throw new ValidationException(string.Format(ExceptionMessages.FieldRequired, "Wachtwoord"));
                }

                bool verified = _authService.VerifyUser(model.UserEmail, model.Password);

                if (!verified)
                {
                    throw new UnauthorizedAccessException();
                }

                string token = _jwtTokenService.GenerateJwtToken(_userService.GetUserByEmail(model.UserEmail));

                return new JsonResult(Ok(new { Token = token }));
            }
            catch (ValidationException ex)
            {
                return new JsonResult(NotFound(ex.Message));
            }
            catch (UnauthorizedAccessException)
            {
                return new JsonResult(NotFound(ExceptionMessages.UserAuthenticationFailed));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest(ExceptionMessages.GeneralException));
            }
           
        }

        [HttpPost("guest/{guestID}/{verificationID}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public JsonResult GuestLogin(Guid guestID, Guid verificationID)
        {
            try
            {
                if(guestID == Guid.Empty || verificationID == Guid.Empty)
                {
                    throw new ValidationException();
                }

                GuestDTO guest = _authService.VerifyGuest(guestID, verificationID);

                string token = _jwtTokenService.GenerateJwtToken(guest);
                return new JsonResult(Ok(new { Token = token }));
            }
            catch (UnauthorizedAccessException)
            {
                return new JsonResult(NotFound(ExceptionMessages.GuestAuthenticationFailed));
            }
            catch (ValidationException)
            {
                return new JsonResult(BadRequest(ExceptionMessages.InvalidInputValue));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest(ExceptionMessages.GeneralException));
            }
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(List<EventDTO>), 200)]
        [ProducesResponseType(typeof(void), 0)]
        [ProducesResponseType(typeof(string), 400)]
        public JsonResult RegisterUser(RegisterViewModel model)
        {
            try
            {
                CreateUserDTO user = new CreateUserDTO
                {
                    UserName = model.FullName,
                    UserEmail = model.Email,
                    Password = model.Password
                };

                _userService.AddUser(user);

                UserDTO result = _userService.GetUserByEmail(model.Email);

                if(result == null)
                    throw new KeyNotFoundException(ExceptionMessages.UserNotFound);

                string token = _jwtTokenService.GenerateJwtToken(result);
                return new JsonResult(Ok(new { Token = token }));
            }
            catch (ValidationException ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
            catch (KeyNotFoundException ex)
            {
                return new JsonResult(NotFound(ex.Message));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
          
        }
    }
}
