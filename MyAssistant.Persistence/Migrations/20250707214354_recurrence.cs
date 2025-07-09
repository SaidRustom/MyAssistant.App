using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAssistant.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class recurrence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MyAssistantServiceType",
                keyColumn: "Code",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "DueAt",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "ScheduledFor",
                table: "TaskItem");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "TaskItem",
                newName: "ScheduledAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "TaskItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Recurrence",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Interval = table.Column<int>(type: "int", nullable: false),
                    RecurrenceTypeCode = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recurrence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recurrence_RecurrenceTypes_RecurrenceTypeCode",
                        column: x => x.RecurrenceTypeCode,
                        principalTable: "RecurrenceTypes",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_RecurrenceId",
                table: "TaskItem",
                column: "RecurrenceId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListItem_RecurrenceId",
                table: "ShoppingListItem",
                column: "RecurrenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Habit_RecurrenceId",
                table: "Habit",
                column: "RecurrenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Recurrence_RecurrenceTypeCode",
                table: "Recurrence",
                column: "RecurrenceTypeCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Habit_Recurrence_RecurrenceId",
                table: "Habit",
                column: "RecurrenceId",
                principalTable: "Recurrence",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListItem_Recurrence_RecurrenceId",
                table: "ShoppingListItem",
                column: "RecurrenceId",
                principalTable: "Recurrence",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_Recurrence_RecurrenceId",
                table: "TaskItem",
                column: "RecurrenceId",
                principalTable: "Recurrence",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habit_Recurrence_RecurrenceId",
                table: "Habit");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListItem_Recurrence_RecurrenceId",
                table: "ShoppingListItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_Recurrence_RecurrenceId",
                table: "TaskItem");

            migrationBuilder.DropTable(
                name: "Recurrence");

            migrationBuilder.DropIndex(
                name: "IX_TaskItem_RecurrenceId",
                table: "TaskItem");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingListItem_RecurrenceId",
                table: "ShoppingListItem");

            migrationBuilder.DropIndex(
                name: "IX_Habit_RecurrenceId",
                table: "Habit");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "TaskItem");

            migrationBuilder.RenameColumn(
                name: "ScheduledAt",
                table: "TaskItem",
                newName: "Time");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueAt",
                table: "TaskItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledFor",
                table: "TaskItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "MyAssistantServiceType",
                columns: new[] { "Code", "Description" },
                values: new object[] { 1, "Recurrence Processing" });
        }
    }
}
