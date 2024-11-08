using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;

namespace TicketBrite.Core.Interfaces
{
    public interface IUserRepository
    {
        public void AddUser(User user);
        public User GetUser(Guid uid);
        public Role GetUserRole(Guid userID);
        public void AddGuest(Guest guest);
        public bool VerifyEmail(string email);
    }
}
