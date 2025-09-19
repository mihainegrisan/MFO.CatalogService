using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Application.DTOs.Company;
using MFO.CatalogService.Domain.Entities;
using MFO.CatalogService.Domain.Errors;

namespace MFO.CatalogService.Application.Features.Companies.Commands.UpdateCompanyCommand;

public sealed record UpdateCompanyCommand(UpdateCompanyDto UpdateCompanyDto) : IRequest<Result<GetCompanyDto>>;

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Result<GetCompanyDto>>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public UpdateCompanyCommandHandler(ICompanyRepository companyRepository, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetCompanyDto>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var existingCompany = await _companyRepository.GetCompanyByIdAsync(request.UpdateCompanyDto.CompanyId, cancellationToken);

        if (existingCompany is null)
        {
            return Result.Fail(new NotFoundError($"Company with ID {request.UpdateCompanyDto.CompanyId} was not found."));
        }

        var company = _mapper.Map<Company>(existingCompany);

        await _companyRepository.UpdateCompanyAsync(company, cancellationToken);

        var companyDto = _mapper.Map<GetCompanyDto>(company);

        return Result.Ok(companyDto);
    }
}