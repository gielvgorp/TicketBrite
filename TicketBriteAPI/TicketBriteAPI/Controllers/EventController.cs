using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Enums;
using TicketBrite.Core.Services;
using TicketBrite.Data.ApplicationDbContext;
using TicketBrite.Data.Repositories;
using TicketBrite.DTO;
using TicketBriteAPI.Models;

namespace TicketBriteAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;
        private readonly TicketService _ticketService;
        private readonly AuthService _authService;

        public EventController(ApplicationDbContext context)
        {
            _eventService = new EventService(new EventRepository(context));
            _ticketService = new TicketService(new TicketRepository(context));
            _authService = new AuthService(new AuthRepository(context), new UserRepository(context));

        }

        [HttpGet("events")]
        public JsonResult GetEvents()
        {
            return new JsonResult(Ok(_eventService.GetEvents()));
        }

        [HttpGet("events/verified/{category}")]
        public JsonResult GetAllVerifiedEvents(string category)
        {
            List<EventDTO> events = _eventService.GetAllVerifiedEvents(category);

            return new JsonResult(Ok(events));
        }

        [HttpGet("verified")]
        public JsonResult GetAllVerifiedEvents()
        {
            List<EventDTO> events = _eventService.GetAllVerifiedEvents("");

            return new JsonResult(Ok(events));
        }

        [HttpGet("unverified")]
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

                List<EventDTO> result = _eventService.GetAllUnVerifiedEvents();

                return new JsonResult(Ok(result));
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

        [HttpPut("event")]
        [Authorize]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 400)]
        public JsonResult SaveEvent(EventDTO model)
        {
            try
            {
                Guid userID;

                if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userID) || !_authService.VerifyAccessPermission(userID, Roles.Organization))
                {
                    throw new UnauthorizedAccessException();
                }

                _eventService.SaveEvent(model);
                return new JsonResult(Ok(string.Format(ExceptionMessages.UpdatedSuccesfully, "Event")));
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

        [HttpGet("events/{category}")]
        public JsonResult GetEvents(string category)
        {
            return new JsonResult(Ok(_eventService.GetEvents(category)));
        }

        [HttpPost("event")]
        [Authorize]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 400)]
        public JsonResult AddEvent(EventDTO model)
        {
            try
            {
                Guid userID;
                if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userID) || (!_authService.VerifyAccessPermission(userID, Roles.Admin) || !_authService.VerifyAccessPermission(userID, Roles.Organization)))
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
            catch (Exception)
            {
                return new JsonResult(BadRequest(ExceptionMessages.GeneralException));
            }
            
        }

        [HttpGet("event/{eventID}")]
        [ProducesResponseType(typeof(EventInfoViewModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public JsonResult GetEvent(Guid eventID)
        {
            try
            {
                EventInfoViewModel result = new EventInfoViewModel();
                List<TicketModel> ticketModel = new List<TicketModel>();
                List<EventTicket> tickets = _ticketService.GetTicketsOfEvent(eventID);

                foreach (var item in tickets)
                {
                    TicketModel ticket = new TicketModel
                    {
                        ticketID = item.ticketID,
                        eventID = item.eventID,
                        ticketMaxAvailbale = item.ticketMaxAvailable,
                        ticketName = item.ticketName,
                        ticketPrice = item.ticketPrice,
                        ticketStatus = item.ticketStatus,
                        ticketsRemaining = _ticketService.CalculateRemainingTickets(item.ticketID)
                    };

                    ticketModel.Add(ticket);
                }

                result.Event = _eventService.GetEvent(eventID);
                result.Tickets = ticketModel;

                return new JsonResult(Ok(result));
            }
            catch (KeyNotFoundException)
            {
                return new JsonResult(NotFound(ExceptionMessages.EventNotFound));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest(ExceptionMessages.GeneralException));
            }
        }

        [HttpPut("event/{eventID}/status")]
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

                _eventService.UpdateEventVerificationStatus(isVerified, eventID);
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
