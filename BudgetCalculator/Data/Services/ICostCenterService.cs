using BudgetCalculator.Data.Base;
using BudgetCalculator.Data.ViewModels;
using BudgetCalculator.Models;

namespace BudgetCalculator.Data.Services
{
    public interface ICostCenterService : IEntityBaseRepository<CostCenterEntity>
    {

            Task UpdateCostCenterAsync(CostCenterEntityVM newCostCenter);
        Task<CostCenterDropDownVM> GetCostCenterDropDownValuesAsync();
        Task AddNewCostCenterAsync(CostCenterEntityVM newCostCenter);
        Task<CostCenterEntity> GetCostCenterByIdAsync(int id);
       
    }
}
