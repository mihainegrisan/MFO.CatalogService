using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Application.DTOs.Category;

namespace MFO.CatalogService.Application.Features.Category.Commands.CreateCategory;

public sealed record CreateCategoryCommand(CreateCategoryDto CreateCategoryDto) : IRequest<Result<GetCategoryDto>>;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<GetCategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetCategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var exists = await _categoryRepository.ExistsByNameAsync(request.CreateCategoryDto.Name, cancellationToken);
        if (exists)
        {
            return Result.Fail<GetCategoryDto>($"The category name {request.CreateCategoryDto.Name} already exists.");
        }

        var category = _mapper.Map<Domain.Entities.Category>(request.CreateCategoryDto);
        category.CategoryId = Guid.CreateVersion7();
        category.CreatedBy = "system";
        category.CreatedDate = DateTime.UtcNow;
        category.LastModifiedBy = "system";
        category.LastModifiedDate = DateTime.UtcNow;

        await _categoryRepository.AddCategoryAsync(category, cancellationToken);

        var categoryDto = _mapper.Map<GetCategoryDto>(category);

        return Result.Ok(categoryDto);
    }
}