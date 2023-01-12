using AutoMapper;
using MediatR;
using MediatrExample.Domain;
using MediatrExample.Exceptions;
using MediatrExample.Helpers;
using MediatrExample.Infrastructure.Persistence;

namespace MediatrExample.Features.Products.Queries.Handlers;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetProductQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetProductQueryResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.ProductId.FromHashId());

        if (product is null)
        {
            throw new NotFoundException(nameof(Product), request.ProductId);
        }

        return _mapper.Map<GetProductQueryResponse>(product);
    }
}