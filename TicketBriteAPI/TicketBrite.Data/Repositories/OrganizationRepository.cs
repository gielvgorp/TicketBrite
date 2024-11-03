using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<Event> GetEventsByOrganization(Guid organizationID)
        {
            return _dbContext.Events.Where(e => e.organizationID == organizationID).ToList();
        }
    }
}
