using FluentValidation;
using MFO.CatalogService.Application.Common;

namespace MFO.CatalogService.Application.Features.Brand.Commands.CreateBrand;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(c => c.CreateBrandDto.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(ValidationConstants.NameMaxLength).WithMessage($"Name must not exceed {ValidationConstants.NameMaxLength} characters.");

        RuleFor(c => c.CreateBrandDto.Code)
            .NotEmpty().WithMessage("Code is required.")
            .Length(ValidationConstants.CodeLength).WithMessage($"Code must have exactly {ValidationConstants.CodeLength} characters.");
            

        RuleFor(c => c.CreateBrandDto.Description)
            .MaximumLength(ValidationConstants.DescriptionMaxLength).WithMessage($"Description must not exceed {ValidationConstants.DescriptionMaxLength} characters.");
    }

}