using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.DTO
{
    public class GuestDTO
    {
        public Guid guestID { get; set; }
        public string guestName { get; set; }
        public string guestEmail { get; set; }
    }
}
