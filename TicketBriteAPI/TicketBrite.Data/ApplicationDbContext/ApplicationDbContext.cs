using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketBrite.Core.Entities;

namespace TicketBrite.Data.ApplicationDbContext
{
    public class ApplicationDbContext : DbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<EventTicket> Tickets { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Event> EventTickets { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserPurchase> UserPurchases { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<SoldTicket> SoldTickets { get; set; }
    }
}
