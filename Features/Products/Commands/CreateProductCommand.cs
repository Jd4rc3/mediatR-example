using MediatR;

namespace MediatrExample.Features.Products.Commands;

public class CreateProductCommand : IRequest
{
    public string Description { get; set; } = default!;
    public double Price { get; set; }
}