using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;

namespace TicketBrite.Core.Interfaces
{
    public interface ITicketRepository
    {
        public void CreateTicket(EventTicket ticket);
        public void UpdateTicket(EventTicket ticket);
        public List<EventTicket> GetTicketsOfEvent(Guid eventID);
        public EventTicket GetTicketByID(Guid ticketID);
        public List<SoldTicket> GetSoldTickets(Guid ticketID);
        public List<ReservedTicket> GetReservedTicketsOfUser(Guid userID);
        public List<ReservedTicket> GetReservedTicketsByTicket(Guid ticketID);
        public void SetReserveTicket(Guid ticketid, Guid userID, Guid reservationID);
        public void SetPurscheTicket(Guid ticketID, Guid userID, Guid purchaseID);
        public void RemoveReserveTicket(Guid reserveID);
        public void CreatePursche(Guid purchaseID, Guid userID);
        public List<EventTicket> GetPurchaseByID(Guid purchaseID);
    }
}
