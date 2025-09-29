using FluentValidation;

namespace MFO.CatalogService.Application.Features.Brand.Commands.DeleteBrand;

public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
{
    public DeleteBrandCommandValidator()
    {
        RuleFor(c => c.BrandId)
            .NotEmpty().WithMessage("BrandId is required.");
    }
}