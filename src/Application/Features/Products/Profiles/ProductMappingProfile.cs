using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Update;
using Application.Utilities.Common.ResponseBases.Concrate;
using AutoMapper;
using Domain.Entites.Products;

namespace Application.Features.Products.Profiles;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, CreateProductCommand>().ReverseMap();
        CreateMap<Product, CreateProductResponse>().ReverseMap();
        CreateMap<Product, ObjectBaseResponse<CreateProductResponse>>()
            .ConstructUsing(src => new ObjectBaseResponse<CreateProductResponse>
            {
                Data = new CreateProductResponse { Id = src.Id },
                StatusCode = System.Net.HttpStatusCode.Created
            });

        CreateMap<Product, UpdateProductCommand>().ReverseMap();
        CreateMap<Product, UpdateProductResponse>().ReverseMap();
        CreateMap<Product, ObjectBaseResponse<UpdateProductResponse>>()
            .ConstructUsing(src => new ObjectBaseResponse<UpdateProductResponse>
            {
                Data = new UpdateProductResponse { Id = src.Id },
                StatusCode = System.Net.HttpStatusCode.OK
            });
    }
}
