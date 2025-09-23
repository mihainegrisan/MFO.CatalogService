using FluentValidation;

namespace MFO.CatalogService.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private const int NameMaxLength = 50;
    private const int DescriptionMaxLength = 500;

    public CreateProductCommandValidator()
    {
        RuleFor(c => c.CreateProductDto.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(NameMaxLength).WithMessage($"Name must not exceed {NameMaxLength} characters.");

        RuleFor(c => c.CreateProductDto.Description)
            .MaximumLength(DescriptionMaxLength).WithMessage($"Description must not exceed {DescriptionMaxLength} characters.");

        RuleFor(c => c.CreateProductDto.Price)
            .NotEmpty().WithMessage("Price is required.");

        RuleFor(c => c.CreateProductDto.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required.");

        RuleFor(c => c.CreateProductDto.BrandId)
            .NotEmpty().WithMessage("BrandId is required.");
    }
}