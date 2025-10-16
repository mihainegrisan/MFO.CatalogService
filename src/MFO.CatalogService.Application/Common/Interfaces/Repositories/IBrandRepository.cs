using MFO.CatalogService.Domain.Entities;

namespace MFO.CatalogService.Application.Common.Interfaces.Repositories;

public interface IBrandRepository
{
    Task<Brand?> GetBrandByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Brand>> GetAllBrandsAsync(CancellationToken cancellationToken);

    Task<Brand> AddBrandAsync(Brand brand, CancellationToken cancellationToken);

    Task<Brand> UpdateBrandAsync(Brand brand, CancellationToken cancellationToken);

    Task<bool> DeleteBrandAsync(Guid brandId, CancellationToken cancellationToken);

    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
}