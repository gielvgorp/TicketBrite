using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;

namespace TicketBrite.Core.Services
{
    public class TicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository c_ticketRepository)
        {
            _ticketRepository = c_ticketRepository;
        }

        public void CreateTicket(EventTicket ticket)
        {
            _ticketRepository.CreateTicket(ticket);
        }

        public List<EventTicket> GetTicketsOfEvent(Guid eventID)
        {
            return _ticketRepository.GetTicketsOfEvent(eventID);
        }

        public EventTicket GetTicket(Guid ticketID)
        {
            return _ticketRepository.GetTicketByID(ticketID);
        }
    }
}
