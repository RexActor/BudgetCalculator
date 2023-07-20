using BudgetCalculator.Data.Base;

using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetCalculator.Models;

public class DepartmentRoleEntity : IEntityBase

{


	public int Id { get; set; }

	public string? Name { get; set; }
	public string? Description { get; set; }
	public string? CreatedBy { get; set; }
	public DateTime CreatedDate { get; set; }

	public int CostCenterId { get; set; }

	[ForeignKey(nameof(CostCenterId))]
	public CostCenterEntity? CostCenter { get; set; }

}