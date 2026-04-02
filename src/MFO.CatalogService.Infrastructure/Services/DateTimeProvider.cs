using MFO.CatalogService.Application.Common.Interfaces;

namespace MFO.CatalogService.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}