using BudgetCalculator.Models;

namespace BudgetCalculator.Data.ViewModels
{
	public class MonthlyBudgetVM
	{



		public List<DailyBudget> dailyBudgets { get; set; }
		public WeeklyBudget weeklyBudgets { get; set;}


	}
}
