using AutoMapper;
using MediatR;
using MediatrExample.Domain;
using MediatrExample.Helpers;

namespace MediatrExample.Features.Products.Queries;

public class GetProductsQuery : IRequest<List<GetProductsQueryResponse>>
{
}

public class GetProductsQueryResponse
{
    public string ProductId { get; set; } = default!;
    public string Description { get; set; } = default!;
    public double Price { get; set; }
    public string ListDescription { get; set; } = default!;
}

public class GetProductsQueryProfile : Profile
{
    public GetProductsQueryProfile() => CreateMap<Product, GetProductsQueryResponse>()
        .ForMember(dest =>
                dest.ListDescription,
            opt => opt.MapFrom(mf => $"{mf.Description} - {mf.Price:c}")
        )
        .ForMember(dest =>
                dest.ProductId,
            opt => opt.MapFrom(mf => mf.ProductId.ToHashId())
        );
}