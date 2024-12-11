using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.DTO
{
    public class CreateUserDTO
    {
        public Guid OrganizationID { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public Guid RoleID { get; set; }
        public string Password { get; set; }
    }
}
