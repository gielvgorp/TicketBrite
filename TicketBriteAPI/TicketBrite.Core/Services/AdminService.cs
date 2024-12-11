using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;
using TicketBrite.Core.Domains;
using TicketBrite.DTO;

namespace TicketBrite.Core.Services
{
    public class AdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        private List<EventDTO> ConvertToEventDTOList(List<EventDomain> events)
        {
            return events.Select(item => new EventDTO
            {
                eventID = item.eventID,
                organizationID = item.organizationID,
                eventAge = item.eventAge,
                eventCategory = item.eventCategory,
                eventDateTime = item.eventDateTime,
                eventDescription = item.eventDescription,
                eventImage = item.eventImage,
                eventLocation = item.eventLocation,
                eventName = item.eventName,
                isVerified = item.isVerified
            }).ToList();
        }

        public List<EventDTO> GetAllUnVerifiedEvents()
        {
            List<EventDomain> unVerifiedEvents = _adminRepository.GetAllUnVerifiedEvents();
            return ConvertToEventDTOList(unVerifiedEvents);
        }

        public List<EventDTO> GetAllVerifiedEvents()
        {
            List<EventDomain> verifiedEvents = _adminRepository.GetAllVerifiedEvents();
            return ConvertToEventDTOList(verifiedEvents);
        }

        public void UpdateEventVerificationStatus(bool value, Guid eventID)
        {
            try
            {
                if (eventID == Guid.Empty)
                    throw new ArgumentException("Event ID is leeg!");

                _adminRepository.UpdateEventVerificationStatus(value, eventID);
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException($"Ongeldige {ex.ParamName}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Algemene foutmelding bij andere fouten
                throw new InvalidOperationException("Er is een fout opgetreden bij het updaten van de evenementstatus.", ex);
            }
        }
    }
}
