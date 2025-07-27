using BookCatalog.Application.DTOs.Book;
using BookCatalog.Application.ViewModel;
using BookCatalog.Domain.Abstractions;

namespace BookCatalog.Application.Interfaces;

public interface IBookService : IBaseCrudService<CreateBookDto, UpdateBookDto, BookViewModel, Guid>
{
    Task<IEnumerable<PagedResult<BookViewModel>>> GetByAuthorIdAsync(Guid authorId, PagedParameters parameters);
    Task<IEnumerable<PagedResult<BookViewModel>>> GetByGenreIdAsync(Guid genreId, PagedParameters parameters);
}
