using MFO.CatalogService.Domain.Common;

namespace MFO.CatalogService.Domain.Entities;

public class Brand : AuditableEntity
{
    public required Guid BrandId { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; } = null;

    public ICollection<Product>? Products { get; set; }
}