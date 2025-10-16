using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces.Repositories;
using MFO.Contracts.Catalog.DTOs.Brand;

namespace MFO.CatalogService.Application.Features.Brand.Queries.GetAllBrands;

public sealed record GetAllBrandsQuery : IRequest<Result<IReadOnlyList<GetBrandDto>>>;

public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, Result<IReadOnlyList<GetBrandDto>>>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public GetAllBrandsQueryHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<Result<IReadOnlyList<GetBrandDto>>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brands = await _brandRepository.GetAllBrandsAsync(cancellationToken);
        if (brands.Count is 0)
        {
            return Result.Ok<IReadOnlyList<GetBrandDto>>(new List<GetBrandDto>());
        }

        var brandsDto = brands
            .Select(brand => _mapper.Map<GetBrandDto>(brand))
            .ToList();

        return Result.Ok<IReadOnlyList<GetBrandDto>>(brandsDto);
    }
}