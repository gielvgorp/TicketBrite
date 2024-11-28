using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;
using TicketBrite.Core.Services;
using TicketBrite.Data.ApplicationDbContext;
using TicketBrite.Data.Migrations;
using TicketBrite.Data.Repositories;

namespace TicketBrite.Test
{
    [TestClass]
    public class TicketServiceTest
    {
        private TicketService ticketService;
        private ApplicationDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            _context = new ApplicationDbContext(options);

            ITicketRepository _dal = new TicketRepository(_context);
            ticketService = new TicketService(_dal);

            // Reset and seed database
            ResetDatabase();
            SeedDatabase();
        }

        #region SeedDatabase
        private void ResetDatabase()
        {
            _context.Events.RemoveRange(_context.Events);
            _context.Users.RemoveRange(_context.Users);
            _context.Organizations.RemoveRange(_context.Organizations);
            _context.Tickets.RemoveRange(_context.Tickets);
            _context.ReservedTickets.RemoveRange(_context.ReservedTickets);

            _context.SaveChanges();
        }

        private void SeedDatabase()
        {
            // Static events
            if (!_context.Events.Any())
            {
                _context.Events.AddRange(
                   new Event
                   {
                       eventID = Guid.Parse("8a333ec2-fdc7-448f-9e14-3ebe36b4d12b"),
                       organizationID = Guid.Parse("492b6808-e751-40c3-a1fe-1b0d64ee01c1"),
                       eventName = "Fontys Festival",
                       eventDateTime = DateTime.Now.AddMonths(1),
                       eventLocation = "Fontys Rachelsmolen",
                       eventAge = 18,
                       eventCategory = "Muziek",
                       eventImage = "event_img_URL",
                       eventDescription = "Fontys Festival",
                       isVerified = false
                   }
               );
            }

            if (!_context.Tickets.Any())
            {
                _context.Tickets.AddRange(
                    new EventTicket
                    {
                        ticketID = Guid.Parse("52a2ba3b-1990-4d96-ab59-af0480534f53"),
                        eventID = Guid.Parse("8a333ec2-fdc7-448f-9e14-3ebe36b4d12b"),
                        ticketMaxAvailable = 10,
                        ticketName = "Staanplaats",
                        ticketPrice = 10,
                        ticketStatus = true
                    }
                ) ;

                _context.Tickets.AddRange(
                   new EventTicket
                   {
                       ticketID = Guid.Parse("2606c475-3e19-440d-90f6-71e40729fadc"),
                       eventID = Guid.Parse("8a333ec2-fdc7-448f-9e14-3ebe36b4d12b"),
                       ticketMaxAvailable = 2,
                       ticketName = "VIP Plaatsen",
                       ticketPrice = 10,
                       ticketStatus = false
                   }
               );

                _context.Tickets.AddRange(
                   new EventTicket
                   {
                       ticketID = Guid.Parse("ccfafcdb-3744-42e1-a84e-7dfc892d015d"),
                       eventID = Guid.Parse("8a333ec2-fdc7-448f-9e14-3ebe36b4d12b"),
                       ticketMaxAvailable = 10,
                       ticketName = "Staanplaats",
                       ticketPrice = 10,
                       ticketStatus = true
                   }
               );
            }

            if (!_context.Users.Any())
            {
                _context.Users.AddRange(
                    new User
                    {
                        userID = Guid.Parse("aee9b9ca-0a5d-4c16-8b1e-f8132b91c491"),
                        organizationID = Guid.Parse("492b6808-e751-40c3-a1fe-1b0d64ee01c1"),
                        userName = "Fontys Hogeschool",
                        roleID = Guid.Empty,
                        userEmail = string.Empty,
                        userPasswordHash = string.Empty
                    }
                );
            }

            if (!_context.Organizations.Any())
            {
                _context.Organizations.AddRange(
                    new Organization
                    {
                        organizationID = Guid.Parse("492b6808-e751-40c3-a1fe-1b0d64ee01c1"),
                        organizationName = "Fontys Hogeschool",
                        organizationEmail = "Unit-test@organization.com",
                        organizationPhone = "0031 6 12345678",
                        organizationAddress = "Rachelsmolen 1, Eindhoven",
                        organizationWebsite = "https://fontys.nl/"
                    }
                );
            }

            _context.SaveChanges();
            #endregion
        }

        [TestCleanup]
        public void TearDown()
        {
            _context.Dispose();
        }

        [TestMethod("Get all tickets of event")]
        [DataRow("8a333ec2-fdc7-448f-9e14-3ebe36b4d12b")]
        public void GetTicketsOfEvent(string eventID)
        {
            Guid parsedEventID = Guid.Parse(eventID);

            List<EventTicket> tickets = ticketService.GetTicketsOfEvent(parsedEventID);

            Assert.IsNotNull(tickets);
            Assert.IsTrue(tickets.All(t => t.eventID == parsedEventID));
        }

        [TestMethod("Reserve ticket")]
        [DataRow("52a2ba3b-1990-4d96-ab59-af0480534f53", "c7bfc35d-fca7-4ea7-a72e-117a2c9c2b96", true)]
        [DataRow("2606c475-3e19-440d-90f6-71e40729fadc", "c2ecb927-8b04-47f6-8c38-a5198711f1ba", false)]
        public void ReservedTicket_Should_Throw_Exception_On_Null_Allow(string ticketID, string reservationID, bool allow)
        {
            Guid parsedTicketID = Guid.Parse(ticketID);
            Guid parsedReservationID = Guid.Parse(reservationID);
            Guid userID = Guid.Parse("aee9b9ca-0a5d-4c16-8b1e-f8132b91c491");

            if (!allow)
            {
                Assert.ThrowsException<Exception>(() =>
                    ticketService.SetReservedTicket(parsedTicketID, userID, parsedReservationID));
            }
            else
            {
                ticketService.SetReservedTicket(parsedTicketID, userID, parsedReservationID);

                ReservedTicket reservatedTicket = ticketService.GetReservedTicket(parsedReservationID);

                Assert.IsNotNull(reservatedTicket);
                Assert.AreEqual(reservatedTicket.ticketID, parsedTicketID);
                Assert.AreEqual(reservatedTicket.userID, userID);
                Assert.AreEqual(reservatedTicket.reservedID, parsedReservationID);
            }
        }

        [TestMethod("Reserve to much tickets")]
        [DataRow("52a2ba3b-1990-4d96-ab59-af0480534f53", 11)]
        public void ReservateToMuchTickets(string ticketID, int quantity)
        {
            Guid parsedTicketID = Guid.Parse(ticketID);
            Guid parsedReservationID = Guid.NewGuid();
            Guid userID = Guid.Parse("aee9b9ca-0a5d-4c16-8b1e-f8132b91c491");

            // Act & Assert
            for (int i = 0; i < quantity; i++)
            {
                if (i < 10)
                {
                    ticketService.SetReservedTicket(parsedTicketID, userID, Guid.NewGuid());
                }
                else
                {
                    Assert.ThrowsException<Exception>(() =>
                        ticketService.SetReservedTicket(parsedTicketID, userID, Guid.NewGuid()));
                }
            }
        }
    }
}
