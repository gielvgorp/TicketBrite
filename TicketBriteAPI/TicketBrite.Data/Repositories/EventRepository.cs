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

        public void UpdateEvent(Event updatedEvent)
        {
            Event existingEvent = _dbContext.Events.Find(updatedEvent.eventID);

            if (existingEvent == null)
                throw new Exception("Event not found!");

            existingEvent.organizationID = updatedEvent.organizationID;
            existingEvent.eventDescription = updatedEvent.eventDescription;
            existingEvent.eventCategory = updatedEvent.eventCategory;
            existingEvent.eventName = updatedEvent.eventName;
            existingEvent.eventDateTime = updatedEvent.eventDateTime;
            existingEvent.eventLocation = updatedEvent.eventLocation;
            existingEvent.eventAge = updatedEvent.eventAge;

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
