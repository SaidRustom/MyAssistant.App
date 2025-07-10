using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAssistant.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class shoppingListItem_Edit_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalPurchaseCount",
                table: "ShoppingListItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPurchaseCount",
                table: "ShoppingListItem");
        }
    }
}
