using TicketBrite.Core.Domains;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;

namespace TicketBrite.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        ApplicationDbContext.ApplicationDbContext _dbContext;

        public EventRepository(ApplicationDbContext.ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public void AddEvent(EventDomain _event)
        {
            Event newEvent = new Event
            {
                eventID = _event.eventID,
                eventAge = _event.eventAge,
                eventCategory = _event.eventCategory,
                eventDateTime = _event.eventDateTime,
                eventDescription = _event.eventDescription,
                eventImage = _event.eventImage,
                eventLocation = _event.eventLocation,
                eventName = _event.eventName,
                isVerified = _event.isVerified,
                organizationID = _event.organizationID
            };

            _dbContext.Events.Add(newEvent);
            _dbContext.SaveChanges();
        }

        public void UpdateEvent(EventDomain updatedEvent)
        {
            Event existingEvent = _dbContext.Events.Find(updatedEvent.eventID);

            if (existingEvent == null)
                throw new KeyNotFoundException("Event not found!");

            existingEvent.organizationID = updatedEvent.organizationID;
            existingEvent.eventDescription = updatedEvent.eventDescription;
            existingEvent.eventCategory = updatedEvent.eventCategory;
            existingEvent.eventName = updatedEvent.eventName;
            existingEvent.eventDateTime = updatedEvent.eventDateTime;
            existingEvent.eventLocation = updatedEvent.eventLocation;
            existingEvent.eventAge = updatedEvent.eventAge;

            _dbContext.SaveChanges();
        }

        private List<EventDomain> ConvertToDomainList(List<Event> events)
        {
            return events.Select(item => new EventDomain
            {
                eventID = item.eventID,
                organizationID = item.organizationID,
                eventAge = item.eventAge,
                eventCategory = item.eventCategory,
                eventDateTime = item.eventDateTime,
                eventDescription = item.eventDescription,
                eventImage = item.eventImage,
                eventLocation = item.eventLocation,
                eventName = item.eventName,
                isVerified = item.isVerified
            }).ToList();
        }

        public List<EventDomain> GetEvents()
        {
            List<Event> events = _dbContext.Events.ToList();
            return ConvertToDomainList(events);
        }

        public List<EventDomain> GetEvents(string category)
        {
            List<Event> events = _dbContext.Events.Where(e => e.eventCategory == category).ToList();
            return ConvertToDomainList(events);
        }

        public EventDomain GetEventByID(Guid eventID)
        {
            Event result = _dbContext.Events.FirstOrDefault(e => e.eventID == eventID);

            if (result == null)
                return null;

            return new EventDomain
            {
                eventID = result.eventID,
                organizationID = result.organizationID,
                eventAge = result.eventAge,
                eventCategory = result.eventCategory,
                eventDateTime = result.eventDateTime,
                eventDescription = result.eventDescription,
                eventImage = result.eventImage,
                eventLocation = result.eventLocation,
                eventName = result.eventName,
                isVerified = result.isVerified
            };
        }

        public List<EventDomain> GetAllVerifiedEvents(string category)
        {
            if(string.IsNullOrEmpty(category))
            {
                List<Event> events = _dbContext.Events.Where(e => e.isVerified).ToList();
                return ConvertToDomainList(events);
            }
            else
            {
                List<Event> events = _dbContext.Events.Where(e => e.isVerified && e.eventCategory == category).ToList();
                return ConvertToDomainList(events);
            }
        }
    }
}
