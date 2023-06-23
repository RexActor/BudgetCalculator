using BudgetCalculator.Data.Services;
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

        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {

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
                        Group =department
                    });
                });

          
            }


            ViewBag.CostCenters = costCenterList;


            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Departments()
        {
            return View();
        }


    }
}
