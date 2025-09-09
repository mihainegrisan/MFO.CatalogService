using MFO.CatalogService.Domain.Entities;

namespace MFO.CatalogService.Application.Common.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken);

    Task<Product> AddProductAsync(Product product, CancellationToken cancellationToken);

    Task<Product> UpdateProductAsync(Product product, CancellationToken cancellationToken);

    Task<Product> SetProductActiveStateAsync(Product product, bool isActive, CancellationToken cancellationToken);

    Task<bool> DeleteProductAsync(Product product, CancellationToken cancellationToken);
}