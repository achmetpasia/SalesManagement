using Application.Features.Orders.Commands.Create;
using Application.Features.Orders.Commands.Update;
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
        CreateMap<Order, ObjectBaseResponse<CreateOrderResponse>>()
            .ConstructUsing(src => new ObjectBaseResponse<CreateOrderResponse>
            {
                Data = new CreateOrderResponse { Id = src.Id },
                StatusCode = System.Net.HttpStatusCode.Created
            });

        CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        CreateMap<Order, UpdateOrderResponse>().ReverseMap();
        CreateMap<Order, ObjectBaseResponse<UpdateOrderResponse>>()
            .ConstructUsing(src => new ObjectBaseResponse<UpdateOrderResponse>
            {
                Data = new UpdateOrderResponse { Id = src.Id },
                StatusCode = System.Net.HttpStatusCode.OK
            });
    }
}
