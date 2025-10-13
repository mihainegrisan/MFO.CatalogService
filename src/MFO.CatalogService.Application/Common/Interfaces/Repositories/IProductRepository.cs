using MFO.CatalogService.Domain.Entities;

namespace MFO.CatalogService.Application.Common.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Product>> GetAllProductsAsync(CancellationToken cancellationToken);

    Task<Product> AddProductAsync(Product product, CancellationToken cancellationToken);

    Task<Product> UpdateProductAsync(Product product, CancellationToken cancellationToken);

    Task<Product?> SetProductActiveStateAsync(Guid productId, bool isActive, CancellationToken cancellationToken);

    Task<bool> DeleteProductAsync(Guid productId, CancellationToken cancellationToken);

    Task<bool> ExistsBySkuAsync(string sku, CancellationToken cancellationToken);
}