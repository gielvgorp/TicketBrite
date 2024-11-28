using System.ComponentModel.DataAnnotations;

namespace TicketBriteAPI.Models
{
    public class RegisterViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
