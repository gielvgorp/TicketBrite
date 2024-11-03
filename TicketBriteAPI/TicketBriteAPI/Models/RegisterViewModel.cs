using System.ComponentModel.DataAnnotations;

namespace TicketBriteAPI.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
