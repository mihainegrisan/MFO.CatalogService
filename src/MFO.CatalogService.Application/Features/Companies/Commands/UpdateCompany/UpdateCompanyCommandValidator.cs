using FluentValidation;

namespace MFO.CatalogService.Application.Features.Companies.Commands.UpdateCompany;

public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
    private const int NameMaxLength = 50;
    private const int DescriptionMaxLength = 500;
    private const int CodeMaxLength = 10;

    public UpdateCompanyCommandValidator()
    {
        RuleFor(c => c.UpdateCompanyDto.CompanyId)
            .NotEmpty().WithMessage("CompanyId is required.");

        RuleFor(c => c.UpdateCompanyDto.Name)
            .MaximumLength(NameMaxLength).WithMessage($"Name must not exceed {NameMaxLength} characters.");

        RuleFor(c => c.UpdateCompanyDto.Code)
            .MaximumLength(CodeMaxLength).WithMessage($"Code must not exceed {CodeMaxLength} characters.");

        RuleFor(c => c.UpdateCompanyDto.Description)
            .MaximumLength(DescriptionMaxLength).WithMessage($"Description must not exceed {DescriptionMaxLength} characters.");
    }
}