using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketBrite.Core.Entities;
using TicketBrite.Core.Services;
using TicketBrite.Data.ApplicationDbContext;
using TicketBrite.Data.Repositories;
using TicketBrite.DTO;
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

        public ShoppingCartController(TicketService ticketService, EventService eventService, ShoppingCartService shoppingCartService)
        {
            _ticketService = ticketService;
            _eventService = eventService;
            _shoppingCartService = shoppingCartService;
        }


        [HttpDelete("{reserveID}/delete")]
        [Authorize]
        [ProducesResponseType(typeof(List<EventDTO>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public JsonResult DeleteItem(Guid reserveID)
        {
            try
            {
                Guid userID;

                if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userID))
                {
                    throw new UnauthorizedAccessException();
                }

                _shoppingCartService.RemoveItemInCart(reserveID, userID);

                return new JsonResult(Ok(string.Format(ExceptionMessages.DeletedSuccesfully, "Ticket uit winkelwagen")));
            }
            catch (UnauthorizedAccessException)
            {
                return new JsonResult(NotFound(ExceptionMessages.ReservationNotFound));
            }
            catch (KeyNotFoundException)
            {
                return new JsonResult(NotFound(ExceptionMessages.ReservatedTicketNotFound));
            }
            catch(Exception)
            {
                return new JsonResult(BadRequest(ExceptionMessages.GeneralException));
            }

        }

        [HttpGet("items")]
        [Authorize]
        [ProducesResponseType(typeof(ShoppingCartModel), 200)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 400)]
        public JsonResult GetItems()
        {
            Guid userID = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (userID == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
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
            catch (UnauthorizedAccessException)
            {
                return new JsonResult(Unauthorized(ExceptionMessages.UnauthorizedAccess));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest(ExceptionMessages.GeneralException));
            }
        }
    }
}
