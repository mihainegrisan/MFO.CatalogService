namespace MFO.CatalogService.Application.DTOs.Category;

public sealed record GetCategoryDto
{
    public required Guid CategoryId { get; init; }

    public required string Name { get; init; }

    public required string Code { get; set; }
}
