using BudgetCalculator.Data.Base;
using BudgetCalculator.Models;

using Microsoft.EntityFrameworkCore;

namespace BudgetCalculator.Data.Services
{
	public class DepartmentRoleService : EntityBaseRepository<DepartmentRoleEntity>, IDepartmentRoleService
	{
		private readonly AppDBContext _context;
		public DepartmentRoleService(AppDBContext context) : base(context)
		{
			{
				_context = context;
			}
		}

		public async Task<IEnumerable<DepartmentRoleEntity>> GetAllRoles()
		{
			return await _context.DepartmentRoles.Include(item => item.CostCenter).ThenInclude(item => item.Department).ToListAsync();
		}

		public async Task<IEnumerable<CostCenterEntity>> GetCostCenterDropDownValuesAsync()
		{
			var costCenters = await _context.CostCenters.Include(item=>item.Department).ToListAsync();

			return costCenters;
		}
	}
}
