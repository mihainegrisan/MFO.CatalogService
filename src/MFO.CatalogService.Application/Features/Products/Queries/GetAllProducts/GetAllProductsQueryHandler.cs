using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Application.DTOs.Product;

namespace MFO.CatalogService.Application.Features.Products.Queries.GetAllProducts;

public sealed record GetAllProductsQuery : IRequest<Result<IReadOnlyList<GetProductDto>>>;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<IReadOnlyList<GetProductDto>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<IReadOnlyList<GetProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllProductsAsync(cancellationToken);
        if (products.Count is 0)
        {
            return Result.Ok<IReadOnlyList<GetProductDto>>(new List<GetProductDto>());
        }

        var productsDto = products
            .Select(product => _mapper.Map<GetProductDto>(product))
            .ToList();

        return Result.Ok<IReadOnlyList<GetProductDto>>(productsDto);
    }
}