using BudgetCalculator.Models;

namespace BudgetCalculator.Data.ViewModels
{
    public class CostCenterDropDownVM
    {

        public CostCenterDropDownVM()
        {
            Departments = new List<DepartmentEntity>();
        }

        public List<DepartmentEntity>  Departments { get; set; }

    }
}
