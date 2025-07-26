using BookCatalog.Domain.Abstractions;
using BookCatalog.Domain.Entities;

namespace BookCatalog.Domain.Interfaces;

public interface IBookRepository : IRepository<Book, Guid>
{
    Task<IEnumerable<PagedResult<Book>>> GetAllWithDetailsAsync(PagedParameters parameters, CancellationToken cancellationToken = default);
    Task<Book?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetByAuthorIdWithDetailsAsync(Guid authorId, PagedParameters parameters, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetByGenreIdWithDetailsAsync(Guid genreId, PagedParameters parameters, CancellationToken cancellationToken = default);
}
