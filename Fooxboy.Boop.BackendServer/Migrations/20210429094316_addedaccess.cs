using Microsoft.EntityFrameworkCore.Migrations;

namespace Fooxboy.Boop.BackendServer.Migrations
{
    public partial class addedaccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccessLevel",
                table: "Users",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "EMail",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessLevel",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EMail",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Group",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "Users");
        }
    }
}
