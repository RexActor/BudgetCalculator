using BudgetCalculator.Data.Base;
using BudgetCalculator.Data.ViewModels;
using BudgetCalculator.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetCalculator.Data.Services
{
	public class CostCenterService : EntityBaseRepository<CostCenterEntity>, ICostCenterService
	{
		private readonly AppDBContext _context;

		public CostCenterService(AppDBContext context) : base(context)
		{
			_context = context;
		}

		public async Task AddNewCostCenterAsync(CostCenterEntityVM newCostCenter)
		{
			var createCostCenter = new CostCenterEntity()
			{
				CreatedAt = DateTime.Now,
				CreatedBy = newCostCenter.CreatedBy,
				Description = newCostCenter.Description,
				DepartmentId = newCostCenter.DepartmentId,
				Name = newCostCenter.Name,


			};
			await _context.CostCenters.AddAsync(createCostCenter);

			await _context.SaveChangesAsync();

		}

		public async Task<List<CostCenterEntity>> GetAllCostCentersAsync()
		{
			return await _context.CostCenters.Include(item => item.Department).ToListAsync();
		}

		public async Task<CostCenterEntity> GetCostCenterByIdAsync(int id)
		{
			var result = await _context.CostCenters.Include(item => item.Department).FirstOrDefaultAsync(item => item.Id == id);


			return result is not null ? result : default!;



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
			var dbcostCenter = _context.CostCenters.FirstOrDefault(item => item.Id == newCostCenter.Id);


			if (dbcostCenter is not null)
			{

				dbcostCenter.DepartmentId = newCostCenter.DepartmentId;
				dbcostCenter.Description = newCostCenter.Description;
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
