using Microsoft.Identity.Client;
using TicketBrite.Core.Domains;
using TicketBrite.Core.Interfaces;
using TicketBrite.DTO;

namespace TicketBrite.Core.Services
{
    public class AuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher _passwordHasher;

        public AuthService(IAuthRepository authRepository, IUserRepository userRepository) 
        { 
            _authRepository = authRepository;
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher();
        }

        public GuestDTO VerifyGuest(Guid guestID, Guid verificationCode)
        {
            GuestDomain guest = _authRepository.VerifyGuest(guestID, verificationCode);

            if (guest == null) 
                throw new InvalidOperationException("Gast niet gevonden!");

            GuestDTO result = new GuestDTO
            {
                guestID = guest.guestID,
                guestEmail = guest.guestEmail,
                guestName = guest.guestName
            };

            return result;
        }

        public bool VerifyUser(string email, string enteredPassword)
        {
            UserDomain user = _userRepository.GetUserByEmail(email);

            if (user == null)
                return false;

            return _passwordHasher.Verify(enteredPassword, user.userPasswordHash);
        }
    }
}
