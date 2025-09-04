using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Application.DTOs.Brand;
using MFO.CatalogService.Domain.Errors;

namespace MFO.CatalogService.Application.Features.Brand.Queries.GetBrandById;

public sealed record GetBrandByIdQuery(int Id) : IRequest<Result<GetBrandDto>>;

public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, Result<GetBrandDto>>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public GetBrandByIdQueryHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetBrandDto>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetBrandByIdAsync(request.Id, cancellationToken);
        if (brand is null)
        {
            return Result.Fail(new NotFoundError($"Brand with ID {request.Id} not found."));
        }

        var brandDto = _mapper.Map<GetBrandDto>(brand);

        return Result.Ok(brandDto);
    }
}