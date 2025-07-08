using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAssistant.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class recurrence1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Recurrence");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "TaskItem",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "LengthInMinutes",
                table: "TaskItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefaultPriority",
                table: "Recurrence",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LengthInMinutes",
                table: "Recurrence",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledAt",
                table: "Recurrence",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LengthInMinutes",
                table: "TaskItem");

            migrationBuilder.DropColumn(
                name: "DefaultPriority",
                table: "Recurrence");

            migrationBuilder.DropColumn(
                name: "LengthInMinutes",
                table: "Recurrence");

            migrationBuilder.DropColumn(
                name: "ScheduledAt",
                table: "Recurrence");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "TaskItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Time",
                table: "Recurrence",
                type: "time",
                nullable: true);
        }
    }
}
