using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces.Repositories;
using MFO.Contracts.Catalog.DTOs.Company;

namespace MFO.CatalogService.Application.Features.Companies.Queries.GetAllCompanies;

public sealed record GetAllCompaniesQuery : IRequest<Result<IReadOnlyList<CompanyDto>>>;

public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, Result<IReadOnlyList<CompanyDto>>>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public GetAllCompaniesQueryHandler(ICompanyRepository companyRepository, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<Result<IReadOnlyList<CompanyDto>>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
    {
        var companies = await _companyRepository.GetAllCompaniesAsync(cancellationToken);
        if (companies.Count == 0)
        {
            return Result.Ok<IReadOnlyList<CompanyDto>>(new List<CompanyDto>());
        }

        var companyDtos = companies
            .Select(c => _mapper.Map<CompanyDto>(c))
            .ToList();

        return Result.Ok<IReadOnlyList<CompanyDto>>(companyDtos);

    }
}