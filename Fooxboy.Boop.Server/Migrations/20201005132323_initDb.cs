using Microsoft.EntityFrameworkCore.Migrations;

namespace Fooxboy.Boop.Server.Migrations
{
    public partial class initDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MsgId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SenderId = table.Column<long>(nullable: false),
                    RecieverId = table.Column<long>(nullable: false),
                    ChatId = table.Column<long>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Time = table.Column<long>(nullable: false),
                    UsersReaded = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MsgId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nickname = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PathProfilePic = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UsersChats",
                columns: table => new
                {
                    LocalId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Owner = table.Column<long>(nullable: false),
                    ChatId = table.Column<long>(nullable: false),
                    Messages = table.Column<string>(nullable: true),
                    LastMessage = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersChats", x => x.LocalId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UsersChats");
        }
    }
}
