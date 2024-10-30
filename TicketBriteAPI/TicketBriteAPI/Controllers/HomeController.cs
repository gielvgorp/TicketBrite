using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketBriteAPI.Models;

namespace TicketBriteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/get-events")]
        public JsonResult GetEvents(){
            var events = 

        return new JsonResult(Ok(events));
        }
    }
}
