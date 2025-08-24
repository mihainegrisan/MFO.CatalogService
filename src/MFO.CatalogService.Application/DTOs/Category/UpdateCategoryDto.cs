namespace MFO.CatalogService.Application.DTOs.Category;

public sealed record UpdateCategoryDto
{
    public required string Name { get; set; }
}
