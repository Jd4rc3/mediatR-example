using AutoMapper;
using MediatR;
using MediatrExample.Domain;
using MediatrExample.Helpers;

namespace MediatrExample.Features.Products.Queries;

public class GetProductQuery : IRequest<GetProductQueryResponse>
{
    public string ProductId { get; set; }
}

public class GetProductQueryResponse
{
    public string ProductId { get; set; }
    public string Description { get; set; } = default!;

    public double Price { get; set; }
}

public class GetProductQueryProfile : Profile
{
    public GetProductQueryProfile() => CreateMap<Product, GetProductQueryResponse>()
        .ForMember(dest =>
                dest.ProductId,
            opt => opt.MapFrom(mf => mf.ProductId.ToHashId())
        );
}