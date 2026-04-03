using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces.Repositories;
using MFO.Contracts.Catalog.DTOs.Brand;

namespace MFO.CatalogService.Application.Features.Brand.Commands.CreateBrand;

public sealed record CreateBrandCommand(CreateBrandDto CreateBrandDto) : IRequest<Result<BrandDto>>;

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Result<BrandDto>>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<Result<BrandDto>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var exists = await _brandRepository.ExistsByNameAsync(request.CreateBrandDto.Name, cancellationToken);
        if (exists)
        {
            return Result.Fail<BrandDto>($"The brand name {request.CreateBrandDto.Name} already exists.");
        }

        var brand = _mapper.Map<Domain.Entities.Brand>(request.CreateBrandDto);
        brand.BrandId = Guid.CreateVersion7();
        brand.CreatedBy = "system";
        brand.CreatedDate = DateTime.UtcNow;
        brand.LastModifiedBy = "system";
        brand.LastModifiedDate = DateTime.UtcNow;

        await _brandRepository.AddBrandAsync(brand, cancellationToken);

        var brandDto = _mapper.Map<BrandDto>(brand);

        return Result.Ok(brandDto);
    }
}