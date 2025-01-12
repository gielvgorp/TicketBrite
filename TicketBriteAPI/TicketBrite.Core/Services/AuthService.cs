using Microsoft.Identity.Client;
using TicketBrite.Core.Domains;
using TicketBrite.Core.Interfaces;
using TicketBrite.DTO;
using TicketBrite.Core.Enums;

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
                throw new UnauthorizedAccessException();

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

            return user != null && _passwordHasher.Verify(enteredPassword, user.userPasswordHash);
        }

        public bool VerifyAccessPermission(Guid userID, Guid requiredRoleID)
        {
            UserDomain user = _userRepository.GetUser(userID);
            RoleDomain role = _userRepository.GetRole(requiredRoleID);

            return user != null && role != null && user.roleID == role.roleID;
        }

        public bool VerifyOrganizationAccessPermission(Guid userID, Guid requiredOrganizationID)
        {
            UserDomain user = _userRepository.GetUser(userID);

            return user != null && user.organizationID == requiredOrganizationID;
        }
    }
}
