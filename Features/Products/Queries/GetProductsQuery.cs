using AutoMapper;
using MediatR;
using MediatrExample.Domain;

namespace MediatrExample.Features.Products.Queries;

public class GetProductsQuery : IRequest<List<GetProductsQueryResponse>>
{
}

public class GetProductsQueryResponse
{
    public int ProductId { get; set; }
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
        );
}