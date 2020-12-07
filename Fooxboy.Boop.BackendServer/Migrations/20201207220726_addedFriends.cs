using Microsoft.EntityFrameworkCore.Migrations;

namespace Fooxboy.Boop.BackendServer.Migrations
{
    public partial class addedFriends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirendsRequests",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Friends",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriendsRequested",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastSeen",
                table: "Users",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirendsRequests",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Friends",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FriendsRequested",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastSeen",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");
        }
    }
}
