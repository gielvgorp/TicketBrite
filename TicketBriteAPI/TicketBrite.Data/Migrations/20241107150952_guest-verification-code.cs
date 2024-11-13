using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBrite.Data.Migrations
{
    /// <inheritdoc />
    public partial class guestverificationcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "verificationCode",
                table: "Guests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "verificationCode",
                table: "Guests");
        }
    }
}
