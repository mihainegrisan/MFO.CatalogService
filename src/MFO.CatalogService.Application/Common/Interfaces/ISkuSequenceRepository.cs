namespace MFO.CatalogService.Application.Common.Interfaces;

public interface ISkuSequenceRepository
{
    Task<int> GetNextNumberForSkuAsync(string company, string category, string brand, CancellationToken cancellationToken);
}