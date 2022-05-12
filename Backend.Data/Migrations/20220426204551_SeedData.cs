using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PermissionModels",
                columns: new[] { "Id", "PermissionName" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "PermissionModels",
                columns: new[] { "Id", "PermissionName" },
                values: new object[] { 2, "User" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PermissionModels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PermissionModels",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
