using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAssistant.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class shoppingList_Add_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "CreatedDate",
                table: "ShoppingList",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ShoppingList");
        }
    }
}
