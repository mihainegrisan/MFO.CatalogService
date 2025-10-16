using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces.Repositories;
using MFO.CatalogService.Domain.Errors;
using MFO.Contracts.Catalog.DTOs.Company;

namespace MFO.CatalogService.Application.Features.Companies.Queries.GetCompanyById;

public sealed record GetCompanyByIdQuery(Guid CompanyId) : IRequest<Result<GetCompanyDto>>;

public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, Result<GetCompanyDto>>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public GetCompanyByIdQueryHandler(ICompanyRepository companyRepository, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetCompanyDto>> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetCompanyByIdAsync(request.CompanyId, cancellationToken);
        if (company is null)
        {
            return Result.Fail(new NotFoundError($"Company with ID {request.CompanyId} not found."));
        }

        var companyDto = _mapper.Map<GetCompanyDto>(company);

        return Result.Ok(companyDto);
    }
}