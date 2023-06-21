using BudgetCalculator.Data.Base;
using BudgetCalculator.Models;

namespace BudgetCalculator.Data.Services
{
	public class CostCenterService : EntityBaseRepository<CostCenters>, ICostCenterService
	{
		public CostCenterService(AppDBContext context) : base(context) { }
		
	}
}
