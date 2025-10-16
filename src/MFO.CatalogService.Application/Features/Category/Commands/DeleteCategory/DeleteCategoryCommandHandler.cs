using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces.Repositories;
using MFO.CatalogService.Domain.Errors;

namespace MFO.CatalogService.Application.Features.Category.Commands.DeleteCategory;

public sealed record DeleteCategoryCommand(Guid CategoryId) : IRequest<Result>;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var ok = await _categoryRepository.DeleteCategoryAsync(request.CategoryId, cancellationToken);
        
        if (!ok)
        {
            return Result.Fail(new NotFoundError($"Category with ID {request.CategoryId} was not found."));
        }

        return Result.Ok();
    }
}