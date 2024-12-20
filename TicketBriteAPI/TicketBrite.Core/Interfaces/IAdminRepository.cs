using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Domains;
using TicketBrite.Core.Entities;

namespace TicketBrite.Core.Interfaces
{
    public interface IAdminRepository
    {
        public List<EventDomain> GetAllUnVerifiedEvents();

        public List<EventDomain> GetAllVerifiedEvents();

        public void UpdateEventVerificationStatus(bool value, Guid eventID);
    }
}
