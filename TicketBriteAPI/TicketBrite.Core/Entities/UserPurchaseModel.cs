using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.Core.Entities
{
    public class UserPurchaseModel
    {
        public UserPurchase userPurchase { get; set; }
        public List<EventTicket> eventTickets { get; set; }
    }
}
