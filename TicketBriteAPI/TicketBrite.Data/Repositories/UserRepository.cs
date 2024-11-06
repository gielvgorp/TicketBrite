using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;

namespace TicketBrite.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        ApplicationDbContext.ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext.ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public User GetUser(Guid uid)
        {
            return _dbContext.Users.FirstOrDefault(u => u.userID == uid);
        }

        public Role GetUserRole(Guid userID)
        {
            User user = GetUser(userID);

            if (user != null)
            {
                return _dbContext.Roles.FirstOrDefault(r => r.roleID == user.roleID);
            }

            return null;
        }
    }
}
