using BudgetCalculator.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;
using System.Xml.Linq;

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

		public IActionResult Create()
		{



			List<CostCenters> costCenters = new List<CostCenters>()
			{
				new CostCenters{
					Id=1,Name="12-34-106",Description="Description"
				},
				new CostCenters{
					Id=2,Name="12-34-110",Description="Description"
				},
				new CostCenters{
					Id=13,Name="12-34-100",Description="Description"
				},
				new CostCenters{
					Id=4,Name="12-34-200",Description="Description"
				},
			};


			@ViewBag.Example = new SelectList(costCenters,"Id","Name");


			return View();
		}


	}
}
