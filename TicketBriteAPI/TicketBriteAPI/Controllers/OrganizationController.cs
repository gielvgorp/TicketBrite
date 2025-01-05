using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Services;
using TicketBrite.Data.ApplicationDbContext;
using TicketBrite.Data.Repositories;
using TicketBrite.DTO;
using TicketBriteAPI.Hubs;
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
            List<EventDTO> events = organizationService.GetAllEventsByOrganization(organizationID);

            return new JsonResult(Ok(events));
        }

        [HttpGet("/organization/get-events/overview/{organizationID}")]
        public JsonResult GetEventsOfOrganizationOverview(Guid organizationID)
        {
            List<EventDTO> verifiedEvents = organizationService.GetVerifiedEventsByOrganization(organizationID);
            List<EventDTO> UnverifiedEvents = organizationService.GetUnVerifiedEventsByOrganization(organizationID);

            return new JsonResult(Ok(new { verifiedEvents, UnverifiedEvents }));
        }

        [HttpPost("event/new")]
        public JsonResult AddNewEvent(EventDTO model)
        {
            try
            {
                eventService.AddEvent(model);

                return new JsonResult(Ok("Evenement aangemaakt!"));
            }
            catch (ValidationException ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
        }
    }
}
