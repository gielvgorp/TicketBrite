using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
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

        [TestMethod]
        public void GetAllEventsByOrganization_ReturnsConvertedDTOList()
        {
            // Arrange
            var organizationId = Guid.NewGuid();
            var eventDomains = new List<EventDomain>
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

            // Act
            var result = _organizationService.GetAllEventsByOrganization(organizationId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(eventDomains[0].eventName, result[0].eventName);
            _mockRepository.Verify(repo => repo.GetAllEventsByOrganization(organizationId), Times.Once);
        }

        [TestMethod]
        public void UpdateOrganization_ValidOrganization_CallsRepositoryUpdate()
        {
            // Arrange
            var organizationDto = new OrganizationDTO
            {
                organizationID = Guid.NewGuid(),
                organizationName = "Test Organization",
                organizationAddress = "123 Street",
                organizationEmail = "test@example.com",
                organizationPhone = "123456789",
                organizationWebsite = "www.test.com"
            };

            // Act
            _organizationService.UpdateOrganization(organizationDto);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateOrganization(It.Is<OrganizationDomain>(
                o => o.organizationID == organizationDto.organizationID &&
                     o.organizationName == organizationDto.organizationName)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateOrganization_InvalidOrganization_ThrowsArgumentException()
        {
            // Arrange
            var invalidOrganizationDto = new OrganizationDTO
            {
                organizationID = Guid.Empty, // Invalid ID
                organizationName = ""
            };

            // Act
            _organizationService.UpdateOrganization(invalidOrganizationDto);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public void GetOrganizationByID_OrganizationExists_ReturnsDTO()
        {
            // Arrange
            var organizationId = Guid.NewGuid();
            var organizationDomain = new OrganizationDomain
            {
                organizationID = organizationId,
                organizationName = "Test Organization",
                organizationAddress = "123 Street",
                organizationEmail = "test@example.com",
                organizationPhone = "123456789",
                organizationWebsite = "www.test.com"
            };

            _mockRepository.Setup(repo => repo.GetOrganizationByID(organizationId)).Returns(organizationDomain);

            // Act
            var result = _organizationService.GetOrganizationByID(organizationId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(organizationDomain.organizationID, result.organizationID);
            Assert.AreEqual(organizationDomain.organizationName, result.organizationName);
            _mockRepository.Verify(repo => repo.GetOrganizationByID(organizationId), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetOrganizationByID_OrganizationDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            var organizationId = Guid.NewGuid();

            _mockRepository.Setup(repo => repo.GetOrganizationByID(organizationId)).Returns((OrganizationDomain)null);

            // Act
            _organizationService.GetOrganizationByID(organizationId);

            // Assert is handled by ExpectedException
        }
    }
}
