using BudgetCalculator.Data.Base;
using BudgetCalculator.Data.ViewModels;
using BudgetCalculator.Finance.Calendar;
using BudgetCalculator.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;

using System.Collections.Generic;
using System.Diagnostics;

namespace BudgetCalculator.Data.Services;

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
				MonthName = item.MonthName ?? default!,
				Year = entity.Year
			});
		});


		await _context.Budgets.AddRangeAsync(budgets);
		await _context.SaveChangesAsync();


		List<WeeklyBudget> weeklyBudgets = new List<WeeklyBudget>();
		int weekNumber = 1;

		budgets.ForEach(budget =>
		{
			for (int i = 0; i < FinanceCalendar.FinanceCalendarWeekModel[budget.MonthName]; i++)
			{
				weeklyBudgets.Add(new WeeklyBudget
				{
					MonthName = budget.MonthName,
					Cases = budget.Cases / FinanceCalendar.FinanceCalendarWeekModel[budget.MonthName],
					DirectProductiveHours = budget.DirectProductiveHours / FinanceCalendar.FinanceCalendarWeekModel[budget.MonthName],
					AgencyProductiveHours = budget.AgencyProductiveHours / FinanceCalendar.FinanceCalendarWeekModel[budget.MonthName],
					CostCenter = budget.CostCenter,
					WeekNumber = weekNumber,
					Budget = budget

				});
				weekNumber++;
			}
		});

		await _context.WeeklyBudgets.AddRangeAsync(weeklyBudgets);
		await _context.SaveChangesAsync();


	}

	public async Task DeleteBudgetAsync(int year, int costCenterId)
	{

		List<BudgetEntity> budgetEntities = await _context.Budgets.Include(item => item.CostCenter).Where(item => item.CostCenter.Id == costCenterId).Where(item => item.Year == year).ToListAsync();

		_context.Budgets.RemoveRange(budgetEntities);
		await _context.SaveChangesAsync();

	}

	public async Task<IEnumerable<BudgetEntity>> GetAllBudgetsAsync()
	{


		var result = await _context.Budgets.Include(item => item.CostCenter).ThenInclude(item => item.Department).GroupBy(c => c.CostCenter).Select(item => item.First()).ToListAsync();
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
		var budgetDB = await _context.Budgets.Include(item => item.CostCenter).Where(item => (item.Year == year) && (item.CostCenter.Id == costCenterId)).ToListAsync();
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

		budgetView.CostCenterId = budgetDB.FirstOrDefault()?.CostCenter?.Id ?? 0;
		budgetView.Year = budgetDB.FirstOrDefault()?.Year ?? 0;
		budgetView.MonthBudgets = monthBudget;
		budgetView.Id = budgetDB.FirstOrDefault()?.Id ?? 0;

		return budgetView;

	}

	public async Task<IEnumerable<DailyBudget>> GetDailyBudgetByWeeklyIdAsync(int weeklyBudgetId)
	{
		return await _context.DailyBudgets.Include(item => item.WeeklyBudgets).ThenInclude(item => item.CostCenter).ThenInclude(item => item.Department).Include(item=>item.DailyRoles).ToListAsync();
	}

	public async Task<IEnumerable<DepartmentRoleEntity>> GetDepartmentRolesAsync(int costCenterId)
	{
		return await _context.DepartmentRoles.Include(item => item.CostCenter).ThenInclude(item => item!.Department).Where(item => item!.CostCenter!.Id == costCenterId).ToListAsync();
	}

	public async Task<IEnumerable<WeeklyBudget>> GetWeeklyBudgetAsync(int year, int costCenterId)
	{
		List<WeeklyBudget> weeklyBudget = await _context.WeeklyBudgets.Include(item => item.CostCenter).Include(item => item.Budget).ThenInclude(item => item!.CostCenter!.Department).Where(item => item!.Budget!.Year == year).Where(item => item.CostCenter!.Id == costCenterId).ToListAsync();

		return weeklyBudget;
	}

	public async Task<WeeklyBudget> GetWeeklyBudgetByIdAsync(int weeklyBudgetId)
	{
		return await _context.WeeklyBudgets.Include(item => item.Budget).ThenInclude(item => item!.CostCenter).ThenInclude(item => item.Department).Where(item => item.Id == weeklyBudgetId).FirstOrDefaultAsync() ?? default!;
	}

	public async Task<WeeklyBudget> GetWeeklyBudgetsByDateAsync(int weekNumber,DateTime budgetDate)
	{
		return await _context.WeeklyBudgets.Include(item => item.Budget).ThenInclude(item => item!.CostCenter).ThenInclude(item => item.Department).Where(item => item.WeekNumber == weekNumber).FirstOrDefaultAsync() ?? default!;
	}

	public async Task UpdateBudget(BudgetEntityVM entity)
	{


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
				objectInDB.MonthName = item.MonthName ?? default!;

				_context.Budgets.Update(objectInDB);


				var weeklyBudgetInDB = await _context.WeeklyBudgets.Where(item => item!.Budget!.Id == objectInDB.Id).ToListAsync();

				foreach (var weeklyBudget in weeklyBudgetInDB)
				{
					weeklyBudget.CostCenter = costCenter;
					weeklyBudget.MonthName = item.MonthName;
					weeklyBudget.DirectProductiveHours = item.DirectProductiveHours / FinanceCalendar.FinanceCalendarWeekModel[item.MonthName ?? default!];
					weeklyBudget.AgencyProductiveHours = item.AgencyProductiveHours / FinanceCalendar.FinanceCalendarWeekModel[item.MonthName ?? default!];

					weeklyBudget.Budget = objectInDB;
				}

			}




		});


		await _context.SaveChangesAsync();


	}

	public async Task UpdateDailyBudget(DailyBudget entity)
	{
		await _context.DailyBudgets.AddAsync(entity);

		await _context.SaveChangesAsync();
	}
}


