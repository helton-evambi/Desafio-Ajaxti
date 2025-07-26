using BookCatalog.Application.DTOs.Book;
using BookCatalog.Application.Interfaces;
using BookCatalog.Application.ViewModel;
using BookCatalog.Domain.Abstractions;
using BookCatalog.Domain.Entities;
using BookCatalog.Domain.Interfaces;
using BookCatalog.Shared.Exceptions;
using Mapster;

namespace BookCatalog.Application.Services;

public class BookService
    (IRepository<Book, Guid> repository, IBookRepository bookRepository)
    : Service<Book, CreateBookDto, UpdateBookDto, BookViewModel, Guid>(repository)
    , IBookService
{
    public async Task<IEnumerable<PagedResult<BookViewModel>>> GetAllWithDetailsAsync(PagedParameters parameters, CancellationToken cancellationToken = default)
    {
        var pagedBooks = await bookRepository.GetAllWithDetailsAsync(parameters, cancellationToken);

       
        var pagedViewModels = pagedBooks.Select(pagedBook => new PagedResult<BookViewModel>
        {
            Items = pagedBook.Items.Select(book => book.Adapt<BookViewModel>() with
            {
                AuthorName = $"{book.Author?.FirstName} {book.Author?.LastName}".Trim(),
                GenreName = book.Genre?.Name ?? string.Empty
            }),
            Page = pagedBook.Page,
            PageSize = pagedBook.PageSize,
            TotalCount = pagedBook.TotalCount
        });

        return pagedViewModels;
    }
    public async Task<BookViewModel?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var book = await bookRepository.GetByIdWithDetailsAsync(id, cancellationToken);

        if (book is null)
            throw new NotFoundException($"Entidade com {id} não encontrada.");

        var result = book.Adapt<BookViewModel>() with
        {
            AuthorName = $"{book.Author?.FirstName} {book.Author?.LastName}".Trim(),
            GenreName = book.Genre?.Name ?? string.Empty
        };

        return result;
    }

    public async Task<IEnumerable<BookViewModel>> GetByAuthorIdWithDetailsAsync(Guid authorId, PagedParameters parameters, CancellationToken cancellationToken = default)
    {
        var books = await bookRepository.GetByAuthorIdWithDetailsAsync(authorId, parameters, cancellationToken);
        if (books is null)
            throw new NotFoundException($"Nenhum livro encontrado para o autor com ID {authorId}.");

        var result = books.Select(book => book.Adapt<BookViewModel>() with
        {
            AuthorName = $"{book.Author?.FirstName} {book.Author?.LastName}".Trim(),
            GenreName = book.Genre?.Name ?? string.Empty
        });

        return result;
    }

    public async Task<IEnumerable<BookViewModel>> GetByGenreIdWithDetailsAsync(Guid genreId, PagedParameters parameters, CancellationToken cancellationToken = default)
    {
        var books = await bookRepository.GetByGenreIdWithDetailsAsync(genreId, parameters, cancellationToken);
        if (books is null)
            throw new NotFoundException($"Nenhum livro encontrado para o gênero com ID {genreId}.");

        var result = books.Select(book => book.Adapt<BookViewModel>() with
        {
            AuthorName = $"{book.Author?.FirstName} {book.Author?.LastName}".Trim(),
            GenreName = book.Genre?.Name ?? string.Empty
        });

        return result;
    }

}
