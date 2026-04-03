using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces.Repositories;
using MFO.Contracts.Catalog.DTOs.Category;

namespace MFO.CatalogService.Application.Features.Category.Queries.GetAllCategories;

public sealed record GetAllCategoriesQuery : IRequest<Result<IReadOnlyList<CategoryDto>>>;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<IReadOnlyList<CategoryDto>>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<IReadOnlyList<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllCategoriesAsync(cancellationToken);
        if (categories.Count is 0)
        {
            return Result.Ok<IReadOnlyList<CategoryDto>>(new List<CategoryDto>());
        }

        var categoriesDto = categories
            .Select(category => _mapper.Map<CategoryDto>(category))
            .ToList();

        return Result.Ok<IReadOnlyList<CategoryDto>>(categoriesDto);
    }
}