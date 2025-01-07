using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Claims;
using TicketBrite.Core.Entities;
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
        private readonly TicketService ticketService;
        private readonly ITicketStatisticsNotifier _ticketStatisticsNotifier;

        public TicketController(ApplicationDbContext context, ITicketStatisticsNotifier ticketNotifier)
        {
            ticketService = new TicketService(new TicketRepository(context));
            _ticketStatisticsNotifier = ticketNotifier;
        }

        [HttpPost("/ticket/new")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public JsonResult AddTIcket(EventTicket model)
        {
            ticketService.CreateTicket(model);

            return new JsonResult(Ok("Ticket successfully created!"));
        }

        [HttpGet("/get-ticket/{ticketID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TicketModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public JsonResult GetTicket(Guid ticketID)
        {
            EventTicket ticket = ticketService.GetTicket(ticketID);

            TicketModel result = new TicketModel
            {
                ticketID = ticket.ticketID,
                eventID = ticket.eventID,
                ticketName = ticket.ticketName,
                ticketMaxAvailbale = ticket.ticketMaxAvailable,
                ticketPrice = ticket.ticketPrice,
                ticketStatus = ticket.ticketStatus,
                ticketsRemaining = ticketService.CalculateRemainingTickets(ticketID)
            };

            return new JsonResult(Ok(result));
        }

        [HttpGet("/event/{eventID}/get-tickets")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TicketModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public JsonResult GetTicketsOfEvent(Guid eventID)
        {
            List<EventTicket> tickets = ticketService.GetTicketsOfEvent(eventID);
            List<TicketModel> result = new List<TicketModel>();

            foreach (EventTicket ticket in tickets)
            {
                result.Add(new TicketModel
                {
                    ticketID = ticket.ticketID,
                    eventID = ticket.eventID,
                    ticketName = ticket.ticketName,
                    ticketMaxAvailbale = ticket.ticketMaxAvailable,
                    ticketPrice = ticket.ticketPrice,
                    ticketStatus = ticket.ticketStatus,
                    ticketsRemaining = ticketService.CalculateRemainingTickets(ticket.ticketID)
                });
            }

            return new JsonResult(Ok(result));
        }

        [HttpPost("/ticket/set-reserve")]
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
                    int remaining = ticketService.CalculateRemainingTickets(ticket.ticketID);

                    if (remaining < ticket.quantity) 
                        throw new Exception("Er zijn niet genoeg tickets meer over!");

                    for (int i = 0; i < ticket.quantity; i++)
                    {
                        ticketService.SetReservedTicket(ticket.ticketID, Guid.Parse(userID), Guid.NewGuid());
                    }

                    await _ticketStatisticsNotifier.NotifyStatisticsUpdated(ticket.ticketID, ticketService.GetSoldTickes(ticket.ticketID).Count, ticketService.GetReservedTicketsByTicket(ticket.ticketID).Count);
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

        [HttpGet("/ticket/reserve-ticket/get-tickets")]
        [Authorize]
        public JsonResult GetReservedTicketsOfUser()
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userID == null) return new JsonResult(Unauthorized());

            List<UserReservedTicketViewModel> result = new List<UserReservedTicketViewModel>();
            List<ReservedTicket> reservedTickets = ticketService.GetReservedTicketsOfUser(Guid.Parse(userID));

            foreach (ReservedTicket ticket in reservedTickets) 
            {
                result.Add(new UserReservedTicketViewModel
                {
                    reservation = ticket,
                    ticket = ticketService.GetTicket(ticket.ticketID)
                });
            }

            return new JsonResult(Ok(result));
        }

        [HttpPost("/tickets/buy")]
        [Authorize]
        public async Task<JsonResult> BuyTickets()
        {
            try
            {
                Guid userID = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                Guid purchaseID = Guid.NewGuid();

                if (userID == null || userID == Guid.Empty) return new JsonResult(Unauthorized("Gebruiker niet ingelogd!"));

                ticketService.UpdateReservedTicketsToPursche(userID, purchaseID);

                foreach (EventTicket ticket in ticketService.GetPurchaseByID(purchaseID))
                {
                    await _ticketStatisticsNotifier.NotifyStatisticsUpdated(ticket.ticketID, ticketService.GetSoldTickes(ticket.ticketID).Count, ticketService.GetReservedTicketsByTicket(ticket.ticketID).Count);
                }

                return new JsonResult(Ok(purchaseID));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
        }

        [HttpGet("/get-purchase/{id}")]
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
                List<EventTicket> result = ticketService.GetPurchaseByID(id);

                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
        }

        [HttpGet("/user/get-purchase")]
        [Authorize]
        public JsonResult GetPurchaseOfUser()
        {
            try
            {
                Guid userID = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                if (userID == null || userID == Guid.Empty) return new JsonResult(Unauthorized("Gebruiker niet ingelogd!"));

                List<UserPurchaseModel> result = ticketService.GetPurchasesOfUser(userID);

                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
        }
    }
}
