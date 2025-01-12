using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Services;
using TicketBrite.Data.ApplicationDbContext;
using TicketBrite.Data.Repositories;
using TicketBriteAPI.Models;

namespace TicketBriteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        TicketService _ticketService;
        EventService _eventService;
        ShoppingCartService _shoppingCartService;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _ticketService = new TicketService(new TicketRepository(context));
            _eventService = new EventService(new EventRepository(context));
            _shoppingCartService = new ShoppingCartService(new ShoppingCartRepository(context));
        }


        [HttpDelete("/shopping-cart/{reserveID}")]
        [Authorize]
        public JsonResult DeleteItem(Guid reserveID)
        {
            try
            {
                _shoppingCartService.RemoveItemInCart(reserveID);

                return new JsonResult(new
                {
                    StatusCode = 204
                });
            }
            catch (KeyNotFoundException ex)
            {
                return new JsonResult(NotFound(ex.Message));
            }
            catch(Exception)
            {
                return new JsonResult(BadRequest("Er heeft zich een onverwachtse fout opgetreden!"));
            }

        }

        [HttpGet("/shopping-cart/get-items")]
        [Authorize]
        public JsonResult GetItems()
        {
            Guid userID = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (userID == Guid.Empty)
            {
                return new JsonResult(Unauthorized("Gebruiker is niet ingelogd!"));
            }

            try
            {
                List<ReservedTicket> tickets = _ticketService.GetReservedTicketsOfUser(userID);

                ShoppingCartModel shoppingCart = new ShoppingCartModel();

                foreach (ReservedTicket reservedTicket in tickets)
                {
                    EventTicket ticket = _ticketService.GetTicket(reservedTicket.ticketID);

                    shoppingCart.Items.Add(new ShoppingCartItem
                    {
                        reservedTicket = reservedTicket,
                        eventTicket = ticket,
                        Event = _eventService.GetEvent(ticket.eventID)
                    });

                    shoppingCart.totalPrice += ticket.ticketPrice;
                }

                return new JsonResult(Ok(shoppingCart));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
        }
    }
}
