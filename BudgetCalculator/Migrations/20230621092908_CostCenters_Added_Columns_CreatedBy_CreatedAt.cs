using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetCalculator.Migrations;

    /// <inheritdoc />
    public partial class CostCenters_Added_Columns_CreatedBy_CreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CostCenters",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CostCenters",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CostCenters");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CostCenters");
        }
    }
