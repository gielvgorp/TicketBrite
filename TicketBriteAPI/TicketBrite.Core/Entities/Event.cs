using System;

namespace TicketBrite.Core.Entities;

public class Event
{
    public Guid eventID { get; set; }
    public void organizationID { get; set; }
    public string eventName { get; set; }
    public DateTime eventDateTime { get; set; }
    public string eventLocation { get; set; }
    public int eventMinAge { get; set; }

}
