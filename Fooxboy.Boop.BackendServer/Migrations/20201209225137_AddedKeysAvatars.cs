using Microsoft.EntityFrameworkCore.Migrations;

namespace Fooxboy.Boop.BackendServer.Migrations
{
    public partial class AddedKeysAvatars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KeyUploadAvatar",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeyUploadAvatar",
                table: "Users");
        }
    }
}
