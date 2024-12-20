using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Domains;
using TicketBrite.Core.Entities;

namespace TicketBrite.Core.Interfaces
{
    public interface IOrganizationRepository
    {
        public List<EventDomain> GetAllEventsByOrganization(Guid organizationID);
        public List<EventDomain> GetUnVerifiedEventsByOrganization(Guid organizationID);
        public List<EventDomain> GetVerifiedEventsByOrganization(Guid organizationID);
        public OrganizationDomain GetOrganizationByID(Guid organizationID);
        public void UpdateOrganization(OrganizationDomain organization);
    }
}
