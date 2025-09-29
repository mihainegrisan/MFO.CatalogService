using FluentValidation;

namespace MFO.CatalogService.Application.Features.Products.Commands.SetProductActiveState;

public class SetProductActiveStateCommandValidator : AbstractValidator<SetProductActiveStateCommand>
{
    public SetProductActiveStateCommandValidator()
    {
        RuleFor(c => c.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");

        RuleFor(c => c.IsActive)
            .NotEmpty().WithMessage("IsActive is required");
    }
}