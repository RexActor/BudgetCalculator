using BudgetCalculator.Data.Base;

namespace BudgetCalculator.Models;

public class WeeklyBudget : IEntityBase
{

	public int Id { get; set; }

	public int WeekNumber { get; set; }
	public BudgetEntity? Budget { get; set; }

	public string? MonthName { get; set; }

	public CostCenterEntity? CostCenter { get; set; }

	public int Cases { get; set; }
	public int DirectProductiveHours { get; set; }
	public int AgencyProductiveHours { get; set; }

}