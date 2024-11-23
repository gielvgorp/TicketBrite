using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;
using Microsoft.AspNetCore.SignalR;

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

        public int CalculateRemainingTickets(Guid ticketID)
        {
            var ticket = GetTicket(ticketID);
            var soldTikets = GetSoldTickes(ticketID);
            var reservedTickets = GetReservedTicketsByTicket(ticketID);

            return ticket.ticketMaxAvailable - soldTikets.Count - reservedTickets.Count;
        }

        public List<ReservedTicket> GetReservedTicketsByTicket(Guid ticketID)
        {
            return _ticketRepository.GetReservedTicketsByTicket(ticketID);
        }

        public List<ReservedTicket> GetReservedTicketsOfUser(Guid ticketID)
        {
            return _ticketRepository.GetReservedTicketsOfUser(ticketID);
        }

        public List<SoldTicket> GetSoldTickes(Guid ticketID)
        {
            return _ticketRepository.GetSoldTickets(ticketID);
        }

        public void SetReservedTicket(Guid ticketID, Guid userID, Guid reservationID)
        {
            _ticketRepository.SetReserveTicket(ticketID, userID, reservationID);
        }

        public void SetPurscheTicket(Guid ticketID, Guid userID, Guid purchaseID)
        {
            if (CalculateRemainingTickets(ticketID) <= 0) throw new Exception("Ticket niet meer op voorraad!");

            _ticketRepository.SetPurscheTicket(ticketID, userID, purchaseID);
        }

        public void RemoveReservedTicket(Guid reservedID)
        {
            _ticketRepository.RemoveReserveTicket(reservedID);
        }

        public void UpdateReservedTicketsToPursche(Guid userID, Guid purchaseID)
        {
            List<ReservedTicket> reservedTickets = GetReservedTicketsOfUser(userID);
            _ticketRepository.CreatePursche(purchaseID, userID);

            foreach (ReservedTicket reserveTicket in reservedTickets)
            {
                SetPurscheTicket(reserveTicket.ticketID, userID, purchaseID);
                RemoveReservedTicket(reserveTicket.reservedID);
            }
        }

        public List<EventTicket> GetPurchaseByID(Guid purchaseID)
        {
            return _ticketRepository.GetPurchaseByID(purchaseID);
        }

        public List<UserPurchaseModel> GetPurchasesOfUser(Guid userID)
        {
            return _ticketRepository.GetPurchasesOfUser(userID);
        }
    }
}
