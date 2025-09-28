using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Domain.Errors;

namespace MFO.CatalogService.Application.Features.Brand.Commands.DeleteBrand;

public sealed record DeleteBrandCommand(Guid BrandId) : IRequest<Result>;

public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Result>
{
    private readonly IBrandRepository _brandRepository;

    public DeleteBrandCommandHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<Result> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var ok = await _brandRepository.DeleteBrandAsync(request.BrandId, cancellationToken);

        if (!ok)
        {
            return Result.Fail(new NotFoundError($"Brand with ID {request.BrandId} was not found."));
        }

        return Result.Ok();
    }
}