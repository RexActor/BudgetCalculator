using System.ComponentModel.DataAnnotations;

namespace BudgetCalculator.Models
{
	public class MonthBudget
	{

		public int Id { get; set; }
		public string MonthName { get; set; }

		public int Cases { get; set; }
		public int DirectProductiveHours { get; set; }
		public int AgencyProductiveHours { get; set; }

	}



}
