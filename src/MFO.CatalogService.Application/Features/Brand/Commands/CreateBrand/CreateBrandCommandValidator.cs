using FluentValidation;

namespace MFO.CatalogService.Application.Features.Brand.Commands.CreateBrand;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    private const int NameMaxLength = 50;
    private const int DescriptionMaxLength = 50;
    private const int CodeMaxLength = 10;

    public CreateBrandCommandValidator()
    {
        RuleFor(c => c.CreateBrandDto.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(NameMaxLength).WithMessage($"Name must not exceed {NameMaxLength} characters.");

        RuleFor(c => c.CreateBrandDto.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(CodeMaxLength).WithMessage($"Code must not exceed {CodeMaxLength} characters.");

        RuleFor(c => c.CreateBrandDto.Description)
            .MaximumLength(DescriptionMaxLength).WithMessage($"Description must not exceed {DescriptionMaxLength} characters.");
    }

}