using FluentValidation;

namespace MFO.CatalogService.Application.Features.Companies.Queries.GetCompanyById;

public class GetCompanyByIdQueryValidator : AbstractValidator<GetCompanyByIdQuery>
{
    public GetCompanyByIdQueryValidator()
    {
        RuleFor(q => q.CompanyId)
            .NotEmpty().WithMessage("CompanyId is required.");
    }
}