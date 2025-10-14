using MFO.CatalogService.Application.Common.Interfaces.Repositories;
using MFO.CatalogService.Domain.Entities;
using MFO.CatalogService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MFO.CatalogService.Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly CatalogDbContext _db;

    public CompanyRepository(CatalogDbContext db)
    {
        _db = db;
    }

    public async Task<Company?> GetCompanyByIdAsync(Guid companyId, CancellationToken cancellationToken)
        => await _db.Companies
            .FindAsync([companyId], cancellationToken);

    public async Task<IReadOnlyList<Company>> GetAllCompaniesAsync(CancellationToken cancellationToken)
        => await _db.Companies
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<Company> AddCompanyAsync(Company company, CancellationToken cancellationToken)
    {
        await _db.Companies.AddAsync(company, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        return company;
    }

    public async Task<Company> UpdateCompanyAsync(Company company, CancellationToken cancellationToken)
    {
        _db.Companies.Update(company);
        await _db.SaveChangesAsync(cancellationToken);
        return company;
    }

    public async Task<Company?> SetCompanyActiveStateAsync(Guid companyId, bool isActive, CancellationToken cancellationToken)
    {
        var company = await _db.Companies.FindAsync([companyId], cancellationToken);
        if (company is null)
        {
            return null;
        }

        company.IsActive = isActive;
        _db.Companies.Update(company);
        await _db.SaveChangesAsync(cancellationToken);
        return company;
    }

    public async Task<bool> DeleteCompanyAsync(Guid companyId, CancellationToken cancellationToken)
    {
        var company = await _db.Companies.FindAsync([companyId], cancellationToken);
        if (company is null)
        {
            return false;
        }

        _db.Companies.Remove(company);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}