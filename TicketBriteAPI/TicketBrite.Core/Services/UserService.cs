using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Domains;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;
using TicketBrite.DTO;

namespace TicketBrite.Core.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher _passwordHasher;

        public UserService(IUserRepository c_userRepository)
        {
            _userRepository = c_userRepository;
            _passwordHasher = new PasswordHasher();
        }

        public void AddUser(CreateUserDTO user)
        {
            VerifyEmail(user.UserEmail);

            string passwordHash = _passwordHasher.Hash(user.Password);

            UserDomain userDomain = new UserDomain
            {
                userID = Guid.NewGuid(),
                organizationID = Guid.Empty,
                userEmail = user.UserEmail,
                userName = user.UserName,
                roleID = Guid.Parse("43A72AC5-91BA-402D-83F5-20F23B637A92"),
                userPasswordHash = passwordHash
            };

            _userRepository.AddUser(userDomain);
        }

        public UserDTO GetUserByEmail(string email)
        {
            UserDomain user = _userRepository.GetUserByEmail(email);

            if (user == null)
                throw new InvalidOperationException("Gebruiker is niet gevonden!");

            return new UserDTO
            {
                userID = user.userID,
                organizationID = user.organizationID,
                roleID = user.roleID,
                userEmail = user.userEmail,
                userName = user.userName,
            };
        }

        public UserDTO GetUser(Guid uid)
        {
            UserDomain user = _userRepository.GetUser(uid);

            if(user == null)
                throw new InvalidOperationException("Gebruiker is niet gevonden!");

            return new UserDTO
            {
                userID = user.userID,
                organizationID = user.organizationID,
                roleID = user.roleID,
                roleName = GetRole(user.roleID).roleName,
                userEmail = user.userEmail,
                userName = user.userName,
            };
        }

        public void VerifyEmail(string email)
        {
            if (!_userRepository.VerifyEmail(email)) 
                throw new InvalidOperationException("Het opgegeven e-mailadres bestaat al!");
        }

        public RoleDTO GetUserRole(Guid userID)
        {
            RoleDomain role = _userRepository.GetUserRole(userID);

            if (role == null)
                throw new InvalidOperationException("Rol niet gevonden!");

            return new RoleDTO
            {
                roleID = role.roleID,
                roleName = role.roleName
            };
        }

        public void AddGuest(GuestDTO guest)
        {
            VerifyEmail(guest.guestEmail);

            GuestDomain guestDomain = new GuestDomain
            {
                guestID = Guid.NewGuid(),
                guestEmail = guest.guestEmail,
                guestName = guest.guestName,
                verificationCode = Guid.NewGuid()
            };

            _userRepository.AddGuest(guestDomain);
        }

        public void SetRole(Guid userID, Guid roleID)
        {
            _userRepository.SetRole(userID, roleID);
        }

        public RoleDTO GetRole(Guid roleID)
        {
            RoleDomain role = _userRepository.GetRole(roleID);

            if (role == null)
                throw new InvalidOperationException("Rol niet gevonden!");

            return new RoleDTO
            {
                roleID = role.roleID,
                roleName = role.roleName
            };
        }

        public void SetOrganization(Guid organizationID, Guid userID)
        {
            _userRepository.SetOrganization(userID, organizationID);
        }
    }
}
