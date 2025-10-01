using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Domain.Entities;
using MFO.CatalogService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MFO.CatalogService.Infrastructure.Repositories;

public class SkuSequenceRepository : ISkuSequenceRepository
{
    private readonly CatalogDbContext _db;

    public SkuSequenceRepository(CatalogDbContext db)
    {
        _db = db;
    }

    public async Task<int> GetNextNumberForSkuAsync(string company, string category, string brand, CancellationToken cancellationToken)
    {
        var sequence = await _db.SkuSequences.SingleOrDefaultAsync(s => s.Company == company && s.Category == category && s.Brand == brand, cancellationToken);

        if (sequence is null)
        {
            sequence = new SkuSequence
            {
                CreatedBy = "system",
                CreatedDate = DateTime.UtcNow,
                LastModifiedBy = "system",
                LastModifiedDate = DateTime.UtcNow,
                SkuSequenceId = Guid.CreateVersion7(),
                Company = company,
                Category = category,
                Brand = brand,
                LastNumber = 0
            };

            await _db.SkuSequences.AddAsync(sequence, cancellationToken);
        }

        sequence.LastNumber++;
        await _db.SaveChangesAsync(cancellationToken);

        return sequence.LastNumber;
    }
}