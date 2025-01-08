using TicketBrite.Core.Domains;
using TicketBrite.Core.Interfaces;
using TicketBrite.DTO;

namespace TicketBrite.Core.Services
{
    public class OrganizationService
    {
        private readonly IOrganizationRepository organizationRepository;

        public OrganizationService(IOrganizationRepository c_organizationRepository)
        {
            organizationRepository = c_organizationRepository;
        }

        private List<EventDTO> ConvertToDTOList(List<EventDomain> events)
        {
            return events.Select(e => new EventDTO
            {
                eventID = e.eventID,
                eventAge = e.eventAge,
                eventCategory = e.eventCategory,
                eventDateTime = e.eventDateTime,
                eventDescription = e.eventDescription,
                eventImage = e.eventImage,
                eventLocation = e.eventLocation,
                eventName = e.eventName,
                isVerified = e.isVerified,
                organizationID = e.organizationID
            }).ToList();
        }

        public List<EventDTO> GetAllEventsByOrganization(Guid organizationID)
        {
            List<EventDomain> eventsDomain = organizationRepository.GetAllEventsByOrganization(organizationID);
            return ConvertToDTOList(eventsDomain);
        }

        public List<EventDTO> GetVerifiedEventsByOrganization(Guid organizationID)
        {
            List<EventDomain> eventsDomain = organizationRepository.GetVerifiedEventsByOrganization(organizationID);
            return ConvertToDTOList(eventsDomain);
        }

        public List<EventDTO> GetUnVerifiedEventsByOrganization(Guid organizationID)
        {
            List<EventDomain> eventsDomain = organizationRepository.GetUnVerifiedEventsByOrganization(organizationID);
            return ConvertToDTOList(eventsDomain);
        }

        private void ValidateOrganization(OrganizationDTO organization)
        {
            if (organization == null)
                throw new ArgumentNullException("Event kan niet null zijn!");

            if (organization.organizationID == Guid.Empty)
                throw new ArgumentNullException("Event ID mag niet leeg zijn!");

            if (string.IsNullOrWhiteSpace(organization.organizationName))
                throw new ArgumentException("Organisatie naam mag niet leeg zijn!");

            if (string.IsNullOrWhiteSpace(organization.organizationEmail))
                throw new ArgumentException("Organisatie email mag niet leeg zijn!");

            if (string.IsNullOrWhiteSpace(organization.organizationWebsite))
                throw new ArgumentException("Organisatie website mag niet leeg zijn!");


            if (string.IsNullOrWhiteSpace(organization.organizationPhone))
                throw new ArgumentException("Organisatie website mag niet leeg zijn!");
        }

        public void UpdateOrganization(OrganizationDTO organization)
        {
            try
            {
                ValidateOrganization(organization);

                OrganizationDomain organizationDomain = new OrganizationDomain
                {
                    organizationID = organization.organizationID,
                    organizationName = organization.organizationName,
                    organizationAddress = organization.organizationAddress,
                    organizationEmail = organization.organizationEmail,
                    organizationPhone = organization.organizationPhone,
                    organizationWebsite = organization.organizationWebsite
                };

                if (GetOrganizationByID(organization.organizationID) == null)
                    throw new KeyNotFoundException("Organisatie niet gevonden!");

                organizationRepository.UpdateOrganization(organizationDomain);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Organisatie niet gevonden: " + ex.Message, ex);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Ongeldige invoer: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Onverwachte fout: " + ex.Message, ex);
            }
        }

        public OrganizationDTO GetOrganizationByID(Guid organizationID)
        {
            OrganizationDomain organizationDomain = organizationRepository.GetOrganizationByID(organizationID);

            if (organizationDomain == null)
            {
                throw new KeyNotFoundException($"Organisatie met ID {organizationID} is niet gevonden.");
            }

            return new OrganizationDTO
            {
                organizationID = organizationDomain.organizationID,
                organizationName=organizationDomain.organizationName,
                organizationAddress =organizationDomain.organizationAddress,
                organizationEmail=organizationDomain.organizationEmail,
                organizationPhone = organizationDomain.organizationPhone,
                organizationWebsite = organizationDomain.organizationWebsite
            };
        }
    }
}
