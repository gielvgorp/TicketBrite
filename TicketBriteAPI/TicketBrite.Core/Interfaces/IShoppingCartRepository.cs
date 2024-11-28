using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.Core.Interfaces
{
    public interface IShoppingCartRepository
    {
        public void RemoveItemInCart(Guid reservationID);
    }
}
