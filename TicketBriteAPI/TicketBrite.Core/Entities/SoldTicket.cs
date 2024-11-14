using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.Core.Entities
{
    public class SoldTicket
    {
        [Key]
        public Guid soldTicketID { get; set; }
        public Guid purchaseID {  get; set; }
        public Guid ticketID { get; set; }
    }
}
