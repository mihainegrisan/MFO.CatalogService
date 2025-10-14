using MFO.CatalogService.Application.Common.Interfaces.Repositories;
using MFO.CatalogService.Domain.Entities;
using MFO.CatalogService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MFO.CatalogService.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly CatalogDbContext _db;

    public CategoryRepository(CatalogDbContext db)
    {
        _db = db;
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _db.Categories.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken)
        => await _db.Categories.ToListAsync(cancellationToken);

    public async Task<Category> AddCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        await _db.Categories.AddAsync(category, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        return category;
    }

    public async Task<Category> UpdateCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        _db.Categories.Update(category);
        await _db.SaveChangesAsync(cancellationToken);
        return category;
    }

    public async Task<bool> DeleteCategoryAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        var category = await _db.Categories.FindAsync([categoryId], cancellationToken);
        if (category is null)
        {
            return false;
        }

        _db.Categories.Remove(category);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
        => await _db.Categories.AnyAsync(category => category.Name == name, cancellationToken);
}