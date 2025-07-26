using BookCatalog.Domain.Abstractions;
using BookCatalog.Domain.Entities;

namespace BookCatalog.Domain.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    Task<IEnumerable<Book>> GetWithRelatedEntitiesAsync (CancellationToken cancellationToken = default);
    Task<Book?> GetByIdWithRelatedEntitiesAsync (Guid id, CancellationToken cancellationToken = default);
}
