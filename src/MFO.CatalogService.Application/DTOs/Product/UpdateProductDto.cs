namespace MFO.CatalogService.Application.DTOs.Product;

public sealed record UpdateProductDto
{
    public required string Name { get; init; }

    public required decimal Price { get; init; }

    public string? Description { get; init; }

    public bool IsActive { get; init; }

    public required Guid CategoryId { get; init; }

    public required Guid BrandId { get; init; }
}

