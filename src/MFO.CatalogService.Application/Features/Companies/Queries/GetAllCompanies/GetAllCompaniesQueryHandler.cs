using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces.Repositories;
using MFO.CatalogService.Application.DTOs.Company;

namespace MFO.CatalogService.Application.Features.Companies.Queries.GetAllCompanies;

public sealed record GetAllCompaniesQuery : IRequest<Result<IReadOnlyList<GetCompanyDto>>>;

public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, Result<IReadOnlyList<GetCompanyDto>>>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public GetAllCompaniesQueryHandler(ICompanyRepository companyRepository, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<Result<IReadOnlyList<GetCompanyDto>>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
    {
        var companies = await _companyRepository.GetAllCompaniesAsync(cancellationToken);
        if (companies.Count == 0)
        {
            return Result.Ok<IReadOnlyList<GetCompanyDto>>(new List<GetCompanyDto>());
        }

        var companyDtos = companies
            .Select(c => _mapper.Map<GetCompanyDto>(c))
            .ToList();

        return Result.Ok<IReadOnlyList<GetCompanyDto>>(companyDtos);

    }
}