using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEndProjectCode.Migrations
{
    public partial class ChangeBasketTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_AspNetUsers_AppUserId",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketProduct_Basket_BasketId",
                table: "BasketProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketProduct_Products_ProductId",
                table: "BasketProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketProduct",
                table: "BasketProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Basket",
                table: "Basket");

            migrationBuilder.RenameTable(
                name: "BasketProduct",
                newName: "BasketProducts");

            migrationBuilder.RenameTable(
                name: "Basket",
                newName: "Baskets");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProduct_ProductId",
                table: "BasketProducts",
                newName: "IX_BasketProducts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProduct_BasketId",
                table: "BasketProducts",
                newName: "IX_BasketProducts_BasketId");

            migrationBuilder.RenameIndex(
                name: "IX_Basket_AppUserId",
                table: "Baskets",
                newName: "IX_Baskets_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketProducts",
                table: "BasketProducts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Baskets",
                table: "Baskets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_Baskets_BasketId",
                table: "BasketProducts",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_Products_ProductId",
                table: "BasketProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_AspNetUsers_AppUserId",
                table: "Baskets",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_Baskets_BasketId",
                table: "BasketProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_Products_ProductId",
                table: "BasketProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_AspNetUsers_AppUserId",
                table: "Baskets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Baskets",
                table: "Baskets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketProducts",
                table: "BasketProducts");

            migrationBuilder.RenameTable(
                name: "Baskets",
                newName: "Basket");

            migrationBuilder.RenameTable(
                name: "BasketProducts",
                newName: "BasketProduct");

            migrationBuilder.RenameIndex(
                name: "IX_Baskets_AppUserId",
                table: "Basket",
                newName: "IX_Basket_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProducts_ProductId",
                table: "BasketProduct",
                newName: "IX_BasketProduct_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProducts_BasketId",
                table: "BasketProduct",
                newName: "IX_BasketProduct_BasketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Basket",
                table: "Basket",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketProduct",
                table: "BasketProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_AspNetUsers_AppUserId",
                table: "Basket",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProduct_Basket_BasketId",
                table: "BasketProduct",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProduct_Products_ProductId",
                table: "BasketProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
