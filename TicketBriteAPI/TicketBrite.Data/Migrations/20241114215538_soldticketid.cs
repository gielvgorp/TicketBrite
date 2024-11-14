using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBrite.Data.Migrations
{
    /// <inheritdoc />
    public partial class soldticketid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "soldTicketID",
                table: "SoldTickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SoldTickets",
                table: "SoldTickets",
                column: "soldTicketID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservedTickets",
                table: "ReservedTickets",
                column: "reservedID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SoldTickets",
                table: "SoldTickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservedTickets",
                table: "ReservedTickets");

            migrationBuilder.DropColumn(
                name: "soldTicketID",
                table: "SoldTickets");
        }
    }
}
