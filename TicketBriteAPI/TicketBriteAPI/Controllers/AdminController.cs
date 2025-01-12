using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketBrite.Core.Enums;
using TicketBrite.Core.Services;
using TicketBrite.Data.ApplicationDbContext;
using TicketBrite.Data.Repositories;
using TicketBrite.DTO;

namespace TicketBriteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;
        private readonly AuthService _authService;

        public AdminController(ApplicationDbContext context)
        {
            _adminService = new AdminService(new AdminRepository(context));
            _authService = new AuthService(new AuthRepository(context), new UserRepository(context));
        }

        [HttpGet("events/unverified")]
        [Authorize]
        [ProducesResponseType(typeof(List<EventDTO>), 200)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 400)]
        public JsonResult GetAdminUnverifiedEvents()
        {
            try
            {
                Guid userID;

                if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userID) || !_authService.VerifyAccessPermission(userID, Roles.Admin))
                {
                    throw new UnauthorizedAccessException();
                }

                List<EventDTO> result = _adminService.GetAllUnVerifiedEvents();

                return new JsonResult(Ok(result));
            }
            catch(UnauthorizedAccessException)
            {
                return new JsonResult(Unauthorized(ExceptionMessages.ForbiddenAccess));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest(ExceptionMessages.GeneralException));
            }
         
        }

        [HttpPut("events/{eventID}/status")]
        [Authorize]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 400)]
        public JsonResult UpdateEventStatus(Guid eventID, [FromBody] bool isVerified)
        {
            try
            {
                Guid userID = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                if (!_authService.VerifyAccessPermission(userID, Roles.Admin))
                {
                    throw new UnauthorizedAccessException();
                }

                _adminService.UpdateEventVerificationStatus(isVerified, eventID);
                return new JsonResult(NoContent());
            }
            catch (UnauthorizedAccessException)
            {
                return new JsonResult(Unauthorized(ExceptionMessages.ForbiddenAccess));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest(ExceptionMessages.GeneralException));
            }
        }
    }
}
