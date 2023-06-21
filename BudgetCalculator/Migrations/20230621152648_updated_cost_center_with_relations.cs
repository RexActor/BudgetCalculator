using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetCalculator.Migrations
{
    /// <inheritdoc />
    public partial class updated_cost_center_with_relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "CostCenters");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "CostCenters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CostCenters_DepartmentId",
                table: "CostCenters",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CostCenters_Departments_DepartmentId",
                table: "CostCenters",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostCenters_Departments_DepartmentId",
                table: "CostCenters");

            migrationBuilder.DropIndex(
                name: "IX_CostCenters_DepartmentId",
                table: "CostCenters");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "CostCenters");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "CostCenters",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
