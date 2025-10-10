using MediatR;
using MFO.CatalogService.Application.Features.Brand.Queries.GetAllBrands;
using MFO.CatalogService.Application.Features.Brand.Queries.GetBrandById;
using MFO.CatalogService.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace MFO.CatalogService.API.Controllers;

[ApiController]
[Route("api/catalog/[controller]")]
public class BrandController : ControllerBase
{
    private readonly ILogger<BrandController> _logger;
    private readonly IMediator _mediator;

    public BrandController(ILogger<BrandController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBrandByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received GET request for brand with Id: {BrandId}", id);

        var result = await _mediator.Send(new GetBrandByIdQuery(id), cancellationToken);

        if (result.IsFailed)
        {

            if (result.HasError<NotFoundError>())
            {
                _logger.LogInformation("Brand with Id: {BrandId} not found.", id);

                return NotFound();
            }
            
            _logger.LogWarning("Failed to retrieve brand with Id: {BrandId}. Errors: {@Errors}", id, result.Errors);

            return BadRequest(result.Errors);
        }

        _logger.LogInformation("Brand with Id: {BrandId} retrieved successfully.", id);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBrandsAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received GET request for all brands.");

        var result = await _mediator.Send(new GetAllBrandsQuery(), cancellationToken);

        if (result.IsFailed)
        {
            _logger.LogWarning("Failed to retrieve brands. Errors: {@Errors}", result.Errors);

            return BadRequest(result.Errors);
        }

        _logger.LogInformation("Retrieved {BrandsCount} brands successfully.", result.Value.Count);

        return Ok(result.Value);
    }
}