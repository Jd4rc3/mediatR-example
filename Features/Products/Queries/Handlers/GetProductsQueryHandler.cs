using MediatR;
using MediatrExample.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.Features.Products.Queries;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<GetProductsQueryResponse>>
{
    private readonly MyAppDbContext _context;

    public GetProductsQueryHandler(MyAppDbContext context)
    {
        _context = context;
    }

    public Task<List<GetProductsQueryResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken) =>
        _context.Products
            .AsNoTracking()
            .Select(s => new GetProductsQueryResponse
            {
                ProductId = s.ProductId,
                Description = s.Description,
                Price = s.Price
            })
            .ToListAsync();
}