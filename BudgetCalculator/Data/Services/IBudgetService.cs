using BudgetCalculator.Controllers;
using BudgetCalculator.Data.Base;
using BudgetCalculator.Data.ViewModels;
using BudgetCalculator.Models;

namespace BudgetCalculator.Data.Services
{
	public interface IBudgetService:IEntityBaseRepository<BudgetEntity>
	{
		Task<BudgetDropDownVM> GetBudgetDropDownValuesAsync();
	}
}
