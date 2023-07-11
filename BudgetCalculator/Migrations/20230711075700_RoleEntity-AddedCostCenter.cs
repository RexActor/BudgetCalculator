using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetCalculator.Migrations
{
    /// <inheritdoc />
    public partial class RoleEntityAddedCostCenter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CostCenterId",
                table: "DepartmentRoles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DepartmentRoles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DepartmentRoles",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentRoles_CostCenterId",
                table: "DepartmentRoles",
                column: "CostCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentRoles_CostCenters_CostCenterId",
                table: "DepartmentRoles",
                column: "CostCenterId",
                principalTable: "CostCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentRoles_CostCenters_CostCenterId",
                table: "DepartmentRoles");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentRoles_CostCenterId",
                table: "DepartmentRoles");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "DepartmentRoles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DepartmentRoles");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DepartmentRoles");
        }
    }
}
