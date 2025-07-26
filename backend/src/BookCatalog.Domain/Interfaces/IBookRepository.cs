using BookCatalog.Domain.Abstractions;
using BookCatalog.Domain.Entities;

namespace BookCatalog.Domain.Interfaces;

public interface IBookRepository : IRepository<Book, Guid>
{
}
