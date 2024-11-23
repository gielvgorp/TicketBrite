using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Identity.Client;
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
using TicketBrite.Data.Repositories;

namespace TicketBrite.Test
{
    [TestClass]
    public class OrganizationServiceTest
    {
        private OrganizationService organizationService;
        private ApplicationDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            _context = new ApplicationDbContext(options);

            IOrganizationRepository _dal = new OrganizationRepository(_context);
            organizationService = new OrganizationService(_dal);

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
                        eventID = Guid.NewGuid(),
                        organizationID = Guid.Parse("492b6808-e751-40c3-a1fe-1b0d64ee01c1"),
                        eventName = "Rock Concert",
                        eventDateTime = DateTime.Now.AddMonths(1),
                        eventLocation = "Johan Cruijff ArenA, Amsterdam",
                        eventAge = 18,
                        eventCategory = "Muziek",
                        eventImage = "event_img_URL",
                        eventDescription = "Rock concert"
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


                if (!_context.Users.Any())
                {
                    _context.Users.AddRange(
                        new User
                        {
                            userID = Guid.NewGuid(),
                            organizationID = Guid.Parse("492b6808-e751-40c3-a1fe-1b0d64ee01c1"),
                            userName = "Fontys Hogeschool",
                            roleID = Guid.Empty,
                            userEmail = string.Empty,
                            userPasswordHash = string.Empty
                        }
                    );
                }

                _context.SaveChanges();
            }
            #endregion
        }

        [TestMethod("Get all events of organization")]
        public void Get_All_Events_Of_Organization()
        {
            // Organization organization = organizationService.get
        }

        [TestMethod("Get all events of organization")]
        [DataRow("492b6808-e751-40c3-a1fe-1b0d64ee01c1")]
        public void Get_All_Events_Of_Organization(Guid organizationID)
        {
            List<Event> events = organizationService.GetAllEventsByOrganization(organizationID);

            Assert.IsNotNull(events);
        }
    }
}