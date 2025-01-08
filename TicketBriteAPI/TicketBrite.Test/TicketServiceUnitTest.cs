﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;
using TicketBrite.Core.Services;

namespace TicketBrite.Test
{
    [TestClass]
    public class TicketServiceTests
    {
        private Mock<ITicketRepository> _ticketRepositoryMock;
        private TicketService _ticketService;

        [TestInitialize]
        public void Setup()
        {
            // ITicketRepo mocken
            _ticketRepositoryMock = new Mock<ITicketRepository>();
            _ticketService = new TicketService(_ticketRepositoryMock.Object);
        }

        [TestMethod("Should return tickets of event")]
        public void GetTicketsOfEvent_ShouldReturnTickets()
        {
            Guid eventID = Guid.NewGuid();
            Guid ticketID = Guid.NewGuid();
            List<EventTicket> tickets = new List<EventTicket>
            {
                new EventTicket { ticketID = ticketID, eventID = eventID, ticketMaxAvailable = 100 }
            };

            _ticketRepositoryMock.Setup(repo => repo.GetTicketsOfEvent(eventID)).Returns(tickets);

            var result = _ticketService.GetTicketsOfEvent(eventID);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(eventID, result[0].eventID);
            Assert.AreEqual(ticketID, result[0].ticketID);
        }

        [TestMethod("Reserve Ticket that doesnt exist")]
        [ExpectedException(typeof(Exception))]
        public void SetReservedTicket_TicketDoesntExist()
        {
            var ticketID = Guid.NewGuid();
            _ticketRepositoryMock.Setup(repo => repo.GetTicketByID(ticketID)).Returns((EventTicket)null);

            _ticketService.SetReservedTicket(ticketID, Guid.NewGuid(), Guid.NewGuid());
        }

        [TestMethod("Should purchase ticket")]
        public void SetPurscheTicket()
        {
            var ticketID = Guid.NewGuid();
            var userID = Guid.NewGuid();
            var purchaseID = Guid.NewGuid();

            var ticket = new EventTicket { ticketID = ticketID, ticketMaxAvailable = 100 };
            var soldTickets = new List<SoldTicket>();

            _ticketRepositoryMock.Setup(repo => repo.GetTicketByID(ticketID)).Returns(ticket);
            _ticketRepositoryMock.Setup(repo => repo.GetSoldTickets(ticketID)).Returns(soldTickets);

            _ticketService.SetPurscheTicket(ticketID, userID, purchaseID);

            _ticketRepositoryMock.Verify(repo => repo.SetPurscheTicket(ticketID, userID, purchaseID), Times.Once);
        }

        [DataTestMethod]
        [DataRow(100, 10, 5, 85)]
        [DataRow(50, 48, 2, 0)]
        [DataRow(0, 0, 0, 0)]
        public void CalculateRemainingTickets(
            int maxAvailable,
            int soldCount,
            int reservedCount,
            int expectedRemaining)
        {
            var ticketID = Guid.NewGuid();
            var ticket = new EventTicket { ticketID = ticketID, ticketMaxAvailable = maxAvailable };
            var soldTickets = new List<SoldTicket>(new SoldTicket[soldCount]);
            var reservedTickets = new List<ReservedTicket>(new ReservedTicket[reservedCount]);

            _ticketRepositoryMock.Setup(repo => repo.GetTicketByID(ticketID)).Returns(ticket);
            _ticketRepositoryMock.Setup(repo => repo.GetSoldTickets(ticketID)).Returns(soldTickets);
            _ticketRepositoryMock.Setup(repo => repo.GetReservedTicketsByTicket(ticketID)).Returns(reservedTickets);

            var remainingTickets = _ticketService.CalculateRemainingTickets(ticketID);

            Assert.AreEqual(expectedRemaining, remainingTickets);
        }
    }
}