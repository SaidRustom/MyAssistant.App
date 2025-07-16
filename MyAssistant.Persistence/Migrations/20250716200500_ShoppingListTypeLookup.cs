using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyAssistant.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingListTypeLookup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoppingListTypeCode",
                table: "ShoppingList",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ShoppingListType",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingListType", x => x.Code);
                });

            migrationBuilder.InsertData(
                table: "ShoppingListType",
                columns: new[] { "Code", "Description" },
                values: new object[,]
                {
                    { 1, "Groceries" },
                    { 2, "Pharmaceuticals" },
                    { 3, "Electronics" },
                    { 4, "Clothing" },
                    { 5, "HomeGoods" },
                    { 6, "Beauty" },
                    { 7, "Toys" },
                    { 8, "Books" },
                    { 9, "Office Supplies" },
                    { 10, "Sports Equipment" },
                    { 11, "Automotive" },
                    { 12, "Pet Supplies" },
                    { 13, "Garden" },
                    { 14, "Baby Products" },
                    { 15, "Furniture" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingList_ShoppingListTypeCode",
                table: "ShoppingList",
                column: "ShoppingListTypeCode");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingList_ShoppingListType_ShoppingListTypeCode",
                table: "ShoppingList",
                column: "ShoppingListTypeCode",
                principalTable: "ShoppingListType",
                principalColumn: "Code",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingList_ShoppingListType_ShoppingListTypeCode",
                table: "ShoppingList");

            migrationBuilder.DropTable(
                name: "ShoppingListType");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingList_ShoppingListTypeCode",
                table: "ShoppingList");

            migrationBuilder.DropColumn(
                name: "ShoppingListTypeCode",
                table: "ShoppingList");
        }
    }
}
