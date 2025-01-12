using TicketBrite.Core.Interfaces;
using TicketBrite.Core.Entities;

namespace TicketBrite.Core.Services
{
    public class ShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ITicketRepository _ticketRepository;
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, ITicketRepository ticketRepository) 
        {
            _shoppingCartRepository = shoppingCartRepository;
            _ticketRepository = ticketRepository;
        }

        public void RemoveItemInCart(Guid reservaionID, Guid userID)
        {
            Entities.ReservedTicket reservatedTicket = _ticketRepository.GetReservedTicket(reservaionID);

            if(reservatedTicket == null)
            {
                throw new KeyNotFoundException();
            }

            if(reservatedTicket.userID != userID)
            {
                throw new UnauthorizedAccessException();
            }

            _shoppingCartRepository.RemoveItemInCart(reservaionID);
        }
    }
}
