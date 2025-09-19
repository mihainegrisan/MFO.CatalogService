using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Application.DTOs.Company;
using MFO.CatalogService.Domain.Entities;

namespace MFO.CatalogService.Application.Features.Companies.Commands.CreateCompanyCommand;

public sealed record CreateCompanyCommand(CreateCompanyDto CreateCompanyDto) : IRequest<Result<GetCompanyDto>>;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Result<GetCompanyDto>>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public CreateCompanyCommandHandler(ICompanyRepository companyRepository, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetCompanyDto>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = _mapper.Map<Company>(request.CreateCompanyDto);
        company.CompanyId = Guid.CreateVersion7();
        company.IsActive = true;
        company.CreatedBy = "system";
        company.CreatedDate = DateTime.UtcNow;
        company.LastModifiedBy = "system";
        company.LastModifiedDate = DateTime.UtcNow;

        await _companyRepository.AddCompanyAsync(company, cancellationToken);

        var companyDto = _mapper.Map<GetCompanyDto>(company);

        return Result.Ok(companyDto);
    }
}