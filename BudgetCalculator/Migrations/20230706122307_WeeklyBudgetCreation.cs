using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetCalculator.Migrations;

    /// <inheritdoc />
    public partial class WeeklyBudgetCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeeklyBudgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WeekNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    BudgetId = table.Column<int>(type: "INTEGER", nullable: false),
                    MonthName = table.Column<string>(type: "TEXT", nullable: false),
                    CostCenterId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cases = table.Column<int>(type: "INTEGER", nullable: false),
                    DirectProductiveHours = table.Column<int>(type: "INTEGER", nullable: false),
                    AgencyProductiveHours = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyBudgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeeklyBudgets_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeeklyBudgets_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyBudgets_BudgetId",
                table: "WeeklyBudgets",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyBudgets_CostCenterId",
                table: "WeeklyBudgets",
                column: "CostCenterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeeklyBudgets");
        }
    }
