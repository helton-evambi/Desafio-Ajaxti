using BookCatalog.Application.DTOs.Genre;
using BookCatalog.Application.Interfaces;
using BookCatalog.Application.ViewModel;
using BookCatalog.Domain.Abstractions;
using BookCatalog.Domain.Entities;
using BookCatalog.Shared.Exceptions;
using FluentValidation;
using Mapster;

namespace BookCatalog.Application.Services;

public class GenreService(
    IRepository<Genre, Guid> repository,
    IValidator<CreateGenreDto> createValidator,
    IValidator<UpdateGenreDto> updateValidator) : IGenreSerivce
{
    public async Task<PagedResult<GenreViewModel>> GetAllAsync(PagedParameters parameters)
    {
        var genres = await repository.GetAllAsync(parameters);
        return genres.Adapt<PagedResult<GenreViewModel>>();
    }

    public async Task<GenreViewModel?> GetByIdAsync(Guid id)
    {
        var genre = await repository.GetByIdAsync(id);
        if (genre is null)
            throw new NotFoundException($"Gênero com ID {id} não encontrado.");
        return genre.Adapt<GenreViewModel>();
    }

    public async Task<GenreViewModel> AddAsync(CreateGenreDto createDto, CancellationToken cancellationToken = default)
    {
        var validationResult = await createValidator.ValidateAsync(createDto, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var genre = createDto.Adapt<Genre>();
        await repository.AddAsync(genre, cancellationToken);
        return genre.Adapt<GenreViewModel>();
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateGenreDto updateDto, CancellationToken cancellationToken = default)
    {
        var validationResult = await updateValidator.ValidateAsync(updateDto, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var genre = await repository.SingleOrDefaultAsync(g => g.Id == id);
        if (genre is null)
            throw new NotFoundException($"Gênero com ID {id} não encontrado.");

        var updatedGenre = updateDto.Adapt(genre);
        var result = await repository.UpdateAsync(updatedGenre, cancellationToken);
        return result;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var genre = await repository.DeleteAsync(id, cancellationToken);
        return true;
    }
}