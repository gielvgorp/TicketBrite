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
            var events = new List<EventModel>
        {
            new EventModel
            {
                Id = 1,
                EventName = "Snelle: LIVE!",
                Artist = "Snelle",
                EventDate = new DateTime(2024, 10, 1, 19, 15, 0),
                EventLocation = "Jaarbeurs, Utrecht",
                EventAge = 12,
                Category = "Muziek",
                Image = "https://www.agentsafterall.nl/wp-content/uploads/Naamloos-1-header-1-1600x740.jpg",
                Description = "Op 1 oktober 2024 staat Snelle in de Johan Cruijff ArenA voor een spectaculaire liveshow...",
                Tickets = new List<TicketModel>
                {
                    new TicketModel { Name = "Staanplaatsen", Price = 45 },
                    new TicketModel { Name = "Zitplaatsen", Price = 65 },
                    new TicketModel { Name = "VIP Area", Price = 65 }
                }
            },
            new EventModel
            {
                Id = 2,
                EventName = "Snollebollekes",
                Artist = "Snollebollekes",
                EventDate = new DateTime(2024, 10, 12, 19, 15, 0),
                EventLocation = "Johan Cruijff ArenA, Amsterdam",
                EventAge = 18,
                Category = "Muziek",
                Image = "https://www.voxweb.nl/wp-content/uploads/2021/10/Nijmeegs-Studentenorkest-Snollebollekes.jpeg",
                Description = "Op 12 oktober 2024 neemt Snollebollekes de Johan Cruijff ArenA over voor een onvergetelijke avond...",
                Tickets = new List<TicketModel>
                {
                    new TicketModel { Name = "Staanplaatsen", Price = 45 },
                    new TicketModel { Name = "Zitplaatsen", Price = 65 },
                    new TicketModel { Name = "Golden Circle", Price = 150 }
                }
            }
        };

        return new JsonResult(Ok(events));
        }
    }
}
