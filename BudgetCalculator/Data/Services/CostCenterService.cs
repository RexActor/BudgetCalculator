﻿using BudgetCalculator.Data.Base;
using BudgetCalculator.Data.ViewModels;
using BudgetCalculator.Models;

using Microsoft.EntityFrameworkCore;

namespace BudgetCalculator.Data.Services
{
	public class CostCenterService : EntityBaseRepository<CostCenterEntity>, ICostCenterService
	{
		private readonly AppDBContext _context;

		public CostCenterService(AppDBContext context) : base(context) {
			_context = context;
		}

		public async Task AddNewCostCenterAsync(CostCenterEntityVM newCostCenter)
		{
			var createCostCenter = new CostCenterEntity()
			{
				CreatedAt = DateTime.Now,
				CreatedBy=newCostCenter.CreatedBy,
				Description=newCostCenter.Description,
				DepartmentId=newCostCenter.DepartmentId,
				Name=newCostCenter.Name,


			};
			await _context.CostCenters.AddAsync(createCostCenter);

			await _context.SaveChangesAsync(); 

		}

		public async Task<CostCenterEntity> GetCostCenterByIdAsync(int id)
		{
			return await _context.CostCenters.Include(item => item.Department).FirstOrDefaultAsync(item=>item.Id==id);
		}

		public async Task<CostCenterDropDownVM> GetCostCenterDropDownValuesAsync()
		{
			var response = new CostCenterDropDownVM()
			{
				Departments = await _context.Departments.ToListAsync()
			};

			return response;
		}

		public async Task UpdateCostCenterAsync(CostCenterEntityVM newCostCenter)
		{
			var dbcostCenter = _context.CostCenters.FirstOrDefault(item=>item.Id == newCostCenter.Id);


			if(dbcostCenter != null)
			{

				//dbcostCenter.Department = newCostCenter.Department;
				dbcostCenter.Description= newCostCenter.Description;
				dbcostCenter.Name = newCostCenter.Name;
				dbcostCenter.CreatedBy = newCostCenter.CreatedBy;
				dbcostCenter.CreatedAt = newCostCenter.CreatedAt;
				dbcostCenter.LastUpdatedAt = DateTime.Now;
				dbcostCenter.LastUpdatedBy = newCostCenter.LastUpdatedBy;
			}

			await _context.SaveChangesAsync();
			
		}
	}
}
