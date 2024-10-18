using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBrite.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateeventtableupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "eventCategory",
                table: "Event",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "eventDescription",
                table: "Event",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "eventImage",
                table: "Event",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "eventCategory",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "eventDescription",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "eventImage",
                table: "Event");
        }
    }
}
