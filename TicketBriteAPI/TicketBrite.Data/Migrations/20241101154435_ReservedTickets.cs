using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBrite.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReservedTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservedTickets");
        }
    }
}
