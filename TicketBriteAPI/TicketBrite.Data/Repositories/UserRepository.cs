using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;
using TicketBrite.Core.Domains;

namespace TicketBrite.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        ApplicationDbContext.ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext.ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public void AddUser(UserDomain userDomain)
        {
            User user = new User
            {
                userID = userDomain.userID,
                organizationID = userDomain.organizationID,
                roleID = userDomain.roleID,
                userEmail = userDomain.userEmail,
                userName = userDomain.userName,
                userPasswordHash = userDomain.userPasswordHash
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public UserDomain GetUser(Guid uid)
        {
            User result = _dbContext.Users.FirstOrDefault(u => u.userID == uid);

            if (result == null)
                return null;

            return new UserDomain
            {
                userID = result.userID,
                organizationID = result.organizationID,
                roleID = result.roleID,
                userEmail = result.userEmail,
                userName = result.userName,
                userPasswordHash = result.userPasswordHash
            };
        }

        public void SetOrganization(Guid userID, Guid organizationID)
        {
            User user = _dbContext.Users.Find(userID);

            if (user == null)
                throw new InvalidOperationException("User not found!");

            user.organizationID = organizationID;

            _dbContext.SaveChanges();
        }

        public void SetRole(Guid userID, Guid roleID)
        {
            User user = _dbContext.Users.Find(userID);

            if (user == null)
                throw new InvalidOperationException("User not found!");

            user.roleID = roleID;

            _dbContext.SaveChanges();
        }

        public RoleDomain GetUserRole(Guid userID)
        {
            UserDomain user = GetUser(userID);

            if (user == null)
                return null;

            return GetRole(user.roleID);
        }

        public UserDomain GetUserByEmail(string email)
        {
            return _dbContext.Users
             .Where(u => u.userEmail == email)
             .Select(u => new UserDomain
             {
                 userID = u.userID,
                 organizationID = u.organizationID,
                 roleID = u.roleID,
                 userEmail = u.userEmail,
                 userName = u.userName,
                 userPasswordHash = u.userPasswordHash
             })
             .FirstOrDefault();
        }
        
        public void AddGuest(GuestDomain guestDomain)
        {
            Guest guest = new Guest
            {
                guestID = guestDomain.guestID,
                guestEmail = guestDomain.guestEmail,
                guestName = guestDomain.guestName,
                verificationCode = guestDomain.verificationCode
            };

            _dbContext.Guests.Add(guest);
            _dbContext.SaveChanges();
        }

        public bool VerifyEmail(string email)
        {
            return !_dbContext.Users.Any(u => u.userEmail == email) && !_dbContext.Guests.Any(g => g.guestEmail == email);
        }

        public RoleDomain GetRole(Guid roleID)
        {
            Role result = _dbContext.Roles.FirstOrDefault(r => r.roleID == roleID);

            if (result == null)
                return null;

            return new RoleDomain
            {
                roleID = roleID,
                roleName = result.roleName,
            };
        }
    }
}
