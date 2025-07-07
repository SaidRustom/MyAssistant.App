using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAssistant.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class servicelog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRecurring",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "RecurrenceTypeCode",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "IsRecurring",
                table: "ShoppingListItem");

            migrationBuilder.DropColumn(
                name: "RecurrenceEndDate",
                table: "ShoppingListItem");

            migrationBuilder.DropColumn(
                name: "RecurrenceTypeCode",
                table: "ShoppingListItem");

            migrationBuilder.DropColumn(
                name: "IsRecurring",
                table: "Habit");

            migrationBuilder.DropColumn(
                name: "RecurrenceEndDate",
                table: "Habit");

            migrationBuilder.DropColumn(
                name: "RecurrenceTypeCode",
                table: "Habit");

            migrationBuilder.RenameColumn(
                name: "RecurrenceEndDate",
                table: "TaskItem",
                newName: "ScheduledFor");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "TaskItem",
                newName: "DueAt");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "TaskItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "RecurrenceId",
                table: "TaskItem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RecurrenceId",
                table: "ShoppingListItem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RecurrenceId",
                table: "Habit",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MyAssistantServiceTypeCode",
                table: "AuditLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MyAssistantServiceType",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyAssistantServiceType", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "MyAssistantServiceLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResultDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyAssistantServiceTypeCode = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyAssistantServiceLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyAssistantServiceLog_MyAssistantServiceType_MyAssistantServiceTypeCode",
                        column: x => x.MyAssistantServiceTypeCode,
                        principalTable: "MyAssistantServiceType",
                        principalColumn: "Code");
                });

            migrationBuilder.InsertData(
                table: "MyAssistantServiceType",
                columns: new[] { "Code", "Description" },
                values: new object[] { 1, "Recurrence Processing" });

            migrationBuilder.CreateIndex(
                name: "IX_MyAssistantServiceLog_MyAssistantServiceTypeCode",
                table: "MyAssistantServiceLog",
                column: "MyAssistantServiceTypeCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyAssistantServiceLog");

            migrationBuilder.DropTable(
                name: "MyAssistantServiceType");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "RecurrenceId",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "RecurrenceId",
                table: "ShoppingListItem");

            migrationBuilder.DropColumn(
                name: "RecurrenceId",
                table: "Habit");

            migrationBuilder.DropColumn(
                name: "MyAssistantServiceTypeCode",
                table: "AuditLogs");

            migrationBuilder.RenameColumn(
                name: "ScheduledFor",
                table: "TaskItem",
                newName: "RecurrenceEndDate");

            migrationBuilder.RenameColumn(
                name: "DueAt",
                table: "TaskItem",
                newName: "DueDate");

            migrationBuilder.AddColumn<bool>(
                name: "IsRecurring",
                table: "TaskItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RecurrenceTypeCode",
                table: "TaskItem",
                type: "int",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRecurring",
                table: "ShoppingListItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecurrenceEndDate",
                table: "ShoppingListItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecurrenceTypeCode",
                table: "ShoppingListItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRecurring",
                table: "Habit",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecurrenceEndDate",
                table: "Habit",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecurrenceTypeCode",
                table: "Habit",
                type: "int",
                nullable: true);
        }
    }
}
