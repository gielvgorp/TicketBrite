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
    public class EventController : ControllerBase
    {
        private readonly EventService eventService;
        private readonly TicketService ticketService;

        public EventController(ApplicationDbContext context)
        {
            eventService = new EventService(new EventRepository(context));
            ticketService = new TicketService(new TicketRepository(context));
        }

        [HttpGet("/get-events")]
        public JsonResult GetEvents()
        {
            return new JsonResult(Ok(eventService.GetEvents()));
        }

        [HttpGet("/event/get-all-verified/{category?}")]
        public JsonResult GetAllVerifiedEvents(string category = "")
        {
            List<Event> events = eventService.GetAllVerifiedEvents(category);

            return new JsonResult(Ok(events));
        }

        [HttpGet("/get-events/{category}")]
        public JsonResult GetEvents(string category)
        {
            return new JsonResult(Ok(eventService.GetEvents(category)));
        }

        [HttpPost("/event/new")]
        public JsonResult AddEvent(Event model)
        {
            eventService.AddEvent(model);

            return new JsonResult(Ok());
        }

        [HttpGet("/get-event/{eventID}")]
        public JsonResult GetEvent(Guid eventID)
        {
            EventInfoViewModel result = new EventInfoViewModel();
            List<TicketModel> ticketModel = new List<TicketModel>();
            var tickets = ticketService.GetTicketsOfEvent(eventID);

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
                    ticketsRemaining = ticketService.CalculateRemainingTickets(item.ticketID)
                };

                ticketModel.Add(ticket);
            }

            result.Event = eventService.GetEvent(eventID);
            result.Tickets = ticketModel;


            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok(result));
        }
    }
}
