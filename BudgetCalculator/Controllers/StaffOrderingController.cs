using BudgetCalculator.Data.Services;

using Microsoft.AspNetCore.Mvc;

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
	}
}
