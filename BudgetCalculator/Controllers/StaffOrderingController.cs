using BudgetCalculator.Data.Services;
using BudgetCalculator.Data.ViewModels;
using BudgetCalculator.Models;

using Microsoft.AspNetCore.Mvc;

using System.Globalization;

namespace BudgetCalculator.Controllers
{
	public class StaffOrderingController : Controller
	{

		readonly IStaffOrderingService _service;
		public StaffOrderingController(IStaffOrderingService service)
		{
			_service = service;
		}

		public IActionResult Index()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> OrderStaff(OrderStaffEntity orderStaffEntity)
		{


			int monthNumber = orderStaffEntity.OrderDate.Month;
			string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthNumber);

			var budgets = await _service.GetAllBudgetsByYearMonth(orderStaffEntity.OrderDate.Year, monthName);


			OrderStaffVM orderStaffVM = new OrderStaffVM();
			orderStaffVM.BudgetEntity = new List<BudgetEntity>();
			orderStaffVM.Roles = new List<DepartmentRoleEntity>();

			budgets.ToList().ForEach(async budget =>
			{

				var roles = await _service.GetRolesForCostCenter(budget.CostCenter.Id);
				orderStaffVM.BudgetEntity.Add(budget);
				orderStaffVM.Roles.AddRange(roles);



			});


			orderStaffVM!.OrderDate = orderStaffEntity.OrderDate;
			orderStaffVM!.ProducingQuantiy = orderStaffEntity.ProducingQuantiy;
			orderStaffVM!.OutboundQuantity = orderStaffEntity.OutboundQuantity;

			return View(orderStaffVM);
		}

	}
}
