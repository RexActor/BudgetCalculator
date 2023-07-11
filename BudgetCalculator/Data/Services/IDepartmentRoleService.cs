using BudgetCalculator.Data.Base;
using BudgetCalculator.Models;

namespace BudgetCalculator.Data.Services
{
	public interface IDepartmentRoleService:IEntityBaseRepository<DepartmentRoleEntity>
	{

		Task<IEnumerable<CostCenterEntity>> GetCostCenterDropDownValuesAsync();

		Task<IEnumerable<DepartmentRoleEntity>> GetAllRoles();
	}
}
