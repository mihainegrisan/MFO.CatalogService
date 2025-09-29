using FluentValidation;

namespace MFO.CatalogService.Application.Features.Category.Commands.DeleteCategory;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(c => c.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required.");
    }
}