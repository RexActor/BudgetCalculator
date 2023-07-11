using BudgetCalculator.Data.Services;
using BudgetCalculator.Data.ViewModels;
using BudgetCalculator.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetCalculator.Controllers
{
	public class DepartmentRoleController : Controller
	{
		readonly IDepartmentRoleService _service;
		public DepartmentRoleController(IDepartmentRoleService service)
		{
			_service = service;
		}

		public async Task<IActionResult> Index()
		{


			var allRoles = await _service.GetAllRoles();
			return View(allRoles);
		}


		//departmentRole/Create
		[HttpGet]
		public async Task<IActionResult> Create()
		{



			var costCenterDropDown = await _service.GetCostCenterDropDownValuesAsync();
			


			
			var costCenterList = new List<SelectListItem>();
			var departments = new List<SelectListGroup>();

			foreach (var dropDown in costCenterDropDown)
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
				costCenterDropDown.AsEnumerable().Where(item => item.Department.Name == department.Name).ToList().ForEach(item =>
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

			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Create(DepartmentRoleEntity departmentRole)
		{



			await _service.AddAsync(departmentRole);

			return RedirectToAction(nameof(Index));

		}


		// GET:departmentRole/Update/{id}
		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{

			var departmentRole = await _service.GetByIdAsync(id);
		if (departmentRole is null) { return View("CustomError", $"Couldn't find role with ID {id}"); }

			var costCenterDropDown = await _service.GetCostCenterDropDownValuesAsync();




			var costCenterList = new List<SelectListItem>();
			var departments = new List<SelectListGroup>();

			foreach (var dropDown in costCenterDropDown)
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
				costCenterDropDown.AsEnumerable().Where(item => item.Department.Name == department.Name).ToList().ForEach(item =>
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

			return View(departmentRole);
		}


		[HttpPost]
		public async Task<IActionResult> Update(DepartmentRoleEntity departmentRole)
		{


			await _service.UpdateAsync(departmentRole);

			return RedirectToAction(nameof(Index));

		}


		// GET:departmentRole/Delete/{id}
		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{

			var departmentRole = await _service.GetByIdAsync(id);
			if (departmentRole is null) { return View("CustomError", $"Couldn't find role with ID {id}"); }

			var costCenterDropDown = await _service.GetCostCenterDropDownValuesAsync();




			var costCenterList = new List<SelectListItem>();
			var departments = new List<SelectListGroup>();

			foreach (var dropDown in costCenterDropDown)
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
				costCenterDropDown.AsEnumerable().Where(item => item.Department.Name == department.Name).ToList().ForEach(item =>
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

			return View(departmentRole);
		}


		[HttpPost,ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirm(DepartmentRoleEntity departmentRole)
		{


			await _service.DeleteAsync(departmentRole.Id);

			return RedirectToAction(nameof(Index));

		}


	}
}
