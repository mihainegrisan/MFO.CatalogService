using FluentValidation;
using MFO.CatalogService.Application.Common;

namespace MFO.CatalogService.Application.Features.Category.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.CreateCategoryDto.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(ValidationConstants.NameMaxLength).WithMessage($"Name must not exceed {ValidationConstants.NameMaxLength} characters.");

        RuleFor(c => c.CreateCategoryDto.Code)
            .NotEmpty().WithMessage("Code is required.")
            .Length(ValidationConstants.CodeLength).WithMessage($"Code must have exactly {ValidationConstants.CodeLength} characters.");
    }
}