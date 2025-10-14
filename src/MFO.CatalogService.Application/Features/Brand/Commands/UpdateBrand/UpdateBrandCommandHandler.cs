using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces.Repositories;
using MFO.CatalogService.Application.DTOs.Brand;
using MFO.CatalogService.Domain.Errors;

namespace MFO.CatalogService.Application.Features.Brand.Commands.UpdateBrand;

public sealed record UpdateBrandCommand(UpdateBrandDto UpdateBrandDto) : IRequest<Result<GetBrandDto>>;

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Result<GetBrandDto>>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public UpdateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }


    public async Task<Result<GetBrandDto>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var existingBrand = await _brandRepository.GetBrandByIdAsync(request.UpdateBrandDto.BrandId, cancellationToken);
        if (existingBrand is null)
        {
            return Result.Fail(new NotFoundError($"Brand with ID {request.UpdateBrandDto.BrandId} was not found."));
        }

        _mapper.Map(request.UpdateBrandDto, existingBrand);
        existingBrand.LastModifiedBy = "system";
        existingBrand.LastModifiedDate = DateTime.UtcNow;

        var updatedBrand = await _brandRepository.UpdateBrandAsync(existingBrand, cancellationToken);

        var brandDto = _mapper.Map<GetBrandDto>(updatedBrand);

        return Result.Ok(brandDto);
    }
}