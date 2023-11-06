using Application.Features.Customers.Commands.Create;
using Application.Features.Customers.Commands.Delete;
using Application.Features.Customers.Commands.Dtos;
using Application.Features.Customers.Commands.Update;
using Application.Utilities.Common.ResponseBases.ComplexTypes;
using Application.Utilities.Common.ResponseBases.Concrate;

namespace Application.Features.Customers.Services;

public interface ICustomerCommandService
{
    Task<ObjectBaseResponse<CustomerDto>> CreateAsync(CreateCustomerCommand command);
    Task<ObjectBaseResponse<CustomerDto>> UpdateAsync(UpdateCustomerCommand command);
    Task<ResponseBase> DeleteAsync(DeleteCustomerCommand command);
}
