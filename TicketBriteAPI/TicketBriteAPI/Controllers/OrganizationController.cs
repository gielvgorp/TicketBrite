using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Services;
using TicketBrite.Data.ApplicationDbContext;
using TicketBrite.Data.Repositories;

namespace TicketBriteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly EventService eventService;
        private readonly TicketService ticketService;
        private readonly OrganizationService organizationService;
        private readonly AdminService adminService;

        public OrganizationController(ApplicationDbContext context) 
        {
            eventService = new EventService(new EventRepository(context));
            ticketService = new TicketService(new TicketRepository(context));
            organizationService = new OrganizationService(new OrganizationRepository(context));
            adminService = new AdminService(new AdminRepository(context));
        }

        [HttpGet("get-events/{organizationID}")]
        public JsonResult GetAllEventsOfOrganization(Guid organizationID)
        {
            List<Event> events = organizationService.GetAllEventsByOrganization(organizationID);

            return new JsonResult(Ok(events));
        }

        [HttpGet("/organization/get-events/overview/{organizationID}")]
        public JsonResult GetEventsOfOrganizationOverview(Guid organizationID)
        {
            List<Event> verifiedEvents = organizationService.GetVerifiedEventsByOrganization(organizationID);
            List<Event> UnverifiedEvents = organizationService.GetUnVerifiedEventsByOrganization(organizationID);

            return new JsonResult(Ok(new { verifiedEvents, UnverifiedEvents }));
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
