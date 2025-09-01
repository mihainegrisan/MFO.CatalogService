using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Domain.Entities;
using MFO.CatalogService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MFO.CatalogService.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly CatalogDbContext _db;

    public ProductRepository(CatalogDbContext db)
    {
        _db = db;
    }

    public async Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        => await _db.Products
            .FindAsync([id], cancellationToken);

    public async Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
        => await _db.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<Product> AddProductAsync(Product product, CancellationToken cancellationToken)
    {
        await _db.Products.AddAsync(product, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<Product> UpdateProductAsync(Product product, CancellationToken cancellationToken)
    {
        _db.Products.Update(product);
        await _db.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<Product> SetProductActiveStateAsync(Product product, bool isActive, CancellationToken cancellationToken)
    {
        product.IsActive = isActive;
        _db.Products.Update(product);
        await _db.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<bool> DeleteProductAsync(Product product, CancellationToken cancellationToken)
    {
        _db.Products.Remove(product);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}