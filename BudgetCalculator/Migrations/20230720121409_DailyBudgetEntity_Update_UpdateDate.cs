using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetCalculator.Migrations
{
    /// <inheritdoc />
    public partial class DailyBudgetEntity_Update_UpdateDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyBudgets_WeeklyBudgets_WeeklyBudgetsId",
                table: "DailyBudgets");

            migrationBuilder.AlterColumn<int>(
                name: "WeeklyBudgetsId",
                table: "DailyBudgets",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "DailyBudgets",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_DailyBudgets_WeeklyBudgets_WeeklyBudgetsId",
                table: "DailyBudgets",
                column: "WeeklyBudgetsId",
                principalTable: "WeeklyBudgets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyBudgets_WeeklyBudgets_WeeklyBudgetsId",
                table: "DailyBudgets");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "DailyBudgets");

            migrationBuilder.AlterColumn<int>(
                name: "WeeklyBudgetsId",
                table: "DailyBudgets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyBudgets_WeeklyBudgets_WeeklyBudgetsId",
                table: "DailyBudgets",
                column: "WeeklyBudgetsId",
                principalTable: "WeeklyBudgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
