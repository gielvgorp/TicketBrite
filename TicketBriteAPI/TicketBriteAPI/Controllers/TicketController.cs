using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Security.Claims;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Enums;
using TicketBrite.Core.Services;
using TicketBrite.Data.ApplicationDbContext;
using TicketBrite.Data.Repositories;
using TicketBriteAPI.Models;

namespace TicketBriteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;
        private readonly ITicketStatisticsNotifier _ticketStatisticsNotifier;
        private readonly AuthService _authService;

        public TicketController(ApplicationDbContext context, ITicketStatisticsNotifier ticketNotifier, TicketService ticketService, AuthService authService)
        {
            _ticketService = ticketService;
            _ticketStatisticsNotifier = ticketNotifier;
            _authService = authService;
        }

        [HttpPost("new")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public JsonResult AddTIcket(EventTicket model)
        {
            _ticketService.CreateTicket(model);

            return new JsonResult(Ok("Ticket successfully created!"));
        }

        [HttpGet("ticket/{ticketID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TicketModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public JsonResult GetTicket(Guid ticketID)
        {
            EventTicket ticket = _ticketService.GetTicket(ticketID);

            TicketModel result = new TicketModel
            {
                ticketID = ticket.ticketID,
                eventID = ticket.eventID,
                ticketName = ticket.ticketName,
                ticketMaxAvailable = ticket.ticketMaxAvailable,
                ticketPrice = ticket.ticketPrice,
                ticketStatus = ticket.ticketStatus,
                ticketsRemaining = _ticketService.CalculateRemainingTickets(ticketID)
            };

            return new JsonResult(Ok(result));
        }

        [HttpGet("/event/{eventID}/tickets")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TicketModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public JsonResult GetTicketsOfEvent(Guid eventID)
        {
            List<EventTicket> tickets = _ticketService.GetTicketsOfEvent(eventID);
            List<TicketModel> result = new List<TicketModel>();

            foreach (EventTicket ticket in tickets)
            {
                result.Add(new TicketModel
                {
                    ticketID = ticket.ticketID,
                    eventID = ticket.eventID,
                    ticketName = ticket.ticketName,
                    ticketMaxAvailable = ticket.ticketMaxAvailable,
                    ticketPrice = ticket.ticketPrice,
                    ticketStatus = ticket.ticketStatus,
                    ticketsRemaining = _ticketService.CalculateRemainingTickets(ticket.ticketID)
                });
            }

            return new JsonResult(Ok(result));
        }

        [HttpPost("set-reserve")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<JsonResult> AddReservedTickets(List<ReservedTicketModel> model)
        {
            try
            {
                var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userID == null)
                    throw new AuthenticationException("Gebruiker is niet ingelogd!");

                foreach (ReservedTicketModel ticket in model)
                {
                    int remaining = _ticketService.CalculateRemainingTickets(ticket.ticketID);

                    if (remaining < ticket.quantity) 
                        throw new Exception("Er zijn niet genoeg tickets meer over!");

                    for (int i = 0; i < ticket.quantity; i++)
                    {
                        _ticketService.SetReservedTicket(ticket.ticketID, Guid.Parse(userID), Guid.NewGuid());
                    }

                    await _ticketStatisticsNotifier.NotifyStatisticsUpdated(ticket.ticketID, _ticketService.GetSoldTickes(ticket.ticketID).Count, _ticketService.GetReservedTicketsByTicket(ticket.ticketID).Count);
                }

                return new JsonResult(Ok());
            }
            catch (AuthenticationException ex)
            {
                return new JsonResult(Unauthorized(ex.Message));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
          
        }

        [HttpGet("reserve-ticket/tickets")]
        [Authorize]
        public JsonResult GetReservedTicketsOfUser()
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userID == null) return new JsonResult(Unauthorized());

            List<UserReservedTicketViewModel> result = new List<UserReservedTicketViewModel>();
            List<ReservedTicket> reservedTickets = _ticketService.GetReservedTicketsOfUser(Guid.Parse(userID));

            foreach (ReservedTicket ticket in reservedTickets) 
            {
                result.Add(new UserReservedTicketViewModel
                {
                    reservation = ticket,
                    ticket = _ticketService.GetTicket(ticket.ticketID)
                });
            }

            return new JsonResult(Ok(result));
        }

        [HttpPost("ticket/buy")]
        [Authorize]
        public async Task<JsonResult> BuyTickets()
        {
            try
            {
                Guid userID = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                Guid purchaseID = Guid.NewGuid();

                if (userID == null || userID == Guid.Empty) return new JsonResult(Unauthorized("Gebruiker niet ingelogd!"));

                _ticketService.UpdateReservedTicketsToPursche(userID, purchaseID);

                foreach (EventTicket ticket in _ticketService.GetPurchaseByID(purchaseID))
                {
                    await _ticketStatisticsNotifier.NotifyStatisticsUpdated(ticket.ticketID, _ticketService.GetSoldTickes(ticket.ticketID).Count, _ticketService.GetReservedTicketsByTicket(ticket.ticketID).Count);
                }

                return new JsonResult(Ok(purchaseID));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
        }

        [HttpGet("purchase/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EventTicket>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        /// <summary>
        /// Get purchased tickets of purchse by id
        /// </summary>
        /// <param name="id">Primary key purchase id.</param>
        /// <returns>Returns a list of tickets.</returns>
        public JsonResult GetPurchaseByID(Guid id)
        {
            try
            {
                List<EventTicket> result = _ticketService.GetPurchaseByID(id);

                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
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
                            ticketMaxAvailable = ticket.ticketMaxAvailable,
                            ticketName = ticket.ticketName,
                            ticketPrice = ticket.ticketPrice,
                            ticketStatus = ticket.ticketStatus,
                        });
                    }

                    _ticketService.SaveTickets(col);
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

        [HttpGet("/user/purchase")]
        [Authorize]
        public JsonResult GetPurchaseOfUser()
        {
            try
            {
                Guid userID = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                if (userID == null || userID == Guid.Empty) return new JsonResult(Unauthorized("Gebruiker niet ingelogd!"));

                List<UserPurchaseModel> result = _ticketService.GetPurchasesOfUser(userID);

                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
        }
    }
}
