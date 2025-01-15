using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Enums;
using TicketBrite.Core.Services;
using TicketBrite.Data.ApplicationDbContext;
using TicketBrite.Data.Repositories;
using TicketBrite.DTO;

namespace TicketBriteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly EventService _eventService;
        private readonly TicketService _ticketService;
        private readonly OrganizationService _organizationService;
        private readonly AuthService _authService;

        public OrganizationController(ApplicationDbContext context)
        {
            _eventService = new EventService(new EventRepository(context));
            _ticketService = new TicketService(new TicketRepository(context));
            _organizationService = new OrganizationService(new OrganizationRepository(context));
            _authService = new AuthService(new AuthRepository(context), new UserRepository(context));
        }

        [HttpGet("events/{organizationID}")]
        [Authorize]
        [ProducesResponseType(typeof(List<EventDTO>), 200)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 400)]
        public JsonResult GetAllEventsOfOrganization(Guid organizationID)
        {
            try
            {
                Guid userID;

                if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userID) || !_authService.VerifyAccessPermission(userID, Roles.Organization) || !_authService.VerifyOrganizationAccessPermission(userID, organizationID))
                {
                    throw new UnauthorizedAccessException();
                }

                List<EventDTO> events = _organizationService.GetAllEventsByOrganization(organizationID);

                return new JsonResult(Ok(events));
            }
            catch (UnauthorizedAccessException)
            {
                return new JsonResult(Unauthorized(ExceptionMessages.UnauthorizedAccess));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest(ExceptionMessages.GeneralException));
            }
        }

        [HttpGet("events/{organizationID}/overview")]
        [ProducesResponseType(typeof(List<EventDTO>), 200)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 400)]
        [Authorize]
        public JsonResult GetEventsOfOrganizationOverview(Guid organizationID)
        {
            try
            {
                Guid userID;

                if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userID) || !_authService.VerifyAccessPermission(userID, Roles.Organization) || !_authService.VerifyOrganizationAccessPermission(userID, organizationID))
                {
                    throw new UnauthorizedAccessException();
                }

                List<EventDTO> verifiedEvents = _organizationService.GetVerifiedEventsByOrganization(organizationID);
                List<EventDTO> UnverifiedEvents = _organizationService.GetUnVerifiedEventsByOrganization(organizationID);

                return new JsonResult(Ok(new { verifiedEvents, UnverifiedEvents }));
            }
            catch (UnauthorizedAccessException)
            {
                return new JsonResult(Unauthorized(ExceptionMessages.UnauthorizedAccess));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest(ExceptionMessages.GeneralException));
            }
        }

        [HttpPost("event/new")]
        [Authorize]
        [ProducesResponseType(typeof(List<EventDTO>), 200)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 400)]
        public JsonResult AddNewEvent(EventDTO model)
        {
            try
            {
                Guid userID;

                if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userID) || !_authService.VerifyAccessPermission(userID, Roles.Organization))
                {
                    throw new UnauthorizedAccessException();
                }
                _eventService.AddEvent(model);

                return new JsonResult(Ok(string.Format(ExceptionMessages.CreatedSuccelfully, "Evenement")));
            }
            catch (UnauthorizedAccessException)
            {
                return new JsonResult(Unauthorized(ExceptionMessages.UnauthorizedAccess));
            }
            catch (ValidationException ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest(ExceptionMessages.GeneralException));
            }
        }
    }
}
