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
    public class OrganizationController : ControllerBase
    {
        private readonly EventService eventService;
        private readonly TicketService ticketService;
        private readonly OrganizationService organizationService;

        public OrganizationController(ApplicationDbContext context) 
        {
            eventService = new EventService(new EventRepository(context));
            ticketService = new TicketService(new TicketRepository(context));
            organizationService = new OrganizationService(new OrganizationRepository(context));
        }

        [HttpGet("get-events/{organizationID}")]
        public JsonResult GetAllEventsOfOrganization(Guid organizationID)
        {
            var events = organizationService.GetEventsByOrganization(organizationID);
            return new JsonResult(Ok(events));
        }

        [HttpPost("event/new")]
        public JsonResult AddNewEvent(Event model)
        {
            eventService.AddEvent(model);

/*            foreach (EventTicket ticket in model.Tickets)
            {
                ticketService.CreateTicket(ticket);
            }*/

            return new JsonResult(Ok("Evenement aangemaakt!"));
        }
    }
}
