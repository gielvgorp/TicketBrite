using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Domains;
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

        public GuestDomain VerifyGuest(Guid guestID, Guid verificationCode)
        {
            Guest guest = _context.Guests.FirstOrDefault(g => g.guestID == guestID && g.verificationCode == verificationCode);

            if (guest == null)
                return null;

            GuestDomain domain = new GuestDomain
            {
                guestID = guestID,
                guestName = guest.guestName,
                guestEmail = guest.guestEmail,
                verificationCode = verificationCode
            };

            return domain;
        }
    }
}
