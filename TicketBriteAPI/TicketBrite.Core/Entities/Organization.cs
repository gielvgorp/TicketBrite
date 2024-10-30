using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.Core.Entities
{
    public class Organization
    {
        [Key]
        public Guid organizationID { get; set; }
        public string organizationName { get; set; }
        public string organizationEmail { get; set; }
        public string organizationPhone { get; set; }
        public string? organizationAddress { get; set; }
        public string? organizationWebsite { get; set; }
    }
}
