using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces.Repositories;
using MFO.Contracts.Catalog.DTOs.Product;
using MFO.CatalogService.Domain.Errors;

namespace MFO.CatalogService.Application.Features.Products.Commands.SetProductActiveState;

public sealed record SetProductActiveStateCommand(Guid ProductId, bool IsActive) : IRequest<Result<ProductDto>>;

public class SetProductActiveStateCommandHandler : IRequestHandler<SetProductActiveStateCommand, Result<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public SetProductActiveStateCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<ProductDto>> Handle(SetProductActiveStateCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.SetProductActiveStateAsync(request.ProductId, request.IsActive, cancellationToken);

        if (product is null)
        {
            return Result.Fail(new NotFoundError($"Product with ID {request.ProductId} was not found."));
        }

        var productDto = _mapper.Map<ProductDto>(product);

        return Result.Ok(productDto);
    }
}