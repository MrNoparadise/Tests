using Microsoft.EntityFrameworkCore.Migrations;

namespace Main.Migrations
{
    public partial class addid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsBaskets_Baskets_BasketId",
                table: "ProductsBaskets");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsBaskets_Products_ProductId",
                table: "ProductsBaskets");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "ProductsBaskets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BasketId",
                table: "ProductsBaskets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsBaskets_Baskets_BasketId",
                table: "ProductsBaskets",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsBaskets_Products_ProductId",
                table: "ProductsBaskets",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsBaskets_Baskets_BasketId",
                table: "ProductsBaskets");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsBaskets_Products_ProductId",
                table: "ProductsBaskets");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "ProductsBaskets",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "BasketId",
                table: "ProductsBaskets",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsBaskets_Baskets_BasketId",
                table: "ProductsBaskets",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsBaskets_Products_ProductId",
                table: "ProductsBaskets",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
