using Microsoft.EntityFrameworkCore.Migrations;

namespace Fooxboy.Boop.BackendServer.Migrations
{
    public partial class addedServiceMessange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ServiceMessage",
                table: "Messages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "GroupChats",
                columns: table => new
                {
                    ChatId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Members = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Admin = table.Column<long>(nullable: false),
                    Messanges = table.Column<string>(nullable: true),
                    Cover = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChats", x => x.ChatId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupChats");

            migrationBuilder.DropColumn(
                name: "ServiceMessage",
                table: "Messages");
        }
    }
}
