using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.Core.Entities
{
    public class Event
    {
        public Guid eventID { get; set; }
        public Guid organizationID { get; set; }
        public string eventName { get; set; }
        public string eventDescription { get; set; }
        public DateTime eventDateTime { get; set; }
        public string eventLocation { get; set; }
        public int eventAge { get; set; }
        public string eventCategory { get; set; }
        public string eventImage { get; set; }
    }
}
