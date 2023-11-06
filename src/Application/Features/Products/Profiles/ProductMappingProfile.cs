using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Dtos;
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
        CreateMap<ProductDto, CreateProductResponse>().ReverseMap();

        CreateMap<Product, UpdateProductCommand>().ReverseMap();
        CreateMap<Product, UpdateProductResponse>().ReverseMap();
        CreateMap<ProductDto, UpdateProductResponse>().ReverseMap();

        CreateMap<ObjectBaseResponse<ProductDto>, ObjectBaseResponse<CreateProductResponse>>().ReverseMap();
        CreateMap<ObjectBaseResponse<ProductDto>, ObjectBaseResponse<UpdateProductResponse>>().ReverseMap();
    }
}
