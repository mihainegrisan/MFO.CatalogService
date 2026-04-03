using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces.Repositories;
using MFO.CatalogService.Domain.Errors;
using MFO.Contracts.Catalog.DTOs.Category;

namespace MFO.CatalogService.Application.Features.Category.Commands.UpdateCategory;

public sealed record UpdateCategoryCommand(UpdateCategoryDto UpdateCategoryDto) : IRequest<Result<CategoryDto>>;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await _categoryRepository.GetCategoryByIdAsync(request.UpdateCategoryDto.CategoryId, cancellationToken);

        if (existingCategory is null)
        {
            return Result.Fail(new NotFoundError($"Category with ID {request.UpdateCategoryDto.CategoryId} was not found."));
        }

        _mapper.Map(request.UpdateCategoryDto, existingCategory);
        existingCategory.LastModifiedBy = "system";
        existingCategory.LastModifiedDate = DateTime.UtcNow;

        var updatedProduct = await _categoryRepository.UpdateCategoryAsync(existingCategory, cancellationToken);

        var categoryDto = _mapper.Map<CategoryDto>(updatedProduct);

        return Result.Ok(categoryDto);
    }
}