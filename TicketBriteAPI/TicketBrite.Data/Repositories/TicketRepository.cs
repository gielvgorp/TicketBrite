﻿using System;
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
                reservedID = Guid.NewGuid(),
                ticketID = ticketid,
                userID = userID,
                reservedAt = DateTime.Now
            });

            _dbContext.SaveChanges();
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
    }
}
