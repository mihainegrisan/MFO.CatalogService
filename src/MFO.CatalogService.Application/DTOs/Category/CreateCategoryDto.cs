namespace MFO.CatalogService.Application.DTOs.Category;

public sealed record CreateCategoryDto
{
    public required string Name { get; set; }

    public required string Code { get; set; }
}