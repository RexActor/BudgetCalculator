using BudgetCalculator.Data.Services;
using BudgetCalculator.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.VisualBasic;

using SQLitePCL;

namespace BudgetCalculator.Controllers
{
	public class CostCenterController : Controller
	{

		private readonly ICostCenterService _service;
		private readonly TelegramService _telegramService;

		public CostCenterController(ICostCenterService service, TelegramService telegramService)
		{
			//, TelegramService telegramService
			_service = service;
			_telegramService = telegramService;
			//_telegramService = telegramService;
		}


		public async Task<IActionResult> Index()
		{
			var allCostCenters = await _service.GetAllCostCentersAsync();




			return View(allCostCenters);
		}


		/// <summary>
		/// GET View for Cost Center Creation
		/// </summary>
		/// <returns></returns>


		[HttpGet]

		public async Task<IActionResult> Create()
		{
			var departmentDropDowns = await _service.GetCostCenterDropDownValuesAsync();


			ViewBag.Departments = new SelectList(departmentDropDowns.Departments, "Id", "Name");
			return View();

		}


		[HttpPost]
		public async Task<IActionResult> Create(CostCenterEntityVM entity)
		{

			if (!ModelState.IsValid)
			{

				var departmentDropDowns = await _service.GetCostCenterDropDownValuesAsync();


				ViewBag.Departments = new SelectList(departmentDropDowns.Departments, "Id", "Name");
				return View(entity);
			}


			await _telegramService.sendMessage(1, $"Cost Center {entity.Name} was created!\nDescription: {entity.Description}\nCreated On: {DateTime.Now.Date.ToShortDateString()}\nAt: {DateTime.Now.ToShortTimeString()}\nBy: {entity.CreatedBy} ");

			await _service.AddNewCostCenterAsync(entity);


			return RedirectToAction(nameof(Index));
		}

		[HttpGet]

		public async Task<IActionResult> Update(int id)
		{
			var costService = await _service.GetCostCenterByIdAsync(id);


			if (costService == null) { return View("NotFound"); }

			//TODO:USERNAME IS HARDCODED in View Use Dynamic

			var costCenter = new CostCenterEntityVM()
			{
				CreatedAt = costService.CreatedAt,
				DepartmentId = costService.Department.Id,
				Description = costService.Description,
				Name = costService.Name,
				CreatedBy = costService.CreatedBy,
				LastUpdatedAt = costService.LastUpdatedAt,
				LastUpdatedBy = costService.LastUpdatedBy,
				Id = id

			};
			var departmentDropDowns = await _service.GetCostCenterDropDownValuesAsync();


			ViewBag.Departments = new SelectList(departmentDropDowns.Departments, "Id", "Name");

			return View(costCenter);

		}


		[HttpPost]
		public async Task<IActionResult> Update(int id, CostCenterEntityVM entity)
		{



			if (!ModelState.IsValid)
			{
				var departmentDropDowns = await _service.GetCostCenterDropDownValuesAsync();


				ViewBag.Departments = new SelectList(departmentDropDowns.Departments, "Id", "Name");
				return View(entity);
			}

			if (id != entity.Id) { return View("NotFound"); }



			await _service.UpdateCostCenterAsync(entity);


			return RedirectToAction(nameof(Index));
		}


		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{

			var costCenter = await _service.GetCostCenterByIdAsync(id);


			var costCenterVM = new CostCenterEntityVM()
			{
				DepartmentId = costCenter.DepartmentId,
				Description = costCenter.Description,
				CreatedAt = costCenter.CreatedAt,
				CreatedBy = costCenter.CreatedBy,
				Id = costCenter.Id,
				LastUpdatedAt = costCenter.LastUpdatedAt,
				LastUpdatedBy = costCenter.LastUpdatedBy,
				Name = costCenter.Name,
			};
			var departmentDropDowns = await _service.GetCostCenterDropDownValuesAsync();


			ViewBag.Departments = new SelectList(departmentDropDowns.Departments, "Id", "Name");


			return View(costCenterVM);
		}


		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> Confirm(int id)
		{

			var costCenter = await _service.GetCostCenterByIdAsync(id);

			if (costCenter == null) { return View("NotFound"); }
			await _service.DeleteAsync(id);
			return RedirectToAction(nameof(Index));

		}

	}
}
