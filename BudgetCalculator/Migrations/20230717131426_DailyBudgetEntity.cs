using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetCalculator.Migrations
{
    /// <inheritdoc />
    public partial class DailyBudgetEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyBudgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    budgetDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DailyCases = table.Column<int>(type: "INTEGER", nullable: false),
                    DailyAllowedHours = table.Column<double>(type: "REAL", nullable: false),
                    MonthlyMinutesPerCase = table.Column<double>(type: "REAL", nullable: false),
                    DailyTotalMinutes = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyBudgets", x => x.Id);
                });

           
            migrationBuilder.CreateTable(
                name: "DailyRolesData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DailyHeadCountOfRole = table.Column<int>(type: "INTEGER", nullable: false),
                    DailyHoursOfRole = table.Column<int>(type: "INTEGER", nullable: false),
                    DailyBudgetId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyRolesData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyRolesData_DailyBudgets_DailyBudgetId",
                        column: x => x.DailyBudgetId,
                        principalTable: "DailyBudgets",
                        principalColumn: "Id");
                });

           

           
         

            migrationBuilder.CreateIndex(
                name: "IX_DailyRolesData_DailyBudgetId",
                table: "DailyRolesData",
                column: "DailyBudgetId");

          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyRolesData");

            migrationBuilder.DropTable(
                name: "DepartmentRoles");

            migrationBuilder.DropTable(
                name: "WeeklyBudgets");

            migrationBuilder.DropTable(
                name: "DailyBudgets");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "CostCenters");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
