using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.Core.Entities
{
    public class ReservedTicket
    {
        [Key]
        public Guid reservedID {  get; set; }
        public Guid ticketID { get; set; }
        public Guid userID { get; set; }
        public DateTime reservedAt { get; set; }
    }
}
