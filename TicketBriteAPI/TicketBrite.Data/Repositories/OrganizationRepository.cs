using TicketBrite.Core.Domains;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;

namespace TicketBrite.Data.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        ApplicationDbContext.ApplicationDbContext _dbContext;

        public OrganizationRepository(ApplicationDbContext.ApplicationDbContext context)
        {
            _dbContext = context;
        }

        private List<EventDomain> ConvertToDomainList(List<Event> events)
        {
            return events.Select(e => new EventDomain
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

        public List<EventDomain> GetAllEventsByOrganization(Guid organizationID)
        {
            List<Event> events = _dbContext.Events.Where(e => e.organizationID == organizationID).ToList();
            return ConvertToDomainList(events);
        }

        public List<EventDomain> GetVerifiedEventsByOrganization(Guid organizationID)
        {
            List<Event> events = _dbContext.Events.Where(e => e.organizationID == organizationID && e.isVerified).ToList();
            return ConvertToDomainList(events);
        }

        public List<EventDomain> GetUnVerifiedEventsByOrganization(Guid organizationID)
        {
            List<Event> events = _dbContext.Events.Where(e => e.organizationID == organizationID && !e.isVerified).ToList();
            return ConvertToDomainList(events);
        }

        public void UpdateOrganization(OrganizationDomain organization)
        {
            Organization existingOrganization = _dbContext.Organizations.Find(organization.organizationID);

            if (existingOrganization == null)
                throw new KeyNotFoundException("Organization not found!");

            existingOrganization.organizationName = organization.organizationName;
            existingOrganization.organizationEmail = organization.organizationEmail;
            existingOrganization.organizationPhone = organization.organizationPhone;
            existingOrganization.organizationAddress = organization.organizationAddress;
            existingOrganization.organizationWebsite = organization.organizationWebsite;
          
            _dbContext.SaveChanges();
        }

        public OrganizationDomain GetOrganizationByID(Guid organizationID)
        {
            Organization result = _dbContext.Organizations.FirstOrDefault(o => o.organizationID == organizationID);

            if (result == null)
                throw new KeyNotFoundException("Organization not found!");

            return new OrganizationDomain
            {
                organizationID = result.organizationID,
                organizationAddress = result.organizationAddress,
                organizationEmail = result.organizationEmail,
                organizationName = result.organizationName,
                organizationPhone = result.organizationPhone,
                organizationWebsite = result.organizationWebsite
            };
        }
    }
}
