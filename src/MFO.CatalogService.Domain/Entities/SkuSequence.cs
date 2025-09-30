using MFO.CatalogService.Domain.Common;

namespace MFO.CatalogService.Domain.Entities;

public class SkuSequence: AuditableEntity
{
    public required Guid SkuSequenceId { get; set; }

    public required string Company { get; set; }

    public required string Category { get; set; }

    public required string Brand { get; set; }

    public required int LastNumber { get; set; }
}