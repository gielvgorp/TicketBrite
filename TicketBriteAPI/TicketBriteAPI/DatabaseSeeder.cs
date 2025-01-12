using TicketBrite.Data.ApplicationDbContext;
using TicketBrite.Core.Entities;
using Microsoft.EntityFrameworkCore;
using TicketBrite.Core.Enums;

namespace TicketBriteAPI
{
    public static class DatabaseSeeder
    {
        public static void SeedDatabase(ApplicationDbContext context)
        {
            // Zorg ervoor dat de database wordt gemigreerd
            context.Database.Migrate();

            // Voeg testdata toe als de database leeg is
            if (!context.Events.Any())
            {
                context.Events.AddRange(
                    new Event
                    {
                        eventID = Guid.Parse("F827D813-E04A-4E84-8D69-72BAEF15FCD4"),
                        eventName = "Snollebollekes: LIVE!",
                        eventImage = "https://snollebollekeslive.nl/wp-content/uploads/SPARK_20240323_221911_SLIC_VB.jpg",
                        eventDateTime = DateTime.Now.AddMonths(2),
                        eventLocation = "Johan Cruijff ArenA, Amsterdam",
                        eventCategory = "Festival",
                        eventDescription = "Koop nu snel je tickets voor Snollebolles LIVE!",
                        isVerified = true,
                        eventAge = 18,
                        organizationID = Guid.Parse("77726785-FA72-4244-A572-AFFEAF20D5F1")
                    }
                );
            }

            if (!context.Tickets.Any())
            {
                context.Tickets.AddRange(
                   new EventTicket
                   {
                       ticketID = Guid.Parse("A5D59493-F56B-4BD4-811D-421A4DE96AEC"),
                       eventID = Guid.Parse("F827D813-E04A-4E84-8D69-72BAEF15FCD4"),
                       ticketName = "Staanplaatsen",
                       ticketMaxAvailable = 100,
                       ticketPrice = 50,
                       ticketStatus = true
                   },
                    new EventTicket
                    {
                        ticketID = Guid.Parse("195CCC56-9A08-433B-A9DE-7BE7881A4C3A"),
                        eventID = Guid.Parse("F827D813-E04A-4E84-8D69-72BAEF15FCD4"),
                        ticketName = "VIP area",
                        ticketMaxAvailable = 100,
                        ticketPrice = 125,
                        ticketStatus = true
                    },
                    new EventTicket
                    {
                        ticketID = Guid.Parse("BB7CEB18-C4A7-4144-8FDB-F584AD2EF758"),
                        eventID = Guid.Parse("F827D813-E04A-4E84-8D69-72BAEF15FCD4"),
                        ticketName = "Zitplaatsen",
                        ticketMaxAvailable = 100,
                        ticketPrice = 50,
                        ticketStatus = true
                    }
                );

                if (!context.Organizations.Any())
                {
                    context.Organizations.AddRange(
                        new Organization
                        {
                            organizationID = Guid.Parse("77726785-FA72-4244-A572-AFFEAF20D5F1"),
                            organizationName = "Snollebollekes",
                            organizationEmail = "info@snollebollekes.nl",
                            organizationAddress = "Vogelwikke 15, Venray",
                            organizationPhone = "06 12345678",
                            organizationWebsite = "https://www.snollebollekes.nl/"
                        }
                    );
                }

                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                        new Role
                        {
                            roleID = Roles.Admin,
                            roleName = "Beheerder",
                        },
                        new Role
                        {
                            roleID = Roles.Customer,
                            roleName = "Klant",
                        },
                        new Role
                        {
                            roleID = Roles.Organization,
                            roleName = "Organization",
                        }
                    );
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User
                        {
                            userID = Guid.Parse("3DA20937-588C-43A3-A599-08DD0BE5C8EF"),
                            userName = "Cypress",
                            roleID = Guid.Parse("43A72AC5-91BA-402D-83F5-20F23B637A92"),
                            organizationID = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                            userEmail = "Cypress@e2e.com",
                            userPasswordHash = "$2a$11$07FGRTooaG.kJMNONIBhL.shYAe7IWnDX5VTVAzbMG/LN5p7lt/fC",
                        },
                        new User
                        {
                            userID = Guid.Parse("B80C80C4-2789-4596-A4BF-F3736C4DE1B1"),
                            userName = "Cypress",
                            roleID = Guid.Parse("B80C80C4-2789-4596-A4BF-F3736C4DE1B1"),
                            organizationID = Guid.Parse("77726785-FA72-4244-A572-AFFEAF20D5F1"),
                            userEmail = "cypress-organization@e2e.com",
                            userPasswordHash = "$2a$11$bahOC/Ei.8SqZ3zescbH8e.r2q2q2vKgE4APF5HqF2yeRZ/lsS/QO"
                        },
                         new User
                         {
                             userID = Guid.Parse("7EEF760F-A39D-4EA6-7964-08DD0EC02129"),
                             userName = "Cypress",
                             roleID = Guid.Parse("3228BA9C-A8DB-48B4-95C4-16998615BB10"),
                             organizationID = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                             userEmail = "cypress-admin@e2e.com",
                             userPasswordHash = "$2a$11$UMucLzDYRbaSpk9ysSvpiuHIc5W2nH7b4KS6ix2DCZRvTENE8ED8C"
                         }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}
