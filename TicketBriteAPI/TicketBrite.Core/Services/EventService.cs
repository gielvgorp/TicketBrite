using System.ComponentModel.DataAnnotations;
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
            try
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
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Message);
            }
            
        }

        public void SaveEvent(EventDTO _event)
        {
            EventDomain eventDomain = new EventDomain
            {
                eventID = _event.eventID,
                organizationID = _event.organizationID,
                isVerified = _event.isVerified,
                eventName = _event.eventName,
                eventLocation = _event.eventLocation,
                eventImage = _event.eventImage,
                eventAge = _event.eventAge,
                eventCategory = _event.eventCategory,
                eventDateTime = _event.eventDateTime,
                eventDescription = _event.eventDescription
            };

            _eventRepository.UpdateEvent(eventDomain);
        }

        private void ValidateEvent(EventDTO _event)
        {
            if (_event == null)
                throw new ValidationException("Event kan niet null zijn.");

            if (string.IsNullOrWhiteSpace(_event.eventName))
                throw new ValidationException("Event naam mag niet leeg zijn.");

            if (string.IsNullOrWhiteSpace(_event.eventLocation))
                throw new ValidationException("Event locatie mag niet leeg zijn.");

            if (_event.eventDateTime < DateTime.Now)
                throw new ValidationException("Eventdatum moet in de toekomst liggen.");

            if (_event.eventAge < 0 || _event.eventAge > 120)
                throw new ValidationException("Event leeftijd moet tussen 0 en 120 liggen.");

            if (string.IsNullOrWhiteSpace(_event.eventCategory))
                throw new ValidationException("Eventcategorie mag niet leeg zijn.");

            if (string.IsNullOrWhiteSpace(_event.eventImage))
                throw new ValidationException("Event afbeelding mag niet leeg zijn.");
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

            if (eventDomain == null)
                throw new KeyNotFoundException("Evenement niet gevonden!");

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

        public List<EventDTO> GetAllUnVerifiedEvents()
        {
            List<EventDomain> unVerifiedEvents = _eventRepository.GetAllUnVerifiedEvents();
            return ConvertToEventDTOList(unVerifiedEvents);
        }

        public void UpdateEventVerificationStatus(bool value, Guid eventID)
        {
            try
            {
                if (eventID == Guid.Empty)
                    throw new ArgumentException("Event ID is leeg!");

                _eventRepository.UpdateEventVerificationStatus(value, eventID);
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException($"Ongeldige {ex.ParamName}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Algemene foutmelding bij andere fouten
                throw new InvalidOperationException("Er is een fout opgetreden bij het updaten van de evenementstatus.", ex);
            }
        }

        private List<EventDTO> ConvertToEventDTOList(List<EventDomain> events)
        {
            return events.Select(item => new EventDTO
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
    }
}
