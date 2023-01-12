using AutoMapper;
using MediatR;
using MediatrExample.Domain;
using MediatrExample.Infrastructure.Persistence;

namespace MediatrExample.Features.Products.Commands.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var newProduct = _mapper.Map<Product>(request);

        _context.Products.Add(newProduct);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

public class CreateProductCommandMapper : Profile
{
    public CreateProductCommandMapper() =>
        CreateMap<CreateProductCommand, Product>();
}