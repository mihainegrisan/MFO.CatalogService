using FluentValidation;

namespace MFO.CatalogService.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(c => c.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");
    }
}