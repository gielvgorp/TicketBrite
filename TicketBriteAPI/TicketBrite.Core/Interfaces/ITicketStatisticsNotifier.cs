using System.Threading.Tasks;

public interface ITicketStatisticsNotifier
{
    Task NotifyStatisticsUpdated(Guid ticketID, int soldTickets, int reservedTickets);
}
