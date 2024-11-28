using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Interfaces;

namespace TicketBrite.Data.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        ApplicationDbContext.ApplicationDbContext _context;
        public ShoppingCartRepository(ApplicationDbContext.ApplicationDbContext context) 
        { 
            _context = context;
        }

        public void RemoveItemInCart(Guid reservationID)
        {
            ReservedTicket ticket = _context.ReservedTickets.Find(reservationID);

            _context.ReservedTickets.Remove(ticket);
            _context.SaveChanges();
        }
    }
}
