using BudgetCalculator.Data.Base;
using BudgetCalculator.Models;

namespace BudgetCalculator.Data.Services;

public class DepartmentService : EntityBaseRepository<DepartmentEntity>, IDepartmentService
{
	private readonly AppDBContext _context;
	public DepartmentService(AppDBContext context) : base(context)
	{
		_context = context;
	}
}