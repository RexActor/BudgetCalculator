using BudgetCalculator.Data.Services;
using BudgetCalculator.Models;

using Microsoft.AspNetCore.Mvc;

namespace BudgetCalculator.Controllers
{
	public class DepartmentsController : Controller
	{

		private readonly IDepartmentService _service;
		public DepartmentsController(IDepartmentService service)
		{
			_service = service;

		}

		public async Task<IActionResult> Index()
		{
			var departments = await _service.GetAllAsync();

			return View(departments);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(DepartmentEntity entity)
		{

			if (!ModelState.IsValid) { return View(entity); }


			await _service.AddAsync(entity);

			return RedirectToAction(nameof(Index));
		}


		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
			var department = await _service.GetByIdAsync(id);

			if (department == null) { return View("NotFound"); }
			return View(department);
		}


		[HttpPost]
		public async Task<IActionResult> Update(int id, DepartmentEntity entity)
		{

			if (id == entity.Id)
			{
				await _service.UpdateAsync(entity);
				return RedirectToAction(nameof(Index));
			}

			return View("NotFound");
			
		}

		[HttpGet]
		public async Task<IActionResult>Delete(int id)
		{
			var department = await _service.GetByIdAsync(id);

			if (department == null) { return View("NotFound"); }


			return View(department);
		}


		[HttpPost,ActionName("Delete")]
		public async Task<IActionResult>Confirm(int id)
		{
			var department = await _service.GetByIdAsync(id);

			if (department == null) { return View("NotFound"); }

			await _service.DeleteAsync(id);
			return RedirectToAction(nameof(Index));

		}



	}
}
