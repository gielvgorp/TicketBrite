using System;

namespace TicketBrite.Core.Entities;

public class Ticket
{
    public Guid ticketID { get; set; }
    public string ticketName { get; set; }
    public decimal ticketPrice { get; set; }
    public  int ticketMaxAvailable { get; set; }
    public bool ticketStatus { get; set; }
}
