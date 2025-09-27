using AutoMapper;
using FluentResults;
using MediatR;
using MFO.CatalogService.Application.Common.Interfaces;
using MFO.CatalogService.Application.DTOs.Company;
using MFO.CatalogService.Domain.Errors;

namespace MFO.CatalogService.Application.Features.Companies.Commands.SetCompanyActiveState;

public sealed record SetCompanyActiveStateCommand(Guid CompanyId, bool IsActive) : IRequest<Result<GetCompanyDto>>;

public class SetCompanyActiveStateCommandHandler : IRequestHandler<SetCompanyActiveStateCommand, Result<GetCompanyDto>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public SetCompanyActiveStateCommandHandler(ICompanyRepository companyRepository, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetCompanyDto>> Handle(SetCompanyActiveStateCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.SetCompanyActiveStateAsync(request.CompanyId, request.IsActive, cancellationToken);
        if (company is null)
        {
            return Result.Fail(new NotFoundError($"Company with ID {request.CompanyId} was not found."));
        }

        var companyDto = _mapper.Map<GetCompanyDto>(company);

        return Result.Ok(companyDto);
    }
}