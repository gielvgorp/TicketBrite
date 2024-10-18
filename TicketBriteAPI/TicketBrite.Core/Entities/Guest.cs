using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.Core.Entities
{
    public class Guest
    {
        [Key]
        public Guid guestID { get; set; }
        public string guestName { get; set; }
        public string guestEmail { get; set; }
    }
}
