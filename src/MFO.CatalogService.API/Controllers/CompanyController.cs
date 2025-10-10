using MediatR;
using MFO.CatalogService.Application.Features.Companies.Queries.GetAllCompanies;
using MFO.CatalogService.Application.Features.Companies.Queries.GetCompanyById;
using MFO.CatalogService.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace MFO.CatalogService.API.Controllers;

[ApiController]
[Route("api/catalog/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ILogger<CompanyController> _logger;
    private readonly IMediator _mediator;

    public CompanyController(ILogger<CompanyController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCompanyByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received GET request for company with Id: {CompanyId}", id);

        var result = await _mediator.Send(new GetCompanyByIdQuery(id), cancellationToken);

        if (result.IsFailed)
        {
            if (result.HasError<NotFoundError>())
            {
                _logger.LogInformation("Company with Id: {CompanyId} not found.", id);

                return NotFound();
            }

            _logger.LogWarning("Failed to retrieve Company with Id: {CompanyId}. Errors: {@Errors}", id, result.Errors);

            return BadRequest(result.Errors);
        }

        _logger.LogInformation("Company with Id: {CompanyId} retrieved successfully.", id);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCompaniesAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received GET request for all companies.");

        var result = await _mediator.Send(new GetAllCompaniesQuery(), cancellationToken);

        if (result.IsFailed)
        {
            _logger.LogWarning("Failed to retrieve companies. Errors: {@Errors}", result.Errors);

            return BadRequest(result.Errors);
        }

        _logger.LogInformation("Retrieved {CompaniesCount} companies successfully.", result.Value.Count);

        return Ok(result.Value);
    }
}