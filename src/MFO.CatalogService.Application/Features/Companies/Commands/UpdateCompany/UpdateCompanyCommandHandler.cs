using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces.Repositories;
using MFO.CatalogService.Application.DTOs.Company;
using MFO.CatalogService.Domain.Errors;

namespace MFO.CatalogService.Application.Features.Companies.Commands.UpdateCompany;

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

        _mapper.Map(request.UpdateCompanyDto, existingCompany);
        existingCompany.LastModifiedBy = "system";
        existingCompany.LastModifiedDate = DateTime.UtcNow;

        await _companyRepository.UpdateCompanyAsync(existingCompany, cancellationToken);

        var companyDto = _mapper.Map<GetCompanyDto>(existingCompany);

        return Result.Ok(companyDto);
    }
}