using FluentValidation;

namespace MFO.CatalogService.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    private const int NameMaxLength = 50;
    private const int DescriptionMaxLength = 500;

    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.UpdateProductDto.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");

        RuleFor(c => c.UpdateProductDto.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(NameMaxLength).WithMessage($"Name must not exceed {NameMaxLength} characters.");

        RuleFor(c => c.UpdateProductDto.Description)
            .MaximumLength(DescriptionMaxLength).WithMessage($"Description must not exceed {DescriptionMaxLength} characters.");

        RuleFor(c => c.UpdateProductDto.Price)
            .NotEmpty().WithMessage("Price is required.");
    }
}