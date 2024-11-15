﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicketBrite.Data.ApplicationDbContext;

#nullable disable

namespace TicketBrite.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext.ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TicketBrite.Core.Entities.Event", b =>
                {
                    b.Property<Guid>("eventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("eventAge")
                        .HasColumnType("int");

                    b.Property<string>("eventCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("eventDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("eventDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("eventImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("eventLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("eventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isVerified")
                        .HasColumnType("bit");

                    b.Property<Guid>("organizationID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("eventID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("TicketBrite.Core.Entities.EventTicket", b =>
                {
                    b.Property<Guid>("ticketID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("eventID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ticketMaxAvailable")
                        .HasColumnType("int");

                    b.Property<string>("ticketName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ticketPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("ticketStatus")
                        .HasColumnType("bit");

                    b.HasKey("ticketID");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("TicketBrite.Core.Entities.Guest", b =>
                {
                    b.Property<Guid>("guestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("guestEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("guestName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("verificationCode")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("guestID");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("TicketBrite.Core.Entities.Organization", b =>
                {
                    b.Property<Guid>("organizationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("organizationAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("organizationEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("organizationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("organizationPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("organizationWebsite")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("organizationID");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("TicketBrite.Core.Entities.ReservedTicket", b =>
                {
                    b.Property<Guid>("reservedID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("reservedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ticketID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("userID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("reservedID");

                    b.ToTable("ReservedTickets");
                });

            modelBuilder.Entity("TicketBrite.Core.Entities.Role", b =>
                {
                    b.Property<Guid>("roleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("roleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("roleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("TicketBrite.Core.Entities.SoldTicket", b =>
                {
                    b.Property<Guid>("soldTicketID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("purchaseID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ticketID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("soldTicketID");

                    b.ToTable("SoldTickets");
                });

            modelBuilder.Entity("TicketBrite.Core.Entities.User", b =>
                {
                    b.Property<Guid>("userID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("organizationID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("roleID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("userEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userPasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TicketBrite.Core.Entities.UserPurchase", b =>
                {
                    b.Property<Guid>("purchaseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("guestID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("userID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("purchaseID");

                    b.ToTable("UserPurchases");
                });
#pragma warning restore 612, 618
        }
    }
}
