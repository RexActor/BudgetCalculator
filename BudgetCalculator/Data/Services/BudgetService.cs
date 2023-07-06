using BudgetCalculator.Data.Base;
using BudgetCalculator.Data.ViewModels;
using BudgetCalculator.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

		public async Task DeleteBudgetAsync(int year, int costCenterId)
		{

			List<BudgetEntity> budgetEntities = await _context.Budgets.Include(item=>item.CostCenter).Where(item=>item.CostCenter.Id== costCenterId).Where(item=>item.Year==year).ToListAsync();

			_context.Budgets.RemoveRange(budgetEntities);
			await _context.SaveChangesAsync();
			
		}

		public async Task<IEnumerable<BudgetEntity>> GetAllBudgetsAsync()
		{
			
			var budgetList = new List<BudgetEntity>();

			var result = await _context.Budgets.Include(item => item.CostCenter).GroupBy(c=>c.CostCenter).Select(item=>item.First()).ToListAsync();


			return result;
		}

		public async Task<BudgetDropDownVM> GetBudgetDropDownValuesAsync()
		{
			var response = new BudgetDropDownVM()
			{
				CostCenters = await _context.CostCenters.Include(item => item.Department).ToListAsync()
			};

			return response;
		}

		public async Task<BudgetEntityVM> GetByYearAndCostCenterAsync(int year, int costCenterId)
		{
			var budgetDB = await _context.Budgets.Include(item => item.CostCenter).Where(item => (item.Year == year) && (item.CostCenter.Id==costCenterId)).ToListAsync();

		


			List<MonthBudget> monthBudget = new List<MonthBudget>();

			var budgetView = new BudgetEntityVM();
			if (!budgetDB.Any())
			{
				return budgetView;
			}

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
			budgetView.CostCenterId = budgetDB.FirstOrDefault().CostCenter.Id;
			budgetView.Year = budgetDB.FirstOrDefault().Year;
			budgetView.MonthBudgets = monthBudget;
			budgetView.Id=budgetDB.FirstOrDefault().Id;


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
