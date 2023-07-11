using BudgetCalculator.Controllers;
using BudgetCalculator.Models;

using Microsoft.EntityFrameworkCore;

namespace BudgetCalculator.Data
{
	public class AppDBContext : DbContext
	{

		public AppDBContext(DbContextOptions options) : base(options)
		{

		}



		public DbSet<CostCenterEntity> CostCenters { get; set; }
		public DbSet<DepartmentEntity> Departments { get; set; }
		public DbSet<BudgetEntity> Budgets { get; set; }
		public DbSet<WeeklyBudget>WeeklyBudgets { get; set; }
		public DbSet<DepartmentRoleEntity> DepartmentRoles { get; set; }
	}
}
