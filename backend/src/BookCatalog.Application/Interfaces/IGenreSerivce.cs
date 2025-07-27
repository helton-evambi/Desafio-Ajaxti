using BookCatalog.Application.DTOs.Genre;
using BookCatalog.Application.ViewModel;

namespace BookCatalog.Application.Interfaces;

public interface IGenreSerivce : IBaseCrudService<CreateGenreDto, UpdateGenreDto, GenreViewModel, Guid>
{
}
