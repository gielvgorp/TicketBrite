using TicketBrite.Core.Entities;

namespace TicketBriteAPI.Models
{
    public class ShoppingCartModel
    {
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public decimal totalPrice { get; set; }
    }

    public partial class ShoppingCartItem
    {
        public ReservedTicket reservedTicket { get; set; }
        public EventTicket eventTicket { get; set; }
        public Event Event { get; set; }
    }
}
