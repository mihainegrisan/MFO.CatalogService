using FluentValidation;

namespace MFO.CatalogService.Application.Features.Brand.Queries.GetBrandById;

public class GetBrandByIdQueryValidator : AbstractValidator<GetBrandByIdQuery>
{
    public GetBrandByIdQueryValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}