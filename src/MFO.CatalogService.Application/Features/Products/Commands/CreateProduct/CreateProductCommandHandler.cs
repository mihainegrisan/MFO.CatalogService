using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Application.DTOs.Product;
using MFO.CatalogService.Domain.Entities;

namespace MFO.CatalogService.Application.Features.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(CreateProductDto CreateProductDto) : IRequest<Result<GetProductDto>>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<GetProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request.CreateProductDto);
        product.ProductId = Guid.CreateVersion7();
        product.IsActive = true;
        product.CreatedBy = "system";
        product.CreatedDate = DateTime.UtcNow;
        product.LastModifiedBy = "system";
        product.LastModifiedDate = DateTime.UtcNow;

        await _productRepository.AddProductAsync(product, cancellationToken);

        var productDto = _mapper.Map<GetProductDto>(product);

        return Result.Ok(productDto);
    }
}