using FluentValidation;

namespace MFO.CatalogService.Application.Features.Companies.Commands.CreateCompany;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    private const int NameMaxLength = 50;
    private const int DescriptionMaxLength = 500;
    private const int CodeMaxLength = 10;

    public CreateCompanyCommandValidator()
    {
        RuleFor(c => c.CreateCompanyDto.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(NameMaxLength).WithMessage($"Name must not exceed {NameMaxLength} characters.");

        RuleFor(c => c.CreateCompanyDto.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(CodeMaxLength).WithMessage($"Code must not exceed {CodeMaxLength} characters.");

        RuleFor(c => c.CreateCompanyDto.Description)
            .MaximumLength(DescriptionMaxLength).WithMessage($"Description must not exceed {DescriptionMaxLength} characters.");
    }
}