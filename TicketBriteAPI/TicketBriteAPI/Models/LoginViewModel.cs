using System.ComponentModel.DataAnnotations;

namespace TicketBriteAPI.Models
{
    public class LoginViewModel
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }
    }
}
