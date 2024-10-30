using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.Core.Entities
{
    public class Role
    {
        [Key]
        public Guid roleID {  get; set; }
        public string roleName { get; set; }
    }
}
