using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Application.DTOs.Product;
using MFO.CatalogService.Domain.Errors;

namespace MFO.CatalogService.Application.Features.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(UpdateProductDto UpdateProductDto) : IRequest<Result<GetProductDto>>;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<GetProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IBrandRepository brandRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await _productRepository.GetProductByIdAsync(request.UpdateProductDto.ProductId, cancellationToken);

        if (existingProduct is null)
        {
            return Result.Fail(new NotFoundError($"Product with ID {request.UpdateProductDto.ProductId} was not found."));
        }
        
        _mapper.Map(request.UpdateProductDto, existingProduct);

        // TODO: Extract into a validation service

        var category = await _categoryRepository.GetCategoryByIdAsync(request.UpdateProductDto.CategoryId, cancellationToken);
        if (category is null)
        {
            return Result.Fail(new NotFoundError($"Category with ID {request.UpdateProductDto.CategoryId} was not found."));
        }
        existingProduct.Category = category;

        var brand = await _brandRepository.GetBrandByIdAsync(request.UpdateProductDto.BrandId, cancellationToken);
        if (brand is null)
        {
            return Result.Fail(new NotFoundError($"Brand with ID {request.UpdateProductDto.BrandId} was not found."));
        }
        existingProduct.Brand = brand;

        // TODO: Automate auditable fields population

        existingProduct.LastModifiedBy = "system";
        existingProduct.LastModifiedDate = DateTime.UtcNow;

        var updatedProduct = await _productRepository.UpdateProductAsync(existingProduct, cancellationToken);

        var productDto = _mapper.Map<GetProductDto>(updatedProduct);

        return Result.Ok(productDto);
    }
}