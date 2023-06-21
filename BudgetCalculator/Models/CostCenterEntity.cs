using BudgetCalculator.Data.Base;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetCalculator.Models
{
	public class CostCenterEntity : IEntityBase
	{
		[Key]
		public int Id { get; set; }

		[DisplayName("Cost Center")]
		[Required]
		public string Name { get; set; }

		public string? Description { get; set; } = null;


		public int DepartmentId { get; set; }

		
		[ForeignKey(nameof(DepartmentId))]
		public DepartmentEntity Department { get; set; }

		public DateTime? CreatedAt { get; set; }
		public string? CreatedBy { get; set; }

		public DateTime? LastUpdatedAt { get; set; }
		public string? LastUpdatedBy { get; set; }

	}
}
