using MFO.CatalogService.Domain.Common;

namespace MFO.CatalogService.Domain.Entities;

public class Product : AuditableEntity
{
    public required Guid ProductId { get; set; } // Internal identifier

    public string SKU { get; set; } = null!; // Business identifier

    public required string Name { get; set; }

    public string? Description { get; set; }

    public required decimal Price { get; set; }

    public bool IsActive { get; set; }

    // public string[] ImageUrls { get; set; }

    public Guid CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public Guid BrandId { get; set; }

    public Brand Brand { get; set; } = null!;
}