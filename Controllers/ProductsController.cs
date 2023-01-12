using MediatR;
using MediatrExample.Features.Products.Commands;
using MediatrExample.Features.Products.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.Controllers;

[Authorize]
[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public Task<List<GetProductsQueryResponse>> GetProducts() => _mediator.Send(new GetProductsQuery());

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    [HttpGet("{ProductId}")]
    public Task<GetProductQueryResponse> GetProductById([FromRoute] GetProductQuery query) =>
        _mediator.Send(query);
}