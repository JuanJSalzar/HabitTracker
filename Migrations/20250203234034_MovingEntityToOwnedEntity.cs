using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitsTracker.Migrations
{
    /// <inheritdoc />
    public partial class MovingEntityToOwnedEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HabitLogs_Habit_HabitId",
                table: "HabitLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HabitLogs",
                table: "HabitLogs");

            migrationBuilder.DropIndex(
                name: "IX_HabitLogs_HabitId",
                table: "HabitLogs");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "HabitLogs");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "HabitLogs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "HabitLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HabitLogs",
                table: "HabitLogs",
                column: "HabitId");

            migrationBuilder.AddForeignKey(
                name: "FK_HabitLogs_Habit_HabitId",
                table: "HabitLogs",
                column: "HabitId",
                principalTable: "Habit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HabitLogs_Habit_HabitId",
                table: "HabitLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HabitLogs",
                table: "HabitLogs");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "HabitLogs",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "HabitLogs",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "HabitLogs",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HabitLogs",
                table: "HabitLogs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HabitLogs_HabitId",
                table: "HabitLogs",
                column: "HabitId");

            migrationBuilder.AddForeignKey(
                name: "FK_HabitLogs_Habit_HabitId",
                table: "HabitLogs",
                column: "HabitId",
                principalTable: "Habit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
