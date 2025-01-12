using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.Core.Enums
{
    public static class Roles
    {
        public static readonly Guid Customer = Guid.Parse("43A72AC5-91BA-402D-83F5-20F23B637A92");
        public static readonly Guid Organization = Guid.Parse("B80C80C4-2789-4596-A4BF-F3736C4DE1B1");
        public static readonly Guid Admin = Guid.Parse("3228BA9C-A8DB-48B4-95C4-16998615BB10");
    }
}
