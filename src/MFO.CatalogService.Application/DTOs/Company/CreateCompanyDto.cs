namespace MFO.CatalogService.Application.DTOs.Company;

public class CreateCompanyDto
{
    public required string Name { get; set; }

    public required string Code { get; set; }

    public string? Description { get; set; }
}