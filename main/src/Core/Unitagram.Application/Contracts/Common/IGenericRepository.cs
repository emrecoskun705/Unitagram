namespace Unitagram.Application.Contracts.Common;

public interface IGenericRepository<T> where T : class
{
    Task<IReadOnlyList<T>> GetAsync();
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}