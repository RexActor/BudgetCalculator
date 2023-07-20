using BudgetCalculator.Data.Base;

using System.ComponentModel.DataAnnotations;

namespace BudgetCalculator.Models;

public class BudgetEntity : IEntityBase
{
	[Key]
	public int Id { get; set; }

	public string MonthName { get; set; }
	public CostCenterEntity CostCenter { get; set; }

	public int Cases { get; set; }
	public int DirectProductiveHours { get; set; }
	public int AgencyProductiveHours { get; set; }
	public int Year { get; set; }
}