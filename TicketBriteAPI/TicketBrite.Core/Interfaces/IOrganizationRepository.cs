using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;

namespace TicketBrite.Core.Interfaces
{
    public interface IOrganizationRepository
    {
        public List<Event> GetAllEventsByOrganization(Guid organizationID);
        public List<Event> GetUnVerifiedEventsByOrganization(Guid organizationID);
        public List<Event> GetVerifiedEventsByOrganization(Guid organizationID);
        public Organization GetOrganizationByID(Guid organizationID);
        public void UpdateOrganization(Organization organization);
    }
}
