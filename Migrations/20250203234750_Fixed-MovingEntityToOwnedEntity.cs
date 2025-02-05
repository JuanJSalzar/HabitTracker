using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitsTracker.Migrations
{
    /// <inheritdoc />
    public partial class FixedMovingEntityToOwnedEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HabitLogs");

            migrationBuilder.AddColumn<long>(
                name: "CurrentLog_Duration",
                table: "Habit",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentLog_IsCompleted",
                table: "Habit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentLog_Notes",
                table: "Habit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CurrentLog_StartTime",
                table: "Habit",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentLog_Duration",
                table: "Habit");

            migrationBuilder.DropColumn(
                name: "CurrentLog_IsCompleted",
                table: "Habit");

            migrationBuilder.DropColumn(
                name: "CurrentLog_Notes",
                table: "Habit");

            migrationBuilder.DropColumn(
                name: "CurrentLog_StartTime",
                table: "Habit");

            migrationBuilder.CreateTable(
                name: "HabitLogs",
                columns: table => new
                {
                    HabitId = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<long>(type: "bigint", nullable: true),
                    IsCompleted = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitLogs", x => x.HabitId);
                    table.ForeignKey(
                        name: "FK_HabitLogs_Habit_HabitId",
                        column: x => x.HabitId,
                        principalTable: "Habit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
