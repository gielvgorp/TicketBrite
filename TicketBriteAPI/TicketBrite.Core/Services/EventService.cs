using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;

namespace TicketBrite.Core.Services
{
    public class EventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository c_eventRepository)
        {
            _eventRepository = c_eventRepository;
        }

        public void AddEvent(Event _event)
        {
            _eventRepository.AddEvent(_event);
        }

        public List<Event> GetEvents()
        {
            return _eventRepository.GetEvents();
        }

        public List<Event> GetEvents(string category)
        {
            return _eventRepository.GetEvents(category);
        }

        public Event GetEvent(Guid eventID)
        {
            return _eventRepository.GetEvent(eventID);
        }
    }
}
