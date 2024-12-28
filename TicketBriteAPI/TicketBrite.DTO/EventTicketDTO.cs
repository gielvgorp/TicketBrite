using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.DTO
{
    public class EventTicketDTO
    {
        public Guid ticketID { get; set; }
        public Guid eventID { get; set; }
        public string ticketName { get; set; }
        public decimal ticketPrice { get; set; }
        public int ticketMaxAvailable { get; set; }
        public int availableTickets { get; set; }
        public Boolean ticketStatus { get; set; }
    }
}
