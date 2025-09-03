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

    public async Task<Brand?> GetBrandByIdAsync(int id, CancellationToken cancellationToken)
        => await _db.Brands.FindAsync([id], cancellationToken);

    public async Task<List<Brand>> GetAllBrandsAsync(CancellationToken cancellationToken)
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

    public async Task<bool> DeleteBrandAsync(Brand brand, CancellationToken cancellationToken)
    {
        _db.Brands.Remove(brand);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}