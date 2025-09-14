using MFO.CatalogService.Domain.Common;

namespace MFO.CatalogService.Domain.Entities;

public class Company : AuditableEntity
{
    public required Guid CompanyId { get; set; }

    public required string Name { get; set; }

    public required string Code { get; set; }

    public string? Description { get; set; } = null;

    public bool IsActive { get; set; }
}