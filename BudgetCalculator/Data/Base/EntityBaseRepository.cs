﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BudgetCalculator.Data.Base;

public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
{
	private readonly AppDBContext _context;

	public EntityBaseRepository(AppDBContext context)
	{

		_context = context;

	}


	public async Task AddAsync(T entity)
	{
		await _context.Set<T>().AddAsync(entity);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var entity = await _context.Set<T>().FirstOrDefaultAsync(item => item.Id == id);
		if (entity is not null)
		{
			_context.Set<T>().Remove(entity);
			await _context.SaveChangesAsync();
		}
	}

	public async Task<IEnumerable<T>> GetAllAsync()
	{
		return await _context.Set<T>().ToListAsync();

	}

	public async Task<T> GetByIdAsync(int id)
	{
		var result = await _context.Set<T>().FirstOrDefaultAsync(item => item.Id == id);


		return result is not null ? result : default!;
	}



	public async Task<T> UpdateAsync(T entity)
	{
		EntityEntry entityEntry = _context.Entry<T>(entity);
		entityEntry.State = EntityState.Modified;

		await _context.SaveChangesAsync();

		return entity;
	}
}