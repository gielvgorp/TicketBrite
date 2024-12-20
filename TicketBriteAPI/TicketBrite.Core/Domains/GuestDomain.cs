using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.Core.Domains
{
    public class GuestDomain
    {
        public Guid guestID { get; set; }
        public string guestName { get; set; }
        public string guestEmail { get; set; }
        public Guid verificationCode { get; set; }
    }
}
