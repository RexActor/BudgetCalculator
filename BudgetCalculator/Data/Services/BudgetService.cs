using BudgetCalculator.Controllers;
using BudgetCalculator.Data.Base;
using BudgetCalculator.Data.ViewModels;
using BudgetCalculator.Models;

using Microsoft.EntityFrameworkCore;

namespace BudgetCalculator.Data.Services
{
	public class BudgetService : EntityBaseRepository<BudgetEntity>, IBudgetService
	{

		private readonly AppDBContext _context;
		public BudgetService(AppDBContext context):base(context)
		{
			_context= context;
		}

		public async Task<BudgetDropDownVM> GetBudgetDropDownValuesAsync()
		{
			var response = new BudgetDropDownVM()
			{
				CostCenters = await _context.CostCenters.ToListAsync()
			};

			return response;
		}
	}
	
}
