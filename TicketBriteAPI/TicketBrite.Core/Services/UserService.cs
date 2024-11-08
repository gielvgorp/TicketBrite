using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;

namespace TicketBrite.Core.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository c_userRepository)
        {
            _userRepository = c_userRepository;
        }

        public void AddUser(User user)
        {
            VerifyEmail(user.userEmail);
            _userRepository.AddUser(user);
        }

        public User GetUser(Guid uid)
        {
            return _userRepository.GetUser(uid);
        }

        public void VerifyEmail(string email)
        {
            if (!_userRepository.VerifyEmail(email)) throw new Exception("Email adres bestaat al!");
        }

        public Role GetUserRole(Guid userID)
        {
            return _userRepository.GetUserRole(userID);
        }

        public void AddGuest(Guest guest)
        {
            VerifyEmail(guest.guestEmail);
            _userRepository.AddGuest(guest);
        }

        public void SetRole(Guid userID, Guid roleID)
        {
            _userRepository.SetRole(userID, roleID);
        }

        public Role GetRole(Guid roleID)
        {
            return _userRepository.GetRole(roleID);
        }

        public void SetOrganization(Guid organizationID, Guid userID)
        {
            _userRepository.SetOrganization(userID, organizationID);
        }
    }
}
