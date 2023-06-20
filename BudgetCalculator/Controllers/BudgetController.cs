using Microsoft.AspNetCore.Mvc;

namespace BudgetCalculator.Controllers
{
	public class BudgetController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Edit()
		{
			return View();
		}

	}
}
