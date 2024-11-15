using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;

namespace TicketBrite.Core.Services
{
    public class AdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public List<Event> GetAllUnVerifiedEvents()
        {
            return _adminRepository.GetAllUnVerifiedEvents();
        }

        public List<Event> GetAllVerifiedEvents()
        {
            return _adminRepository.GetAllVerifiedEvents();
        }

        public void UpdateEventVerificationStatus(bool value, Guid eventID)
        {
           _adminRepository.UpdateEventVerificationStatus(value, eventID);
        }
    }
}
