using System;
using TicketBrite.Core.Entities;
using TicketBriteAPI.Models;

namespace TicketBriteAPI.Models;

public class EventInfoViewModel
{
    public Event Event { get; set; }
    public List<EventTicket> Tickets { get; set; }
}
