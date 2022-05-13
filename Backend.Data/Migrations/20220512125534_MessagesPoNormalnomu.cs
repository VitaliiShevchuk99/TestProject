using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Data.Migrations
{
    public partial class MessagesPoNormalnomu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverName",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderName",
                table: "Messages");

            migrationBuilder.CreateTable(
                name: "MessageUserModel",
                columns: table => new
                {
                    MessagesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageUserModel", x => new { x.MessagesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_MessageUserModel_Messages_MessagesId",
                        column: x => x.MessagesId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageUserModel_UserModels_UsersId",
                        column: x => x.UsersId,
                        principalTable: "UserModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageUserModel_UsersId",
                table: "MessageUserModel",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageUserModel");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverName",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
