using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.Core.Entities
{
    public class TicketStatistic
    {
        public EventTicket ticket { get; set; }
        public int ticketReserve { get; set; }
        public int ticketSold { get; set; }
    }
}
