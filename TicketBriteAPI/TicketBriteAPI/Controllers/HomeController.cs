using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketBriteAPI.Models;
using TicketBrite.Core.Entities;
using TicketBrite.Data;
using TicketBrite.Core.Services;
using TicketBrite.Data.ApplicationDbContext;
using TicketBrite.Data.Repositories;

namespace TicketBriteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly EventService eventService;
        private readonly TicketService ticketService;

        public HomeController(ApplicationDbContext context)
        {
            eventService = new EventService(new EventRepository(context));
            ticketService = new TicketService(new TicketRepository(context));
        }

        [HttpGet("/get-events")]
        public JsonResult GetEvents(){
            return new JsonResult(Ok(eventService.GetEvents()));
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
