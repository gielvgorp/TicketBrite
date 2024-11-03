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
    public class TicketController : ControllerBase
    {
        private readonly TicketService ticketService;

        public TicketController(ApplicationDbContext context)
        {
            ticketService = new TicketService(new TicketRepository(context));
        }

        [HttpPost("/ticket/new")]
        public JsonResult AddTIcket(EventTicket model)
        {
            ticketService.CreateTicket(model);

            return new JsonResult(Ok());
        }

        [HttpGet("/get-ticket/{ticketID}")]
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
    }
}
