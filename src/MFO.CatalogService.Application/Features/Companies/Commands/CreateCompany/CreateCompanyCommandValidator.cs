using FluentValidation;
using MFO.CatalogService.Application.Common;

namespace MFO.CatalogService.Application.Features.Companies.Commands.CreateCompany;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(c => c.CreateCompanyDto.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(ValidationConstants.NameMaxLength).WithMessage($"Name must not exceed {ValidationConstants.NameMaxLength} characters.");

        RuleFor(c => c.CreateCompanyDto.Code)
            .NotEmpty().WithMessage("Code is required.")
            .Length(ValidationConstants.CodeLength).WithMessage($"Code must have exactly {ValidationConstants.CodeLength} characters.");

        RuleFor(c => c.CreateCompanyDto.Description)
            .MaximumLength(ValidationConstants.DescriptionMaxLength).WithMessage($"Description must not exceed {ValidationConstants.DescriptionMaxLength} characters.");
    }
}