using System;
using System.Collections.Generic;
using TicketBrite.Core.Domains;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;
using TicketBrite.DTO;

namespace TicketBrite.Core.Services
{
    public class DashboardService
    {
        private readonly ITicketRepository _ticketRepository;
        public DashboardService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
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
