using FluentValidation;

namespace MFO.CatalogService.Application.Features.Companies.Commands.SetCompanyActiveState;

public class SetCompanyActiveStateCommandValidator : AbstractValidator<SetCompanyActiveStateCommand>
{
    public SetCompanyActiveStateCommandValidator()
    {
        RuleFor(c => c.CompanyId)
            .NotEmpty().WithMessage("CompanyId is required.");

        RuleFor(c => c.IsActive)
            .NotEmpty().WithMessage("IsActive is required.");
    }
}