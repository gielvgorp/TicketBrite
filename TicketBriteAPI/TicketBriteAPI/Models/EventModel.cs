using System;
using TicketBriteAPI.Models;

namespace TicketBriteAPI.Models;

public class EventModel
{
    public int Id { get; set; }
    public string EventName { get; set; }
    public string Artist { get; set; }
    public DateTime EventDate { get; set; }
    public string EventLocation { get; set; }
    public int EventAge { get; set; }
    public string Category { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public List<TicketModel> Tickets { get; set; }
}
