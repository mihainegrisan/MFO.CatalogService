using FluentValidation;
using MFO.CatalogService.Application.Common;

namespace MFO.CatalogService.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.CreateProductDto.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(ValidationConstants.NameMaxLength).WithMessage($"Name must not exceed {ValidationConstants.NameMaxLength} characters.");

        RuleFor(c => c.CreateProductDto.Description)
            .MaximumLength(ValidationConstants.DescriptionMaxLength).WithMessage($"Description must not exceed {ValidationConstants.DescriptionMaxLength} characters.");

        RuleFor(c => c.CreateProductDto.Price)
            .NotEmpty().WithMessage("Price is required.");

        RuleFor(c => c.CreateProductDto.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required.");

        RuleFor(c => c.CreateProductDto.BrandId)
            .NotEmpty().WithMessage("BrandId is required.");
    }
}