using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Domain.Entities;
using MFO.CatalogService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MFO.CatalogService.Infrastructure.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly CatalogDbContext _db;

    public BrandRepository(CatalogDbContext db)
    {
        _db = db;
    }

    public async Task<Brand?> GetBrandByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _db.Brands.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<Brand>> GetAllBrandsAsync(CancellationToken cancellationToken)
        => await _db.Brands.ToListAsync(cancellationToken);

    public async Task<Brand> AddBrandAsync(Brand brand, CancellationToken cancellationToken)
    {
        await _db.Brands.AddAsync(brand, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        return brand;
    }

    public async Task<Brand> UpdateBrandAsync(Brand brand, CancellationToken cancellationToken)
    {
        _db.Brands.Update(brand);
        await _db.SaveChangesAsync(cancellationToken);
        return brand;
    }

    public async Task<bool> DeleteBrandAsync(Guid brandId, CancellationToken cancellationToken)
    {
        var brand = await _db.Brands.FindAsync([brandId], cancellationToken);
        if (brand is null)
        {
            return false;
        }

        _db.Brands.Remove(brand);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
        => await _db.Brands.AnyAsync(brand => brand.Name == name, cancellationToken);
}