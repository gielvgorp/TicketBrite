using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TicketBrite.Core.Domains;
using TicketBrite.Core.Interfaces;
using TicketBrite.Core.Services;
using TicketBrite.DTO;

namespace TicketBrite.Test
{
    [TestClass]
    public class OrganizationServiceUnitTests
    {
        private Mock<IOrganizationRepository> _mockRepository;
        private OrganizationService _organizationService;

        [TestInitialize]
        public void SetUp()
        {
            _mockRepository = new Mock<IOrganizationRepository>();
            _organizationService = new OrganizationService(_mockRepository.Object);
        }

        [TestMethod("Get all events of organization")]
        public void GetAllEventsByOrganization()
        {
            Guid organizationId = Guid.NewGuid();
            List<EventDomain> eventDomains = new List<EventDomain>
            {
                new EventDomain
                {
                    eventID = Guid.NewGuid(),
                    eventName = "Test Event",
                    eventDateTime = DateTime.Now,
                    eventCategory = "Music",
                    eventDescription = "Description",
                    eventImage = "image.jpg",
                    eventLocation = "Location",
                    eventAge = 18,
                    isVerified = true,
                    organizationID = organizationId
                }
            };

            _mockRepository.Setup(repo => repo.GetAllEventsByOrganization(organizationId)).Returns(eventDomains);

            List<EventDTO> result = _organizationService.GetAllEventsByOrganization(organizationId);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(eventDomains[0].eventName, result[0].eventName);

            _mockRepository.Verify(repo => repo.GetAllEventsByOrganization(organizationId), Times.Once);
        }

        [TestMethod("Update organization valid data")]
        public void UpdateOrganization_ValidData()
        {
            OrganizationDomain existingOrganization = new OrganizationDomain
            {
                organizationID = Guid.NewGuid(),
                organizationName = "Old Organization",
                organizationAddress = "Old Street",
                organizationEmail = "old@example.com",
                organizationPhone = "987654321",
                organizationWebsite = "www.old.com"
            };

            _mockRepository.Setup(repo => repo.GetOrganizationByID(existingOrganization.organizationID))
                .Returns(existingOrganization);

            OrganizationDTO organizationDto = new OrganizationDTO
            {
                organizationID = existingOrganization.organizationID,
                organizationName = "Test Organization",
                organizationAddress = "123 Street",
                organizationEmail = "test@example.com",
                organizationPhone = "123456789",
                organizationWebsite = "www.test.com"
            };

            _organizationService.UpdateOrganization(organizationDto);

            _mockRepository.Verify(repo => repo.UpdateOrganization(It.Is<OrganizationDomain>(
                o => o.organizationID == organizationDto.organizationID &&
                     o.organizationName == organizationDto.organizationName &&
                     o.organizationAddress == organizationDto.organizationAddress &&
                     o.organizationWebsite == organizationDto.organizationWebsite &&
                     o.organizationEmail == organizationDto.organizationEmail)), Times.Once);
        }

        [TestMethod("Update organization that doesnt exist")]
        public void UpdateOrganization_DoesntExist()
        {
            OrganizationDTO organizationDto = new OrganizationDTO
            {
                organizationID = Guid.NewGuid(),
                organizationName = "Non-Existing Organization",
                organizationAddress = "Non-existing Street",
                organizationEmail = "nonexisting@example.com",
                organizationPhone = "0000000000",
                organizationWebsite = "www.nonexisting.com"
            };

            _mockRepository.Setup(repo => repo.GetOrganizationByID(organizationDto.organizationID))
                .Returns((OrganizationDomain)null);

            Assert.ThrowsException<KeyNotFoundException>(() =>
                _organizationService.UpdateOrganization(organizationDto),
                "Expected exception was not thrown.");
        }



        [TestMethod("Update organization invalid data")]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateOrganization_InvalidData()
        {
            OrganizationDTO invalidOrganizationDto = new OrganizationDTO
            {
                organizationID = Guid.Empty, // Invalid ID
                organizationName = ""
            };

            _organizationService.UpdateOrganization(invalidOrganizationDto);
        }

        [TestMethod("Get organization that exists")]
        public void GetOrganizationByID_OrganizationExists()
        {
            Guid organizationId = Guid.NewGuid();
            OrganizationDomain organizationDomain = new OrganizationDomain
            {
                organizationID = organizationId,
                organizationName = "Test Organization",
                organizationAddress = "123 Street",
                organizationEmail = "test@example.com",
                organizationPhone = "123456789",
                organizationWebsite = "www.test.com"
            };

            _mockRepository.Setup(repo => repo.GetOrganizationByID(organizationId)).Returns(organizationDomain);

            OrganizationDTO result = _organizationService.GetOrganizationByID(organizationId);

            Assert.IsNotNull(result);
            Assert.AreEqual(organizationDomain.organizationID, result.organizationID);
            Assert.AreEqual(organizationDomain.organizationName, result.organizationName);
            _mockRepository.Verify(repo => repo.GetOrganizationByID(organizationId), Times.Once);
        }

        [TestMethod("Get organization that doesnt exist")]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetOrganizationByID_OrganizationDoesNotExist()
        {
            Guid organizationID = Guid.NewGuid();

            _mockRepository.Setup(repo => repo.GetOrganizationByID(organizationID)).Returns((OrganizationDomain)null);

            _organizationService.GetOrganizationByID(organizationID);
        }
    }
}
