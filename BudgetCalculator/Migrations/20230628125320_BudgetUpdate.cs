using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetCalculator.Migrations;

    /// <inheritdoc />
    public partial class BudgetUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostCenter",
                table: "Budgets");

            migrationBuilder.AddColumn<int>(
                name: "CostCenterId",
                table: "Budgets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_CostCenterId",
                table: "Budgets",
                column: "CostCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_CostCenters_CostCenterId",
                table: "Budgets",
                column: "CostCenterId",
                principalTable: "CostCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_CostCenters_CostCenterId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_CostCenterId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "Budgets");

            migrationBuilder.AddColumn<string>(
                name: "CostCenter",
                table: "Budgets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
