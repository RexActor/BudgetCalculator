using BudgetCalculator.Data.Base;
using BudgetCalculator.Data.ViewModels;
using BudgetCalculator.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

using System.Collections.Generic;

namespace BudgetCalculator.Data.Services
{
	public class BudgetService : EntityBaseRepository<BudgetEntity>, IBudgetService
	{

		private readonly AppDBContext _context;
		public BudgetService(AppDBContext context) : base(context)
		{
			_context = context;
		}

		public async Task CreateBudget(BudgetEntityVM entity)
		{
			List<BudgetEntity> budgets = new List<BudgetEntity>();

			var costCenter = await _context.CostCenters.FirstOrDefaultAsync(item => item.Id == entity.CostCenterId);
			if (costCenter is null)
			{
				return;
			}

			entity.MonthBudgets.ForEach(item =>
			{
				budgets.Add(new BudgetEntity
				{
					Cases = item.Cases,
					CostCenter = costCenter,
					AgencyProductiveHours = item.AgencyProductiveHours,
					DirectProductiveHours = item.DirectProductiveHours,
					MonthName = item.MonthName,
					Year = entity.Year

				});


			});


			await _context.Budgets.AddRangeAsync(budgets);

			await _context.SaveChangesAsync();


		}

		public async Task<BudgetDropDownVM> GetBudgetDropDownValuesAsync()
		{
			var response = new BudgetDropDownVM()
			{
				CostCenters = await _context.CostCenters.Include(item => item.Department).ToListAsync()
			};

			return response;
		}

		public async Task<BudgetEntityVM> GetByYearAsync(int year)
		{
			var budgetDB = await _context.Budgets.Include(item => item.CostCenter).Where(item => item.Year == year).ToListAsync();

			List<MonthBudget> monthBudget = new List<MonthBudget>();

			var budgetView = new BudgetEntityVM();
			budgetDB.ForEach(item =>
			{
				monthBudget.Add(new MonthBudget
				{
					Id = item.Id,
					Cases = item.Cases,
					AgencyProductiveHours = item.AgencyProductiveHours,
					DirectProductiveHours = item.DirectProductiveHours,
					MonthName = item.MonthName,

				});
			});
			budgetView.CostCenterId = budgetDB.First().CostCenter.Id;
			budgetView.Year = budgetDB.First().Year;
			budgetView.MonthBudgets = monthBudget;



			return budgetView;

		}

		public async Task UpdateBudget(BudgetEntityVM entity)
		{
			List<BudgetEntity> budgets = new List<BudgetEntity>();

			var costCenter = await _context.CostCenters.FirstOrDefaultAsync(item => item.Id == entity.CostCenterId);
			if (costCenter is null)
			{
				return;
			}

			entity.MonthBudgets.ForEach(async item =>
			{

				var objectInDB = await _context.Budgets.FindAsync(item.Id);

				if (objectInDB is not null)
				{
					objectInDB.AgencyProductiveHours = item.AgencyProductiveHours;
					objectInDB.DirectProductiveHours = item.DirectProductiveHours;
					objectInDB.CostCenter = costCenter;
					objectInDB.Year = entity.Year;
					objectInDB.Cases = item.Cases;
					objectInDB.MonthName = item.MonthName;

					_context.Budgets.Update(objectInDB);
				}



			});





			await _context.SaveChangesAsync();
		}
	}

}
