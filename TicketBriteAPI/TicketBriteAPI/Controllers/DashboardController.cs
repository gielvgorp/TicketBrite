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
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _dashboardService;
        private readonly AuthService _authService;
        public DashboardController(ApplicationDbContext context) 
        { 
            _dashboardService = new DashboardService(new TicketRepository(context), new EventRepository(context));
            _authService = new AuthService(new AuthRepository(context), new UserRepository(context));
        }

        [HttpPut("tickets/save")]
        [Authorize]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 400)]
        public JsonResult SaveTickets(List<TicketModel> tickets)
        {
            try
            {
                Guid userID;

                if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userID) || !_authService.VerifyAccessPermission(userID, Roles.Organization))
                {
                    throw new UnauthorizedAccessException();
                }

                List<EventTicket> col = new List<EventTicket>();

                if (tickets.Count > 0)
                {
                    Guid eventID = tickets[0].eventID;

                    foreach (TicketModel ticket in tickets)
                    {
                        // Als guid empty is, is het een nieuwe ticket
                        if (ticket.ticketID == Guid.Empty)
                        {
                            ticket.ticketID = Guid.NewGuid();
                        }

                        col.Add(new EventTicket
                        {
                            ticketID = ticket.ticketID,
                            eventID = ticket.eventID,
                            ticketMaxAvailable = ticket.ticketMaxAvailbale,
                            ticketName = ticket.ticketName,
                            ticketPrice = ticket.ticketPrice,
                            ticketStatus = ticket.ticketStatus,
                        });
                    }

                    _dashboardService.SaveTickets(col);
                }

                return new JsonResult(Ok(string.Format(ExceptionMessages.UpdatedSuccesfully, "Tickets")));
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

        [HttpPut("event/save")]
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

                _dashboardService.SaveEvent(model);
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

        [HttpGet("tickets-statistics/{eventID}")]
        [Authorize]
        [ProducesResponseType(typeof(List<TicketStatistic>), 200)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 400)]
        public JsonResult GetTicketStats(Guid eventID)
        {
            try
            {
                Guid userID;

                if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userID) || !_authService.VerifyAccessPermission(userID, Roles.Organization))
                {
                    throw new UnauthorizedAccessException();
                }

                List<TicketStatistic> model = _dashboardService.GetTicketStatistics(eventID);
                return new JsonResult(Ok(model));
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
    }
}
