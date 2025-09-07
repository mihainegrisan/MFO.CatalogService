namespace MFO.CatalogService.Application.DTOs.Product;

public sealed record CreateProductDto
{
    public required string Name { get; init; }

    public string SKU { get; set; } = null!;

    public string? Description { get; init; }

    public required decimal Price { get; init; }

    public required Guid CategoryId { get; init; }

    public required Guid BrandId { get; init; }
}

