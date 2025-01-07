using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Services;
using TicketBrite.Data.ApplicationDbContext;
using TicketBrite.Data.Repositories;
using TicketBrite.DTO;

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
        [Authorize] // Beveilig het eindpunt met JWT-authenticatie
        public JsonResult GetUserData()
        {
            try
            {
                var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userID == null)
                {
                    throw new KeyNotFoundException("Gebruiker heeft geen geldige ID");
                }

                UserDTO user = _userService.GetUser(Guid.Parse(userID));

                return new JsonResult(Ok(user));
            }
            catch (KeyNotFoundException ex)
            {
                return new JsonResult(NotFound(ex.Message));
            } 
            catch(InvalidOperationException ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
        }

        [HttpPost("/guest/create")]
        public JsonResult CreateGuest(Guest model)
        {
            try
            {

                GuestDTO guestDTO = new GuestDTO
                {
                    guestName = model.guestName,
                    guestEmail = model.guestEmail,
                };

                _userService.AddGuest(guestDTO);

                var token = _jwtTokenService.GenerateJwtToken(guestDTO); // Token genereren
                return new JsonResult(Ok(new { Token = token }));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
        }
    }
}
