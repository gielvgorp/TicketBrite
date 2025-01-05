using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicketBrite.Core.Interfaces;
using TicketBrite.Data.ApplicationDbContext;
using TicketBrite.Data.Repositories;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Services;
using TicketBrite.DTO;

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
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
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
            _context.Events.RemoveRange(_context.Events);
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
                        eventName = "Snollebollekes: LIVE!",
                        eventDateTime = DateTime.Now.AddMonths(1),
                        eventLocation = "Johan Cruijff ArenA, Amsterdam",
                        eventAge = 18,
                        eventCategory = "Muziek",
                        eventImage = "event_img_URL",
                        eventDescription = "Rock concert"
                    },
                    new Event
                    {
                        eventID = Guid.Parse("ee4819fa-157f-49c5-a2ba-a2541d56e54b"),
                        organizationID = Guid.NewGuid(),
                        eventName = "Fontys Festival",
                        eventDateTime = DateTime.Now.AddDays(7),
                        eventLocation = "Fontys Rachelsmolen, Eindhoven",
                        eventAge = 18,
                        eventCategory = "Muziek",
                        eventImage = "event_img_URL",
                        eventDescription = "Rock concert"
                    },
                    new Event
                    {
                        eventID = Guid.Parse("a21c1e59-4edf-4ca9-a6ee-f74142ed6529"),
                        organizationID = Guid.NewGuid(),
                        eventName = "Rock Concert",
                        eventDateTime = DateTime.Now.AddDays(13),
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

        [TestCleanup]
        public void TearDown()
        {
            _context.Dispose();
        }

        [TestMethod("Get event by event id")]
        public void Get_Event_By_ID()
        {
            Guid eventID = Guid.Parse("1A07CD1A-81F5-4CA9-B85D-AB12B35BEF97");

            EventDTO result = eventService.GetEvent(eventID);

            Assert.AreEqual(eventID, result.eventID);
        }

        [TestMethod("Get events by category")]
        [DataRow("Muziek", 3, new[] { "1A07CD1A-81F5-4CA9-B85D-AB12B35BEF97", "ee4819fa-157f-49c5-a2ba-a2541d56e54b", "a21c1e59-4edf-4ca9-a6ee-f74142ed6529" })]               // Category that exists
        [DataRow("FakeTestCategory", 0, new string[] { })]    // Category that doesn't exist
        public void Get_Events_By_Category(string category, int expectedAmount, string[] expectedEventIds)
        {
            List<EventDTO> events = eventService.GetEvents(category);

            Assert.AreEqual(expectedAmount, events.Count, $"Expected {expectedAmount} events, but found {events.Count} events for category '{category}'");

            Guid[] actualEventIds = events.Select(e => e.eventID).ToArray();
            Guid[] expectedGuids = expectedEventIds.Select(id => Guid.Parse(id)).ToArray();

            CollectionAssert.AreEquivalent(expectedGuids, actualEventIds, $"The event IDs for category '{category}' do not match the expected IDs.");
            Assert.IsTrue(events.All(e => e.eventCategory == category), string.Format("Not all events have the category: '{0}'", category));
        }

        [TestMethod("Create event and add to repository")]
        public void Create_New_Event_Add_To_Repository()
        {
            int initialCount = eventService.GetEvents().Count;

            Guid id = Guid.NewGuid();

            EventDTO newEvent = new EventDTO
            {
                eventID = id,
                organizationID = Guid.NewGuid(),
                eventName = "Unit test event",
                eventCategory = "Unit test",
                eventDescription = "Description",
                eventImage = "image.jpg",
                eventDateTime = DateTime.Now.AddDays(1),
                eventLocation = "Fontys Rachelsmolen",
                eventAge = 18
            };

            eventService.AddEvent(newEvent);

            EventDTO result = eventService.GetEvent(id);

            Assert.AreEqual(newEvent.eventID, result.eventID);
            Assert.AreEqual(eventService.GetEvents().Count, initialCount + 1);
        }

        [TestMethod("Update event and save to repository")]
        [DataRow("1A07CD1A-81F5-4CA9-B85D-AB12B35BEF97", true)]  // Event exists
        //[DataRow("6510d601-b1dc-4049-8f76-c63450fad82c", false)] // Event does not exist
        public void Update_Event_Save_To_Repository(string eventID, bool eventExists)
        {
            Guid parsedEventID = Guid.Parse(eventID);
            EventDTO result = eventService.GetEvent(parsedEventID);

            if (!eventExists)
            {
                Assert.IsNull(result, "Expected no event to be found, but an event was returned.");
                return;
            }

            Assert.IsNotNull(result, "Expected an event, but none was found.");

            string newEventName = "Updated event name";
            result.eventName = newEventName;
            eventService.UpdateEvent(result);

            EventDTO updatedResult = eventService.GetEvent(parsedEventID);

            Assert.IsNotNull(updatedResult, "Updated event was not found.");
            Assert.AreEqual(newEventName, updatedResult.eventName, "Event name was not updated correctly.");
            Assert.AreEqual(result.eventCategory, updatedResult.eventCategory, "Event category should remain unchanged.");
        }


        [TestMethod("Get all events")]
        public void Get_All_Events()
        {
            List<EventDTO> events = eventService.GetEvents();

            Assert.IsNotNull(events);
        }
    }
}