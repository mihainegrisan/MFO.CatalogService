using FluentValidation;

namespace MFO.CatalogService.Application.Features.Category.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    private const int NameMaxLength = 50;
    private const int CodeMaxLength = 10;

    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.CreateCategoryDto.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(NameMaxLength).WithMessage($"Name must not exceed {NameMaxLength} characters.");

        RuleFor(c => c.CreateCategoryDto.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(CodeMaxLength).WithMessage($"Code must not exceed {CodeMaxLength} characters.");
    }
}