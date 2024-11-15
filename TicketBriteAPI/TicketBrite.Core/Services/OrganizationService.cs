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

        public List<Event> GetAllEventsByOrganization(Guid organizationID)
        {
            return organizationRepository.GetAllEventsByOrganization(organizationID);
        }

        public List<Event> GetVerifiedEventsByOrganization(Guid organizationID)
        {
            return organizationRepository.GetVerifiedEventsByOrganization(organizationID);
        }

        public List<Event> GetUnVerifiedEventsByOrganization(Guid organizationID)
        {
            return organizationRepository.GetUnVerifiedEventsByOrganization(organizationID);
        }

        public void UpdateOrganization(Organization organization)
        {
            organizationRepository.UpdateOrganization(organization);
        }

        public Organization GetOrganizationByID(Guid organizationID)
        {
            return organizationRepository.GetOrganizationByID(organizationID);
        }
    }
}
