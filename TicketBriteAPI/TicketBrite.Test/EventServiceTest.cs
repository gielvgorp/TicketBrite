using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicketBrite.Core.Interfaces;
using TicketBrite.Data.ApplicationDbContext;
using TicketBrite.Data.Repositories;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Services;
using Microsoft.Extensions.Configuration;
using Bogus;

namespace TicketBrite.Test
{
    [TestClass]
    public class EventServiceTest
    {
        private EventService eventService;
        private ApplicationDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            var testConnectionString = "Server=DESKTOP-SML637F;Database=TicketBrite_Test;Trusted_Connection=True;TrustServerCertificate=True;";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(testConnectionString) // Gebruik je echte SQL Server voor de testdatabase
                .Options;

            _context = new ApplicationDbContext(options);

            IEventRepository _dal = new EventRepository(_context);
            eventService = new EventService(_dal);

            // Reset and seed database
            ResetDatabase();
            SeedDatabase();
        }

        #region SeedDatabase
        private void ResetDatabase()
        {
            // Remove all data from the relevant tables (cascade delete if necessary)
            _context.Database.ExecuteSqlRaw("DELETE FROM Events");
            // Add other tables here if necessary

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
                        eventID = Guid.Parse("1A07CD1A-81F5-4CA9-B85D-AB12B35BEF97"),
                        organizationID = Guid.NewGuid(),
                        eventName = "Rock Concert",
                        eventDateTime = DateTime.Now.AddMonths(1),
                        eventLocation = "Johan Cruijff ArenA, Amsterdam",
                        eventAge = 18,
                        eventCategory = "Muziek",
                        eventImage = "event_img_URL",
                        eventDescription = "Rock concert"
                    },
                    new Event
                    {
                        eventID = Guid.NewGuid(),
                        organizationID = Guid.NewGuid(),
                        eventName = "Theater Play",
                        eventDateTime = DateTime.Now.AddMonths(2),
                        eventLocation = "Gelredome, Arnhem",
                        eventAge = 18,
                        eventCategory = "Theater",
                        eventImage = "event_img_URL",
                        eventDescription = "Theater play!"
                    }
                );
            }

            _context.SaveChanges();
        }
        #endregion

        [TestMethod("Get event by event id")]
        public void Get_Event_By_ID()
        {
            Guid eventID = Guid.Parse("1A07CD1A-81F5-4CA9-B85D-AB12B35BEF97");

            Event result = eventService.GetEvent(eventID);

            Assert.AreEqual(eventID, result.eventID);
        }

        [TestMethod("Get events by category")]
        [DataRow("Muziek", true)]               // Category that exists
        [DataRow("FakeTestCategory", false)]    // Category that doesn't exist
        public void Get_Events_By_Category(string category, bool categoryExists)
        {
            List<Event> events = eventService.GetEvents(category);

            if (categoryExists)
            {
                Assert.IsTrue(events.All(e => e.eventCategory == category), string.Format("Not all events have the category: '{0}'", category));
            }
            else
            {
                Assert.AreEqual(0, events.Count, $"Expected no events, but found {events.Count} events for category '{category}'");
            }
        }

        [TestMethod("Create event and add to repository")]
        public void Create_New_Event_Add_To_Repository()
        {
            Guid id = Guid.NewGuid();

            Event newEvent = new Event
            {
                eventID = id,
                organizationID = Guid.NewGuid(),
                eventName = "Unit test event",
                eventCategory = "Unit test",
                eventDescription = "Description",
                eventImage = "",
                eventDateTime = DateTime.Now.AddDays(1),
                eventLocation = "Fontys Rachelsmolen",
                eventAge = 18
            };

            eventService.AddEvent(newEvent);

            Event result = eventService.GetEvent(id);

            Assert.AreEqual(newEvent, result);
        }

        [TestMethod("Update event and save to repository")]
        public void Update_Event_Save_To_Repository()
        {
            Event result = eventService.GetEvent(Guid.Parse("1A07CD1A-81F5-4CA9-B85D-AB12B35BEF97"));

            Assert.IsNotNull(result);

            if (result != null) 
            {
                result.eventName = "Updated event name";
                eventService.UpdateEvent(result);

                Event updated_result = eventService.GetEvent(result.eventID);

                Assert.AreEqual(updated_result, result);
            }
        }

        [TestMethod("Get all events")]
        public void Get_All_Events()
        {
            List<Event> events = eventService.GetEvents();

            Assert.IsNotNull(events);
        }
    }
}