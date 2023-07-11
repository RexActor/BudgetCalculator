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

			var weeklyBudgets = await _service.GetWeeklyBudgetAsync(year: year, costCenterId: CostCenterId);

			if (monthIndex == 0)
			{
				ViewBag.PreviousMonthIndex = monthIndex;
			}
			else
			{
				ViewBag.PreviousMonthIndex = monthIndex - 1;

			}
			if (monthIndex == 12)
			{
				ViewBag.NextMonthIndex = monthIndex;
			}
			else
			{
				ViewBag.NextMonthIndex = monthIndex + 1;
			}

			IEnumerable<DepartmentRoleEntity> roles = await _service.GetDepartmentRolesAsync(costCenterId: CostCenterId);
			List<string> roleNames = new List<string>();
			if (!roles.Any())
			{
				roleNames.Add("DEFAULT ROLE");
			}

			roles.AsEnumerable().ToList().ForEach(role =>
			{
				roleNames.Add(role.Name);
			});


			ViewBag.RolesList = roleNames;
			ViewBag.CurrentMonth = monthName;
			ViewBag.CurrentMonthIndex = monthIndex;


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


			var budgetDb = await _service.GetByYearAndCostCenterAsync(year: budget.Year, costCenterId: budget.CostCenterId);

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

			var budgetList = await _service.GetByYearAndCostCenterAsync(year: year, costCenterId);
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
			var budgetList = await _service.GetByYearAndCostCenterAsync(year: year, costCenterId: costCenterId);
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
			var budgetList = await _service.GetByYearAndCostCenterAsync(year: year, costCenterId: costCenterId);
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

			var budgetList = await _service.GetByYearAndCostCenterAsync(year: year, costCenterId: costCenterId);

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



		//GET: EditWeek/{id}
		[HttpGet]

		public async Task<IActionResult> EditWeek(int id)
		{
			var weekBudget = await _service.GetWeeklyBudgetByIdAsync(weeklyBudgetId: id);
			if (weekBudget is null)
			{
				return View("CustomError", $"Couldn't find Weekly budget with ID {id}");
			}

			IEnumerable<DepartmentRoleEntity> roles = await _service.GetDepartmentRolesAsync(costCenterId: weekBudget.CostCenter.Id);
			List<string> roleNames = new List<string>();
			if (!roles.Any())
			{
				roleNames.Add("DEFAULT ROLE");
			}

			roles.AsEnumerable().ToList().ForEach(role =>
			{
				roleNames.Add(role.Name);
			});


			ViewBag.RolesList = roleNames;

			return View(weekBudget);
		}

	}
}
