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

		public IActionResult Weekly()
		{
			return View();
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

			if (budgetDb is not null) {
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
