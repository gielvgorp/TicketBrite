using TicketBrite.DTO;
namespace TicketBriteAPI.Models;

public class EventInfoViewModel
{
    public EventDTO Event { get; set; }
    public List<TicketModel> Tickets { get; set; }
}
