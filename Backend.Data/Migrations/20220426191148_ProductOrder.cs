using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Data.Migrations
{
    public partial class ProductOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductModel_OrderModel_OrderModelId",
                table: "ProductModel");

            migrationBuilder.DropIndex(
                name: "IX_ProductModel_OrderModelId",
                table: "ProductModel");

            migrationBuilder.DropColumn(
                name: "OrderModelId",
                table: "ProductModel");

            migrationBuilder.CreateTable(
                name: "OrderModelProductModel",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    ProductsId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderModelProductModel", x => new { x.ProductsId, x.ProductsId1 });
                    table.ForeignKey(
                        name: "FK_OrderModelProductModel_OrderModel_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "OrderModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderModelProductModel_ProductModel_ProductsId1",
                        column: x => x.ProductsId1,
                        principalTable: "ProductModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderModelProductModel_ProductsId1",
                table: "OrderModelProductModel",
                column: "ProductsId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderModelProductModel");

            migrationBuilder.AddColumn<int>(
                name: "OrderModelId",
                table: "ProductModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductModel_OrderModelId",
                table: "ProductModel",
                column: "OrderModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductModel_OrderModel_OrderModelId",
                table: "ProductModel",
                column: "OrderModelId",
                principalTable: "OrderModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
