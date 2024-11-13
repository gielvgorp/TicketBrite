using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;

namespace TicketBrite.Core.Services
{
    public class AuthService
    {
        IAuthRepository _authRepository;
        public AuthService(IAuthRepository authRepository) 
        { 
            _authRepository = authRepository;
        }

        public User VerifyUser(string email, string password)
        {
            User user = _authRepository.VerifyUser(email, password);

            if (user == null) throw new Exception("Gebruiker niet gevonden!");

            return user;
        }

        public Guest VerifyGuest(Guid guestID, Guid verificationCode)
        {
            Guest guest = _authRepository.VerifyGuest(guestID, verificationCode);

            if (guest == null) throw new Exception("Gast niet gevonden!");

            return guest;
        }
    }
}
