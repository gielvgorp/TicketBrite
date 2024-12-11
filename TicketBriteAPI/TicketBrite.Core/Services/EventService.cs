using TicketBrite.Core.Domains;
using TicketBrite.Core.Interfaces;
using TicketBrite.DTO;

namespace TicketBrite.Core.Services
{
    public class EventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository c_eventRepository)
        {
            _eventRepository = c_eventRepository;
        }

        public void AddEvent(EventDTO _event)
        {

            ValidateEvent(_event);

            EventDomain domain = new EventDomain
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

            _eventRepository.AddEvent(domain);
        }

        public void UpdateEvent(EventDTO _event)
        {
            ValidateEvent(_event);

            EventDomain domain = new EventDomain
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

            _eventRepository.UpdateEvent(domain);
        }

        private void ValidateEvent(EventDTO _event)
        {
            if (_event == null)
                throw new ArgumentNullException("Event kan niet null zijn.");

            if (string.IsNullOrWhiteSpace(_event.eventName))
                throw new ArgumentException("Event naam mag niet leeg zijn.");

            if (string.IsNullOrWhiteSpace(_event.eventLocation))
                throw new ArgumentException("Event locatie mag niet leeg zijn.");

            if (_event.eventDateTime < DateTime.Now)
                throw new ArgumentException("Eventdatum moet in de toekomst liggen.");

            if (_event.eventAge < 0 || _event.eventAge > 120)
                throw new ArgumentException("Event leeftijd moet tussen 0 en 120 liggen.");

            if (string.IsNullOrWhiteSpace(_event.eventCategory))
                throw new ArgumentException("Eventcategorie mag niet leeg zijn.");

            if (string.IsNullOrWhiteSpace(_event.eventImage))
                throw new ArgumentException("Event afbeelding mag niet leeg zijn.");
        }

        private List<EventDTO> ConvertToDtoList(List<EventDomain> domainList)
        {
            return domainList.Select(item => new EventDTO
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

        public List<EventDTO> GetEvents()
        {
            List<EventDomain> domainList = _eventRepository.GetEvents();
            return ConvertToDtoList(domainList);
        }

        public List<EventDTO> GetEvents(string category)
        {
            List<EventDomain> domainList = _eventRepository.GetEvents(category);
            return ConvertToDtoList(domainList);
        }

        public EventDTO GetEvent(Guid eventID)
        {
            EventDomain eventDomain = _eventRepository.GetEventByID(eventID);

            return new EventDTO
            {
                eventID = eventDomain.eventID,
                organizationID = eventDomain.organizationID,
                eventAge = eventDomain.eventAge,
                eventCategory = eventDomain.eventCategory,
                eventDateTime = eventDomain.eventDateTime,
                eventDescription = eventDomain.eventDescription,
                eventImage = eventDomain.eventImage,
                eventLocation = eventDomain.eventLocation,
                eventName = eventDomain.eventName,
                isVerified = eventDomain.isVerified
            };
        }

        public List<EventDTO> GetAllVerifiedEvents(string category)
        {
            List<EventDomain> domainList = _eventRepository.GetAllVerifiedEvents(category);
            return ConvertToDtoList(domainList);
        }
    }
}
