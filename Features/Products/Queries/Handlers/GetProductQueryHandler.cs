using System.Threading.RateLimiting;
using MediatR;
using MediatrExample.Domain;
using MediatrExample.Exceptions;
using MediatrExample.Infrastructure.Persistence;

namespace MediatrExample.Features.Products.Queries;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductQueryResponse>
{
    private readonly MyAppDbContext _context;

    public GetProductQueryHandler(MyAppDbContext context)
    {
        _context = context;
    }

    public async Task<GetProductQueryResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.ProductId);

        if (product is null)
        {
            throw new NotFoundException(nameof(Product), request.ProductId);
        }

        return new GetProductQueryResponse
        {
            Description = product.Description,
            ProductId = product.ProductId,
            Price = product.Price
        };
    }
}