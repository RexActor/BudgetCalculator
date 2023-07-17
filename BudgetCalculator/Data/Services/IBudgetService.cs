using BudgetCalculator.Controllers;
using BudgetCalculator.Data.Base;
using BudgetCalculator.Data.ViewModels;
using BudgetCalculator.Models;

namespace BudgetCalculator.Data.Services;

public interface IBudgetService : IEntityBaseRepository<BudgetEntity>
{
	Task<BudgetDropDownVM> GetBudgetDropDownValuesAsync();
	Task CreateBudget(BudgetEntityVM entity);

	Task UpdateDailyBudget(DailyBudget entity);
	Task UpdateBudget(BudgetEntityVM entity);
	Task DeleteBudgetAsync(int year,int costCenterId);
	Task<BudgetEntityVM> GetByYearAndCostCenterAsync(int year,int costCenterId);
	Task<IEnumerable<WeeklyBudget>>GetWeeklyBudgetAsync(int year,int costCenterId);
	Task<IEnumerable<BudgetEntity>> GetAllBudgetsAsync();
	Task<IEnumerable<DepartmentRoleEntity>> GetDepartmentRolesAsync(int costCenterId);
	Task<WeeklyBudget>GetWeeklyBudgetByIdAsync(int weeklyBudgetId);
	Task<IEnumerable<DailyBudget>> GetDailyBudgetByWeeklyIdAsync(int weeklyBudgetId);
}
