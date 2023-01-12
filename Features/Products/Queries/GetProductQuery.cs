using MediatR;

namespace MediatrExample.Features.Products.Queries;

public class GetProductQuery : IRequest<GetProductQueryResponse>
{
    public int ProductId { get; set; }
}

public class GetProductQueryResponse
{
    public int ProductId { get; set; }
    public string Description { get; set; } = default!;

    public double Price { get; set; }
}