using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.DTO
{
    public class ReservatedTicketDTO
    {
        public Guid reservedID { get; set; }
        public Guid ticketID { get; set; }
        public Guid userID { get; set; }
        public DateTime reservedAt { get; set; }
    }
}
