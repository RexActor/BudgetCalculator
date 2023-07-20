using BudgetCalculator.Models;

namespace BudgetCalculator.Data.ViewModels
{
	public class OrderStaffVM
	{


		public List<BudgetEntity>? BudgetEntity { get; set; }
		public List<DepartmentRoleEntity> Roles { get; set; }
		public int ProducingQuantiy { get; set; }
		public int OutboundQuantity { get; set; }
		public DateTime OrderDate { get; set; }
	}
}
