using BudgetCalculator.Data.Base;

using Microsoft.AspNetCore.Mvc;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BudgetCalculator.Models;

public class DepartmentEntity : IEntityBase
{

	public int Id { get; set; }

	[Required]
	[DisplayName("Department name")]
	public string? Name { get; set; }

	public string? Description { get; set; }

	public DateTime? CreatedDate { get; set; }
	public string? CreatedBy { get; set; }

	public DateTime? LastUpdatedAt { get; set; }
	public string? LastUpdatedBy { get; set; }

}