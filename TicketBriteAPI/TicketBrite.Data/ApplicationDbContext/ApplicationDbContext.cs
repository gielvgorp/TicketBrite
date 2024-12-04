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
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserPurchase> UserPurchases { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<SoldTicket> SoldTickets { get; set; }
        public DbSet<ReservedTicket> ReservedTickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Voorbeeld seeding
            modelBuilder.Entity<Role>().HasData(
                new Role { roleID = Guid.Parse("43A72AC5-91BA-402D-83F5-20F23B637A92"), roleName = "Klant" },
                new Role { roleID = Guid.Parse("B80C80C4-2789-4596-A4BF-F3736C4DE1B1"), roleName = "Organization" },
                new Role { roleID = Guid.Parse("3228BA9C-A8DB-48B4-95C4-16998615BB10"), roleName = "Beheerder" }
            );
        }
    }
}
