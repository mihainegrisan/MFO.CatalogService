using MFO.CatalogService.Application.DTOs.Brand;
using MFO.CatalogService.Application.DTOs.Category;

namespace MFO.CatalogService.Application.DTOs.Product;

public sealed record GetProductDto
{
    public required Guid ProductId { get; init; }

    public required string Name { get; init; }

    public required decimal Price { get; init; }

    public string? Description { get; init; }

    public bool IsActive { get; init; }

    // public Guid CategoryId { get; init; }

    public GetCategoryDto Category { get; init; } = null!;

    // public Guid BrandId { get; init; }

    public GetBrandDto Brand { get; init; } = null!;
}