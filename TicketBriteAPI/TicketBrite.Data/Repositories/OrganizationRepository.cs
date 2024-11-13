using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;
using TicketBrite.Data.Migrations;

namespace TicketBrite.Data.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        ApplicationDbContext.ApplicationDbContext _dbContext;

        public OrganizationRepository(ApplicationDbContext.ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public List<Event> GetEventsByOrganization(Guid organizationID)
        {
            return _dbContext.Events.Where(e => e.organizationID == organizationID).ToList();
        }

        public void UpdateOrganization(Organization organization)
        {
            Organization existingOrganization = _dbContext.Organizations.Find(organization.organizationID);

            if (existingOrganization == null)
                throw new Exception("Event not found!");

            existingOrganization.organizationName = organization.organizationName;
            existingOrganization.organizationEmail = organization.organizationEmail;
            existingOrganization.organizationPhone = organization.organizationPhone;
            existingOrganization.organizationAddress = organization.organizationAddress;
            existingOrganization.organizationWebsite = organization.organizationWebsite;
          

            _dbContext.SaveChanges();
        }

        public Organization GetOrganizationByID(Guid organizationID)
        {
            return _dbContext.Organizations.FirstOrDefault(o => o.organizationID == organizationID);
        }
    }
}
