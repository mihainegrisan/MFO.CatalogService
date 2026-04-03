using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces.Repositories;
using MFO.Contracts.Catalog.DTOs.Product;

namespace MFO.CatalogService.Application.Features.Products.Queries.GetAllProducts;

public sealed record GetAllProductsQuery : IRequest<Result<IReadOnlyList<ProductDto>>>;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<IReadOnlyList<ProductDto>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<IReadOnlyList<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllProductsAsync(cancellationToken);
        if (products.Count is 0)
        {
            return Result.Ok<IReadOnlyList<ProductDto>>(new List<ProductDto>());
        }

        var productsDto = products
            .Select(product => _mapper.Map<ProductDto>(product))
            .ToList();

        return Result.Ok<IReadOnlyList<ProductDto>>(productsDto);
    }
}