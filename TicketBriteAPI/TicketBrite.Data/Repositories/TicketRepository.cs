using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;
using TicketBrite.Data.Migrations;

namespace TicketBrite.Data.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        ApplicationDbContext.ApplicationDbContext _dbContext;

        public TicketRepository(ApplicationDbContext.ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public void CreateTicket(EventTicket ticket)
        {
            _dbContext.Tickets.Add(ticket);
            _dbContext.SaveChanges();
        }

        public List<EventTicket> GetTicketsOfEvent(Guid eventID)
        {
            return _dbContext.Tickets.Where(t => t.eventID == eventID).ToList();
        }

        public EventTicket GetTicketByID(Guid ticketID)
        {
            return _dbContext.Tickets.FirstOrDefault(t => t.ticketID == ticketID);
        }

        public List<SoldTicket> GetSoldTickets(Guid ticketID)
        {
            return _dbContext.SoldTickets.Where(t => t.ticketID == ticketID).ToList();
        }

        public List<ReservedTicket> GetReservedTicketsOfUser(Guid userID)
        {
            DateTime deadlineTime = DateTime.UtcNow.AddMinutes(-10);
            return _dbContext.ReservedTickets.Where(t => t.userID == userID && t.reservedAt >= deadlineTime).ToList();
        }

        public List<ReservedTicket> GetReservedTicketsByTicket(Guid ticketID)
        {
            DateTime deadlineTime = DateTime.UtcNow.AddMinutes(-10);
            return _dbContext.ReservedTickets.Where(t => t.ticketID == ticketID && t.reservedAt >= deadlineTime).ToList();
        }

        public void SetReserveTicket(Guid ticketid, Guid userID, Guid reservationID)
        {
            _dbContext.ReservedTickets.Add(new ReservedTicket
            {
                reservedID = reservationID,
                ticketID = ticketid,
                userID = userID,
                reservedAt = DateTime.Now
            });

            _dbContext.SaveChanges();
        }

        public ReservedTicket GetReservedTicket(Guid reservationID)
        {
            return _dbContext.ReservedTickets.FirstOrDefault(t => t.reservedID == reservationID);
        }

        public void UpdateTicket(EventTicket ticket)
        {

            EventTicket existingTicket = _dbContext.Tickets.Find(ticket.ticketID);

            if (existingTicket == null)
                throw new Exception("Ticket not found!");

            existingTicket.ticketName = ticket.ticketName;
            existingTicket.ticketPrice = ticket.ticketPrice;
            existingTicket.ticketStatus = ticket.ticketStatus;
            existingTicket.ticketMaxAvailable = ticket.ticketMaxAvailable;

            _dbContext.SaveChanges();
        }

        public void CreatePursche(Guid purchaseID, Guid userID)
        {
            _dbContext.UserPurchases.Add(new UserPurchase
            {
                userID = userID,
                guestID = Guid.Empty,
                purchaseID = purchaseID
            });

            _dbContext.SaveChanges();
        }

        public void SetPurscheTicket(Guid ticketID, Guid userID, Guid purchaseID)
        {
            _dbContext.SoldTickets.Add(new SoldTicket
            {
                purchaseID = purchaseID,
                ticketID = ticketID
            });

            _dbContext.SaveChanges();
        }

        public void RemoveReserveTicket(Guid reserveID)
        {
            var tickets = _dbContext.ReservedTickets.Where(ticket => ticket.reservedID == reserveID).ToList();

            if (tickets.Any())
            {
                // Verwijder alle gevonden tickets
                _dbContext.ReservedTickets.RemoveRange(tickets);
                _dbContext.SaveChanges();
            }
        }

        public List<EventTicket> GetPurchaseByID(Guid purchaseID)
        {
            List<EventTicket> result = new List<EventTicket>();
            List<SoldTicket> soldTickets = _dbContext.SoldTickets.Where(t => t.purchaseID == purchaseID).ToList();

            foreach (SoldTicket ticket in soldTickets)
            {
                result.Add(GetTicketByID(ticket.ticketID));
            }

            return result;
        }

        public List<UserPurchase> GetUserPurchasesOfUser(Guid userID)
        {
            return _dbContext.UserPurchases.Where(u => u.userID == userID).ToList();
        }

        public List<UserPurchaseModel> GetPurchasesOfUser(Guid userID)
        {
            List<UserPurchaseModel> result = new List<UserPurchaseModel>();

            List<UserPurchase> purchases = GetUserPurchasesOfUser(userID);

            foreach (UserPurchase purchase in purchases)
            {
                result.Add(new UserPurchaseModel
                {
                    userPurchase = purchase,
                    eventTickets = GetPurchaseByID(purchase.purchaseID)
                });
            }

            return result;
        }
    }
}
