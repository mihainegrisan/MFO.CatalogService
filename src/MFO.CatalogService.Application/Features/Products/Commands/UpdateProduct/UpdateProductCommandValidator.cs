using FluentValidation;
using MFO.CatalogService.Application.Common;

namespace MFO.CatalogService.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.UpdateProductDto.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");

        RuleFor(c => c.UpdateProductDto.Name)
            .MaximumLength(ValidationConstants.NameMaxLength).WithMessage($"Name must not exceed {ValidationConstants.NameMaxLength} characters.");

        RuleFor(c => c.UpdateProductDto.Description)
            .MaximumLength(ValidationConstants.DescriptionMaxLength).WithMessage($"Description must not exceed {ValidationConstants.DescriptionMaxLength} characters.");
    }
}