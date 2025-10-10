using MediatR;
using MFO.CatalogService.Application.Features.Category.Queries.GetAllCategories;
using MFO.CatalogService.Application.Features.Category.Queries.GetCategoryById;
using MFO.CatalogService.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace MFO.CatalogService.API.Controllers;

[ApiController]
[Route("api/catalog/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly IMediator _mediator;

    public CategoryController(ILogger<CategoryController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received GET request for category with Id: {CategoryId}", id);

        var result = await _mediator.Send(new GetCategoryByIdQuery(id), cancellationToken);

        if (result.IsFailed)
        {
            if (result.HasError<NotFoundError>())
            {
                _logger.LogInformation("Category with Id: {CategoryId} not found.", id);

                return NotFound();
            }

            _logger.LogWarning("Failed to retrieve category with Id: {CategoryId}. Errors: {@Errors}", id, result.Errors);

            return BadRequest(result.Errors);
        }

        _logger.LogInformation("Category with Id: {CategoryId} retrieved successfully.", id);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategoriesAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received GET request for all categories.");

        var result = await _mediator.Send(new GetAllCategoriesQuery(), cancellationToken);

        if (result.IsFailed)
        {
            _logger.LogWarning("Failed to retrieve categories. Errors: {@Errors}", result.Errors);

            return BadRequest(result.Errors);
        }

        _logger.LogInformation("Retrieved {CategoriesCount} categories successfully.", result.Value.Count);

        return Ok(result.Value);
    }
}