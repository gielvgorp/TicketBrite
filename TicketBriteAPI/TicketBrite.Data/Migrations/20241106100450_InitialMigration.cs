using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBrite.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    eventID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    organizationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    eventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    eventDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    eventDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    eventLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    eventAge = table.Column<int>(type: "int", nullable: false),
                    eventCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    eventImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.eventID);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    guestID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    guestName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    guestEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.guestID);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    organizationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    organizationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    organizationEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    organizationPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    organizationAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organizationWebsite = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.organizationID);
                });

            migrationBuilder.CreateTable(
                name: "ReservedTickets",
                columns: table => new
                {
                    ticketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    reservedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    roleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    roleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.roleID);
                });

            migrationBuilder.CreateTable(
                name: "SoldTickets",
                columns: table => new
                {
                    purchaseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ticketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    ticketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    eventID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ticketName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ticketPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ticketMaxAvailable = table.Column<int>(type: "int", nullable: false),
                    ticketStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.ticketID);
                });

            migrationBuilder.CreateTable(
                name: "UserPurchases",
                columns: table => new
                {
                    purchaseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    guestID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPurchases", x => x.purchaseID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    roleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userPasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    organizationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "ReservedTickets");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "SoldTickets");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "UserPurchases");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
