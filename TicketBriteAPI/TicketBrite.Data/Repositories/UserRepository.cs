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

        public void SetOrganization(Guid userID, Guid organizationID)
        {
            User user = _dbContext.Users.Find(userID);

            if (user == null)
                throw new Exception("User not found!");

            user.organizationID = organizationID;

            _dbContext.SaveChanges();
        }

        public void SetRole(Guid userID, Guid roleID)
        {
            User user = _dbContext.Users.Find(userID);

            if (user == null)
                throw new Exception("User not found!");

            user.roleID = roleID;

            _dbContext.SaveChanges();
        }

        public Role GetUserRole(Guid userID)
        {
            User user = GetUser(userID);

            if (user != null)
            {
                return GetRole(user.roleID);
            }

            return null;
        }

        public User GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.userEmail == email);
        }
        
        public Role GetRole(Guid roleID)
        {
            return _dbContext.Roles.FirstOrDefault(r => r.roleID == roleID);
        }
    }
}
