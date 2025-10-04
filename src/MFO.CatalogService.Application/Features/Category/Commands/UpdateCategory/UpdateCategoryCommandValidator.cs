using FluentValidation;
using MFO.CatalogService.Application.Common;

namespace MFO.CatalogService.Application.Features.Category.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(c => c.UpdateCategoryDto.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required.");

        RuleFor(c => c.UpdateCategoryDto.Name)
            .MaximumLength(ValidationConstants.NameMaxLength).WithMessage($"Name must not exceed {ValidationConstants.NameMaxLength} characters.");

        RuleFor(c => c.UpdateCategoryDto.Code)
            .Length(ValidationConstants.CodeLength).WithMessage($"Code must have exactly {ValidationConstants.CodeLength} characters.");
    }
}