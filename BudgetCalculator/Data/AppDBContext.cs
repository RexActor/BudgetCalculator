using Microsoft.EntityFrameworkCore;

namespace BudgetCalculator.Data
{
	public class AppDBContext : DbContext
	{

		public AppDBContext(DbContextOptions options) : base(options)
		{

		}
	}
}
