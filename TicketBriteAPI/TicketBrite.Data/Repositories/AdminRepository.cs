using TicketBrite.Core.Domains;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;

namespace TicketBrite.Data.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext.ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext.ApplicationDbContext context)
        {
            _context = context;
        }

        private List<EventDomain> GetEventsByVerificationStatus(bool isVerified)
        {
            return _context.Events
                .Where(e => e.isVerified == isVerified)
                .Select(item => new EventDomain
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

        public List<EventDomain> GetAllUnVerifiedEvents()
        {
            return GetEventsByVerificationStatus(false);
        }

        public List<EventDomain> GetAllVerifiedEvents()
        {
            return GetEventsByVerificationStatus(true);
        }

        public void UpdateEventVerificationStatus(bool value, Guid eventID)
        {
            Event result = _context.Events.Find(eventID);

            if (result == null)
                throw new InvalidOperationException("Evenement niet gevonden!");

            result.isVerified = value;

            _context.SaveChanges();
        }
    }
}
