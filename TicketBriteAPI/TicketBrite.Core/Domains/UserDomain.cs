using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.Core.Domains
{
    public class UserDomain
    {
        public Guid userID { get; set; }
        public Guid roleID { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public string userPasswordHash { get; set; }
        public Guid? organizationID { get; set; }
    }
}
