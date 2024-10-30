using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;
using TicketBrite.Data.ApplicationDbContext;

namespace TicketBrite.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        ApplicationDbContext.ApplicationDbContext _dbContext;

        public EventRepository(ApplicationDbContext.ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public void AddEvent(Event newEvent)
        {
            _dbContext.Events.Add(newEvent);
            _dbContext.SaveChanges();
        }

        public List<Event> GetEvents()
        {
            return _dbContext.Events.ToList();
        }

        public List<Event> GetEvents(string category)
        {
            return _dbContext.Events.Where(e => e.eventCategory == category).ToList();
        }

        public Event GetEvent(Guid eventID)
        {
            return _dbContext.Events.FirstOrDefault(e => e.eventID == eventID);
        }
    }
}
