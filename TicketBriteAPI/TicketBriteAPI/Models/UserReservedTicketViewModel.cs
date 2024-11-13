using TicketBrite.Core.Entities;

namespace TicketBriteAPI.Models
{
    public class UserReservedTicketViewModel
    {
        public EventTicket ticket {  get; set; }
        public ReservedTicket reservation { get; set; }
    }
}
