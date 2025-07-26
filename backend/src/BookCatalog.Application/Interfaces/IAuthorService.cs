using BookCatalog.Application.DTOs.Author;
using BookCatalog.Application.ViewModel;
using BookCatalog.Domain.Abstractions;
using BookCatalog.Domain.Entities;

namespace BookCatalog.Application.Interfaces;

public interface IAuthorService : IService<CreateAuthorDto, UpdateAuthorDto, AuthorViewModel, Guid>
{
    Task<IEnumerable<PagedResult<AuthorViewModel>>> GetAllWithBooksAsync(PagedParameters parameters, CancellationToken cancellationToken = default);
    Task<AuthorViewModel?> GetByIdWithBooksAsync(Guid id, CancellationToken cancellationToken = default);
}
