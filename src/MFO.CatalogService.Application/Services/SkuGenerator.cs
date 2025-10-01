using MFO.CatalogService.Application.Common.Interfaces;

namespace MFO.CatalogService.Application.Services;

public class SkuGenerator : ISkuGenerator
{
    private readonly ISkuSequenceRepository _skuSequenceRepository;

    public SkuGenerator(ISkuSequenceRepository skuSequenceRepository)
    {
        _skuSequenceRepository = skuSequenceRepository;
    }

    public async Task<string> GenerateSku(string companyCode, string categoryCode, string brandCode, CancellationToken cancellationToken)
    {
        var number = await _skuSequenceRepository.GetNextNumberForSkuAsync(companyCode, categoryCode, brandCode, cancellationToken);

        return FormatSku(companyCode, categoryCode, brandCode, number);
    }

    private string FormatSku(string company, string category, string brand, int number)
        => $"{company}-{category}-{brand}-{number:D5}";
}