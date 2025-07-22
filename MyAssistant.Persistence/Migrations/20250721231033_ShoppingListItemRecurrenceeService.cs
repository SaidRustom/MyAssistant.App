using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAssistant.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingListItemRecurrenceeService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ShoppingList",
                newName: "Title");

            migrationBuilder.AddColumn<bool>(
                name: "NotifyOwnerOnChange",
                table: "TaskItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RecurrenceInterval",
                table: "ShoppingListItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "NotifyOwnerOnChange",
                table: "ShoppingList",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NotifyOwnerOnChange",
                table: "Habit",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NotifyOwnerOnChange",
                table: "Goal",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NotifyOwnerOnChange",
                table: "BillingInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "BillingInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "ServiceType",
                columns: new[] { "Code", "Description" },
                values: new object[] { 1, "Recurring Shopping List Item Activation Service" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ServiceType",
                keyColumn: "Code",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "NotifyOwnerOnChange",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "RecurrenceInterval",
                table: "ShoppingListItem");

            migrationBuilder.DropColumn(
                name: "NotifyOwnerOnChange",
                table: "ShoppingList");

            migrationBuilder.DropColumn(
                name: "NotifyOwnerOnChange",
                table: "Habit");

            migrationBuilder.DropColumn(
                name: "NotifyOwnerOnChange",
                table: "Goal");

            migrationBuilder.DropColumn(
                name: "NotifyOwnerOnChange",
                table: "BillingInfo");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "BillingInfo");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ShoppingList",
                newName: "Name");
        }
    }
}
