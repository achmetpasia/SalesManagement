using Application.Features.Customers.Commands.Create;
using Application.Features.Customers.Commands.Dtos;
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
        CreateMap<CustomerDto, CreateCustomerResponse>().ReverseMap();

        CreateMap<Customer, UpdateCustomerCommand>().ReverseMap();
        CreateMap<Customer, UpdateCustomerResponse>().ReverseMap();
        CreateMap<CustomerDto, UpdateCustomerResponse>().ReverseMap();

        CreateMap<ObjectBaseResponse<CustomerDto>, ObjectBaseResponse<CreateCustomerResponse>>().ReverseMap();
        CreateMap<ObjectBaseResponse<CustomerDto>, ObjectBaseResponse<UpdateCustomerResponse>>().ReverseMap();
    }
}
