using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Domain.Errors;

namespace MFO.CatalogService.Application.Features.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(Guid ProductId) : IRequest<Result>;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var ok = await _productRepository.DeleteProductAsync(request.ProductId, cancellationToken);

        if (!ok)
        {
            return Result.Fail(new NotFoundError($"Product with ID {request.ProductId} was not found."));
        }

        return Result.Ok();
    }
}