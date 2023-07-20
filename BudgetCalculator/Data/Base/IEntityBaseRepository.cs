namespace BudgetCalculator.Data.Base;

public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
{

	Task<IEnumerable<T>> GetAllAsync();
	Task<T> GetByIdAsync(int id);

	Task AddAsync(T entity);
	Task<T> UpdateAsync(T entity);
	Task DeleteAsync(int id);


}