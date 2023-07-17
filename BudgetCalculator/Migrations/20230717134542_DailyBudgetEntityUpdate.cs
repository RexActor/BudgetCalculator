using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetCalculator.Migrations
{
    /// <inheritdoc />
    public partial class DailyBudgetEntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WeeklyBudgetsId",
                table: "DailyBudgets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DailyBudgets_WeeklyBudgetsId",
                table: "DailyBudgets",
                column: "WeeklyBudgetsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyBudgets_WeeklyBudgets_WeeklyBudgetsId",
                table: "DailyBudgets",
                column: "WeeklyBudgetsId",
                principalTable: "WeeklyBudgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyBudgets_WeeklyBudgets_WeeklyBudgetsId",
                table: "DailyBudgets");

            migrationBuilder.DropIndex(
                name: "IX_DailyBudgets_WeeklyBudgetsId",
                table: "DailyBudgets");

            migrationBuilder.DropColumn(
                name: "WeeklyBudgetsId",
                table: "DailyBudgets");
        }
    }
}
