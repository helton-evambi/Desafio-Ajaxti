namespace BookCatalog.Domain.Abstractions;

public interface IRepository<T, TId>
    where T : IEntity<TId>
{
    Task<IEnumerable<PagedResult<T>>> GetAllAsync(PagedParameters parameters, CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(TId id, CancellationToken cancellationToken = default);
}