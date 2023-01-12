using MediatR;
using MediatrExample.Domain;
using MediatrExample.Infrastructure.Persistence;

namespace MediatrExample.Features.Products.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
{
    private readonly MyAppDbContext _context;

    public CreateProductCommandHandler(MyAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var newProduct = new Product
        {
            Description = request.Description,
            Price = request.Price
        };

        _context.Products.Add(newProduct);

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}