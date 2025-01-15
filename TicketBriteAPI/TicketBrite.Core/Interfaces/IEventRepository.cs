using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Domains;
using TicketBrite.Core.Entities;
using TicketBrite.DTO;

namespace TicketBrite.Core.Interfaces
{
    public interface IEventRepository
    {
        public void AddEvent(EventDomain newEvent);
        public void UpdateEvent(EventDomain _event);
        public List<EventDomain> GetEvents();
        public List<EventDomain> GetEvents(string category);
        public EventDomain GetEventByID(Guid eventID);
        public List<EventDomain> GetAllVerifiedEvents(string category);
        public List<EventDomain> GetAllUnVerifiedEvents();
        public void UpdateEventVerificationStatus(bool value, Guid eventID);
    }
}
