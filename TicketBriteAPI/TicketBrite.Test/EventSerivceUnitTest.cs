using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.ComponentModel.DataAnnotations;
using TicketBrite.Core.Domains;
using TicketBrite.Core.Interfaces;
using TicketBrite.Core.Services;
using TicketBrite.DTO;

namespace TicketBrite.Test
{
    [TestClass]
    public class EventSerivceUnitTest
    {
        private Mock<IEventRepository> _mockRepository;
        private EventService _eventService;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IEventRepository>();
            _eventService = new EventService(_mockRepository.Object);
        }

        [TestMethod("Create event with valid data")]
        public void AddEvent_ValidEvent()
        {
            EventDTO eventDto = new EventDTO
            {
                eventID = Guid.NewGuid(),
                eventName = "Test Event",
                eventLocation = "Test Location",
                eventDateTime = DateTime.Now.AddDays(1),
                eventAge = 18,
                eventCategory = "Music",
                eventImage = "image.jpg",
                isVerified = false,
                organizationID = Guid.NewGuid()
            };

            _eventService.AddEvent(eventDto);

            _mockRepository.Verify(repo => repo.AddEvent(It.Is<EventDomain>(e =>
                e.eventName == eventDto.eventName &&
                e.eventLocation == eventDto.eventLocation &&
                e.eventAge == eventDto.eventAge &&
                e.eventCategory == eventDto.eventCategory &&
                e.eventImage == eventDto.eventImage &&
                e.isVerified == eventDto.isVerified &&
                e.organizationID == eventDto.organizationID)), Times.Once);
        }

        [TestMethod("Create event with invalid data")]
        [ExpectedException(typeof(ValidationException))]
        public void AddEvent_InvalidEvent()
        {
            EventDTO eventDto = new EventDTO
            {
                eventID = Guid.NewGuid(),
                eventName = "",
                eventLocation = "Test Location",
                eventDateTime = DateTime.Now.AddDays(1),
                eventAge = 18,
                eventCategory = "Music",
                eventImage = "image.jpg",
                isVerified = false,
                organizationID = Guid.NewGuid()
            };

            _eventService.AddEvent(eventDto);
        }

        [TestMethod("Update event with valid data")]
        public void UpdateEvent_ValidEvent()
        {
            EventDTO eventDto = new EventDTO
            {
                eventID = Guid.NewGuid(),
                eventName = "Updated Event",
                eventLocation = "Updated Location",
                eventDateTime = DateTime.Now.AddDays(1),
                eventAge = 20,
                eventCategory = "Sports",
                eventImage = "updatedimage.jpg",
                isVerified = true,
                organizationID = Guid.NewGuid()
            };

            _eventService.UpdateEvent(eventDto);

            _mockRepository.Verify(repo => repo.UpdateEvent(It.Is<EventDomain>(e =>
                e.eventID == eventDto.eventID &&
                e.eventName == eventDto.eventName &&
                e.eventLocation == eventDto.eventLocation &&
                e.eventAge == eventDto.eventAge &&
                e.eventCategory == eventDto.eventCategory &&
                e.eventImage == eventDto.eventImage &&
                e.isVerified == eventDto.isVerified &&
                e.organizationID == eventDto.organizationID)), Times.Once);
        }

        [TestMethod("Update event with invalid data")]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateEvent_InvalidData()
        {
            EventDTO eventDto = new EventDTO
            {
                eventID = Guid.NewGuid(),
                eventName = "",
                eventLocation = "Updated Location",
                eventDateTime = DateTime.Now.AddDays(1),
                eventAge = 20,
                eventCategory = "Sports",
                eventImage = "updatedimage.jpg",
                isVerified = true,
                organizationID = Guid.NewGuid()
            };

            _eventService.UpdateEvent(eventDto);
        }

        [TestMethod("Get event by ID")]
        public void GetEvent_ValidEventID()
        {
            Guid eventID = Guid.NewGuid();
            EventDomain eventDomain = new EventDomain
            {
                eventID = eventID,
                eventName = "Test Event",
                eventLocation = "Test Location",
                eventDateTime = DateTime.Now.AddDays(1),
                eventAge = 18,
                eventCategory = "Music",
                eventImage = "image.jpg",
                isVerified = true,
                organizationID = Guid.NewGuid()
            };

            _mockRepository.Setup(repo => repo.GetEventByID(eventID)).Returns(eventDomain);

            EventDTO result = _eventService.GetEvent(eventID);

            Assert.IsNotNull(result);
            Assert.AreEqual(eventDomain.eventID, result.eventID);
            Assert.AreEqual(eventDomain.eventName, result.eventName);
        }

        [TestMethod("Get event that dont exist")]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetEvent_InvalidEventID()
        {
            Guid eventID = Guid.NewGuid();

            _mockRepository.Setup(repo => repo.GetEventByID(eventID)).Returns((EventDomain)null);

            _eventService.GetEvent(eventID);
        }

        [TestMethod("Get all events")]
        public void GetEvents()
        {
            List<EventDomain> domainList = new List<EventDomain>
            {
                new EventDomain
                {
                    eventID = Guid.NewGuid(),
                    eventName = "Event 1",
                    eventLocation = "Location 1",
                    eventDateTime = DateTime.Now.AddDays(1),
                    eventAge = 18,
                    eventCategory = "Music",
                    eventImage = "image1.jpg",
                    isVerified = true,
                    organizationID = Guid.NewGuid()
                }
            };

            _mockRepository.Setup(repo => repo.GetEvents()).Returns(domainList);

            List<EventDTO> result = _eventService.GetEvents();

            Assert.IsNotNull(result);
            Assert.AreEqual(domainList.Count, result.Count);
            Assert.AreEqual(domainList[0].eventName, result[0].eventName);
        }
    }
}
