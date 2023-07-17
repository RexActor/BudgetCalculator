using BudgetCalculator.Data.Base;
using BudgetCalculator.Models;

using System.ComponentModel.DataAnnotations;

namespace BudgetCalculator.Data.ViewModels;

public class BudgetEntityVM
{
	public int Id { get; set; }
	public List<MonthBudget> MonthBudgets { get; set; } = new List<MonthBudget>();
	public int CostCenterId { get; set; }
	public int Year { get; set; }


}
