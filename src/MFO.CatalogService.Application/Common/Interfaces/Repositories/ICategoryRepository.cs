using MFO.CatalogService.Domain.Entities;

namespace MFO.CatalogService.Application.Common.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken);

    Task<Category> AddCategoryAsync(Category category, CancellationToken cancellationToken);

    Task<Category> UpdateCategoryAsync(Category category, CancellationToken cancellationToken);

    Task<bool> DeleteCategoryAsync(Guid categoryId, CancellationToken cancellationToken);

    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
}