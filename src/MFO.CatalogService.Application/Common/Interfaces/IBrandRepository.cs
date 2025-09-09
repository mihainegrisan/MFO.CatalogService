using MFO.CatalogService.Domain.Entities;

namespace MFO.CatalogService.Application.Common.Interfaces;

public interface IBrandRepository
{
    Task<Brand?> GetBrandByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<List<Brand>> GetAllBrandsAsync(CancellationToken cancellationToken);

    Task<Brand> AddBrandAsync(Brand brand, CancellationToken cancellationToken);

    Task<Brand> UpdateBrandAsync(Brand brand, CancellationToken cancellationToken);

    Task<bool> DeleteBrandAsync(Brand brand, CancellationToken cancellationToken);
}