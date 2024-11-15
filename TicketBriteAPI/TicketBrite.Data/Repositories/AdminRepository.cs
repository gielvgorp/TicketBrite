using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;
using TicketBrite.Data.ApplicationDbContext;

namespace TicketBrite.Data.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext.ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Event> GetAllUnVerifiedEvents()
        {
            return _context.Events.Where(e => !e.isVerified).ToList();
        }

        public List<Event> GetAllVerifiedEvents()
        {
            return _context.Events.Where(e => e.isVerified).ToList();
        }

        public void UpdateEventVerificationStatus(bool value, Guid eventID)
        {
            Event result = _context.Events.Find(eventID);
            result.isVerified = value;

            _context.SaveChanges();
        }
    }
}
