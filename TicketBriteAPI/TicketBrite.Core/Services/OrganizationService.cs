using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;

namespace TicketBrite.Core.Services
{
    public class OrganizationService
    {
        public readonly IOrganizationRepository organizationRepository;

        public OrganizationService(IOrganizationRepository c_organizationRepository)
        {
            organizationRepository = c_organizationRepository;
        }

        public List<Event> GetEventsByOrganization(Guid organizationID)
        {
            return organizationRepository.GetEventsByOrganization(organizationID);
        }
    }
}
