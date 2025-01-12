using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
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
    public class UserController : ControllerBase
    {
        private UserService _userService;
        private JwtTokenService _jwtTokenService;

        public UserController(ApplicationDbContext context, IConfiguration iConig) 
        {
            _userService = new UserService(new UserRepository(context));
            _jwtTokenService = new JwtTokenService(iConig, _userService);
        }

        [HttpGet("get-user")]
        [Authorize]
        [ProducesResponseType(typeof(UserDTO), 200)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public JsonResult GetUserData()
        {
            try
            {
                var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userID == null)
                {
                    throw new UnauthorizedAccessException();
                }

                UserDTO user = _userService.GetUser(Guid.Parse(userID));

                return new JsonResult(Ok(user));
            }
            catch (UnauthorizedAccessException)
            {
                return new JsonResult(Unauthorized(ExceptionMessages.UnauthorizedAccess));
            }
            catch (KeyNotFoundException)
            {
                return new JsonResult(NotFound(ExceptionMessages.UserNotFound));
            } 
            catch (Exception)
            {
                return new JsonResult(BadRequest(ExceptionMessages.GeneralException));
            }
        }

        [HttpPost("/guest/create")]
        [ProducesResponseType(typeof(ShoppingCartModel), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 400)]
        public JsonResult CreateGuest(Guest model)
        {
            try
            {
                if(string.IsNullOrEmpty(model.guestEmail) || string.IsNullOrEmpty(model.guestName))
                {
                    throw new ValidationException();
                }

                GuestDTO guestDTO = new GuestDTO
                {
                    guestName = model.guestName,
                    guestEmail = model.guestEmail,
                };

                _userService.AddGuest(guestDTO);

                var token = _jwtTokenService.GenerateJwtToken(guestDTO);
                return new JsonResult(Ok(new { Token = token }));
            }
            catch (ValidationException)
            {
                return new JsonResult(BadRequest(ExceptionMessages.FieldsEmpty));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest(ExceptionMessages.GeneralException));
            }
        }
    }
}
