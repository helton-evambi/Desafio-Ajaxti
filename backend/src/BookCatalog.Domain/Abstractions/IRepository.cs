using System.Linq.Expressions;

namespace BookCatalog.Domain.Abstractions;

public interface IRepository<T, TId>
    where T : IEntity<TId>
{
    Task<IEnumerable<PagedResult<T>>> GetAllAsync(PagedParameters parameters, params Expression<Func<T, object>>[] includes);
    Task<T?> GetByIdAsync(TId id, params Expression<Func<T, object>>[] includes);
    Task<IEnumerable<PagedResult<T>>> FindAsync(PagedParameters parameters, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(TId id, CancellationToken cancellationToken = default);
}