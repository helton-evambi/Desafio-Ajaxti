using BookCatalog.Application.DTOs.Author;
using BookCatalog.Application.Interfaces;
using BookCatalog.Application.ViewModel;
using BookCatalog.Domain.Abstractions;
using BookCatalog.Domain.Entities;
using BookCatalog.Shared.Exceptions;
using Mapster;

namespace BookCatalog.Application.Services;

public class AuthorService(IRepository<Author, Guid> repository) : IAuthorService
{
    public async Task<IEnumerable<PagedResult<AuthorViewModel>>> GetAllAsync(PagedParameters parameters)
    {
        var authors = await repository.GetAllAsync(parameters);
        return authors.Adapt<IEnumerable<PagedResult<AuthorViewModel>>>();
    }

    public async Task<AuthorViewModel?> GetByIdAsync(Guid id)
    {
        var author = await repository.GetByIdAsync(id);
        if (author is null)
            throw new NotFoundException($"Author com ID {id} não encontrado.");

        return author.Adapt<AuthorViewModel>();
    }
    public async Task<IEnumerable<PagedResult<AuthorViewModel>>> GetAllWithBooksAsync(PagedParameters parameters, CancellationToken cancellationToken = default)
    {
        var authors = await repository.GetAllAsync(parameters, a => a.Books);
        return authors.Adapt<IEnumerable<PagedResult<AuthorViewModel>>>();
    }
    public async Task<AuthorViewModel?> GetByIdWithBooksAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var author = await repository.GetByIdAsync(id, a => a.Books);
        if (author is null)
            throw new NotFoundException($"Author com ID {id} não encontrado.");

        return author.Adapt<AuthorViewModel>();
    }

    public async Task<AuthorViewModel> AddAsync(CreateAuthorDto createDto, CancellationToken cancellationToken = default)
    {
        var author = createDto.Adapt<Author>();
        await repository.AddAsync(author, cancellationToken);

        return author.Adapt<AuthorViewModel>();
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var author = await repository.DeleteAsync(id, cancellationToken);

        return true;
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateAuthorDto updateDto, CancellationToken cancellationToken = default)
    {
        var author = await repository.SingleOrDefaultAsync(a => a.Id == id);
        if (author is null)
            throw new NotFoundException($"Author com ID {id} não encontrado.");

        var updatedAuthor = updateDto.Adapt(author);
        var result = await repository.UpdateAsync(updatedAuthor, cancellationToken);
        return result;
    }
}
