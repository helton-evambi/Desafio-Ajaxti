using BookCatalog.Domain.Abstractions;
using BookCatalog.Domain.Entities;

namespace BookCatalog.Domain.Interfaces;

public interface IAuthorRepository : IRepository<Author, Guid>
{
    Task<IEnumerable<PagedResult<Author>>> GetAllWithBooksAsync(PagedParameters parameters, CancellationToken cancellationToken = default);
    Task<Author?> GetByIdWithBooksAsync(Guid id, CancellationToken cancellationToken = default);
}
