using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Domains;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;
using TicketBrite.DTO;

namespace TicketBrite.Core.Services
{
    public class DashboardService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IEventRepository _eventRepository;
        public DashboardService(ITicketRepository ticketRepository, IEventRepository eventRepository) 
        { 
            _ticketRepository = ticketRepository;
            _eventRepository = eventRepository;
        }

        public void SaveTickets(Guid eventID, List<EventTicket> tickets)
        {
            foreach (EventTicket ticket in tickets)
            {
                EventTicket result = _ticketRepository.GetTicketByID(ticket.ticketID);

                if (result != null)
                {
                    _ticketRepository.UpdateTicket(ticket);
                }
                else
                {
                    _ticketRepository.CreateTicket(ticket);
                }
            }
        }

        public void SaveEvent(EventDTO _event)
        {
            EventDomain eventDomain = new EventDomain
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

            _eventRepository.UpdateEvent(eventDomain);
        }

        public int GetSoldTickets(Guid ticketID)
        {
            return _ticketRepository.GetSoldTickets(ticketID).Count;
        }

        public int GetReserveTickets(Guid ticketID)
        {
            return _ticketRepository.GetReservedTicketsByTicket(ticketID).Count;
        }

        public List<TicketStatistic> GetTicketStatistics(Guid eventID)
        {
            List<EventTicket> tickets = _ticketRepository.GetTicketsOfEvent(eventID);

            List<TicketStatistic> result = new List<TicketStatistic>();

            foreach (EventTicket ticket in tickets)
            {
                result.Add(new TicketStatistic
                {
                    ticket = ticket,
                    ticketReserve = GetReserveTickets(ticket.ticketID),
                    ticketSold = GetSoldTickets(ticket.ticketID)
                });
            }

            return result;
        }
    }
}
