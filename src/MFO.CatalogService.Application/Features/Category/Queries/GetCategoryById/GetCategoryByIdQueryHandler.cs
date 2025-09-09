using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Application.DTOs.Category;
using MFO.CatalogService.Domain.Errors;

namespace MFO.CatalogService.Application.Features.Category.Queries.GetCategoryById;

public sealed record GetCategoryByIdQuery(Guid Id) : IRequest<Result<GetCategoryDto>>;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<GetCategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetCategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(request.Id, cancellationToken);
        if (category is null)
        {
            return Result.Fail(new NotFoundError($"Category with ID {request.Id} not found."));
        }

        var categoryDto = _mapper.Map<GetCategoryDto>(category);

        return Result.Ok(categoryDto);
    }
}