namespace MFO.CatalogService.Application.Common.Interfaces;

public interface ISkuGenerator
{
    Task<string> GenerateSku(string companyCode, string categoryCode, string brandCode, CancellationToken cancellationToken);
}