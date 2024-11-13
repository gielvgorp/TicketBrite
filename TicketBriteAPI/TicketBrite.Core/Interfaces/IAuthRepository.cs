using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;

namespace TicketBrite.Core.Interfaces
{
    public interface IAuthRepository
    {
        public User VerifyUser(string email, string password);
        public User RegisterUser();
        public Guest VerifyGuest(Guid guestID, Guid verificationCode);
    }
}
