using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces.Repositories;
using MFO.Contracts.Catalog.DTOs.Product;
using MFO.CatalogService.Domain.Errors;

namespace MFO.CatalogService.Application.Features.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductDto>>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(request.Id, cancellationToken);
        if (product is null)
        {
            return Result.Fail(new NotFoundError($"Product with ID {request.Id} not found."));
        }

        var productDto = _mapper.Map<ProductDto>(product);

        return Result.Ok(productDto);
    }
}