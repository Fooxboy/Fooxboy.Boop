using Microsoft.EntityFrameworkCore.Migrations;

namespace Fooxboy.Boop.BackendServer.Migrations
{
    public partial class blyat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Attach",
                table: "Messages",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "UploadAttachments",
                columns: table => new
                {
                    AttachmentId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AttachmentKey = table.Column<long>(nullable: false),
                    TypeAttach = table.Column<long>(nullable: false),
                    User = table.Column<long>(nullable: false),
                    File = table.Column<string>(nullable: true),
                    ChatId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadAttachments", x => x.AttachmentId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadAttachments");

            migrationBuilder.DropColumn(
                name: "Attach",
                table: "Messages");
        }
    }
}
