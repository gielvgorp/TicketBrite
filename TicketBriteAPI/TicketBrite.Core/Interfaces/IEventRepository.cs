using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;

namespace TicketBrite.Core.Interfaces
{
    public interface IEventRepository
    {
        public void AddEvent(Event newEvent);
        public void UpdateEvent(Event _event);
        public List<Event> GetEvents();
        public List<Event> GetEvents(string category);
        public Event GetEvent(Guid eventID);
    }
}
