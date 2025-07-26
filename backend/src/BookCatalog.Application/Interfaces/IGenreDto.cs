using BookCatalog.Application.DTOs.Genre;
using BookCatalog.Application.ViewModel;

namespace BookCatalog.Application.Interfaces;

public interface IGenreDto : IService<CreateGenreDto, UpdateGenreDto, GenreViewModel, Guid>
{
}
