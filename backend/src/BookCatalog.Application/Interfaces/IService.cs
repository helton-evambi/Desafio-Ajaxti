using BookCatalog.Domain.Abstractions;

namespace BookCatalog.Application.Interfaces;

public interface IService<TCreateDto, TUpdateDto, TViewModel, TId>
{
    Task<IEnumerable<PagedResult<TViewModel>>> GetAllAsync(PagedParameters parameters, CancellationToken cancellationToken = default);
    Task<TViewModel?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task<TViewModel> AddAsync(TCreateDto createDto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(TId id, TUpdateDto updateDto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(TId id, CancellationToken cancellationToken = default);
}