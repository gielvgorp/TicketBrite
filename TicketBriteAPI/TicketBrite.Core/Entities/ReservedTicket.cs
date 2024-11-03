using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.Core.Entities
{
    [Keyless]
    public class ReservedTicket
    {
        public Guid ticketID { get; set; }
        public Guid userID { get; set; }
        public DateTime reservedAt { get; set; }
    }
}
