using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAssistant.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingListItemRecurrence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListItem_Recurrence_RecurrenceId",
                table: "ShoppingListItem");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingListItem_RecurrenceId",
                table: "ShoppingListItem");

            migrationBuilder.DropColumn(
                name: "RecurrenceId",
                table: "ShoppingListItem");

            migrationBuilder.AddColumn<bool>(
                name: "IsRecurring",
                table: "ShoppingListItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "NextOccurrenceDate",
                table: "ShoppingListItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecurrenceTypeCode",
                table: "ShoppingListItem",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListItem_RecurrenceTypeCode",
                table: "ShoppingListItem",
                column: "RecurrenceTypeCode");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListItem_RecurrenceType_RecurrenceTypeCode",
                table: "ShoppingListItem",
                column: "RecurrenceTypeCode",
                principalTable: "RecurrenceType",
                principalColumn: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListItem_RecurrenceType_RecurrenceTypeCode",
                table: "ShoppingListItem");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingListItem_RecurrenceTypeCode",
                table: "ShoppingListItem");

            migrationBuilder.DropColumn(
                name: "IsRecurring",
                table: "ShoppingListItem");

            migrationBuilder.DropColumn(
                name: "NextOccurrenceDate",
                table: "ShoppingListItem");

            migrationBuilder.DropColumn(
                name: "RecurrenceTypeCode",
                table: "ShoppingListItem");

            migrationBuilder.AddColumn<Guid>(
                name: "RecurrenceId",
                table: "ShoppingListItem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListItem_RecurrenceId",
                table: "ShoppingListItem",
                column: "RecurrenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListItem_Recurrence_RecurrenceId",
                table: "ShoppingListItem",
                column: "RecurrenceId",
                principalTable: "Recurrence",
                principalColumn: "Id");
        }
    }
}
