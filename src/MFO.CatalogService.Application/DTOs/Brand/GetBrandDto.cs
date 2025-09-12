namespace MFO.CatalogService.Application.DTOs.Brand;

public sealed record GetBrandDto
{
    public required Guid BrandId { get; init; }

    public required string Name { get; init; }

    public required string Code { get; set; }

    public string? Description { get; set; } = null;
}