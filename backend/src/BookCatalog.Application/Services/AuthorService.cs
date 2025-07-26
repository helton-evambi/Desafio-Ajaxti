using BookCatalog.Application.DTOs.Author;
using BookCatalog.Application.Interfaces;
using BookCatalog.Application.ViewModel;
using BookCatalog.Domain.Abstractions;
using BookCatalog.Domain.Entities;
using BookCatalog.Domain.Interfaces;
using BookCatalog.Shared.Exceptions;
using Mapster;

namespace BookCatalog.Application.Services;

public class AuthorService(IRepository<Author, Guid> repository, IAuthorRepository authorRepository)
    : Service<Author, CreateAuthorDto, UpdateAuthorDto, AuthorViewModel, Guid>(repository)
    , IAuthorService
{
    public async Task<IEnumerable<PagedResult<AuthorViewModel>>> GetAllWithBooksAsync(PagedParameters parameters, CancellationToken cancellationToken = default)
    {
        var pagedResult = await authorRepository.GetAllWithBooksAsync(parameters, cancellationToken);
        return pagedResult.Adapt<IEnumerable<PagedResult<AuthorViewModel>>>();
    }

    public async Task<AuthorViewModel?> GetByIdWithBooksAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var author = await authorRepository.GetByIdWithBooksAsync(id, cancellationToken);
        if (author is null)
            throw new NotFoundException($"Entidade com id {id} não foi encontrada");

        return author.Adapt<AuthorViewModel>();
    }
}
