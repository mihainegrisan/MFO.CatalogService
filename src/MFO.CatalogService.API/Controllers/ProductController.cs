using MediatR;
using MFO.CatalogService.Application.Features.Products.Queries.GetAllProducts;
using MFO.CatalogService.Application.Features.Products.Queries.GetProductById;
using MFO.CatalogService.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace MFO.CatalogService.API.Controllers;

[ApiController]
[Route("api/catalog/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IMediator _mediator;

    public ProductController(ILogger<ProductController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received GET request for product with Id {ProductId}", id);

        var result = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);

        if (result.IsFailed)
        {
            if (result.HasError<NotFoundError>())
            {
                _logger.LogInformation("Product with Id: {ProductId} not found.", id);

                return NotFound();
            }

            _logger.LogWarning("Failed to retrieve product with Id: {ProductId}. Errors: {@Errors}", id, result.Errors);

            return BadRequest(result.Errors);
        }

        _logger.LogInformation("Product with Id: {ProductId} retrieved successfully.", id);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received GET request for all products.");

        var result = await _mediator.Send(new GetAllProductsQuery(), cancellationToken);

        if (result.IsFailed)
        {
            _logger.LogInformation("Failed to retrieve products. Errors: {@Errors}", result.Errors);

            return BadRequest(result.Errors);
        }

        _logger.LogInformation("Retrieved {ProductsCount} products successfully.", result.Value.Count);

        return Ok(result.Value);
    }
}