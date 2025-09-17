using FluentValidation;

namespace MFO.CatalogService.Application.Features.Brand.Commands.UpdateBrand;

public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
{
    private const int NameMaxLength = 50;
    private const int DescriptionMaxLength = 500;
    private const int CodeMaxLength = 10;

    public UpdateBrandCommandValidator()
    {
        RuleFor(c => c.UpdateBrandDto.BrandId)
            .NotEmpty().WithMessage("BrandId is required.");

        RuleFor(c => c.UpdateBrandDto.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(NameMaxLength).WithMessage($"Name must not exceed {NameMaxLength} characters.");

        RuleFor(c => c.UpdateBrandDto.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(CodeMaxLength).WithMessage($"Code must not exceed {CodeMaxLength} characters.");

        RuleFor(c => c.UpdateBrandDto.Description)
            .MaximumLength(DescriptionMaxLength).WithMessage($"Description must not exceed {DescriptionMaxLength} characters.");
    }
}