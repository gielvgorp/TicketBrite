using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Domains;
using TicketBrite.Core.Entities;

namespace TicketBrite.Core.Interfaces
{
    public interface IAuthRepository
    {
        public GuestDomain VerifyGuest(Guid guestID, Guid verificationCode);
    }
}
