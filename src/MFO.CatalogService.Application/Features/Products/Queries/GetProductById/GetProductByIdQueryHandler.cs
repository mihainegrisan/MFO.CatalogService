using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Application.DTOs.Product;
using MFO.CatalogService.Domain.Errors;

namespace MFO.CatalogService.Application.Features.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<Result<GetProductDto>>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<GetProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(request.Id, cancellationToken);
        if (product is null)
        {
            return Result.Fail(new NotFoundError($"Product with ID {request.Id} not found."));
        }

        var productDto = _mapper.Map<GetProductDto>(product);

        return Result.Ok(productDto);
    }
}