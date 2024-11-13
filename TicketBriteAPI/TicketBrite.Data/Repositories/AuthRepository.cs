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
    public class AuthRepository : IAuthRepository
    {
        ApplicationDbContext.ApplicationDbContext _context;
        public AuthRepository(ApplicationDbContext.ApplicationDbContext context) 
        {
            _context = context;
        }


        public User VerifyUser(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.userEmail == email && u.userPasswordHash == password);
        }

        public User RegisterUser()
        {
            return null;
        }

        public Guest VerifyGuest(Guid guestID, Guid verificationCode)
        {
            return _context.Guests.FirstOrDefault(g => g.guestID == guestID && g.verificationCode == verificationCode);
        }
    }
}
