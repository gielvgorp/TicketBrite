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
            _userRepository.AddUser(user);
        }

        public User GetUser(Guid uid)
        {
            return _userRepository.GetUser(uid);
        }
    }
}
