using FluentValidation;
using MFO.CatalogService.Application.Common;

namespace MFO.CatalogService.Application.Features.Companies.Commands.UpdateCompany;

public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyCommandValidator()
    {
        RuleFor(c => c.UpdateCompanyDto.CompanyId)
            .NotEmpty().WithMessage("CompanyId is required.");

        RuleFor(c => c.UpdateCompanyDto.Name)
            .MaximumLength(ValidationConstants.NameMaxLength).WithMessage($"Name must not exceed {ValidationConstants.NameMaxLength} characters.");

        RuleFor(c => c.UpdateCompanyDto.Code)
            .Length(ValidationConstants.CodeLength).WithMessage($"Code must have exactly {ValidationConstants.CodeLength} characters.");

        RuleFor(c => c.UpdateCompanyDto.Description)
            .MaximumLength(ValidationConstants.DescriptionMaxLength).WithMessage($"Description must not exceed {ValidationConstants.DescriptionMaxLength} characters.");
    }
}