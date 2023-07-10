using BudgetCalculator.Data.Services;
using BudgetCalculator.Data.ViewModels;
using BudgetCalculator.Finance.Calendar;
using BudgetCalculator.Migrations;
using BudgetCalculator.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace BudgetCalculator.Controllers
{
	public class BudgetController : Controller
	{
		private readonly IBudgetService _service;
		public BudgetController(IBudgetService service)
		{
			_service = service;
		}


		public async Task<IActionResult> Index()
		{
			var budgets = await _service.GetAllBudgetsAsync();
			return View(budgets);
		}




		//GET: weekly?year={year}&costCenterId={costcenterID}
		[HttpGet]
		public async Task<IActionResult> Weekly(int year, int CostCenterId, int monthIndex)
		{
			if (monthIndex > 11)
			{
				monthIndex = 11;
			}
			if (monthIndex < 0)
			{
				monthIndex = 0;
			}

			string monthName = FinanceCalendar.FinanceCalendarWeekModel.ElementAt(monthIndex).Key;
			int amountToDisplay = FinanceCalendar.FinanceCalendarWeekModel[monthName];
			var weeklyBudgets = await _service.GetWeeklyBudgetAsync(year, CostCenterId);

			if (monthIndex > 1)
			{

				ViewBag.PreviousMonth = monthIndex-1;

			}
			if (monthIndex == 12)
			{
				ViewBag.NextMonth = monthIndex;
			}
			else
			{
				ViewBag.NextMonth = monthIndex + 1;
			}


			ViewBag.CurrentMonth = monthName;
			ViewBag.CurrentMonthIndex = monthIndex;

			//return RedirectToRoute(new 
			//{
			//	controller="Budget",
			//	action="Weekly",
			//	CostCenterId = CostCenterId,
			//	year =year,

			//	monthIndex= monthIndex
			//});

			//return RedirectToAction(nameof(Weekly), new {year=year, CostCenterId= CostCenterId, monthIndex = monthIndex});
			return View(weeklyBudgets.Where(item => item.MonthName == monthName));
		}



		[HttpGet]

		public async Task<IActionResult> Create()
		{
			var budgetList = new BudgetEntityVM();
			var budgetDropDowns = await _service.GetBudgetDropDownValuesAsync();
			var costCenterList = new List<SelectListItem>();
			var departments = new List<SelectListGroup>();

			foreach (var dropDown in budgetDropDowns.CostCenters)
			{
				if (!departments.Any(item => item.Name == dropDown.Department.Name.ToString()))
				{
					departments.Add(
						new SelectListGroup { Name = dropDown.Department.Name.ToString() }
						);
				}
			}

			foreach (var department in departments)
			{
				budgetDropDowns.CostCenters.AsEnumerable().Where(item => item.Department.Name == department.Name).ToList().ForEach(item =>
				{
					costCenterList.Add(new SelectListItem
					{
						Value = item.Id.ToString(),
						Text = item.Name,
						Group = department
					});
				});
			}

			foreach (var month in FinanceCalendar.FinanceCalendarWeekModel.Keys)
			{
				budgetList.MonthBudgets.Add(new MonthBudget
				{
					MonthName = month,
					Cases = 0,
					AgencyProductiveHours = 0,
					DirectProductiveHours = 0

				});
			}

			ViewBag.CostCenters = costCenterList;
			return View(budgetList);
		}

		[HttpPost]
		public async Task<IActionResult> Create(BudgetEntityVM budget)
		{


			var budgetDb = await _service.GetByYearAndCostCenterAsync(budget.Year, budget.CostCenterId);

			if (budgetDb is null)
			{
				return View("CustomError", $"Budget Exists for {budget.Year} this Cost Center with ID: {budgetDb.CostCenterId}");
			}








			await _service.CreateBudget(budget);



			return RedirectToAction(nameof(Index));
		}

		//GET:update?year={year}&CostCenter={costCenter}
		[HttpGet]
		public async Task<IActionResult> Update(int year, int costCenterId)
		{

			var budgetList = await _service.GetByYearAndCostCenterAsync(year, costCenterId);
			if (!budgetList.MonthBudgets.Any())
			{
				return View("CustomError", $"We couldn't locate budget for Cost Center with ID: {costCenterId} and Year: {year}");
			}

			var budgetDropDowns = await _service.GetBudgetDropDownValuesAsync();
			var costCenterList = new List<SelectListItem>();
			var departments = new List<SelectListGroup>();

			if (budgetDropDowns.CostCenters is not null)
			{
				foreach (var dropDown in budgetDropDowns.CostCenters)
				{
					if (!departments.Any(item => item.Name == dropDown.Department.Name.ToString()))
					{
						departments.Add(
							new SelectListGroup { Name = dropDown.Department.Name.ToString() }
							);
					}
				}
			}

			foreach (var department in departments)
			{
				budgetDropDowns.CostCenters.AsEnumerable().Where(item => item.Department.Name == department.Name).ToList().ForEach(item =>
				{
					costCenterList.Add(new SelectListItem
					{
						Value = item.Id.ToString(),
						Text = item.Name,
						Group = department
					});
				});
			}
			ViewBag.CostCenters = costCenterList;
			return View(budgetList);
		}

		[HttpPost]
		public async Task<IActionResult> Update(BudgetEntityVM budget)
		{
			await _service.UpdateBudget(budget);
			return RedirectToAction(nameof(Index));
		}

		//GET:Delete?year={year}&CostCenter={costCenter}
		[HttpGet]
		public async Task<IActionResult> Delete(int year, int costCenterId)
		{
			var budgetList = await _service.GetByYearAndCostCenterAsync(year, costCenterId);
			var budgetDropDowns = await _service.GetBudgetDropDownValuesAsync();
			var costCenterList = new List<SelectListItem>();
			var departments = new List<SelectListGroup>();

			if (budgetDropDowns.CostCenters is not null)
			{
				foreach (var dropDown in budgetDropDowns.CostCenters)
				{
					if (!departments.Any(item => item.Name == dropDown.Department.Name.ToString()))
					{
						departments.Add(
							new SelectListGroup { Name = dropDown.Department.Name.ToString() }
							);
					}
				}
			}

			foreach (var department in departments)
			{
				budgetDropDowns.CostCenters.AsEnumerable().Where(item => item.Department.Name == department.Name).ToList().ForEach(item =>
				{
					costCenterList.Add(new SelectListItem
					{
						Value = item.Id.ToString(),
						Text = item.Name,
						Group = department
					});
				});
			}
			ViewBag.CostCenters = costCenterList;
			return View(budgetList);
		}

		[HttpPost, ActionName("Delete")]

		public async Task<IActionResult> DeleteConfirm(int year, int costCenterId)
		{
			var budgetList = await _service.GetByYearAndCostCenterAsync(year, costCenterId);
			if (!budgetList.MonthBudgets.Any())
			{
				return View("CustomError", $"We couldn't locate Budget for {year} with Cost Center ID {costCenterId}");
			}
			await _service.DeleteBudgetAsync(year, costCenterId);
			return RedirectToAction(nameof(Index));
		}


		//GET:view?year={year}&CostCenter={costCenter}
		[HttpGet]
		public async Task<IActionResult> View(int year, int costCenterId)
		{

			var budgetList = await _service.GetByYearAndCostCenterAsync(year, costCenterId);

			if (!budgetList.MonthBudgets.Any())
			{
				return View("CustomError", $"We couldn't locate budget for Cost Center with ID: {costCenterId} and Year: {year}");
			}

			var budgetDropDowns = await _service.GetBudgetDropDownValuesAsync();
			var costCenterList = new List<SelectListItem>();
			var departments = new List<SelectListGroup>();

			if (budgetDropDowns.CostCenters is not null)
			{
				foreach (var dropDown in budgetDropDowns.CostCenters)
				{
					if (!departments.Any(item => item.Name == dropDown.Department.Name.ToString()))
					{
						departments.Add(
							new SelectListGroup { Name = dropDown.Department.Name.ToString() }
							);
					}
				}
			}

			foreach (var department in departments)
			{
				budgetDropDowns.CostCenters.AsEnumerable().Where(item => item.Department.Name == department.Name).ToList().ForEach(item =>
				{
					costCenterList.Add(new SelectListItem
					{
						Value = item.Id.ToString(),
						Text = item.Name,
						Group = department
					});
				});

			}

			ViewBag.CostCenters = costCenterList;
			return View(budgetList);
		}


	}
}
