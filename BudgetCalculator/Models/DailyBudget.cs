using BudgetCalculator.Data.Base;

namespace BudgetCalculator.Models;

public class DailyBudget : IEntityBase
{

	public int Id { get; set; }

	public DateTime budgetDate { get; set; }
	public WeeklyBudget? WeeklyBudgets { get; set; }


	public int DailyCases { get; set; }

	public List<DailyRolesData>? DailyRoles { get; set; }
	public double DailyAllowedHours { get; set; }
	public double MonthlyMinutesPerCase { get; set; }
	public double DailyTotalMinutes { get; set; }

}
