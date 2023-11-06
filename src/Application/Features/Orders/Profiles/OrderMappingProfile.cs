using Application.Features.Orders.Commands.Create;
using Application.Features.Orders.Commands.Update;
using Application.Features.Orders.Dtos;
using Application.Utilities.Common.ResponseBases.Concrate;
using AutoMapper;
using Domain.Entites.Orders;

namespace Application.Features.Orders.Profiles;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, CreateOrderCommand>().ReverseMap();
        CreateMap<Order, CreateOrderResponse>().ReverseMap();
        CreateMap<OrderDto, CreateOrderResponse>().ReverseMap();

        CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        CreateMap<Order, UpdateOrderResponse>().ReverseMap();
        CreateMap<OrderDto, UpdateOrderResponse>().ReverseMap();

        CreateMap<ObjectBaseResponse<OrderDto>, ObjectBaseResponse<CreateOrderResponse>>().ReverseMap();
        CreateMap<ObjectBaseResponse<OrderDto>, ObjectBaseResponse<UpdateOrderResponse>>().ReverseMap();
    }
}
