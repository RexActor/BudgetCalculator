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

    }
}
