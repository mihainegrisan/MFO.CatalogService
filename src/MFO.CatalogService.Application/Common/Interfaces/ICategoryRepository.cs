using MFO.CatalogService.Domain.Entities;

namespace MFO.CatalogService.Application.Common.Interfaces;

public interface ICategoryRepository
{
    Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<List<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken);

    Task<Category> AddCategoryAsync(Category category, CancellationToken cancellationToken);

    Task<Category> UpdateCategoryAsync(Category category, CancellationToken cancellationToken);

    Task<bool> DeleteCategoryAsync(Category category, CancellationToken cancellationToken);
}