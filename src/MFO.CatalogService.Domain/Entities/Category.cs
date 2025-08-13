using MFO.CatalogService.Domain.Common;

namespace MFO.CatalogService.Domain.Entities;

public class Category : AuditableEntity
{
    public required Guid CategoryId { get; set; }

    public required string Name { get; set; }

    public ICollection<Product>? Products { get; set; }
}