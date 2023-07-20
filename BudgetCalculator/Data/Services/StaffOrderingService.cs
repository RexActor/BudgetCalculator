using BudgetCalculator.Data.Base;
using BudgetCalculator.Models;

using Microsoft.EntityFrameworkCore;

namespace BudgetCalculator.Data.Services
{
	public class StaffOrderingService : EntityBaseRepository<BudgetEntity>, IStaffOrderingService
	{
		private readonly AppDBContext _context;
		public StaffOrderingService(AppDBContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<BudgetEntity>> GetAllBudgetsByYearMonth(int year, string month)
		{
			var result = await _context.Budgets.Where(item => item.Year == year).Where(item => item.MonthName == month).Include(item => item.CostCenter).ThenInclude(item => item.Department).GroupBy(c => c.CostCenter).Select(item => item.First()).ToListAsync();
			return result;
		}

		public async Task<IEnumerable<DepartmentRoleEntity>> GetRolesForCostCenter(int costCenterId)
		{
			var result = await _context.DepartmentRoles.Where(item => item.CostCenter.Id == costCenterId).Include(item=>item.CostCenter).ThenInclude(item=>item.Department).ToListAsync();
			return result;
		}
	}
}
