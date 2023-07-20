using BudgetCalculator.Data.Base;
using BudgetCalculator.Models;

namespace BudgetCalculator.Data.Services;

public interface IStaffOrderingService : IEntityBaseRepository<BudgetEntity>
{
	Task<IEnumerable<BudgetEntity>> GetAllBudgetsByYearMonth(int year, string month);
	Task<IEnumerable<DepartmentRoleEntity>> GetRolesForCostCenter(int costCenterId);
}
