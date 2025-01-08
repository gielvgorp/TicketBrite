using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Services;
using TicketBrite.Data.ApplicationDbContext;
using TicketBrite.Data.Repositories;
using TicketBrite.DTO;
using TicketBriteAPI.Models;

namespace TicketBriteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly DashboardService _dashboardService;
        public DashboardController(ApplicationDbContext context) 
        { 
            _context = context;
            _dashboardService = new DashboardService(new TicketRepository(_context), new EventRepository(_context));
        }

        [HttpPost("/dashboard/tickets/save")]
        public JsonResult SaveTickets(List<TicketModel> tickets)
        {
            try
            {
                List<EventTicket> col = new List<EventTicket>();

                if (tickets.Count > 0)
                {
                    Guid eventID = tickets[0].eventID;

                    foreach (TicketModel ticket in tickets)
                    {
                        if (ticket.ticketID == Guid.Empty) ticket.ticketID = Guid.NewGuid();

                        col.Add(new EventTicket
                        {
                            ticketID = ticket.ticketID,
                            eventID = ticket.eventID,
                            ticketMaxAvailable = ticket.ticketMaxAvailbale,
                            ticketName = ticket.ticketName,
                            ticketPrice = ticket.ticketPrice,
                            ticketStatus = ticket.ticketStatus,
                        });
                    }

                    _dashboardService.SaveTickets(col);
                }

                return new JsonResult(Ok("Tickets succesfully edited!"));
            }
            catch (Exception ex)
            {
                return new JsonResult(NotFound(ex.Message));
            }

           
        }

        [HttpPost("/dashboard/event/save")]
        public JsonResult SaveEvent(EventDTO model)
        {
            try
            {
                _dashboardService.SaveEvent(model);

                return new JsonResult(Ok("Succesfully saved"));
            }
            catch (Exception ex)
            {

                return new JsonResult(NotFound(ex.Message));
            }
           
        }

        [HttpGet("/dashboard/tickets-statistics/{eventID}")]
        public JsonResult GetTicketStats(Guid eventID)
        {
            try
            {
                List<TicketStatistic> model = _dashboardService.GetTicketStatistics(eventID);

                return new JsonResult(Ok(model));
            }
            catch (Exception ex)
            {
                return new JsonResult(NotFound(ex.Message));
            }
        }
    }
}
