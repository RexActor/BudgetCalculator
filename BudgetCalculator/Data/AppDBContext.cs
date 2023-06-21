﻿using BudgetCalculator.Controllers;
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

	}
}
