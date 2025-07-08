using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAssistant.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class recurrence2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduledAt",
                table: "Recurrence");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Recurrence",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Recurrence");

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledAt",
                table: "Recurrence",
                type: "datetime2",
                nullable: true);
        }
    }
}
