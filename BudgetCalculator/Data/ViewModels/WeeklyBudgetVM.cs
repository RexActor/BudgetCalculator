using BudgetCalculator.Models;

namespace BudgetCalculator.Data.ViewModels;

public class WeeklyBudgetVM
{


	public int Id { get; set; }

	public int WeekNumber { get; set; }
	public BudgetEntity? Budget { get; set; }

	public string? MonthName { get; set; }

	public CostCenterEntity? CostCenter { get; set; }

	public int Cases { get; set; }
	public int DirectProductiveHours { get; set; }
	public int AgencyProductiveHours { get; set; }

	public List<DailyBudget>dailyBudgets { get; set; }



}
