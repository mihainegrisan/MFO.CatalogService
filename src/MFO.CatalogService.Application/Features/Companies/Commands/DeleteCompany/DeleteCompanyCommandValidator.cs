using FluentValidation;

namespace MFO.CatalogService.Application.Features.Companies.Commands.DeleteCompany;

public class DeleteCompanyCommandValidator : AbstractValidator<DeleteCompanyCommand>
{
    public DeleteCompanyCommandValidator()
    {
        RuleFor(c => c.CompanyId)
            .NotEmpty().WithMessage("CompanyId is required.");
    }
}