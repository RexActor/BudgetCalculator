using BudgetCalculator.Models;

namespace BudgetCalculator.Data.ViewModels;

public class BudgetDropDownVM
{

	public BudgetDropDownVM()
	{
		CostCenters = new List<CostCenterEntity>();
	}

	public List<CostCenterEntity> CostCenters { get; set; }
}