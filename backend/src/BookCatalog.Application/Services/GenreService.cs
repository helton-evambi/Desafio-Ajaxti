using BookCatalog.Application.DTOs.Genre;
using BookCatalog.Application.ViewModel;
using BookCatalog.Domain.Abstractions;
using BookCatalog.Domain.Entities;
using BookCatalog.Domain.Interfaces;

namespace BookCatalog.Application.Services;

public class GenreService
    (IRepository<Genre, Guid> repository)
    : Service<Genre, CreateGenreDto, UpdateGenreDto, GenreViewModel, Guid>(repository)
{
}
