using MFO.CatalogService.Domain.Entities;

namespace MFO.CatalogService.Application.Common.Interfaces;

public interface ICompanyRepository
{
    Task<Company?> GetCompanyByIdAsync(Guid companyId, CancellationToken cancellationToken);

    Task<IReadOnlyList<Company>> GetAllCompaniesAsync(CancellationToken cancellationToken);

    Task<Company> AddCompanyAsync(Company company, CancellationToken cancellationToken);

    Task<Company> UpdateCompanyAsync(Company company, CancellationToken cancellationToken);

    Task<Company> SetCompanyActiveStateAsync(Company company, bool isActive, CancellationToken cancellationToken);

    Task<bool> DeleteCompanyAsync(Company company, CancellationToken cancellationToken);
}