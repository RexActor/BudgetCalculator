using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetCalculator.Migrations;

    /// <inheritdoc />
    public partial class CostCenters_Added_Columns_LastUpdateAt_LastUpdateBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "CostCenters",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedBy",
                table: "CostCenters",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "CostCenters");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "CostCenters");
        }
    }
