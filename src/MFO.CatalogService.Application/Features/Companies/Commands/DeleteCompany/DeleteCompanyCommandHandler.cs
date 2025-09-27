using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Domain.Errors;

namespace MFO.CatalogService.Application.Features.Companies.Commands.DeleteCompany;

public sealed record DeleteCompanyCommand(Guid CompanyId) : IRequest<Result>;

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, Result>
{
    private readonly ICompanyRepository _companyRepository;

    public DeleteCompanyCommandHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<Result> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var ok = await _companyRepository.DeleteCompanyAsync(request.CompanyId, cancellationToken);

        if (!ok)
        {
            return Result.Fail(new NotFoundError($"Company with ID {request.CompanyId} was not found."));
        }

        return Result.Ok();
    }
}