using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Services;
using TicketBrite.Data.ApplicationDbContext;
using TicketBrite.Data.Repositories;
using TicketBrite.DTO;

namespace TicketBriteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminService adminService;

        public AdminController(ApplicationDbContext context)
        {
            adminService = new AdminService(new AdminRepository(context));
        }

        [HttpGet("/admin/get-unverified-events")]
        public JsonResult GetAdminUnverifiedEvents()
        {
            List<EventDTO> result = adminService.GetAllUnVerifiedEvents();

            return new JsonResult(Ok(result));
        }

        [HttpPost("/admin/update-event-status/{eventID}/{value}")]
        public JsonResult UpdateEventStatus(Guid eventID, bool value)
        {
            adminService.UpdateEventVerificationStatus(value, eventID);

            return new JsonResult(Ok("Status changed!"));
        }
    }
}
