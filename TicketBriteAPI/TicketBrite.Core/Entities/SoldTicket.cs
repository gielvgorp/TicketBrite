using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.Core.Entities
{
    [Keyless]
    public class SoldTicket
    {
        public Guid purchaseID {  get; set; }
        public Guid ticketID { get; set; }
    }
}
