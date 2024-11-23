using Microsoft.AspNetCore.SignalR;
using TicketBriteAPI.Hubs;
public class TicketStatisticsNotifier : ITicketStatisticsNotifier
{
    private readonly IHubContext<TicketStatisticsHub> _hubContext;

    public TicketStatisticsNotifier(IHubContext<TicketStatisticsHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyStatisticsUpdated(Guid ticketID, int soldTickets, int reservedTickets)
    {
        await _hubContext.Clients.All.SendAsync("UpdateTicketStats", new { TicketID = ticketID, SoldTickets = soldTickets, ReservedTickets = reservedTickets });
    }
}
