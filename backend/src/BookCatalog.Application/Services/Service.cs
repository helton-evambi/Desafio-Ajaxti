using BookCatalog.Application.Interfaces;
using BookCatalog.Domain.Abstractions;
using BookCatalog.Shared.Exceptions;
using Mapster;

namespace BookCatalog.Application.Services;

public class Service<TEntity, TCreateDto, TUpdateDto, TViewModel, TId>
    (IRepository<TEntity, TId> repository)
    : IService<TCreateDto, TUpdateDto, TViewModel, TId>
    where TEntity : IEntity<TId>
    where TCreateDto : class
    where TUpdateDto : class
    where TViewModel : class
    where TId : struct
{
    public async Task<IEnumerable<PagedResult<TViewModel>>> GetAllAsync(PagedParameters parameters, CancellationToken cancellationToken = default)
    {
        var pagedResult = await repository.GetAllAsync(parameters, cancellationToken);
        var viewModels = pagedResult.Adapt<IEnumerable<PagedResult<TViewModel>>>();
        return viewModels;
    }

    public async Task<TViewModel?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
            throw new NotFoundException($"Entidade com {id} não encontrada.");

        return entity.Adapt<TViewModel>();
    }

    public async Task<TViewModel> AddAsync(TCreateDto createDto, CancellationToken cancellationToken = default)
    {
        var entity = createDto.Adapt<TEntity>();
        var addedEntity = await repository.AddAsync(entity, cancellationToken);
        return addedEntity.Adapt<TViewModel>();
    }

    public async Task<bool> UpdateAsync(TId id, TUpdateDto updateDto, CancellationToken cancellationToken = default)
    {
        var entity = updateDto.Adapt<TEntity>();
        entity.Id = id;
        return await repository.UpdateAsync(entity, cancellationToken);
    }

    public Task<bool> DeleteAsync(TId id, CancellationToken cancellationToken = default)
    {
        return repository.DeleteAsync(id, cancellationToken);
    }

}
