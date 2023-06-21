using Microsoft.AspNetCore.Mvc;

namespace BudgetCalculator.Controllers
{
	public class CostCenter : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
