namespace MFO.CatalogService.Application.DTOs.Brand;

public sealed record UpdateBrandDto
{
    public Guid BrandId { get; set; }

    public required string Name { get; set; }

    public required string Code { get; set; }

    public string? Description { get; set; }
}