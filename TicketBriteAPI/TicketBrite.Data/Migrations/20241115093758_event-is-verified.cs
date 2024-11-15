using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBrite.Data.Migrations
{
    /// <inheritdoc />
    public partial class eventisverified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isVerified",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isVerified",
                table: "Events");
        }
    }
}
