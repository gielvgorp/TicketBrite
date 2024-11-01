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
        public List<Event> GetEventsByOrganization(Guid organizationID);
    }
}
