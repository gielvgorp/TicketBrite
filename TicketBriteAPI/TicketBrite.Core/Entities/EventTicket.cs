using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.Core.Entities
{
    public class EventTicket
    {
        [Key]
        public Guid ticketID {  get; set; }
        public Guid eventID { get; set; }
        public string ticketName { get; set; }
        public decimal ticketPrice { get; set; }
        public int ticketMaxAvailable { get; set; }
        public Boolean ticketStatus {  get; set; }
    }
}
