using Microsoft.EntityFrameworkCore.Migrations;

namespace Fooxboy.Boop.BackendServer.Migrations
{
    public partial class AddedLastSeen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastSeenText",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastSeenText",
                table: "Users");
        }
    }
}
