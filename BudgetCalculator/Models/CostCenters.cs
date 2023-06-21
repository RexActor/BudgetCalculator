using BudgetCalculator.Data.Base;

using System.ComponentModel.DataAnnotations;

namespace BudgetCalculator.Models
{
	public class CostCenters:IEntityBase
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

	}
}
