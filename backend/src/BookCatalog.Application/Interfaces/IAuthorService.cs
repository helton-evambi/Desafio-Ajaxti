using BookCatalog.Application.DTOs.Author;
using BookCatalog.Application.ViewModel;
using BookCatalog.Domain.Abstractions;

namespace BookCatalog.Application.Interfaces;

public interface IAuthorService : IBaseCrudService<CreateAuthorDto, UpdateAuthorDto, AuthorViewModel, Guid>
{
    Task<IEnumerable<PagedResult<AuthorViewModel>>> GetAllWithBooksAsync(PagedParameters parameters, CancellationToken cancellationToken = default);
    Task<AuthorViewModel?> GetByIdWithBooksAsync(Guid id, CancellationToken cancellationToken = default);
}
