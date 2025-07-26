using BookCatalog.Application.DTOs.Book;
using BookCatalog.Application.ViewModel;
using BookCatalog.Domain.Abstractions;
namespace BookCatalog.Application.Interfaces;

public interface IBookService : IService<CreateBookDto, UpdateBookDto, BookViewModel, Guid>
{
    Task<IEnumerable<PagedResult<BookViewModel>>> GetAllWithDetailsAsync(PagedParameters parameters, CancellationToken cancellationToken = default);
    Task<BookViewModel?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<BookViewModel>> GetByAuthorIdWithDetailsAsync(Guid authorId, PagedParameters parameters, CancellationToken cancellationToken = default);
    Task<IEnumerable<BookViewModel>> GetByGenreIdWithDetailsAsync(Guid genreId, PagedParameters parameters, CancellationToken cancellationToken = default);
}
