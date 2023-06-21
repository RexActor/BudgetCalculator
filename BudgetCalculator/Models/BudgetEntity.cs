using BudgetCalculator.Data.Base;

using System.ComponentModel.DataAnnotations;

namespace BudgetCalculator.Models
{
	public class BudgetEntity : IEntityBase
	{
		[Key]
		public int Id { get; set; }
	}
}
