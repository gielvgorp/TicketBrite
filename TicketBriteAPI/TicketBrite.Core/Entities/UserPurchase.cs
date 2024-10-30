using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.Core.Entities
{
    public class UserPurchase
    {
        [Key]
        public Guid purchaseID { get; set; }
        public Guid? userID { get; set; }
        public Guid? guestID { get; set; }
    }
}
