using BudgetCalculator.Data.Base;
using BudgetCalculator.Data.ViewModels;
using BudgetCalculator.Models;

using System.Collections.Generic;

namespace BudgetCalculator.Data.Services;

public interface ICostCenterService : IEntityBaseRepository<CostCenterEntity>
{

	Task UpdateCostCenterAsync(CostCenterEntityVM newCostCenter);
	Task<CostCenterDropDownVM> GetCostCenterDropDownValuesAsync();
	Task AddNewCostCenterAsync(CostCenterEntityVM newCostCenter);
	Task<CostCenterEntity> GetCostCenterByIdAsync(int id);
	Task<List<CostCenterEntity>> GetAllCostCentersAsync();

}