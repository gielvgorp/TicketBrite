using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Interfaces;

namespace TicketBrite.Core.Services
{
    public class ShoppingCartService
    {
        IShoppingCartRepository _shoppingCartRepository;
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository) 
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public void RemoveItemInCart(Guid reservaionID)
        {
            _shoppingCartRepository.RemoveItemInCart(reservaionID);
        }
    }
}
