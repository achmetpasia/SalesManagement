using Application.Features.Core;
using Application.Utilities.Common.ResponseBases.Concrate;
using MediatR;

namespace Application.Features.Customers.Commands.Update;

public class UpdateCustomerCommand : BaseUpdateCommand, IRequest<ObjectBaseResponse<UpdateCustomerResponse>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }

    public UpdateCustomerCommand(Guid id, string firstName, string lastName, string address, string postalCode)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        PostalCode = postalCode;
    }
}
