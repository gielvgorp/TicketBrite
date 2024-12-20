using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBrite.Core.Services
{
    public class PasswordHasher
    {
        public bool Verify(string enteredPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
        }

        public string Hash(string enteredPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(enteredPassword);
        }
    }
}
