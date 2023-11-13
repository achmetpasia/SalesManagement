using Application.Features.Customers.Commands.Create;
using Application.Features.Customers.Commands.Update;
using Application.Utilities.Common.ResponseBases.Concrate;
using AutoMapper;
using Domain.Entites.Customers;

namespace Application.Features.Customers.Profiles;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
        CreateMap<Customer, CreateCustomerResponse>().ReverseMap();
        CreateMap<Customer, ObjectBaseResponse<CreateCustomerResponse>>()
            .ConstructUsing(src => new ObjectBaseResponse<CreateCustomerResponse>
            {
            Data = new CreateCustomerResponse { Id = src.Id },
            StatusCode = System.Net.HttpStatusCode.Created
            });

        CreateMap<Customer, UpdateCustomerCommand>().ReverseMap();
        CreateMap<Customer, UpdateCustomerResponse>().ReverseMap();
        CreateMap<Customer, ObjectBaseResponse<UpdateCustomerResponse>>()
            .ConstructUsing(src => new ObjectBaseResponse<UpdateCustomerResponse>
            {
                Data = new UpdateCustomerResponse { Id = src.Id },
                StatusCode = System.Net.HttpStatusCode.OK
            });
    }
}
