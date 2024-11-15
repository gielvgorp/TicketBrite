using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;

namespace TicketBrite.Core.Interfaces
{
    public interface IAdminRepository
    {
        public List<Event> GetAllUnVerifiedEvents();

        public List<Event> GetAllVerifiedEvents();

        public void UpdateEventVerificationStatus(bool value, Guid eventID);
    }
}
