using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;

namespace TicketBrite.Data.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        ApplicationDbContext.ApplicationDbContext _dbContext;

        public TicketRepository(ApplicationDbContext.ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public void CreateTicket(EventTicket ticket)
        {
            _dbContext.Tickets.Add(ticket);
            _dbContext.SaveChanges();
        }

        public List<EventTicket> GetTicketsOfEvent(Guid eventID)
        {
            return _dbContext.Tickets.Where(t => t.eventID == eventID).ToList();
        }

        public EventTicket GetTicketByID(Guid ticketID)
        {
            return _dbContext.Tickets.FirstOrDefault(t => t.ticketID == ticketID);
        }
    }
}
