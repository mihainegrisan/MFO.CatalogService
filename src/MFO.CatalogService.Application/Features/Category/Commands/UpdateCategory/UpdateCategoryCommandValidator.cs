using FluentValidation;

namespace MFO.CatalogService.Application.Features.Category.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    private const int NameMaxLength = 50;
    private const int CodeMaxLength = 10;

    public UpdateCategoryCommandValidator()
    {
        RuleFor(c => c.UpdateCategoryDto.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required.");

        RuleFor(c => c.UpdateCategoryDto.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(NameMaxLength).WithMessage($"Name must not exceed {NameMaxLength} characters.");

        RuleFor(c => c.UpdateCategoryDto.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(CodeMaxLength).WithMessage($"Code must not exceed {CodeMaxLength} characters.");
    }
}