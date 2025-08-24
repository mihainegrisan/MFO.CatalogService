namespace MFO.CatalogService.Application.DTOs.Brand;

public sealed record UpdateBrandDto
{
    public required string Name { get; set; }

    public string? Description { get; set; }
}