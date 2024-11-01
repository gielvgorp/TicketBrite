using System.Net.Sockets;
using TicketBrite.Core.Entities;

namespace TicketBriteAPI.Models
{
    public class NewOrganizaionEvent
    {
        public Event Event { get; set; }  // Dit is je bestaande Event model
        public List<EventTicket> Tickets { get; set; }  // Dit is je Ticket model
    }
}
