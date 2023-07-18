using BudgetCalculator.Data.Base;
using BudgetCalculator.Models;

namespace BudgetCalculator.Data.Services
{
	public class StaffOrderingService:EntityBaseRepository<BudgetEntity>, IStaffOrderingService
	{
		private readonly AppDBContext _context;
		public StaffOrderingService(AppDBContext context) : base(context)
		{
			_context = context;
		}
	}
}
