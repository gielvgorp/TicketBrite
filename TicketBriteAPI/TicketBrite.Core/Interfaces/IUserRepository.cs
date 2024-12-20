using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Domains;


namespace TicketBrite.Core.Interfaces
{
        public interface IUserRepository
        {
                public void AddUser(UserDomain user);
                public UserDomain GetUser(Guid uid);
                public RoleDomain GetUserRole(Guid userID);
                public UserDomain GetUserByEmail(string email);
                public void AddGuest(GuestDomain guest);
                public bool VerifyEmail(string email);
                public RoleDomain GetRole(Guid roleID);
                public void SetRole(Guid userID, Guid roleID);
                public void SetOrganization(Guid userID, Guid organizationID);
        }
}
