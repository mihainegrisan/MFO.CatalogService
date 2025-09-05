using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Application.DTOs.Category;

namespace MFO.CatalogService.Application.Features.Category.Queries.GetAllCategories;

public sealed record GetAllCategoriesQuery : IRequest<Result<IReadOnlyList<GetCategoryDto>>>;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<IReadOnlyList<GetCategoryDto>>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<IReadOnlyList<GetCategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllCategoriesAsync(cancellationToken);
        if (categories.Count is 0)
        {
            return Result.Ok<IReadOnlyList<GetCategoryDto>>(new List<GetCategoryDto>());
        }

        var categoriesDto = categories
            .Select(category => _mapper.Map<GetCategoryDto>(category))
            .ToList();

        return Result.Ok<IReadOnlyList<GetCategoryDto>>(categoriesDto);
    }
}