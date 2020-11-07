using Microsoft.EntityFrameworkCore.Migrations;

namespace Fooxboy.Boop.BackendServer.Migrations
{
    public partial class addNameOnMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageSender",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameSender",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageSender",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "NameSender",
                table: "Messages");
        }
    }
}
